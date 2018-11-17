# Usb.Hid.Connection & Usb.GameControllers

https://www.nuget.org/packages/Usb.Hid.Connection/ <br/>
https://www.nuget.org/packages/Usb.GameControllers <br/>

Allows reading USB HID reports directly from the listed devices.  Reports are mapped into device specific mappings.  Allows setting of the lights on the supported devices.  Reports are subscribable with the IObservable interface. <br/>

### Devices Supported: <br/>
Microsoft Sidwinder Force Feedback 2 (FFB2) <br/>
Microsoft Sidwinder Strategic Commander <br/>
Microsoft Sidwinder Game Voice <br/>
Thrustmaster Warthog Throttle <br/>
CH Products Pro Pedals <br/>
Leo Bodnar BBI32   http://www.leobodnar.com/ <br/>

```C#
// Get the path to the first Microsoft Force Feedback 2 Joystick
var path = Usb.Hid.Connection.Devices.RetrieveAllDevicePath(0x045e, 0x001b).First();

// Set the controller to read from the device path
var controller = new Controller(path);

// Do something when a report is sent
controller.Subscribe(x => Console.WriteLine("Report Received"));

// Start reading data from the controller
controller.Initialize();
```

Translate the report into button and axis movements
```
var path = Usb.Hid.Connection.Devices.RetrieveAllDevicePath(
	Usb.GameControllers.Thrustmaster.Warthog.Throttle.VendorId, 
	Usb.GameControllers.Thrustmaster.Warthog.Throttle.ProductId).First();

// Create a Warthog controller	
var warthog = Usb.GameControllers.Thrustmaster.Warthog.Throttle.Joystick(path);

warthog.Subscribe(x => 
	Console.WriteLine($"Buttons: {((Usb.GameControllers.Thrustmaster.Warthog.Throttle.Model.State)x.Current).buttons}") );
	