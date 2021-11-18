using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Usb.Hid.Connection.Win32Api;

namespace Usb.Hid.Connection
{
    /// <summary>
    /// Encapsulates the calls to the <c>hid</c> library that provides read/write capabilities.
    /// </summary>
    internal sealed class HidStream : Stream, IDisposable
    {
        /// <summary>
        /// The identifier of the HID interface.
        /// </summary>
        private static Guid hidGuid;

        /// <summary>
        /// The path of the associated device.
        /// </summary>
        private readonly string devicePath;

        /// <summary>
        /// The handle of the associated device.
        /// </summary>
        private SafeFileHandle handle;

        /// <summary>
        /// The stream of the associated device;
        /// </summary>
        private Stream stream;

        /// <summary>
        /// The capabilities of the associated device.
        /// </summary>
        private HidCaps capabilities;

        /// <summary>
        /// A value indicating whether the capabilities are initialized.
        /// </summary>
        private bool capabilitiesInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="HidStream"/> class.
        /// </summary>
        /// <param name="devicePath">The path of the associated device.</param>
        public HidStream(string devicePath)
        {
            this.devicePath = devicePath;

            // Create the file handler from the device path
            // Win10 requires shared access
            if (OpenFileHandle(Win32FileAccess.GenericRead | Win32FileAccess.GenericWrite))
                return;

            if (OpenFileHandle(0))
                return;

            if (this.handle.IsInvalid)
            {
                throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to create device file");
            }
        }

        private bool OpenFileHandle(Win32FileAccess desiredAccess)
        {
            handle = Kernel32Methods.CreateFile(
                devicePath,
                desiredAccess,
                FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, (uint)Win32FileAttributes.Overlapped, IntPtr.Zero);

            return handle.IsInvalid == false;
        }

        /// <summary>
        /// Gets the identifier of the HID interface.
        /// </summary>
        public static Guid HidGuid
        {
            get
            {
                if (hidGuid == Guid.Empty)
                {
                    HidMethods.GetHidGuid(out hidGuid);
                }

                return hidGuid;
            }
        }

        public string PhysicalDescriptor
        {
            get
            {
                const Int32 length = 100;
                var buffer = new System.Text.StringBuilder(length);

                if (false == HidMethods.GetPhysicalDescriptor(handle, buffer, length))
                    throw new Exception("Failed GetPhysicalDescriptor", new Win32Exception(Marshal.GetLastWin32Error()));

                return buffer.ToString();
            }
        }

        public string ManufacturerString
        {
            get
            {
                const Int32 length = 100;
                var buffer = new System.Text.StringBuilder(length);

                if (false == HidMethods.GetManufacturerString(handle, buffer, length))
                    throw new Exception("Failed GetManufacturerString", new Win32Exception(Marshal.GetLastWin32Error()));

                return buffer.ToString();
            }
        }

        public string ProductString
        {
            get
            {
                const Int32 length = 100;
                var buffer = new System.Text.StringBuilder(length);

                if (false == HidMethods.GetProductString(handle, buffer, length))
                    throw new Exception("Failed GetProductString", new Win32Exception(Marshal.GetLastWin32Error()));

                return buffer.ToString();
            }
        }

        public string SerialNumberString
        {
            get
            {
                const Int32 length = 100;
                var buffer = new System.Text.StringBuilder(length);

                if (false == HidMethods.GetSerialNumberString(handle, buffer, length))
                    throw new Exception("Failed GetSerialNumberString", new Win32Exception(Marshal.GetLastWin32Error()));

                return buffer.ToString();
            }
        }

        /// <summary>
        /// Gets the capabilities of the device.
        /// </summary>
        public HidCaps Capabilities
        {
            get
            {
                if (!this.capabilitiesInitialized)
                {
                    using (SafePreparsedDataHandle preparsedData = HidMethods.GetPreparsedData(this.handle))
                    {
                        HidMethods.GetCaps(preparsedData, out this.capabilities);
                        this.capabilitiesInitialized = true;
                    }
                }

                return this.capabilities;
            }
        }

