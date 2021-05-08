using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Options : ContentPage {

        ViewModel_Options _viewModel;

        public Page_Options() {
            InitializeComponent();
            BindingContext = _viewModel = new ViewModel_Options();
        }
    }
}