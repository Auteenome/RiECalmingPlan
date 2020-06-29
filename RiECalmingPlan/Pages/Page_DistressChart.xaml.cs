using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microcharts;
using Microcharts.Forms;
using SkiaSharp;
using RiECalmingPlan.ViewModels;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_DistressChart : ContentPage {

        DistressChartViewModel model = new DistressChartViewModel();

        public Page_DistressChart() {
            InitializeComponent();
            BindingContext = model;
        }

        private void Button_Clicked(object sender, EventArgs e) {
            model.LineChart = new LineChart() { Entries = model.GetEntries() };
        }
    }
}