        public byte[] InputReport
        {
            get
            {
                if (false == HidMethods.FlushQueue(handle))
                    throw new Exception("Failed to flush queue", new Win32Exception(Marshal.GetLastWin32Error()));

                using (SafePreparsedDataHandle preparsedData = HidMethods.GetPreparsedData(this.handle))
                {
                    int inputReportByteLength = Capabilities.InputReportByteLength;
                    byte[] data = new byte[inputReportByteLength];

                    // Initialize a report buffer to receive the report
                    var ret = HidMethods.InitializeReportForID(
                        (int)HidMethods.HID_REPORT_TYPE.HidP_Input,
                        1,  // Report 1
                        preparsedData,
                        data,
                        inputReportByteLength);

                    if (ret == HidpStatus.HIDP_STATUS_REPORT_DOES_NOT_EXIST)
                        throw new Exception("Failed to initialize read input report: Report does not exist");

                    if (ret != HidpStatus.HIDP_STATUS_SUCCESS)
                        throw new Exception("Failed to initialize read input report", new Win32Exception(Marshal.GetLastWin32Error()));

                    // Get input report 1
                    if (false == HidMethods.GetInputReport(handle, data, inputReportByteLength))
                        throw new Exception("Failed to read input report", new Win32Exception(Marshal.GetLastWin32Error()));

                    return data;
                }
            }
        }

        /// <summary>
        /// Returns the pressed buttons from the input report
        /// </summary>
        /// <param name="inputReport">An input report</param>
        /// <returns></returns>
        public short[] GetButtonUsages(byte[] inputReport)
        {
            if (inputReport.Length != capabilities.InputReportByteLength)
                throw new ArgumentException("Report length does not match capabilities report length", nameof(inputReport));

            using (SafePreparsedDataHandle preparsedData = HidMethods.GetPreparsedData(this.handle))
            {
                var inputButtonCapCount = capabilities.NumberInputButtonCaps;

                HidButtonCaps[] buttonCaps = new HidButtonCaps[inputButtonCapCount];

                var buttonCount = buttonCaps[0].Range.UsageMax - buttonCaps[0].Range.UsageMin + 1;

                short[] usages = new short[buttonCount];

                int reportLength = capabilities.InputReportByteLength;
                byte[] data = new byte[reportLength];

                var ret = HidMethods.GetUsages(
                    (int)HidMethods.HID_REPORT_TYPE.HidP_Input,
                    buttonCaps[0].UsagePage,
                    0,
                    usages,
                    ref buttonCount,
                    preparsedData,
                    data,
                    reportLength);

                if (ret != HidpStatus.HIDP_STATUS_SUCCESS)
                    throw new Exception("Failed to parse input report", new Win32Exception(Marshal.GetLastWin32Error()));

                return usages;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this stream supports reading.
        /// </summary>
        public override bool CanRead
        {
            get { return this.Stream.CanRead; }
        }

        /// <summary>
        /// Gets a value indicating whether this stream supports seeking.
        /// </summary>
        public override bool CanSeek
        {
            get { return this.Stream.CanSeek; }
        }

        /// <summary>
        /// Gets a value indicating whether this stream supports writing.
        /// </summary>
        public override bool CanWrite
        {
            get { return this.Stream.CanWrite; }
        }

        /// <summary>
        /// Gets the length - in bytes - of the stream.
        /// </summary>
        public override long Length
        {
            get { return this.Stream.Length; }
        }

        /// <summary>
        /// Gets or sets the position within the stream.
        /// </summary>
        public override long Position
        {
            get { return this.Stream.Position; }
            set { this.Stream.Position = value; }
        }

        /// <summary>
        /// Gets the encapsulated stream.
        /// </summary>
        private Stream Stream
        {
            get
            {
                return this.stream ?? (this.stream = new FileStream(this.handle,
                        System.IO.FileAccess.Read | System.IO.FileAccess.Write,
                        this.Capabilities.InputReportByteLength, true));
            }
        }

        /// <summary>
        /// Flushes the cache.
        /// </summary>
        public override void Flush()
        {
            this.Stream.Flush();
        }

        /// <summary>
        /// Reads a sequence of bytes from the current stream.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes that will contain the read bytes.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in buffer at which to begin storing the data read.
        /// </param>
        /// <param name="count">
        /// The maximum number of bytes to be read from the current stream.
        /// </param>
        /// <returns>
        /// The total number of bytes read into the buffer.
        /// </returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.Stream.Read(buffer, offset, count);
        }

        /// <summary>
        /// sets the position within the current stream.
        /// </summary>
        /// <param name="offset">
        /// A byte offset relative to the <paramref name="origin"/> parameter.
        /// </param>
        /// <param name="origin">
        /// A value indicating the reference point used to obtain the new position.
        /// </param>
        /// <returns>
        /// The new position within the current stream.
        /// </returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.Stream.Seek(offset, origin);
        }

