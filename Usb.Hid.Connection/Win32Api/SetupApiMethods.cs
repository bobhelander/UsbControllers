using System;
using System.Runtime.InteropServices;

namespace Usb.Hid.Connection.Win32Api
{
    /// <summary>
    /// Provides direct access to the methods or the "setupapi.dll" library.
    /// </summary>
    /// <seealso href="http://msdn.microsoft.com/library/ff550897"/>
    public static class SetupApiMethods
    {
        /// <summary>
        /// Retrieves the set of devices for a specified class.
        /// </summary>
        /// <param name="deviceClass">
        /// The identifier of the device class.
        /// </param>
        /// <param name="enumerator">
        /// This parameter is not used and should be <c>null</c>.
        /// </param>
        /// <param name="parent">
        /// This parameter should be set to <see cref="IntPtr.Zero"/>.
        /// </param>
        /// <param name="flags">
        /// The constraints to retrieve the devices.</param>
        /// <returns>
        /// A pointer to a device information set that contains all installed devices that
        /// matched the supplied parameters.
        /// </returns>
        /// <seealso href="http://msdn.microsoft.com/library/ff551069"/>
        [DllImport("setupapi.dll", EntryPoint = "SetupDiGetClassDevs", SetLastError = true)]
        public static extern SafeDeviceSetHandle GetClassDevs(
            ref Guid deviceClass,
            [MarshalAs(UnmanagedType.LPStr)] string enumerator,
            IntPtr parent,
            DiGetClassFlags flags);

        /// <summary>
        /// Frees the memory associated to a device information set retrieved with
        /// <see cref="SetupApiMethods.GetClassDevs"/>.
        /// </summary>
        /// <param name="infoSet">The pointer to the device information set.</param>
        /// <returns><c>true</c> if the memory is free; <c>false</c> otherwise.</returns>
        [DllImport("setupapi.dll", EntryPoint = "SetupDiDestroyDeviceInfoList", SetLastError = true)]
        public static extern bool DestroyDeviceInfoList(IntPtr infoSet);

        /// <summary>
        /// Enumerates the device interfaces that are contained in a device information set.
        /// </summary>
        /// <param name="deviceInfoSet">
        /// A pointer to a device information set that contains the device interfaces for
        /// which to return information.
        /// </param>
        /// <param name="deviceInfoData">
        /// This parameter is not used and should be set to <c>0</c>.
        /// </param>
        /// <param name="deviceClass">
        /// A GUID that specifies the device interface class for the requested interface.
        /// </param>
        /// <param name="index">
        /// A zero-based index into the list of interfaces in the device information set. 
        /// </param>
        /// <param name="interfaceData">
        /// A structure that identifies an interface that meets the search parameters.
        /// The caller must set interfaceData.Size to sizeof(DeviceInterfaceData) before
        /// calling this function.
        /// </param>
        /// <returns>
        /// <c>true</c> if successful; <c>false</c> otherwise.
        /// The error code for the failure can be retrieved by calling GetLastError.
        /// </returns>
        /// <remarks>
        /// The caller should call this function first with <paramref name="index"/> set to zero
        /// to obtain the first interface. Then, repeatedly increment <paramref name="index"/>
        /// and retrieve an interface until this function fails and GetLastError returns
        /// <c>ERROR_NO_MORE_ITEMS</c>.
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/library/ff551015"/>
        [DllImport("setupapi.dll", EntryPoint = "SetupDiEnumDeviceInterfaces", SetLastError = true)]
        public static extern bool EnumDeviceInterfaces(
            SafeDeviceSetHandle deviceInfoSet,
            uint deviceInfoData,
            ref Guid deviceClass,
            uint index,
            DeviceInterfaceData interfaceData);

