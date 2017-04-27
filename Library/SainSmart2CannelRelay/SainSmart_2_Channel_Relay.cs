using System;
using System.Diagnostics;
using Windows.Devices.Gpio;

namespace HiGHTECHNiX.AutoPlant.Drivers.Library.SainSmart2CannelRelay
{
    public class SainSmart_2_Channel_Relay : IDisposable
    {
        private GpioPin relay1Pin;
        private bool m_blnIsDisposed = false;

        public SainSmart_2_Channel_Relay(GpioPin gpioPin)
        {
            relay1Pin = gpioPin;
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
            Debug.WriteLine("Dispose is TRUE");
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool blnDisposing)
        {
            if (!m_blnIsDisposed)
            {
                // Methode wird zum ersten Mal aufgerufen
                if (blnDisposing)
                {
                    // Managed Objekte freigeben. Wenn diese Obbjekte selbst IDisposable implementieren, dann deren Dispose() aufrufen
                    if (relay1Pin != null)
                    {
                        relay1Pin.Dispose();
                        relay1Pin = null;
                    }
                }

                // Hier unmanaged Objekte freigeben (z.B. IntPtr)
                if (relay1Pin != null)
                {
                    relay1Pin.Dispose();
                    relay1Pin = null;
                }
            }
            // Dafür sorgen, dass Methode nicht mehr aufgerufen werden kann.
            m_blnIsDisposed = true;
        }
    }
}
