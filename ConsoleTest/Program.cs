using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Sidewinder.ForceFeedback2.models;
using Usb.Hid.Connection;

namespace ConsoleTest
{
    class Program
    {
        public class Inspector : IObserver<Thrustmaster.Warthog.Throttle.models.States>
        {
            public void OnCompleted()
            {
                
            }

            public void OnError(Exception error)
            {
                
            }

            public void OnNext(Thrustmaster.Warthog.Throttle.models.States value)
            {
                Console.WriteLine(((Thrustmaster.Warthog.Throttle.models.State)value.Current).X);
            }
        }

        static void Main(string[] args)
        {
            var devices = Devices.RetrieveAllDevicePath(
                Thrustmaster.Warthog.Throttle.Joystick.VendorId,
                Thrustmaster.Warthog.Throttle.Joystick.ProductId);

            var devicePath = devices.First();

            var joystick = new Thrustmaster.Warthog.Throttle.Joystick(devicePath);



            joystick.Subscribe(new Inspector());

            joystick.Initialize();

            joystick.Lights = (byte)(//Thrustmaster.Warthog.Throttle.models.Light.LEDBacklight |
                               Thrustmaster.Warthog.Throttle.models.Light.LED1 |
                               Thrustmaster.Warthog.Throttle.models.Light.LED2 |
                               Thrustmaster.Warthog.Throttle.models.Light.LED3 |
                               Thrustmaster.Warthog.Throttle.models.Light.LED4 |
                               Thrustmaster.Warthog.Throttle.models.Light.LED5);

            joystick.LightIntensity = 5;




            while (true)
            {
                ;
            }
        }

        /*
        static void Main2(string[] args)
        {
            var devices = Devices.RetrieveAllDevicePath(
                Microsoft.Sidewinder.ForceFeedback2.Joystick.VendorId, 
                Microsoft.Sidewinder.ForceFeedback2.Joystick.ProductId);

            var devicePath = devices.First();

            var joystick = new Microsoft.Sidewinder.ForceFeedback2.Joystick(devicePath);

            

            joystick.Subscribe(new Inspector());

            joystick.Initialize();

            var effects = joystick.LoadEffects(@".\effects\FEdit2.ffe");

            joystick.PlayEffect("SawtoothDown1");

            while (true)
            {
                ;
            }
        }
        */
    }
}
