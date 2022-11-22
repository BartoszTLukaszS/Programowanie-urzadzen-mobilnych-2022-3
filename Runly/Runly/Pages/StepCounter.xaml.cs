using Runly.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Runly.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepCounter : ContentPage
    {
        //inicjalizacja listy przechowujacej liczbe krokow
        List<double> accData = new List<double>();
        int stepsNumber = 0;
        DateTime czas = DateTime.Now;
        TimeSpan interval = TimeSpan.FromSeconds(0.1);
        

        public StepCounter()
        {
            InitializeComponent();

            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        }

        //implementacja modul akcelerometra
        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs args)
        {
            if (DateTime.Now - czas > interval)
            {
                czas = DateTime.Now;
                var xVal = args.Reading.Acceleration.X * 10;
                var yVal = args.Reading.Acceleration.Y * 10;
                var zVal = args.Reading.Acceleration.Z * 10;
                var accValue = Math.Sqrt(xVal * xVal + yVal * yVal + zVal * zVal) - 10;
                if (accData.Count > 240)
                    accData.RemoveAt(0);
                accData.Add(accValue);

                //petla liczaca kroki
                if (accData.Count > 1)
                {
                    for (int i = 1; i < accData.Count - 1; i++)
                    {
                        if (accData[i] > 1)
                        {
                            if (accData[i] > accData[i - 1] && accData[i] > accData[i + 1])
                            {
                                stepsNumber++;
                                accData.Clear();
                            }
                        }

                    }
                }
                amountSteps.Text = stepsNumber.ToString();
            }
        }

        //rozpoczecie i zakonczenie liczenia krokow
        void Button_Cliked(object sender, EventArgs e) 
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                {
                    Accelerometer.Stop();
                    btn.Text = "Start";
                }   
                else
                {
                    Accelerometer.Start(SensorSpeed.UI);
                    btn.Text = "Stop";
                }
                    
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Not supported on device
            }
            catch (Exception ex)
            {
                // Something else went wrong
            }
        }
    }
}