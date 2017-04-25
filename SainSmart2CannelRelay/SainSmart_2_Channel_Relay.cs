using System;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace HiGHTECHNiX.AutoPlant.Drivers.SainSmart2CannelRelay
{
    public class SainSmart_2_Channel_Relay
    {
        private GpioController gpio;
        private int PinRelay1 = 0;
        private GpioPin relay1Pin;
        public bool IsRunning { get; set; }

        public SainSmart_2_Channel_Relay(int gpioPin)
        {
            PinRelay1 = gpioPin;
            
        }

        public bool Initialize()
        {
            bool success = false;

            try
            {
                gpio = GpioController.GetDefault();
                if (gpio == null)
                {
                    Debug.WriteLine("There is no GPIO controller on this device.");
                    return success;
                }

                relay1Pin = gpio.OpenPin(PinRelay1);
                success = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            return success;
        }

        public void SwitchOn()
        {            
            relay1Pin.Write(GpioPinValue.Low);
            relay1Pin.SetDriveMode(GpioPinDriveMode.Output);            
        }

        public void SwitchOff()
        {            
            relay1Pin.Write(GpioPinValue.High);
            relay1Pin.SetDriveMode(GpioPinDriveMode.Output);
        }

        public void Dispose()
        {
            relay1Pin.Dispose();
        }
    }
}
