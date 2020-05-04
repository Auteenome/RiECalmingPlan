using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RiECalmingPlan

{
    //Not really used int his app. The Login form is set as the starting page
    //  in the App.xaml cs code


    //Sqlite-net-pcl is added to the main solution and this automatically adds to
    //  Android and iOS

    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
    }
}
