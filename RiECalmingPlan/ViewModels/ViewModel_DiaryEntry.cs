using MvvmHelpers;
using Plugin.Media;
using RiECalmingPlan.Models.JSON;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_DiaryEntry : ViewModel_Base {


        private DiaryEntry _Entry;
        public DiaryEntry Entry { get { return _Entry; } set { SetProperty(ref _Entry, value); } }

        private DiaryEntryState _CurrentState;
        public DiaryEntryState CurrentState { get { return _CurrentState; } set { SetProperty(ref _CurrentState, value); } }

        public Command Command_AddImageFromGallery { get; private set; }
        public Command Command_AddImageFromCamera { get; private set; }
        public Command<string> Command_RemoveImage { get; private set; }

        public enum DiaryEntryState { 
            COMPLETED,
            EDITING,
            NEWSPACE
        }

        public ViewModel_DiaryEntry(DiaryEntry entry) {
            Entry = entry;
            CurrentState = DiaryEntryState.COMPLETED;//When loading a diary entry from save file, it becomes a completed diary entry

            Command_AddImageFromCamera = new Command(AddImageFromCamera);
            Command_AddImageFromGallery = new Command(AddImageFromGallery);
            Command_RemoveImage = new Command<string>(RemoveImage);
        }

        public ViewModel_DiaryEntry() {
            Entry = null;
            CurrentState = DiaryEntryState.NEWSPACE;//Just for at the end of the carousel

            Command_AddImageFromCamera = new Command(AddImageFromCamera);
            Command_AddImageFromGallery = new Command(AddImageFromGallery);
            Command_RemoveImage = new Command<string>(RemoveImage);
        }

        private async void AddImageFromGallery() {
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
