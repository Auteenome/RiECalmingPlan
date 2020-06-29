using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microcharts;
using Microcharts.Forms;
using MvvmHelpers;
using RiECalmingPlan.Models;
using SkiaSharp;

namespace RiECalmingPlan.ViewModels {
    public class DistressChartViewModel : ViewModel_Base {


        public DistressHistoryViewModel Model = new DistressHistoryViewModel();

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
            LineChart = new LineChart() { Entries = GetEntries() };
        }

        public List<Entry> GetEntries() {
            Console.WriteLine("Getting Entries now");
            List<Entry> entries = new List<Entry>();
            foreach(var d in Model.FullHistory) {
                if (d is NonCalmDistressLevelResponse) {
                    Magnitude.TryGetValue(((NonCalmDistressLevelResponse)d).DistressLevelType, out int value);
                    entries.Add(new Entry(value) {
                        ValueLabel = ((NonCalmDistressLevelResponse)d).DistressLevelType,
                        Color = SKColor.Parse("#b455b6")
                    });
                }
                else {
                    entries.Add(new Entry(1) {
                        ValueLabel = "Calm",
                        Color = SKColor.Parse("#b455b6")
                    });
                }
            }

            return entries;
        }

    }
}
