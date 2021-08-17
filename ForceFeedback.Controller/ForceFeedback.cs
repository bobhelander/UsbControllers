using Microsoft.DirectX.DirectInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForceFeedback.Controller
{
    public class ForceFeedback
    {
        private Device Device { get; set; }

        private readonly Dictionary<string, EffectObject> effectList = new Dictionary<string, EffectObject>();

        public bool InitializeInput(string productId)
        {
            var product = new Guid(productId);

            // Create our joystick device
            foreach (DeviceInstance di in Manager.GetDevices(DeviceClass.GameControl, EnumDevicesFlags.AttachedOnly | EnumDevicesFlags.ForceFeeback))
            {
                if (di.ProductGuid == product)
                {
                    // Pick the first joystick that matches
                    Device = new Device(di.InstanceGuid);
                    break;
                }
            }

            if (Device == null) // We couldn't find a joystick
                return false;

            Device.SetDataFormat(DeviceDataFormat.Joystick);
            //Device.SetCooperativeLevel(this, CooperativeLevelFlags.Exclusive | CooperativeLevelFlags.Background);
            Device.Properties.AxisModeAbsolute = true;
            //device.Properties.AutoCenter = false;
            Device.Acquire();

            return true;
        }

        public List<string> LoadEffects(string fileName = @".\effects\FEdit2.ffe")
        {
            // Load our feedback file
            EffectList effects = Device.GetEffects(fileName, FileEffectsFlags.ModifyIfNeeded);

            foreach (FileEffect fe in effects)
            {
                EffectObject myEffect = new EffectObject(fe.EffectGuid, fe.EffectStruct, Device);
                myEffect.Download();
                effectList.Add(fe.Name, myEffect);
            }

            return effectList.Keys.ToList();
        }

        public void PlayEffect(string name)
        {
            var effect = effectList[name];
            PlayEffect(effect);
        }

        public void PlayEffect(string name, int[] direction)
        {
            var effect = effectList[name];
            SetDirection(effect, direction);
            PlayEffect(effect);
        }

        public void StopEffect(string name)
        {
            var effect = effectList[name];
            StopEffect(effect);
        }

        private void SetDirection(EffectObject effect, int[] direction)
        {
            // https://github.com/tloimu/adapt-ffb-joy/blob/master/FeedbackTester/frmMain.cs

            Effect param = new Effect
            {
                Flags = EffectFlags.Cartesian | EffectFlags.ObjectOffsets
            };

            effect.GetParameters(ref param, EffectParameterFlags.AllParams);

            param.SetDirection(direction);

            effect.SetParameters(param, EffectParameterFlags.Direction);
        }

        private void PlayEffect(EffectObject effect)
        {
            // See if our effects are playing.
            if (false == effect.EffectStatus.Playing)
            {
                effect.Start(1, EffectStartFlags.NoDownload);
            }
        }

        private void StopEffect(EffectObject effect)
        {
            // See if our effects are playing.
            if (effect.EffectStatus.Playing)
            {
                effect.Stop();
            }
        }
    }
}
