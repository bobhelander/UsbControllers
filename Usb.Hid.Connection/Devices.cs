using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.Hid.Connection
{
    public static class Devices
    {
        /// <summary>
        /// Retrieves all the device paths for a specified vendor and product ids.
        /// </summary>
        /// <param name="vendorId">The vendor id.</param>
        /// <param name="productId">The product id.</param>
        /// <returns>
        /// The device paths associated to <paramref name="vendorId"/> and
        /// <paramref name="productId"/>.
        /// </returns>
        public static IEnumerable<string> RetrieveAllDevicePath(int vendorId, int productId)
        {
            // build the path search string
            string search = string.Format("vid_{0:x4}&pid_{1:x4}", vendorId, productId);

            return Device.GetInterfaceDevices(HidStream.HidGuid)
                .Where(d => d.DevicePath?.Contains(search) == true)
                .Select(d => d.DevicePath);
        }

        /// <summary>
        /// Retrieves all device path strings.  Create a Controller from the path to interrogate device identifiers.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> RetrieveAllDevicePath()
        {
            return Device.GetInterfaceDevices(HidStream.HidGuid).Select(d => d.DevicePath);
        }
    }
}
