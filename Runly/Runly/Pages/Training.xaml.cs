using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;
using Runly.Models;
using Xamarin.Forms.Maps;
using System.Timers;

namespace Runly.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Training : ContentPage
    {
        Timer timer;
        int hours = 0, mins = 0, secs = 0;

        bool isTraining = false;

        Position position;

        List<PositionList> positionsList = new List<PositionList>();

        public Training()
        {
            InitializeComponent();

            GetLocation();
        }

        private async void GetLocation()
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {

                //map.Pins.Remove(currentLocation);
                position = new Position(location.Latitude, location.Longitude);
                /*currentLocation = new Pin
                {
                    Label = "Current Location",
                    Type = PinType.Generic,
                    Position = position
                };
                map.Pins.Add(currentLocation);*/
                map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5)));

                if (isTraining)
                {
                    positionsList.Add(new PositionList { location = location, TimeLasted = (hours * 3600 + mins * 60 + secs) });
                    //positionsList.Add(new PositionList { Latitude = location.Latitude, Longitude = location.Longitude, TimeLasted = (hours * 3600 + mins * 60 + secs) });
                    //UpdateInfo();
                }

            }

            await Task.Delay(1000);
            GetLocation();
        }

        private void StartTraining(object sender, EventArgs e)
        {
            btnStartF.IsVisible = false;
            isTraining = true;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            secs++;
            if (secs == 59)
            {
                mins++;
                secs = 0;
            }
            if (mins == 59)
            {
                hours++;
                mins = 0;
            }
            Device.BeginInvokeOnMainThread(() =>
            {
                timerValue.Text = string.Format("{0:00}:{1:00}:{2:00}", hours, mins, secs);
            });
        }
    }
}