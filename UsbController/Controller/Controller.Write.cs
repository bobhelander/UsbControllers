using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsbController
{
    public partial class Controller
    {
        /// <summary>
        /// Gets the prefered length for the write buffer.
        /// </summary>
        protected int WriteLength => stream.Capabilities.OutputReportByteLength;

        /// <summary>
        /// Write a buffer to the USB device
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="length"></param>
        public async Task Write(byte[] buffer, int length)
        {
            if (stream.CanWrite)
                await stream.WriteAsync(buffer, 0, length);
        }
    }
}
