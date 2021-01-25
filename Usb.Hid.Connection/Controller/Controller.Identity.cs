using System;
using System.Collections.Generic;
using System.Text;

namespace Usb.Hid.Connection
{
    public partial class Controller
    {
        public string PhysicalDescriptor
        {
            get => this.stream?.PhysicalDescriptor;
        }

        public string ManufacturerString
        {
            get => this.stream?.ManufacturerString;
        }

        public string ProductString
        {
            get => this.stream?.ProductString;
        }

        public string SerialNumberString
        {
            get => this.stream?.SerialNumberString;
        }
    }
}
