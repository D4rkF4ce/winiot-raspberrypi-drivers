using System.Diagnostics;
using System.Threading.Tasks;
using HiGHTECHNiX.AutoPlant.Drivers.ConnectionInterfaces.GPIO;
using HiGHTECHNiX.AutoPlant.Drivers.Library.SainSmart2CannelRelay;
using HiGHTECHNiX.AutoPlant.Helper;

namespace HiGHTECHNiX.AutoPlant.Drivers.Collection
{
    public class PowerRelay
    {
        private SainSmart_2_Channel_Relay _relay;
        private GPIODevices _deviceType;
        public bool IsRunning { get; set; }

        public PowerRelay(GPIODevices gpioDevice)
        {
            _deviceType = gpioDevice;
        }
        public async Task SwitchOn()
        {
            if (SystemHelper.Instance.IsARMDevice)
            {
                if (!IsRunning)
                {
                    var slave = await GPIODeviceFactory.GetGPIODevice(_deviceType);
                    using (_relay = new SainSmart_2_Channel_Relay(slave))
                    {
                        _relay.SwitchOn();
                    }

                    IsRunning = true;
                }
                else
                {
                    Debug.WriteLine("GPIO Device already running");
                }
            }
            else
            {
                Debug.WriteLine(SystemHelper.Instance.GetNoARMDeviceErrorMessage());
            }
        }
        public async Task SwitchOff()
        {
            if (SystemHelper.Instance.IsARMDevice)
            {
                if (IsRunning)
                {
                    var slave = await GPIODeviceFactory.GetGPIODevice(_deviceType);
                    using (_relay = new SainSmart_2_Channel_Relay(slave))
                    {
                        _relay.SwitchOff();
                    }

                    IsRunning = false;
                }
                else
                {
                    Debug.WriteLine("GPIO Device is not running");
                }
            }
            else
            {
                Debug.WriteLine(SystemHelper.Instance.GetNoARMDeviceErrorMessage());
            }
        }
    }
}
