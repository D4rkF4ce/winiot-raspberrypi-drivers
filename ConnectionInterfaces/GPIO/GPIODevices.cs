using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.GPIO
{
    public enum GPIODevices
    {
        // Provides the GPIO Pin Numbers for our GPIO-Modules according to the GPIOs on Raspberry Pi
        // https://www.raspberrypi.org/documentation/usage/gpio-plus-and-raspi2/
        FanRelay = 18,
        LightRelay = 23,
        HumidityRelay = 24
    }
}
