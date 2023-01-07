using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Usb.Hid.Connection;

namespace ConsoleApp1
{
    internal static class Program
    {
        public class LogTest { }
        /// <summary>
        /// Just testing things...
        /// </summary>
        /// <param name="args"></param>
        private static async Task Main(string[] args)
        {
            try
            {
                const ushort TTC_VENDORID = 0xF00F;
                const ushort TTC_PRODUCTID_KEYBOARD = 0x00000003;
                // \\?\hid#hidclass#1&1731f3ea&1&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}\kbd
                var test = Usb.Hid.Connection.Devices.RetrieveAllDevicePath(TTC_VENDORID, TTC_PRODUCTID_KEYBOARD).FirstOrDefault();

                var list = Usb.Hid.Connection.Devices.RetrieveAllDevicePath().ToList();

                var controller0 = new Controller(@"\\?\hid#hidclass#1&1731f3ea&1&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}\kbd", new NullLogger<LogTest>());

                var message = GetMessage(0, 0, 0x05, 0, 0, 0, 0, 0);

                var equal = controller0.FeatureLength == message.Length;


                controller0.Feature = message;




                // A lot of commented out calls to different part of the code to determine that the libraries are doing what I want them to do.

                //var paths = Devices.RetrieveAllDevicePath(
                //    Usb.GameControllers.LeoBodnar.BBI32.Joystick.VendorId,
                //    Usb.GameControllers.LeoBodnar.BBI32.Joystick.ProductId);

                var paths = Devices.RetrieveAllDevicePath(
                   Usb.GameControllers.Microsoft.Sidewinder.ForceFeedback2.Joystick.VendorId,
                   Usb.GameControllers.Microsoft.Sidewinder.ForceFeedback2.Joystick.ProductId);

                //var paths = Devices.RetrieveAllDevicePath(
                //   Usb.GameControllers.Microsoft.Sidewinder.StrategicCommander.Joystick.VendorId,
                //    Usb.GameControllers.Microsoft.Sidewinder.StrategicCommander.Joystick.ProductId);

                //var paths = Devices.RetrieveAllDevicePath(
                //    Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick.VendorId,
                //    Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick.ProductId);

                //var paths = Devices.RetrieveAllDevicePath(
                //    Usb.GameControllers.CHProducts.ProPedals.Joystick.VendorId,
                //    Usb.GameControllers.CHProducts.ProPedals.Joystick.ProductId);

                //var paths = Devices.RetrieveAllDevicePath(
                //   Usb.GameControllers.Pioneer.DDJSB2.Joystick.VendorId,
                //   Usb.GameControllers.Pioneer.DDJSB2.Joystick.ProductId);

                //                int idVendor = 0x0C45;
                //              int ipProduct = 0x760A;

                //int idVendor = 0x2B73;
                //int ipProduct = 0x0001;

                // \\?\hid#vid_0c45&pid_760a&mi_01&col06#b&146a75ad&0&0005#{4d1e55b2-f16f-11cf-88cb-001111000030}\kbd
                // \\?\hid#vid_0c45&pid_760a&mi_00#b&279a91eb&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}\kbd
                // \\?\hid#vid_0c45&pid_760a&mi_01&col04#b&146a75ad&0&0003#{4d1e55b2-f16f-11cf-88cb-001111000030}\kbd
                // \\?\hid#vid_0c45&pid_760a&mi_01&col01#b&146a75ad&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}       //mute vol+ vol-
                // \\?\hid#vid_0c45&pid_760a&mi_01&col02#b&146a75ad&0&0001#{4d1e55b2-f16f-11cf-88cb-001111000030}
                // \\?\hid#vid_0c45&pid_760a&mi_01&col03#b&146a75ad&0&0002#{4d1e55b2-f16f-11cf-88cb-001111000030}
                // \\?\hid#vid_0c45&pid_760a&mi_01&col05#b&146a75ad&0&0004#{4d1e55b2-f16f-11cf-88cb-001111000030}\kbd

                //idVendor: 0x0C45(Sonix Technology Co., Ltd.)
                //idProduct: 0x760A

                //var paths = Devices.RetrieveAllDevicePath(idVendor, ipProduct).ToList();
                //Usb.GameControllers.Microsoft.Sidewinder.ForceFeedback2.Joystick.VendorId,
                //Usb.GameControllers.Microsoft.Sidewinder.ForceFeedback2.Joystick.ProductId);

                //var usb = new Usb.Hid.Connection.Controller(paths[6]);

                //usb.ContinuousUsb = true;
                //usb.ContinuousUsbReportSize = 15;
                //usb.ContinuousUsbDebounce = true;
                //usb.ContinuousUsbDebounceButtonsIndex = 1;
                if (false)
                {
                    var bbi32Paths = Devices.RetrieveAllDevicePath(
                        Usb.GameControllers.LeoBodnar.BBI32.Joystick.VendorId,
                        Usb.GameControllers.LeoBodnar.BBI32.Joystick.ProductId);

                    var usb = new Usb.Hid.Connection.Controller(bbi32Paths.First(), null);
                    usb.Subscribe(x => Console.WriteLine($"bbi32: {DateTime.Now}"));
                    usb.Initialize();
                    usb.ProcessInputReport().Wait();
                }
                if (false)
                {
                    using (ILoggerFactory loggerFactory =
                    LoggerFactory.Create(builder => builder.AddSimpleConsole(options =>
                    { options.SingleLine = true; options.TimestampFormat = "hh:mm:ss "; }).SetMinimumLevel(LogLevel.Debug)))
                    {
                        ILogger<Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick> logger 
                            = loggerFactory.CreateLogger<Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick>();

                        var warthog = Devices.RetrieveAllDevicePath(
                            Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick.VendorId,
                            Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick.ProductId);

                        var usb = new Usb.Hid.Connection.Controller(warthog.First(), logger)
                        {
                            ContinuousUsb = true,
                            // Warthog returns raw vales starting at 16.  Only look for changes before that.
                            ContinuousUsbReportSize = 15,

                            // Debounce the buttons
                            ContinuousUsbDebounce = true,
                            // Button data is bytes 1-4 
                            ContinuousUsbDebounceButtonsIndex = 1,
                        };

                        usb.Subscribe(x => Console.WriteLine($"warthog: {DateTime.Now}"));
                        usb.Initialize();
                        //usb.ProcessInputReport().Wait();

                        Console.ReadKey();

                        usb.Stop();
                    }
                }
                if (false)
                {
                    using (ILoggerFactory loggerFactory =
                        LoggerFactory.Create(builder => builder.AddSimpleConsole(options =>
                        { options.SingleLine = true; options.TimestampFormat = "hh:mm:ss "; }).SetMinimumLevel(LogLevel.Debug)))
                    {
                        ILogger<Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick> logger
                            = loggerFactory.CreateLogger<Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick>();

                        var controller = new Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick(paths.First(), logger);

                        controller.Subscribe(x => Console.WriteLine($"warthog: {DateTime.Now}"));

                        controller.Initialize();

                        //var test = controller.Controller.GetInputReport();

                        controller.Lights = (byte)Usb.GameControllers.Thrustmaster.Warthog.Throttle.Models.Light.LED1;
                        System.Threading.Thread.Sleep(500);

                        controller.Lights = (byte)Usb.GameControllers.Thrustmaster.Warthog.Throttle.Models.Light.LED2;
                        System.Threading.Thread.Sleep(500);

                        controller.Lights = (byte)Usb.GameControllers.Thrustmaster.Warthog.Throttle.Models.Light.LED3;
                        System.Threading.Thread.Sleep(500);
                        //usb.ProcessSerialMessage(test.Length, test, test.Length, 0).Wait();

                        Console.ReadKey();
                    }
                }
                if (false)
                {
                    var controller = new Usb.GameControllers.CHProducts.ProPedals.Joystick(paths.First(), null);

                    controller.Initialize();
                }
                if (true)
                {
                    var controller = new Usb.GameControllers.Microsoft.Sidewinder.ForceFeedback2.Joystick(paths.First(), null);

                    controller.Initialize();

                    Console.ReadKey();
                }
            }
            catch(Exception ex)
            {
                 Console.WriteLine(ex.Message);
            }

            while (true) ;
            Console.ReadKey();
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SetFeatureKeyboard
        {
            public Byte ReportID;
            public Byte CommandCode;
            public uint Timeout;
            public Byte Modifier;
            public Byte Padding;
            public Byte Key0;
            public Byte Key1;
            public Byte Key2;
            public Byte Key3;
            public Byte Key4;
            public Byte Key5;
        }

        private static byte[] GetMessage(Byte Modifier, Byte Padding, Byte Key0, Byte Key1, Byte Key2, Byte Key3, Byte Key4, Byte Key5)
        {
            SetFeatureKeyboard KeyboardData = new SetFeatureKeyboard
            {
                ReportID = 1,
                CommandCode = 2,
                Timeout = 1000, //5 because we count in blocks of 5 in the driver
                Modifier = Modifier,
                //padding should always be zero.
                Padding = Padding,
                Key0 = Key0,
                Key1 = Key1,
                Key2 = Key2,
                Key3 = Key3,
                Key4 = Key4,
                Key5 = Key5
            };
            //convert struct to buffer
            return getBytesSFJ(KeyboardData, Marshal.SizeOf(KeyboardData));
        }

        //for converting a struct to byte array
        private static byte[] getBytesSFJ(SetFeatureKeyboard sfj, int size)
        {
            byte[] arr = new byte[size];
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(sfj, ptr, false);
            Marshal.Copy(ptr, arr, 0, size);
            Marshal.FreeHGlobal(ptr);
            return arr;
        }
    }
}
