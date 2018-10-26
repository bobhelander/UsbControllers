using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsbController.models
{
    public class ReadBuffer
    {
        public byte[] Buffer { get; set; }
        public ulong StateCounter { get; set; }
    }
}
