using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Usb.Hid.Connection.models
{
    public class ReadBuffer
    {
        public byte[] Buffer { get; set; }
        public ulong StateCounter { get; set; }
    }
}
