using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usb.Hid.Connection;

namespace ConsoleApp1
{
    class Program
    {
        /// <summary>
        /// Just testing things...
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                // A lot of commented out calls to different part of the code to determine that the libraries are doing what I want them to do.

                //var paths = Devices.RetrieveAllDevicePath(
                //    Usb.GameControllers.LeoBodnar.BBI32.Joystick.VendorId,
                //    Usb.GameControllers.LeoBodnar.BBI32.Joystick.ProductId);

                //var paths = Devices.RetrieveAllDevicePath(
                //   Usb.GameControllers.Microsoft.Sidewinder.StrategicCommander.Joystick.VendorId,
                //    Usb.GameControllers.Microsoft.Sidewinder.StrategicCommander.Joystick.ProductId);

                //var paths = Devices.RetrieveAllDevicePath(
                //    Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick.VendorId,
                //    Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick.ProductId);

                var paths = Devices.RetrieveAllDevicePath(
                    Usb.GameControllers.CHProducts.ProPedals.Joystick.VendorId,
                    Usb.GameControllers.CHProducts.ProPedals.Joystick.ProductId);

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
                    var usb = new Usb.Hid.Connection.Controller(paths.First());
                    usb.Subscribe(x => Console.WriteLine($"bbi32: {DateTime.Now}"));
                    usb.Initialize();
                    usb.ProcessInputReport().Wait();
                }
                if (false)
                {
                    var controller = new Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick(paths.First());

                    controller.Initialize();

                    //var test = usb.GetInputReport();

                    controller.Lights = (byte)Usb.GameControllers.Thrustmaster.Warthog.Throttle.Models.Light.LED1;
                    System.Threading.Thread.Sleep(500);

                    controller.Lights = (byte)Usb.GameControllers.Thrustmaster.Warthog.Throttle.Models.Light.LED2;
                    System.Threading.Thread.Sleep(500);

                    controller.Lights = (byte)Usb.GameControllers.Thrustmaster.Warthog.Throttle.Models.Light.LED3;
                    System.Threading.Thread.Sleep(500);
                    //usb.ProcessSerialMessage(test.Length, test, test.Length, 0).Wait();
                }
                if (true)
                {
                    var controller = new Usb.GameControllers.CHProducts.ProPedals.Joystick(paths.First());

                    controller.Initialize();
                }
            }
            catch(Exception ex)
            {
                 ;
            }

            while (true) ;
            Console.ReadKey();
        }
    }
}
