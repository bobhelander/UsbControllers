namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Defines the return status for the <see cref="HidMethods"/>.
    /// </summary>
    public enum HidpStatus
    {
        /// <summary>
        /// A return code for the <see cref="HidMethods.GetCaps(SafePreparsedDataHandle, out HidCaps)"/> method.
        /// </summary>
        /// <remarks>
        /// Associated to the p/invoke constant <c>HIDP_STATUS_SUCCESS</c>.
        /// </remarks>
        /// <seealso cref="HidMethods.GetCaps(SafePreparsedDataHandle, out HidCaps)"/>
        Success = 1114112,

        /// <summary>
        /// A return code for the <see cref="HidMethods.GetCaps(SafePreparsedDataHandle, out HidCaps)"/> method.
        /// </summary>
        /// <remarks>
        /// Associated to the p/invoke constant <c>HIDP_STATUS_INVALID_PREPARSED_DATA</c>.
        /// </remarks>
        /// <seealso cref="HidMethods.GetCaps(SafePreparsedDataHandle, out HidCaps)"/>
        InvalidPreparsedData = -1072627711,
    }
}
