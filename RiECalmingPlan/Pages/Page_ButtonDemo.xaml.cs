using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RiECalmingPlan.ViewModels;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_ButtonDemo : ContentPage {
        public Page_ButtonDemo()
        {
            InitializeComponent();
            Frame_Buttons.BindingContext = new DistressButtonDemoViewModel();   // ViewModel inherits TapViewModel_Base
        }
    }
}