        /// <summary>
        /// Retrieves details about a device interface.
        /// </summary>
        /// <param name="deviceInfoSet">
        /// A pointer to a device information set that contains the device interfaces for
        /// which to return information.
        /// </param>
        /// <param name="interfaceData">
        /// The interface in a device information set for which to retrieve details.
        /// </param>
        /// <param name="deviceInterfaceDetailData">
        /// The instance that receives the information about the specified interface.
        /// This parameter is optional and can be <c>null</c> - see remarks.
        /// </param>
        /// <param name="deviceInterfaceDetailDataSize">
        /// The size of the <paramref name="deviceInterfaceDetailData"/> buffer.
        /// </param>
        /// <param name="requiredSize">
        /// the required size of the <paramref name="deviceInterfaceDetailData"/> buffer.
        /// </param>
        /// <param name="deviceInfoData">
        /// This parameter is not used and should be set to <see cref="IntPtr.Zero"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if successful; <c>false</c> otherwise.
        /// The error code for the failure can be retrieved by calling GetLastError.
        /// </returns>
        /// <remarks>
        /// This method should be called two times:
        /// <list type="number">
        /// <item>
        /// with <paramref name="deviceInterfaceDetailData"/> set to <c>null</c> and
        /// <paramref name="deviceInterfaceDetailDataSize"/> set to <c>0</c> to retrieve through
        /// <paramref name="requiredSize"/> the valid buffer length for the
        /// <paramref name="deviceInterfaceDetailData"/> buffer.
        /// </item>
        /// <item>
        /// with and initialized buffer and its associated size to retrieve the detailed info
        /// about the device interface.
        /// </item>
        /// </list>
        /// <para>
        /// Note: The first call will fail with GetLastError returning ERROR_INSUFFICIENT_BUFFER.
        /// </para>
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/library/ff551120"/>
        [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDeviceInterfaceDetail", SetLastError = true)]
        public static extern bool GetDeviceInterfaceDetail(
            SafeDeviceSetHandle deviceInfoSet,
            DeviceInterfaceData interfaceData,
            ref DeviceInterfaceDetailData deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,
            ref uint requiredSize,
            IntPtr deviceInfoData);


        /// <summary>
        /// Retrieves details about a device interface.
        /// </summary>
        /// <param name="deviceInfoSet">
        /// A pointer to a device information set that contains the device interfaces for
        /// which to return information.
        /// </param>
        /// <param name="interfaceData">
        /// The interface in a device information set for which to retrieve details.
        /// </param>
        /// <param name="deviceInterfaceDetailData">
        /// The instance that receives the information about the specified interface.
        /// This parameter is optional and can be <c>null</c> - see remarks.
        /// </param>
        /// <param name="deviceInterfaceDetailDataSize">
        /// The size of the <paramref name="deviceInterfaceDetailData"/> buffer.
        /// </param>
        /// <param name="requiredSize">
        /// the required size of the <paramref name="deviceInterfaceDetailData"/> buffer.
        /// </param>
        /// <param name="deviceInfoData">
        /// This parameter is not used and should be set to <see cref="IntPtr.Zero"/>.
        /// </param>
        /// <returns>
        /// <c>true</c> if successful; <c>false</c> otherwise.
        /// The error code for the failure can be retrieved by calling GetLastError.
        /// </returns>
        /// <remarks>
        /// This method should be called two times:
        /// <list type="number">
        /// <item>
        /// with <paramref name="deviceInterfaceDetailData"/> set to <c>null</c> and
        /// <paramref name="deviceInterfaceDetailDataSize"/> set to <c>0</c> to retrieve through
        /// <paramref name="requiredSize"/> the valid buffer length for the
        /// <paramref name="deviceInterfaceDetailData"/> buffer.
        /// </item>
        /// <item>
        /// with and initialized buffer and its associated size to retrieve the detailed info
        /// about the device interface.
        /// </item>
        /// </list>
        /// <para>
        /// Note: The first call will fail with GetLastError returning ERROR_INSUFFICIENT_BUFFER.
        /// </para>
        /// </remarks>
        /// <seealso href="http://msdn.microsoft.com/library/ff551120"/>
        [DllImport("setupapi.dll", EntryPoint = "SetupDiGetDeviceInterfaceDetail", SetLastError = true)]
        public static extern bool GetDeviceInterfaceDetail(
            SafeDeviceSetHandle deviceInfoSet,
            DeviceInterfaceData interfaceData,
            IntPtr deviceInterfaceDetailData,
            uint deviceInterfaceDetailDataSize,
            ref uint requiredSize,
            IntPtr deviceInfoData);
    }
}
