using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.I2c;

namespace HiGHTECHNiX.AutoPlant.Drivers
{
    public static class I2cSlave
    {
        private static string i2cControllerName = "I2C1";
        private static byte i2cAddress;

        public static async Task<I2cDevice> Initialize(byte i2cDevice)
        {
            Debug.WriteLine("I2cSlave::Initialize on I2C_Address: " + i2cDevice);
            I2cDevice I2CModul = null;

            try
            {
                i2cAddress = i2cDevice;

                //Instantiate the I2CConnectionSettings using the device address 
                I2cConnectionSettings settings = new I2cConnectionSettings(i2cDevice);

                //Set the I2C bus speed of connection to fast mode
                settings.BusSpeed = I2cBusSpeed.FastMode;
                
                //Set I2C connection to shared mode
                settings.SharingMode = I2cSharingMode.Shared;
                
                //Use the I2CBus device selector to create an advanced query syntax string
                string aqs = I2cDevice.GetDeviceSelector(i2cControllerName);
                
                //Use the Windows.Devices.Enumeration.DeviceInformation class to create a collection using the advanced query syntax string
                DeviceInformationCollection dis = await DeviceInformation.FindAllAsync(aqs);
                
                //Instantiate the I2C device using the device id of the I2CBus and the I2CConnectionSettings
                I2CModul = await I2cDevice.FromIdAsync(dis[0].Id, settings);
                
                //Check if device was found
                if (I2CModul == null)
                {
                    Debug.WriteLine("Device not found");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception: " + e.Message + "\n" + e.StackTrace);
                throw;
            }

            return I2CModul;
        }

        public static async Task<byte> GetI2cAddress()
        {
            Debug.WriteLine("I2cSlave::GetI2cAddress");
            return i2cAddress;
        }
    }
}
