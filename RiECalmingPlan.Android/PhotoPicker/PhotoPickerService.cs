using System.IO;
using System.Threading.Tasks;
using Android.Content;
using RiECalmingPlan.Droid.PhotoPicker;
using RiECalmingPlan.PhotoPicker;
using Xamarin.Forms;

[assembly: Dependency(typeof(PhotoPickerService))]
namespace RiECalmingPlan.Droid.PhotoPicker {
    public class PhotoPickerService : IPhotoPickerService {
        public Task<string> GetImagePathAsync() {
            // Define the Intent for getting images
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);

            // Get the MainActivity instance
            MainActivity activity = Forms.Context as MainActivity;

            // Start the picture-picker activity (resumes in MainActivity.cs)
            activity.StartActivityForResult(
                Intent.CreateChooser(intent, "Select Picture"),
                MainActivity.PickImageId);

            // Save the TaskCompletionSource object as a MainActivity property
            activity.GetImagePathAsync = new TaskCompletionSource<string>();

            // Return Task object
            return activity.GetImagePathAsync.Task;
        }
    }
}