using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using RiECalmingPlan.iOS.PhotoPicker;
using RiECalmingPlan.PhotoPicker;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace RiECalmingPlan.iOS.PhotoPicker {
    public class PhotoPickerService : IPhotoPickerService {

        TaskCompletionSource<string> stringTaskCompletionSource;
        UIImagePickerController imagePicker;

        public Task<string> GetImagePathAsync() {
            // Create and define UIImagePickerController
            imagePicker = new UIImagePickerController {
                SourceType = UIImagePickerControllerSourceType.PhotoLibrary,
                MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary)
            };

            // Set event handlers
            imagePicker.FinishedPickingMedia += OnImagePickerFinished;
            imagePicker.Canceled += OnImagePickerCancelled;

            // Present UIImagePickerController;
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null) {
                vc = vc.PresentedViewController;
            }
            vc.PresentModalViewController(imagePicker, true);

            // Return Task object
            stringTaskCompletionSource = new TaskCompletionSource<string>();
            return stringTaskCompletionSource.Task;
        }

        private void OnImagePickerFinished(object sender, UIImagePickerMediaPickedEventArgs args) {
            UIImage image = args.EditedImage ?? args.OriginalImage;

            if (image != null) {

                var url = (NSUrl)args.Info.ValueForKey(new NSString("UIImagePickerControllerImageURL"));


                stringTaskCompletionSource.SetResult(url.Path);
            }
            else {
                stringTaskCompletionSource.SetResult(null);
            }
            imagePicker.DismissModalViewController(true);
        }

        void OnImagePickerCancelled(object sender, EventArgs args) {
            UnregisterEventHandlers();
            taskCompletionSource.SetResult(null);
            imagePicker.DismissModalViewController(true);
        }

        void UnregisterEventHandlers() {
            imagePicker.FinishedPickingMedia -= OnImagePickerFinishedPickingMedia;
            imagePicker.Canceled -= OnImagePickerCancelled;
        }
    }

}