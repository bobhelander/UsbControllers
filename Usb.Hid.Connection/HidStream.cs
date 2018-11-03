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
            this.handle = Kernel32Methods.CreateFile(
                devicePath, 
                Win32Api.Win32FileAccess.GenericRead | Win32FileAccess.GenericWrite,
                FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, Win32FileAttributes.Overlapped, IntPtr.Zero);

            //this.handle = Kernel32Methods.CreateFile(
            //    devicePath,
            //    Win32Api.Win32FileAccess.GenericRead | Win32FileAccess.GenericWrite,
            //    FileShare.ReadWrite, IntPtr.Zero, FileMode.Open, Win32FileAttributes.Overlapped, IntPtr.Zero);

            if (this.handle.IsInvalid)
            {
                // TODO: raise a better exception
                throw new Exception("Failed to create device file", new Win32Exception(Marshal.GetLastWin32Error()));
            }
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
                if (this.stream == null)
                {
                    this.stream = new FileStream(this.handle, 
                        System.IO.FileAccess.Read | System.IO.FileAccess.Write, 
                        this.Capabilities.InputReportByteLength, true);
                }

                return this.stream;
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
                if (this.stream != null)
                {
                    this.stream.Dispose();
                }
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
