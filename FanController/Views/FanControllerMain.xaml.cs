using FanController.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace FanController.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FanControllerMain : Page
    {
        VM_FanControllerMain vm_FanControlMain;

        public FanControllerMain()
        {
            this.InitializeComponent();
            vm_FanControlMain = new VM_FanControllerMain();
            this.DataContext = vm_FanControlMain;
            vm_FanControlMain.OnFanChanged += OnFanChanged;
        }

        private void OnFanChanged(bool FanIsOn)
        {
            if (FanIsOn == true)
            {
                this.RotatingFan.RepeatBehavior = new Windows.UI.Xaml.Media.Animation.RepeatBehavior(new TimeSpan(1, 0, 0));
                this.RotatingFan.Begin();
            }
            else {
                this.RotatingFan.Stop();
            }

        }

       
    }
}
