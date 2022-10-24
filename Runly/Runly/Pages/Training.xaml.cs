using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Xamarin.Essentials;


namespace Runly.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Training : ContentPage
    {

        public Training()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {

            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            var location = await Geolocation.GetLocationAsync(request);

            if (location != null)
            {
                if (location.IsFromMockProvider)
                {
                    //Put a message if detect a mock location.
                }
                else
                {
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    LongitudeLabel.Text = location.Longitude.ToString();
                    LatitudeLabel.Text = location.Latitude.ToString();
                }
            }

        }
    }
}