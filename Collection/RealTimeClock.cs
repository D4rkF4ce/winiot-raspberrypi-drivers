using System;
using System.Diagnostics;
using System.Threading.Tasks;
using HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.I2C;
using HiGHTECHNiX.AutoPlant.Drivers.Library.SainSmartDS1307RTC;

namespace HiGHTECHNiX.AutoPlant.Drivers.Collection
{
    public class RealTimeClock
    {
        private SainSmart_DS_1307_RTC _rtc;
        private I2CDevices _deviceType;
        private string family = Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily;

        public RealTimeClock(I2CDevices i2cDevice)
        {
            _deviceType = i2cDevice;
        }
        public async Task SetDateTime(DateTime dateTime)
        {
            if (family != "Windows.Desktop")
            {
                var slave = await I2CDeviceFactory.GetI2CDevice(_deviceType);
                using (_rtc = new SainSmart_DS_1307_RTC(slave))
                {
                    _rtc.SetDateTime(dateTime);
                }
            }
            else
            {
                Debug.WriteLine("This is not a ARM device.");
            }
        }
        public async Task<DateTime> GetDateTime()
        {
            DateTime returnDate = DateTime.MinValue;

            if (family != "Windows.Desktop")
            {
                var slave = await I2CDeviceFactory.GetI2CDevice(_deviceType);
                using (_rtc = new SainSmart_DS_1307_RTC(slave))
                {
                    returnDate = _rtc.GetDateTimeNow();
                }
            }
            else
            {
                Debug.WriteLine("This is not a ARM device.");
            }

            return returnDate;
        }
    }
}
