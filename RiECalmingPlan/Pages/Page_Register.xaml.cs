using RiECalmingPlan.Models;
using RiECalmingPlan.ViewModels;
using RiECalmingPlan.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Register : ContentPage {
        public Page_Register()
        {
            InitializeComponent();
            BindingContext = new ViewModel_Register();
        }
    }
}