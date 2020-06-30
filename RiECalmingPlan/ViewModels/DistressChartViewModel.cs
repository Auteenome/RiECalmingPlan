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
            GetEntries();
        }

        public async void GetEntries() {
            List<DistressResponse> FullHistory = new List<DistressResponse>();
            var calm = await App.database.GetCalmDistressResponseHistory();
            var noncalm = await App.database.GetNonCalmDistressResponseHistory();
            FullHistory.AddRange(calm);
            FullHistory.AddRange(noncalm);
            FullHistory = FullHistory.OrderBy(x => x.TimeStamp).ToList();

            Console.WriteLine("Getting Entries now");
            List<Entry> entries = new List<Entry>();
            foreach(var d in FullHistory) {
                if (d is NonCalmDistressLevelResponse) {
                    Magnitude.TryGetValue(((NonCalmDistressLevelResponse)d).DistressLevelType, out int value);
                    entries.Add(new Entry(value) {
                        ValueLabel = d.TimeStamp.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    });
                } else {
                    entries.Add(new Entry(1) {
                        ValueLabel = d.TimeStamp.ToString(),
                        Color = SKColor.Parse("#b455b6")
                    });
                }
            }
            LineChart = new LineChart() { Entries = entries };
        }

    }
}
