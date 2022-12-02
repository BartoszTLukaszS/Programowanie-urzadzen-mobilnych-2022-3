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
        }
    }
}