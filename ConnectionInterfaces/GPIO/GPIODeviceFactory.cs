using System;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.GPIO
{
    public static class GPIODeviceFactory
    {
        public static async Task<GpioPin> GetGPIODevice(GPIODevices gpioDevice)
        {
            switch (gpioDevice)
            {
                case GPIODevices.FanRelay:
                    return await GPIOSlave.Initialize(Convert.ToInt32(GPIODevices.FanRelay));
                case GPIODevices.HumidityRelay:
                    return await GPIOSlave.Initialize(Convert.ToInt32(GPIODevices.HumidityRelay));
                case GPIODevices.LightRelay:
                    return await GPIOSlave.Initialize(Convert.ToInt32(GPIODevices.LightRelay));
                default:
                    throw new ArgumentOutOfRangeException(nameof(gpioDevice), gpioDevice, null);
            }
        }
    }
}
