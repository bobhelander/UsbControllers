using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usb.Hid.Connection.models;

namespace Usb.Hid.Connection
{
    public partial class Controller
    {
        /// <summary>
        /// Gets the prefered length for the read buffer.
        /// </summary>
        protected int ReadLength => stream.Capabilities.InputReportByteLength;

        /// <summary>
        /// The number of buffers available.
        /// </summary>
        private ulong readBufferCount = 9;

        /// <summary>
        /// The buffers used when reading the stream.
        /// </summary>
        private List<byte[]> readBuffers = new List<byte[]>();

        /// <summary>
        /// Serial reading task
        /// </summary>
        private Task SerialProcessingTask { get; set; }

        /// <summary>
        /// Flag to keep reading the serial stream.
        /// </summary>
        public bool ContinueProcessing { get; set; }

        /// <summary>
        /// Flag when the USB device continuously sends reports.  Setting this will compare the reports
        /// and only send when the reports change.
        /// </summary>
        public bool ContinuousUsb { get; set; } = false;

        /// <summary>
        /// Only compare this much of the report for changes.
        /// </summary>
        public int ContinuousUsbReportSize { get; set; } = 0;

        /// <summary>
        /// Look at all of the reports to determine button state
        /// </summary>
        public bool ContinuousUsbDebounce { get; set; } = false;

        /// <summary>
        /// Button state start index in the read buffer
        /// </summary>
        public int ContinuousUsbDebounceButtonsIndex { get; set; } = 0;

        /// <summary>
        /// Holder the the last debounced read
        /// </summary>
        private byte[] lastBuffer;

        /// <summary>
        /// Reads a message from the device
        /// </summary>
        /// <returns></returns>
        private async Task ReadSerial()
        {
            ulong counter = 0;
            while (ContinueProcessing)
            {
                // Round robin the available buffers
                var bufferIndex = (int)(counter % readBufferCount);

                var size = await this.stream.ReadAsync(readBuffers[bufferIndex], 0, ReadLength);

                if (ContinuousUsb)
                    await ProcessSerialMessageRemoveJitter(size, readBuffers[bufferIndex], ReadLength, counter);
                else
                    await ProcessSerialMessage(size, readBuffers[bufferIndex], ReadLength, counter);

                counter++;
            }
        }

        /// <summary>
        /// Processes the buffer received from the device
        /// </summary>
        /// <param name="size"></param>
        /// <param name="buffer"></param>
        /// <param name="requestedLength"></param>
        /// <param name="stateCounter"></param>
        /// <returns></returns>
        private async Task<bool> ProcessSerialMessage(int size, byte[] buffer, int requestedLength, ulong stateCounter)
        {
            if (size != requestedLength)
            {
                // Throw this read out.  
                //log.Error($"Read Invalid Size {requestedLength}, Actual size {size}");
                return false;
            }
            try
            {
                if (0 < observers.Count)
                {
                    // Process the change
                    CallReadEventAsync(buffer, size, stateCounter);

                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Processes the buffer received from the device
        /// </summary>
        /// <param name="size"></param>
        /// <param name="buffer"></param>
        /// <param name="requestedLength"></param>
        /// <param name="stateCounter"></param>
        /// <returns></returns>
        private async Task<bool> ProcessSerialMessageRemoveJitter(int size, byte[] rawbuffer, int requestedLength, ulong stateCounter)
        {
            if (size != requestedLength)
            {
                // Throw this read out.  
                //log.Error($"Read Invalid Size {requestedLength}, Actual size {size}");
                return false;
            }
            try
            {
                if (0 < observers.Count)
                {
                    

                    if (stateCounter > 0)
                    {
                        var compareBuffer = CopyBuffer(lastBuffer, size);
                        var buffer = CopyBuffer(rawbuffer, size);

                        if (ContinuousUsbDebounce)
                        {
                            // Debounce buffer across all ReadBuffers
                            var buttons = DebounceButtons();

                            // Replace the buttons
                            Array.Copy(BitConverter.GetBytes(buttons), 0, buffer, ContinuousUsbDebounceButtonsIndex, 4);
                        }

                        // If the value has been set, use it. 
                        var takeCount = (0 == ContinuousUsbReportSize) ? size : ContinuousUsbReportSize;

                        // Update the last state
                        lastBuffer = buffer;

                        // Call only if changed.  Take is used to reduce the report size to only the first part of the report needed.
                        if (false == buffer.Take(takeCount).SequenceEqual(compareBuffer.Take(takeCount)))
                        {

                            // The read buffer changed since the last read
                            CallReadEventAsync(buffer, size, stateCounter);
                        }
                    }

                    return await Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Calls the event handler
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <param name="stateCounter"></param>
        private void CallReadEventAsync(byte[] buffer, int size, ulong stateCounter)
        {
            // Allow this to process in the thread pool
            Task.Run(() => Notify(new ReadBuffer { StateCounter = stateCounter, Buffer = CopyBuffer(buffer, size) }))
                .ContinueWith(t => { log.Error($"Read EventHandler Exception: {t.Exception}"); }, TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// Deep copy buffer
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        private byte[] CopyBuffer(byte[] buffer, int size)
        {
            byte[] result = new byte[size];
            Array.Copy(buffer, result, size);
            return result;
        }

        /// <summary>
        /// Read across all of the read buffers to determine if the button should be set
        /// </summary>
        /// <returns>The state of the buttons after debouncing</returns>
        private UInt32 DebounceButtons()
        {
            var buttons = readBuffers.Select(x => BitConverter.ToUInt32(x, ContinuousUsbDebounceButtonsIndex)).ToArray();

            return CompareButtons(
                CompareButtons(buttons[0], buttons[1], buttons[2]),
                CompareButtons(buttons[3], buttons[4], buttons[5]),
                CompareButtons(buttons[6], buttons[7], buttons[8]));
        }

        private UInt32 CompareButtons(UInt32 A, UInt32 B, UInt32 C)
        {
            // Truth Table
            // A  B  C  Result
            // 0  0  0  0
            // 0  0  1  0 
            // 0  1  0  0
            // 0  1  1  1
            // 1  0  0  0
            // 1  0  1  1
            // 1  1  0  1
            // 1  1  1  1

            return (A & B) | (A & C) | (B & C);
        }
    }
}
