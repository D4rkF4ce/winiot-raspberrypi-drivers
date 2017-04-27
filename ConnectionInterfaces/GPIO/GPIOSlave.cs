using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.GPIO
{
    public static class GPIOSlave
    {
        private static string gpioControllerName = "GPIO";
        private static byte gpioPinAddress;
        private static GpioController gpio;

        public static async Task<GpioPin> Initialize(int gpioPinNumber)
        {
            Debug.WriteLine("GPIOSlave::Initialize on GPIO-Pin: " + gpioPinNumber);
            GpioPin gpioPin = null;

            try
            {
                gpio = GpioController.GetDefault();
                if (gpio == null)
                {
                    Debug.WriteLine("There is no GPIO controller on this device.");
                    return null;
                }

                gpioPin = gpio.OpenPin(gpioPinNumber);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            return gpioPin;
        }

        public static async Task<byte> GetGPIOPinAddress()
        {
            Debug.WriteLine("GPIOSlave::GetGPIOPinAddress");
            return gpioPinAddress;
        }
    }
}
