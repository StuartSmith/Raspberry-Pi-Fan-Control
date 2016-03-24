
using FanController.ViewModels.BaseViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.System.Profile;

namespace FanController.ViewModels
{
    /// <summary>
    /// View Model Class to turn the fan on or off
    /// </summary>
    class VM_FanControllerMain:ViewModelBase
    {
        private const int  FAN_GPIO_PIN = 5;
        private GpioPin GpioFanPin;

        public VM_FanControllerMain()
        {
           ///Only look for a GPIO pin on an IOT device...
           if  (AnalyticsInfo.VersionInfo.DeviceFamily.ToUpper() == "WINDOWS.IOT".ToUpper())
            InitGPIO();
        }

        /// <summary>
        /// Event that is published each time the toggle switch is pressed
        /// </summary>
        public  Action<bool> OnFanChanged { get; set; }

        private bool _IsFanOn =false;
        /// <summary>
        /// Determines if the fan is on or off
        /// </summary>
       
        public bool IsFanOn {
            get { return (_IsFanOn); }
            set {
                if (_IsFanOn != value)
                {
                    _IsFanOn = value;

                    //Call the delegate action when the fan is turned off or on
                    this?.OnFanChanged(value);
                    this.TurnFanOffOrOn(value);
                    OnPropertyChanged();
                }
            }
        }

        private string _GpioStatus="";
        public string GpioStatus {
            get { return (_GpioStatus); }
            set {
                if (_GpioStatus != value)
                {
                    _GpioStatus = value;
                    OnPropertyChanged();
                }
            }
        }

        private void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                GpioFanPin = null;
                GpioStatus = "There is no GPIO controller on this device.";
                return;
            }

            GpioFanPin = gpio.OpenPin(FAN_GPIO_PIN);           
            GpioFanPin.Write(GpioPinValue.Low);
            GpioFanPin.SetDriveMode(GpioPinDriveMode.Output);

        }

        private void TurnFanOffOrOn(bool turnFanOn)
        {
            if  (GpioFanPin == null)
                return;

            if (turnFanOn)
            {
                GpioFanPin.Write(GpioPinValue.High);
                GpioFanPin.SetDriveMode(GpioPinDriveMode.Output);
            }
            else
            {
                GpioFanPin.Write(GpioPinValue.Low);
                GpioFanPin.SetDriveMode(GpioPinDriveMode.Output);
            }

        }

    }
}
