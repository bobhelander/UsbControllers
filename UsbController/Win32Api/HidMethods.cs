using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace UsbController.Win32Api
{
    /// <summary>
    /// Provides direct access to the methods or the "hid.dll" library.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538865(v=vs.85).aspx"/>
    public static class HidMethods
    {
        /// <summary>
        /// Gets the GUID that Windows uses to represent HID class devices.
        /// </summary>
        /// <param name="hidGuid">
        /// The instance used to retrieve the device interface GUID for HIDClass devices.
        /// </param>
        /// <seealso href="http://msdn.microsoft.com/library/windows/hardware/ff538924.aspx"/>
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
        /// <seealso cref="HidMethods.GetCaps(SafePreparsedDataHandle, out HidCaps)"/>
        /// <seealso cref="HidMethods.GetCaps(SafePreparsedDataHandle)"/>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539679.aspx"/>
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
        /// <seealso cref="HidMethods.GetCaps(SafePreparsedDataHandle, out HidCaps)"/>
        /// <seealso cref="HidMethods.GetCaps(SafePreparsedDataHandle)"/>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539679.aspx"/>
        public static SafePreparsedDataHandle GetPreparsedData(SafeFileHandle device)
        {
            SafePreparsedDataHandle result;
            if (!GetPreparsedData(device, out result))
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
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538893.aspx"/>
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
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539715.aspx"/>
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
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539715.aspx"/>
        public static HidCaps GetCaps(SafePreparsedDataHandle preparsedData)
        {
            HidCaps result;
            HidpStatus status = GetCaps(preparsedData, out result);
            if (status != HidpStatus.Success)
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
        /// <remarks>
        /// <para>The report length is defined in the capabilities of the HID device.</para>
        /// <para>The buffer can be initialized with the following code:</para>
        /// <code><![CDATA[
        /// byte[] buffer = new byte[count];
        /// IntPtr ptr = Marshal.AllocCoTaskMem(count);
        /// Marshal.Copy(buffer, offset, ptr, count);
        /// HidMethods.GetFeature(handle, ptr, count);
        /// Marshal.Copy(ptr, buffer, offset, count);
        /// Marshal.FreeCoTaskMem(ptr);
        /// ]]></code>
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff538910.aspx"/>
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
        /// <remarks>
        /// <para>The report length is defined in the capabilities of the HID device.</para>
        /// <para>The buffer can be initialized with the following code:</para>
        /// <code><![CDATA[
        /// byte[] buffer = new byte[count];
        /// IntPtr ptr = Marshal.AllocCoTaskMem(count);
        /// Marshal.Copy(buffer, offset, ptr, count);
        /// HidMethods.SetFeature(handle, ptr, count);
        /// Marshal.Copy(ptr, buffer, offset, count);
        /// Marshal.FreeCoTaskMem(ptr);
        /// ]]></code>
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/en-us/library/windows/hardware/ff539684(v=vs.85).aspx"/>
        [DllImport("hid.dll", EntryPoint = "HidD_SetFeature", SetLastError = true)]
        public static extern bool SetFeature(SafeFileHandle hidDeviceObject, byte[] reportBuffer, int reportBufferLength);
    }
}
