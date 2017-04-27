using System;
using System.Threading.Tasks;
using Windows.Devices.I2c;

namespace HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.I2C
{
    public static class I2CDeviceFactory
    {
        public static async Task<I2cDevice> GetI2CDevice(I2CDevices i2cDevice)
        {
            switch (i2cDevice)
            {
                case I2CDevices.Hygromether:
                    return await I2CSlave.Initialize(Convert.ToByte(I2CDevices.Hygromether));

                case I2CDevices.RealTimeClock:
                    return await I2CSlave.Initialize(Convert.ToByte(I2CDevices.RealTimeClock));

                default:
                    throw new ArgumentOutOfRangeException(nameof(i2cDevice), i2cDevice, null);
            }
        }
    }
}
