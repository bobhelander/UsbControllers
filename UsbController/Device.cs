using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UsbController.Win32Api;

namespace UsbController
{
    /// <summary>
    /// Encapsulates the calls to the <c>setupapi</c> library that provides a list of devices.
    /// </summary>
    internal class Device
    {
        /// <summary>
        /// A pointer to the associated set of devices.
        /// </summary>
        private SafeDeviceSetHandle deviceSet;

        /// <summary>
        /// The details about the device.
        /// </summary>
        private DeviceInterfaceData interfaceData;

        /// <summary>
        /// The path of the device.
        /// </summary>
        private string devicePath;

        /// <summary>
        /// Initializes a new instance of the <see cref="Device"/> class.
        /// </summary>
        /// <param name="deviceSet"></param>
        /// <param name="interfaceData"></param>
        private Device(SafeDeviceSetHandle deviceSet, DeviceInterfaceData interfaceData)
        {
            this.deviceSet = deviceSet;
            this.interfaceData = interfaceData;
        }

        /// <summary>
        /// Gets the identifier of the interface class.
        /// </summary>
        public Guid InterfaceClassGuid
        {
            get { return this.interfaceData.InterfaceClassGuid; }
        }

        /// <summary>
        /// Retrieves the path of the device.
        /// </summary>
        public string DevicePath
        {
            get
            {
                if (this.devicePath == null)
                {
                    uint requiredSize = 0;
                    if (!SetupApiMethods.GetDeviceInterfaceDetail(this.deviceSet, this.interfaceData, IntPtr.Zero, 0, ref requiredSize, IntPtr.Zero))
                    {
                        DeviceInterfaceDetailData detail = new DeviceInterfaceDetailData { Size = 5 };

                        if (SetupApiMethods.GetDeviceInterfaceDetail(this.deviceSet, this.interfaceData, ref detail, requiredSize, ref requiredSize, IntPtr.Zero))
                        {
                            this.devicePath = detail.DevicePath;
                        }
                    }
                }

                return this.devicePath;
            }
        }

        /// <summary>
        /// Retrieves the present interface devices of the specified class.
        /// </summary>
        /// <param name="deviceClass">The interface device class identifier.</param>
        /// <returns>A list of all the devices that are associated with this search.</returns>
        public static IEnumerable<Device> GetInterfaceDevices(Guid deviceClass)
        {
            return new DeviceCollection(deviceClass, DiGetClassFlags.DeviceInterface | DiGetClassFlags.Present);
        }

        /// <summary>
        /// Implements an enumerable collection of devices.
        /// </summary>
        private class DeviceCollection : IEnumerable<Device>
        {
            /// <summary>
            /// The identifier of the device class.
            /// </summary>
            private readonly Guid deviceClass;

            /// <summary>
            /// The flags used to defined the type of expected devices.
            /// </summary>
            private readonly DiGetClassFlags flags;

            /// <summary>
            /// Initializes a new instance of the <see cref="DeviceCollection"/> class.
            /// </summary>
            /// <param name="deviceClass">The identifier of the device class.</param>
            /// <param name="flags">The flags used to defined the type of expected devices.</param>
            public DeviceCollection(Guid deviceClass, DiGetClassFlags flags)
            {
                this.deviceClass = deviceClass;
                this.flags = flags;
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// An enumerator that can be used to iterate through the collection.
            /// </returns>
            public IEnumerator<Device> GetEnumerator()
            {
                return new DeviceEnumerator(deviceClass, flags);
            }

            /// <summary>
            /// Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>
            /// An enumerator that can be used to iterate through the collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }

        /// <summary>
        /// Supports a simple iteration over a collection of devices.
        /// </summary>
        private class DeviceEnumerator : IEnumerator<Device>
        {
            /// <summary>
            /// The identifier of the associated devices.
            /// </summary>
            private Guid deviceClass;

            /// <summary>
            /// The pointer to the set of devices.
            /// </summary>
            private SafeDeviceSetHandle deviceSet;

            /// <summary>
            /// The index of the current device in the device set.
            /// </summary>
            private int index;

            /// <summary>
            /// The current device data.
            /// </summary>
            private DeviceInterfaceData interfaceData;

            /// <summary>
            /// Initializes a new instance of the <see cref="DeviceEnumerator"/> class.
            /// </summary>
            /// <param name="deviceClass">The identifier of the associated devices.</param>
            /// <param name="flags">The flags used to defined the type of expected devices.</param>
            public DeviceEnumerator(Guid deviceClass, DiGetClassFlags flags)
            {
                this.deviceClass = deviceClass;
                this.deviceSet = SetupApiMethods.GetClassDevs(ref deviceClass, null, IntPtr.Zero, flags);
                this.interfaceData = new DeviceInterfaceData();
                this.index = -1;
            }

            /// <summary>
            /// Gets the current device.
            /// </summary>
            public Device Current { get; private set; }

            /// <summary>
            /// Gets the current device.
            /// </summary>
            object IEnumerator.Current
            {
                get { return this.Current; }
            }

            /// <summary>
            /// Disposes the encapsulated set of devices.
            /// </summary>
            public void Dispose()
            {
                this.deviceSet.Dispose();
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            /// <c>true</c> if the enumerator was successfully advanced to the next element;
            /// <c>false</c> if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                this.index++;
                bool result = SetupApiMethods.EnumDeviceInterfaces(this.deviceSet, 0, ref this.deviceClass, (uint)this.index, this.interfaceData);
                this.Current = new Device(this.deviceSet, this.interfaceData);
                return result;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element
            /// in the collection.
            /// </summary>
            public void Reset()
            {
                this.index = -1;
            }
        }
    }
}
