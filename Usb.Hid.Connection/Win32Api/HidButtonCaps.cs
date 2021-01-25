using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Usb.Hid.Connection.Win32Api
{
    [StructLayout(LayoutKind.Sequential)]
    public struct HidP_Range
    {
        public short UsageMin;
        public short UsageMax;
        public short StringMin;
        public short StringMax;
        public short DesignatorMin;
        public short DesignatorMax;
        public short DataIndexMin;
        public short DataIndexMax;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HidP_NotRange
    {
        public short Usage;
        public short Reserved1;
        public short StringIndex;
        public short Reserved2;
        public short DesignatorIndex;
        public short Reserved3;
        public short DataIndex;
        public short Reserved4;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct HidButtonCaps
    {
        [FieldOffset(0)]
        public short UsagePage;
        [FieldOffset(2)]
        public byte ReportID;
        [FieldOffset(3), MarshalAs(UnmanagedType.U1)]
        public bool IsAlias;
        [FieldOffset(4)]
        public short BitField;
        [FieldOffset(6)]
        public short LinkCollection;
        [FieldOffset(8)]
        public short LinkUsage;
        [FieldOffset(10)]
        public short LinkUsagePage;
        [FieldOffset(12), MarshalAs(UnmanagedType.U1)]
        public bool IsRange;
        [FieldOffset(13), MarshalAs(UnmanagedType.U1)]
        public bool IsStringRange;
        [FieldOffset(14), MarshalAs(UnmanagedType.U1)]
        public bool IsDesignatorRange;
        [FieldOffset(15), MarshalAs(UnmanagedType.U1)]
        public bool IsAbsolute;
        [FieldOffset(16), MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] Reserved;

        [FieldOffset(56)]
        public HidP_Range Range;
        [FieldOffset(56)]
        public HidP_NotRange NotRange;
    }
}
