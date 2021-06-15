using RiECalmingPlan.Models.JSON;
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
        string Path;
        bool InEditState;

        public Page_ImageViewer() {
            InitializeComponent();
        }

        public Page_ImageViewer(string path, bool inEditState) {
            Path = path;
            InEditState = inEditState;
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            //!((string)Carousel.CurrentItem).Equals(Path)
            Console.WriteLine("Current Path: " + ((BindingContext as ViewModel_DiaryEntry).Entry.PhotoLinks[Carousel.Position]));
            Console.WriteLine("Clicked Path" + Path);
            if (InEditState == true) {
                ToolbarDelete.IconImageSource = "baseline_delete_24.png";
            } else {
                ToolbarItems.Remove(ToolbarDelete);
            }


            await Task.Delay(50);//Just realised this is a thing that we need to use in order to scroll automatically more than one item on a carousel
            Carousel.ScrollTo(Path);
            base.OnAppearing();
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) {
            object item = Carousel.CurrentItem;

            if (await DisplayAlert("Delete Image", "Do you want to delete this image?", "Yes", "No")) {

                ((ViewModel_DiaryEntry)BindingContext).Command_RemoveImage.Execute(item);
            }
        }
    }
}