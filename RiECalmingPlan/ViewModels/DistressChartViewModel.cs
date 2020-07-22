using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Forms;
using RiECalmingPlan.Models;
using SkiaSharp;

namespace RiECalmingPlan.ViewModels {
    public class DistressChartViewModel : ViewModel_Base {

        private Chart _LineChart;
        public Chart LineChart { get { return _LineChart; } set { SetProperty(ref _LineChart, value); } }

        readonly Dictionary<string, int> Magnitude = new Dictionary<string, int>() {
            {"None", 0 },
            {"Calm", 1 },
            {"Mild", 2 },
            {"Moderate", 3 },
            {"Acute", 4 }
        };


        public DistressChartViewModel() {
           
        }

    }
}
