using MvvmHelpers;
using Plugin.Media;
using RiECalmingPlan.Models;
using RiECalmingPlan.Models.JSON;
using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_NewDiaryEntry : ContentPage {
        /*
         * Using James Montemagno's Media Plugin for getting the paths to photo gallery and camera
         * https://github.com/jamesmontemagno/MediaPlugin
         * 
         * After taking a photo within the app and saving it to the phone, it becomes a lot harder to find since it is stored in its own namespace folder,
         * rather than taking the photo directly from the camera separately and using the photo gallery section of the app to pull the path.
         */
        public event EventHandler SaveHandler;
        public event EventHandler UpdateHandler;
        public string EntryType = "New";

        ViewModel_DiaryStarters starters;

        public Page_NewDiaryEntry(ViewModel_DiaryStarters s) {
            starters = s;
            InitializeComponent();
        }

        public void Init() {
            starterPicker.BindingContext = starters;
            starters.SelectedIndex = ((DiaryEntry)BindingContext).Starter;
            starterPicker.SelectedIndex = starters.SelectedIndex;
        }

        async void SetImageFromPath() {
            var permissions = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
            if (permissions != PermissionStatus.Granted) {
                permissions = await Permissions.RequestAsync<Permissions.StorageRead>();
            }

            if (permissions != PermissionStatus.Granted) {
                return;
            }

            //image.Source = ((DiaryEntry)BindingContext).PhotoLink;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            SetImageFromPath();

        }

        async void OnPickPhotoButtonClicked(object sender, EventArgs e) {
            (sender as Button).IsEnabled = false;

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file != null) {
                //((DiaryEntry)BindingContext).PhotoLink = file.Path;
                SetImageFromPath();
            }

            (sender as Button).IsEnabled = true;

        }

        async void OnCameraPhotoButtonClicked(object sender, EventArgs e) {
                await CrossMedia.Current.Initialize();

                //Check if the camera is available and if camera is supported
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                //Attempt to take a photo
                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {
                    Directory = "Auteenome",//By convention, the photos should belong in a folder with the same name as the app's name
                    Name = "test.jpg"//Duplicates of this name will append the next available number after an underscore, example: test_5.jpg
                });

                //If the user takes the photo, it will use the path as the image source
                if (file == null) {
                    return;
                } else {
                    //await DisplayAlert("File Location", file.Path, "OK");

                    //((DiaryEntry)BindingContext).PhotoLink = file.Path;
                    SetImageFromPath();

                }
        }

        async void OnSaveButtonClicked(object sender, EventArgs e) {
            ((DiaryEntry)BindingContext).Starter = starters.SelectedIndex;
            if (EntryType.Equals("New")) {
                SaveHandler(this.BindingContext, EventArgs.Empty);
            } else {
                UpdateHandler(this.BindingContext, EventArgs.Empty);
            }
            await Navigation.PopAsync();
        }

    }
}