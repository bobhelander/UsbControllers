using System.Runtime.ConstrainedExecution;
using Microsoft.Win32.SafeHandles;

namespace UsbController.Win32Api
{
    /// <summary>
    /// Provides a disposable handle linked to a set of devices.
    /// </summary>
    /// <seealso cref="SetupApiMethods.GetClassDevs"/>
    public class SafeDeviceSetHandle : SafeHandleMinusOneIsInvalid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SafeDeviceSetHandle"/> class.
        /// </summary>
        /// <remarks>
        /// The associated handle will be released during the finalization phase.
        /// </remarks>
        public SafeDeviceSetHandle()
            : base(true)
        { }

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
            return SetupApiMethods.DestroyDeviceInfoList(this.handle);
        }
    }
}
