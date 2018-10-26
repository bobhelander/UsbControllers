using System;
using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace UsbController.Win32Api
{
    /// <summary>
    /// Provides a disposable handle linked a preparsed data associated to an open device.
    /// </summary>
    /// <seealso cref="HidMethods.GetPreparsedData(SafeFileHandle)"/>
    public class SafePreparsedDataHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafePreparsedDataHandle"/> class.
        /// </summary>
        /// <remarks>
        /// The associated handle will be released during the finalization phase.
        /// </remarks>
        public SafePreparsedDataHandle()
            : base(true)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SafePreparsedDataHandle"/> class
        /// with a specific handle.
        /// </summary>
        /// <param name="handle">The encapsulated handle.</param>
        /// <param name="ownsHandle">
        /// A value indicating whether the handle should be released  during the finalization phase;
        /// (setting this parameter to <c>true</c> is highly recommended).
        /// </param>
        /// <remarks>
        /// The associated handle will be released during the finalization phase.
        /// </remarks>
        public SafePreparsedDataHandle(IntPtr handle, bool ownsHandle)
            : base(ownsHandle)
        {
            this.handle = handle;
        }

        /// <summary>
        /// Releases the associated handle.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the handle is released successfully;
        /// otherwise, in the event of a catastrophic failure, <c>false</c>.
        /// </returns>
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        protected override bool ReleaseHandle()
        {
            return HidMethods.FreePreparsedData(this.handle);
        }
    }
}
