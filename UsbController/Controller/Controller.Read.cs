using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbController.models;

namespace UsbController
{
    public partial class Controller
    {
        /// <summary>
        /// Gets the prefered length for the read buffer.
        /// </summary>
        protected int ReadLength => stream.Capabilities.InputReportByteLength;

        /// <summary>
        /// Remove jitter from continuous button events
        /// </summary>
        public Stopwatch Stopwatch { get; set; }

        /// <summary>
        /// The number of buffers available.
        /// </summary>
        private ulong readBufferCount = 3;

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
                    // Copy the buffer first
                    //var buffer = await CopyBuffer(this.readBuffer);
                    
                    // Process the change
                    CallReadEventAsync(buffer, size, stateCounter);

                    /*
                    if (ProcessAllReports)
                    {
                        // Process the change
                        CallReadEventAsync(buffer, size, stateCounter);
                    }
                    else if (stateCounter > 0)
                    {
                        var lastBufferIndex = (int)((stateCounter - 1) % readBufferCount);
                        var lastReadBuffer = this.readBuffers[lastBufferIndex];

                        // Remove jitter around the buttons
                        if (false == buffer.SequenceEqual(lastReadBuffer))
                        {
                            // The read buffer changed since the last read
                            // Restart the stopwatch
                            Stopwatch.Restart();
                        }

                        // After the state remains stable
                        if (Stopwatch.ElapsedMilliseconds > 100)
                        {
                            if (buffer.SequenceEqual(lastReadBuffer))
                            {
                                // Process the change
                                CallReadEventAsync(buffer, size, stateCounter);
                            }
                        }
                    }
                    */
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

    }
}
