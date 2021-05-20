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
    public partial class Page_ImageViewer : ContentPage {
        /*
         * When this page is created, the binding context will be the current entry of the carousel.
         * 
         * Upon deletion of an image, the diary reflects that deletion
         */


        public Page_ImageViewer() {
            InitializeComponent();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) {

            if (await DisplayAlert("Delete Image", "Do you want to delete this image?", "Yes", "No")) {
                ((ViewModel_DiaryEntry)BindingContext).Command_RemoveImage.Execute(Carousel.CurrentItem);
                await Navigation.PopAsync();
            }
        }
    }
}