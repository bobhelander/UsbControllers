using System.Runtime.InteropServices;

namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Represents the capabilities of a HID device.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/ms899397.aspx"/>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HidCaps
    {
        /// <summary>
        /// The top-level collection's usage identifier.
        /// </summary>
        public short Usage;

        /// <summary>
        /// The top-level collection's usage page.
        /// </summary>
        public short UsagePage;

        /// <summary>
        /// The maximum size, in bytes, of the input reports.
        /// </summary>
        /// <remarks>
        /// Including the report identifier, if report identifiers are used,
        /// which is added to the beginning of the report data.
        /// </remarks>
        public short InputReportByteLength;

        /// <summary>
        /// The maximum size, in bytes, of all the output reports.
        /// </summary>
        /// <remarks>
        /// Including the report identifier, if report identifiers are used,
        /// which is added to the beginning of the report data.
        /// </remarks>
        public short OutputReportByteLength;

        /// <summary>
        /// The maximum length, in bytes, of all the feature reports.
        /// </summary>
        /// <remarks>
        /// Including the report identifier, if report identifiers are used,
        /// which is added to the beginning of the report data.
        /// </remarks>
        public short FeatureReportByteLength;

        /// <summary>
        /// Reserved for internal system use.
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)]
        public short[] Reserved;

        /// <summary>
        /// The number of HIDP_LINK_COLLECTION_NODE structures that are returned for this
        /// top-level collection by HidP_GetLinkCollectionNodes.
        /// </summary>
        public short NumberLinkCollectionNodes;

        /// <summary>
        /// The number of input HIDP_BUTTON_CAPS structures that HidP_GetButtonCaps returns.
        /// </summary>
        public short NumberInputButtonCaps;

        /// <summary>
        /// The number of input HIDP_VALUE_CAPS structures that HidP_GetValueCaps returns.
        /// </summary>
        public short NumberInputValueCaps;

        /// <summary>
        /// The number of data indexes assigned to buttons and values in all input reports.
        /// </summary>
        public short NumberInputDataIndices;

        /// <summary>
        /// The number of output HIDP_BUTTON_CAPS structures that HidP_GetButtonCaps returns.
        /// </summary>
        public short NumberOutputButtonCaps;

        /// <summary>
        /// The number of output HIDP_VALUE_CAPS structures that HidP_GetValueCaps returns.
        /// </summary>
        public short NumberOutputValueCaps;

        /// <summary>
        /// The number of data indexes assigned to buttons and values in all output reports.
        /// </summary>
        public short NumberOutputDataIndices;

        /// <summary>
        /// Total number of feature HIDP_BUTTONS_CAPS structures that HidP_GetButtonCaps returns.
        /// </summary>
        public short NumberFeatureButtonCaps;

        /// <summary>
        /// Total number of feature HIDP_VALUE_CAPS structures that HidP_GetValueCaps returns.
        /// </summary>
        public short NumberFeatureValueCaps;

        /// <summary>
        /// The number of data indexes assigned to buttons and values in all feature reports.
        /// </summary>
        public short NumberFeatureDataIndices;
    }
}
