using MvvmHelpers;
using Plugin.Media;
using RiECalmingPlan.Models.JSON;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DiaryEntry : ViewModel_DiaryPage {


        private DiaryEntry _Entry;
        public DiaryEntry Entry { get { return _Entry; } set { SetProperty(ref _Entry, value); } }

        public Command Command_AddImageFromGallery { get; private set; }
        public Command Command_AddImageFromCamera { get; private set; }
        public Command<string> Command_RemoveImage { get; private set; }

        public ViewModel_DiaryEntry(DiaryEntry page) {
            Entry = page;
            CurrentState = PageState.COMPLETED;//When loading a diary entry from save file, it becomes a completed diary entry

            BindCommands();
        }

        public ViewModel_DiaryEntry() {
            Entry = null;
            CurrentState = PageState.EDITING;//When loading a diary entry from save file, it becomes a completed diary entry

            BindCommands();
        }

        private void BindCommands() {
            Command_AddImageFromCamera = new Command(AddImageFromCamera);
            Command_AddImageFromGallery = new Command(AddImageFromGallery);
            Command_RemoveImage = new Command<string>(RemoveImage);
        }

        private async void AddImageFromGallery() {
            /*
             * Utilises the Image Gallery function on the mobile device to let the user choose an image to add to the diary page.
             * 
             * At the moment, only one image can be chosen
             * 
             * 
             */
            Console.WriteLine("Adding Image From Gallery");
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file != null) {
                if (Entry.PhotoLinks == null) {
                    Entry.PhotoLinks = new ObservableRangeCollection<string>();
                }
                Entry.PhotoLinks.Add(file.Path);
            }
        }

        private async void AddImageFromCamera() {
            /*
             * Utilises the Camera function on the mobile device to let the user take a photo and add it to the image collection.
             * 
             * 
             * 
             * 
             */
            Console.WriteLine("Adding Image From Camera");
            await CrossMedia.Current.Initialize();

            //Check if the camera is available and if camera is supported
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                Console.WriteLine("No camera available or is not supported");
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
                if (Entry.PhotoLinks == null) {
                    Entry.PhotoLinks = new ObservableRangeCollection<string>();
                }
                Entry.PhotoLinks.Add(file.Path);
            }
        }

        private void RemoveImage(string s) {
            Entry.PhotoLinks.Remove(s);
        }
    }
}
