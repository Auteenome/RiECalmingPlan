using Android.Content;
using Android.Graphics.Drawables;
using Android.Views;
using RiECalmingPlan.Droid.Renderers;
using RiECalmingPlan.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace RiECalmingPlan.Droid.Renderers {
    // custom renderer for Android to make an entry control borderless
    class BorderlessEntryRenderer : EntryRenderer {
        public BorderlessEntryRenderer(Context context) : base(context) {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                this.Control.SetBackground(null);                   // background usually overridden
                this.Control.Gravity = GravityFlags.CenterVertical;
                this.Control.SetPadding(10, 10, 10, 10);             // padding gives space around text
            }
        }
    }
}