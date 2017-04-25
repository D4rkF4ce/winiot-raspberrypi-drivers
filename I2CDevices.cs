namespace HiGHTECHNiX.AutoPlant.Drivers
{
    // Provides the Adress Numbers for our I2C-Modules according to the datasheets
    public enum I2CDevices
    {
        // The BME280 datasheet: http://www.adafruit.com/datasheets/BST-BME280-DS001-11.pdf
        Hygromether = 0x77,

        // The DS1307 RTC datasheet: http://www.netmftoolbox.com/documents/Hardware.DS1307.pdf
        RealTimeClock = 0x68
    }
}
