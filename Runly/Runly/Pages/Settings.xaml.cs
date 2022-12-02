using Runly.Models;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Runly.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();

            List<SettingsList> settingsOptions = new List<SettingsList>
            {
                new SettingsList{OptionName="Waga", OptionValue="70"},
                new SettingsList{OptionName="Waga", OptionValue="70"},
                new SettingsList{OptionName="Waga", OptionValue="70"}
            };
            //settingsList.ItemsSource = settingsOptions;
        }
    }
}