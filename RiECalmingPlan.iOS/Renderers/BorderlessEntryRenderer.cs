using RiECalmingPlan.iOS.Renderers;
using RiECalmingPlan.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof (BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace RiECalmingPlan.iOS.Renderers {
    // custom renderer for IOS to make an entry control borderless
    public class BorderlessEntryRenderer : EntryRenderer{
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e) {
            base.OnElementChanged(e);

            if (Control != null) {
                this.Control.BorderStyle = UITextBorderStyle.None;
            }
        }
    }
}