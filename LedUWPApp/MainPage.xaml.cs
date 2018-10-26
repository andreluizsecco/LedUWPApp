using System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LedUWPApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private GpioPin redLed;
        private GpioPin greenLed;
        private DispatcherTimer timer = new DispatcherTimer();

        public MainPage()
        {
            this.InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            greenLed = GpioController.GetDefault().OpenPin(17, GpioSharingMode.Exclusive);
            redLed = GpioController.GetDefault().OpenPin(27, GpioSharingMode.Exclusive);
            greenLed.Write(GpioPinValue.High);
            redLed.Write(GpioPinValue.Low);
            greenLed.SetDriveMode(GpioPinDriveMode.Output);
            redLed.SetDriveMode(GpioPinDriveMode.Output);

            timer.Start();
        }

        private void timer_Tick(object sender, object e)
        {
            var redLedState = redLed.Read();
            var greenLedState = greenLed.Read();

            greenLed.Write(redLedState);
            redLed.Write(greenLedState);
        }
    }
}