        /// <summary>
        /// Sets the length of the current stream.
        /// </summary>
        /// <param name="value">The desired length of the current stream in bytes.</param>
        public override void SetLength(long value)
        {
            this.Stream.SetLength(value);
        }

        /// <summary>
        /// Writes a sequence of bytes into the current stream.
        /// </summary>
        /// <param name="buffer">
        /// An array of bytes that contains the bytes to be written.
        /// </param>
        /// <param name="offset">
        /// The zero-based byte offset in buffer at which to begin copying bytes.
        /// </param>
        /// <param name="count">
        /// The number of bytes to be written to the current stream.
        /// </param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.Stream.Write(buffer, offset, count);
        }

        public int ReadFeature(byte[] buffer, int offset, int count, byte initialByte=1)
        {
            // TODO: explain and generalize
            buffer[offset] = initialByte;

            // Prepare a buffer without offset, if required
            byte[] reportBuffer;
            if (offset == 0)
            {
                reportBuffer = buffer;
            }
            else
            {
                reportBuffer = new byte[count];
                Array.ConstrainedCopy(buffer, offset, reportBuffer, 0, count);
            }

            if (!Win32Api.HidMethods.GetFeature(this.handle, reportBuffer, count))
            {
                throw new Exception("Can't retrieve feature report", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            // Copy back the value when a secondary buffer were used.
            if (offset != 0)
            {
                Array.ConstrainedCopy(reportBuffer, 0, buffer, offset, count);
            }

            return count;
        }

        public void WriteFeature(byte[] buffer, int offset, int count, byte initialByte = 1)
        {
            // TODO: explain and generalize
            buffer[offset] = initialByte;

            // Prepare a buffer without offset, if required
            byte[] reportBuffer;
            if (offset == 0)
            {
                reportBuffer = buffer;
            }
            else
            {
                reportBuffer = new byte[count];
                Array.ConstrainedCopy(buffer, offset, reportBuffer, 0, count);
            }

            if (!Win32Api.HidMethods.SetFeature(this.handle, reportBuffer, count))
            {
                throw new Exception("Can't write feature report", new Win32Exception(Marshal.GetLastWin32Error()));
            }

            // Copy back the value when a secondary buffer were used.
            if (offset != 0)
            {
                Array.ConstrainedCopy(reportBuffer, 0, buffer, offset, count);
            }
        }

        /// <summary>
        /// Disposes the encapsulated resources.
        /// </summary>
        /// <param name="disposing">
        /// A value indicating whether the managed resources should be disposed.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources
                this.stream?.Dispose();
            }

            // Dispose unmanaged resources
            if (this.handle != null)
            {
                this.handle.Dispose();
                this.handle = null;
            }

            base.Dispose(disposing);
        }
    }
}
