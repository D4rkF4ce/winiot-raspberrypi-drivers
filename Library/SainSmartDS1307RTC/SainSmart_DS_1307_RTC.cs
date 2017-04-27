using System;
using System.Diagnostics;
using Windows.Devices.I2c;

namespace HiGHTECHNiX.AutoPlant.Drivers.Library.SainSmartDS1307RTC
{
    public class SainSmart_DS_1307_RTC : IDisposable
    {
        public I2cDevice Slave { get; set; }
        private bool m_blnIsDisposed = false;

        public SainSmart_DS_1307_RTC(I2cDevice slave)
        {
            this.Slave = slave;
        }

        ~SainSmart_DS_1307_RTC()
        {
            Dispose(false);
        }

        
        public DateTime GetDateTimeNow()
        {
            Debug.WriteLine("DS_1307_RTC::GetCurrentTime");

            try
            {
                byte[] readBuffer = new byte[7];
                this.Slave.WriteRead(new byte[] { 0x00 }, readBuffer);
                return ConvertByteBufferToDateTime(readBuffer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }

            return DateTime.MinValue;
        }        
        public void SetDateTime(DateTime dateTime)
        {
            Debug.WriteLine("DS_1307_RTC::SetDateTime");

            try
            {
                this.Slave.Write(ConvertTimeToByteArray(dateTime));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }            
        }




        private int BinaryCodedDecimalToInteger(int value)
        {
            var lowerNibble = value & 0x0F;
            var upperNibble = value >> 4;

            var multipleOfOne = lowerNibble;
            var multipleOfTen = upperNibble * 10;

            return multipleOfOne + multipleOfTen;
        }
        private DateTime ConvertByteBufferToDateTime(byte[] dateTimeBuffer)
        {
            var second = BinaryCodedDecimalToInteger(dateTimeBuffer[0]);
            var minute = BinaryCodedDecimalToInteger(dateTimeBuffer[1]);
            var hour = BinaryCodedDecimalToInteger(dateTimeBuffer[2]);
            var dayofWeek = BinaryCodedDecimalToInteger(dateTimeBuffer[3]);
            var day = BinaryCodedDecimalToInteger(dateTimeBuffer[4]);
            var month = BinaryCodedDecimalToInteger(dateTimeBuffer[5]);
            var year = 2000 + BinaryCodedDecimalToInteger(dateTimeBuffer[6]);

            return new DateTime(year, month, day, hour, minute, second);
        }
        private byte[] ConvertTimeToByteArray(DateTime dateTime)
        {
            var dateTimeByteArray = new byte[8];

            dateTimeByteArray[0] = 0;
            dateTimeByteArray[1] = IntegerToBinaryCodedDecimal(dateTime.Second);
            dateTimeByteArray[2] = IntegerToBinaryCodedDecimal(dateTime.Minute);
            dateTimeByteArray[3] = IntegerToBinaryCodedDecimal(dateTime.Hour);
            dateTimeByteArray[4] = IntegerToBinaryCodedDecimal((byte)dateTime.DayOfWeek);
            dateTimeByteArray[5] = IntegerToBinaryCodedDecimal(dateTime.Day);
            dateTimeByteArray[6] = IntegerToBinaryCodedDecimal(dateTime.Month);
            dateTimeByteArray[7] = IntegerToBinaryCodedDecimal(dateTime.Year - 2000);

            return dateTimeByteArray;
        }
        private byte IntegerToBinaryCodedDecimal(int value)
        {
            var multipleOfOne = value % 10;
            var multipleOfTen = value / 10;

            // convert to nibbles
            var lowerNibble = multipleOfOne;
            var upperNibble = multipleOfTen << 4;

            return (byte)(lowerNibble + upperNibble);
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
                    if (Slave != null)
                    {
                        Slave.Dispose();
                        Slave = null;
                    }
                }

                // Hier unmanaged Objekte freigeben (z.B. IntPtr)
                if (Slave != null)
                {
                    Slave.Dispose();
                    Slave = null;
                }
            }
            // Dafür sorgen, dass Methode nicht mehr aufgerufen werden kann.
            m_blnIsDisposed = true;
        }
    }
}
