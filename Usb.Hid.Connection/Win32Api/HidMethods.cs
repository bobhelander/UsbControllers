using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Provides direct access to the methods or the "hid.dll" library.
    /// </summary>
    public static class HidMethods
    {
        // https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/_hid/

        /// <summary>
        /// Gets the GUID that Windows uses to represent HID class devices.
        /// </summary>
        /// <param name="hidGuid">
        /// The instance used to retrieve the device interface GUID for HIDClass devices.
        /// </param>
        [DllImport("hid.dll", EntryPoint = "HidD_GetHidGuid", SetLastError = true)]
        public static extern void GetHidGuid(out Guid hidGuid);

        /// <summary>
        /// Provides preparsed data about a specified HID device that could be used to
        /// retrieve the capabilities of the device.
        /// </summary>
        /// <param name="device">Specifies an open handle to a top-level collection.</param>
        /// <param name="preparsedData">A pointer to the preparsed data.</param>
        /// <returns>
        /// <c>true</c> if the preparsed data could be retrieved;
        /// <c>false</c> otherwise.
        /// </returns>
        [DllImport("hid.dll", EntryPoint = "HidD_GetPreparsedData", SetLastError = true)]
        public static extern bool GetPreparsedData(SafeFileHandle device, out SafePreparsedDataHandle preparsedData);

        /// <summary>
        /// Provides preparsed data about a specified HID device that could be used to
        /// retrieve the capabilities of the device.
        /// </summary>
        /// <param name="device">Specifies an open handle to a top-level collection.</param>
        /// <returns>
        /// The prepzrsed data.
        /// </returns>
        /// <exception cref="Win32Exception">
        /// The preparsed data can't be retrieved.
        /// </exception>
        public static SafePreparsedDataHandle GetPreparsedData(SafeFileHandle device)
        {
            if (false == GetPreparsedData(device, out SafePreparsedDataHandle result))
            {
                throw new Win32Exception(Marshal.GetHRForLastWin32Error());
            }

            return result;
        }

        /// <summary>
        /// Frees the memory associated with a preparsed data block.
        /// </summary>
        /// <param name="preparsedData">The pointer to the preparsed data to free.</param>
        /// <returns>
        /// <c>true</c> if the preparsed data could be free;
        /// <c>false</c> otherwise.
        /// </returns>
        [DllImport("hid.dll", EntryPoint = "HidD_FreePreparsedData", SetLastError = true)]
        public static extern bool FreePreparsedData(IntPtr preparsedData);

        /// <summary>
        /// Retrieves the capabilities of a specified HID device.
        /// </summary>
        /// <param name="preparsedData">
        /// The preparsed data associated with the HID device.
        /// </param>
        /// <param name="capacities">
        /// The capabilities of the HID device, if the method call succeed.</param>
        /// <returns>
        /// <list type="table">
        /// <item>
        /// <term><see cref="HidpStatus.Success"/></term>
        /// <description>The routine successfully returned the collection capability information.</description>
        /// </item>
        /// <item>
        /// <term><see cref="HidpStatus.InvalidPreparsedData"/></term>
        /// <description>The specified preparsed data is invalid.</description>
        /// </item>
        /// </list>
        /// </returns>
        [DllImport("hid.dll", EntryPoint = "HidP_GetCaps", SetLastError = true)]
        public static extern HidpStatus GetCaps(SafePreparsedDataHandle preparsedData, out HidCaps capacities);

        /// <summary>
        /// Retrieves the capabilities of a specified HID device.
        /// </summary>
        /// <param name="preparsedData">
        /// The preparsed data associated with the HID device.
        /// </param>
        /// <returns>
        /// The capabilities of the HID device.
        /// </returns>
        /// <exception cref="Win32Exception">
        /// The specified preparsed data is invalid.
        /// </exception>
        public static HidCaps GetCaps(SafePreparsedDataHandle preparsedData)
        {
            HidpStatus status = GetCaps(preparsedData, out HidCaps result);
            if (status != HidpStatus.HIDP_STATUS_SUCCESS)
            {
                throw new Win32Exception("The specified preparsed data is invalid");
            }

            return result;
        }

        /// <summary>
        /// Retrieves the feature report of a specified HID device.
        /// </summary>
        /// <param name="hidDeviceObject">A pointer to a HID device.</param>
        /// <param name="reportBuffer">A buffer to store the report.</param>
        /// <param name="reportBufferLength">The length of the report.</param>
        /// <returns>
        /// <c>true</c> if the feature report has been retrieved;
        /// <c>false</c> otherwise.
        /// </returns>
        [DllImport("hid.dll", EntryPoint = "HidD_GetFeature", SetLastError = true)]
        public static extern bool GetFeature(SafeFileHandle hidDeviceObject, byte[] reportBuffer, int reportBufferLength);

        /// <summary>
        /// Defines the feature report of a specified HID device.
        /// </summary>
        /// <param name="hidDeviceObject">A pointer to a HID device.</param>
        /// <param name="reportBuffer">A buffer that contains the report.</param>
        /// <param name="reportBufferLength">The length of the report.</param>
        /// <returns>
        /// <c>true</c> if the feature report has been defined;
        /// <c>false</c> otherwise.
        /// </returns>
        [DllImport("hid.dll", EntryPoint = "HidD_SetFeature", SetLastError = true)]
        public static extern bool SetFeature(SafeFileHandle hidDeviceObject, byte[] reportBuffer, int reportBufferLength);

        [DllImport("hid.dll", EntryPoint = "HidP_MaxDataListLength", SetLastError = true)]
        public static extern Int32 MaxDataListLength(Int32 ReportType, SafePreparsedDataHandle PreparsedData);

        public enum HID_REPORT_TYPE
        {
            HidP_Input,
            HidP_Output,
            HidP_Feature
        };

        [StructLayout(LayoutKind.Explicit)]
        public struct HIDP_DATA
        {
            [FieldOffset(0)]
            public short DataIndex;
            [FieldOffset(2)]
            public short Reserved;

            [FieldOffset(4)]
            public int RawValue;
            [FieldOffset(4), MarshalAs(UnmanagedType.U1)]
            public bool On;
        }

        // https://github.com/tpn/winsdk-10/blob/master/Include/10.0.14393.0/shared/hidsdi.h
        [DllImport("hid.dll", EntryPoint = "HidD_GetInputReport", SetLastError = true)]
        public static extern bool GetInputReport(SafeFileHandle hidDeviceObject, byte[] reportBuffer, int reportBufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_SetOutputReport", SetLastError = true)]
        static public extern bool SetOutputReport(SafeFileHandle HidDeviceObject, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] ReportBuffer, int ReportBufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_GetPhysicalDescriptor", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean GetPhysicalDescriptor(SafeFileHandle HidDeviceObject, System.Text.StringBuilder Buffer, Int32 BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_GetManufacturerString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean GetManufacturerString(SafeFileHandle HidDeviceObject, System.Text.StringBuilder Buffer, Int32 BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_GetProductString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean GetProductString(SafeFileHandle HidDeviceObject, System.Text.StringBuilder Buffer, Int32 BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_GetSerialNumberString", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern Boolean GetSerialNumberString(SafeFileHandle HidDeviceObject, System.Text.StringBuilder Buffer, Int32 BufferLength);

        [DllImport("hid.dll", EntryPoint = "HidD_FlushQueue", SetLastError = true)]
        public static extern bool FlushQueue(SafeFileHandle HidDeviceObject);

        /// <summary>
        /// Initialize a report based on the given report ID.
        /// </summary>
        /// <param name="ReportType">One of HidP_Input, HidP_Output, or HidP_Feature.</param>
        /// <param name="ReportID"></param>
        /// <param name="PreparsedData">Preparsed data structure returned by HIDCLASS</param>
        /// <param name="Report">Buffer which to set the data into.</param>
        /// <param name="ReportLength">Length of Report...Report should be at least as long as the
        ///        value indicated in the HIDP_CAPS structure for the device and
        ///        the corresponding ReportType</param>
        /// <returns></returns>
        [DllImport("hid.dll", EntryPoint = "HidP_InitializeReportForID", SetLastError = true)]
        public static extern HidpStatus InitializeReportForID(
            Int32 ReportType,
            byte ReportID,
            SafePreparsedDataHandle PreparsedData,
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] byte[] Report,
            int ReportLength);

        // https://github.com/tpn/winsdk-10/blob/master/Include/10.0.14393.0/shared/hidpi.h
        [DllImport("hid.dll", SetLastError = true)]
        public static extern HidpStatus HidP_GetData(Int32 ReportType, [In, Out] HIDP_DATA[] DataList,
        ref int DataLength, SafePreparsedDataHandle PreparsedData, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 5)] byte[] Report,
        int ReportLength);

        [DllImport("hid.dll", EntryPoint = "HidP_GetButtonCaps", SetLastError = true)]
        public static extern HidpStatus GetButtonCaps(Int32 ReportType, [In, Out] HidButtonCaps[] hidButtonCaps, ref short ButtonCapsLength, SafePreparsedDataHandle PreparsedData);

        [DllImport("hid.dll", EntryPoint = "HidP_GetUsages", SetLastError = true)]
        public static extern HidpStatus GetUsages(
            Int32 ReportType,
            short UsagePage,
            ushort LinkCollection,
            [In, Out] short[] UsageList,
            ref int UsageLength,
            SafePreparsedDataHandle PreparsedData,
            byte[] Report,
            int ReportLength
            );
    }
}
