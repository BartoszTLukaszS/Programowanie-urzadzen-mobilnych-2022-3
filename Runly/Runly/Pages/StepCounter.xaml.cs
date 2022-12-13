using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Runly.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepCounter : ContentPage
    {

        public StepCounter()
        {
            InitializeComponent();

            GetStepCounter steps = new GetStepCounter();

            MessagingCenter.Subscribe<string>(this, "steps", message =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    amountSteps.Text = message;
                });
            });

            if (Preferences.Get("LocationServiceRunning", false) == true)
            {
                StartService();
            }
        }

        private void StartService()
        {
            var startServiceMessage = new StartServiceMessage();
            MessagingCenter.Send(startServiceMessage, "ServiceStarted");
            Preferences.Set("StepCounterService", true);
            //amountSteps.Text = "Location Service has been started!";
        }
    }
}