using System.Diagnostics;
using System.Threading.Tasks;
using HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.I2C;
using HiGHTECHNiX.AutoPlant.Drivers.Library.AdafruitBME280;

namespace HiGHTECHNiX.AutoPlant.Drivers.Collection
{
    public class Hygrometer
    {
        private BME280_Sensor _bme280;
        private I2CDevices _deviceType;
        private string family = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily; 
           
        public Hygrometer(I2CDevices i2cDevice)
        {
            _deviceType = i2cDevice;
        }
        public async Task<float> ReadTemperature()
        {
            float returnValue = -1;

            if (family != "Windows.Desktop")
            {
                var slave = await I2CDeviceFactory.GetI2CDevice(_deviceType);
                if (slave != null)
                {
                    using (_bme280 = new BME280_Sensor(slave))
                    {
                        returnValue = await _bme280.ReadTemperature();
                    }
                }
            }
            else
            {
                Debug.WriteLine("This is not a ARM device.");
            }

            return returnValue;
        }
        public async Task<float> ReadPressure()
        {
            float returnValue = -1;

            if (family != "Windows.Desktop")
            {
                var slave = await I2CDeviceFactory.GetI2CDevice(_deviceType);
                if (slave != null)
                {
                    using (_bme280 = new BME280_Sensor(slave))
                    {
                        returnValue = await _bme280.ReadPressure();
                    }
                }
            }
            else
            {
                Debug.WriteLine("This is not a ARM device.");
            }

            return returnValue;
        }
        public async Task<float> ReadHumidity()
        {
            float returnValue = -1;

            if (family != "Windows.Desktop")
            {
                var slave = await I2CDeviceFactory.GetI2CDevice(_deviceType);
                if (slave != null)
                {
                    using (_bme280 = new BME280_Sensor(slave))
                    {
                        returnValue = await _bme280.ReadHumidity();
                    }
                }
            }
            else
            {
                Debug.WriteLine("This is not a ARM device.");
            }

            return returnValue;
        }
    }
}
