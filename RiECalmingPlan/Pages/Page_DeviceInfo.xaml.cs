using RiECalmingPlan.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_DeviceInfo : ContentPage {
        // public string phoneNumber { get; } = "0490715342";
        public Page_DeviceInfo()
        {
            InitializeComponent();

            // test labels for device info
            Label_Height.Text = "The height resolution for this device is: " + Application.Current.Properties["heightResolution"].ToString();
            Label_Width.Text = "The width resolution for this device is: " + Application.Current.Properties["widthResolution"].ToString();

            Label_PhoneNumber.BindingContext = new TapPhoneDialerViewModel();
            Label_URLLink.BindingContext = new TapOpenLinkViewModel();
            // BindingContext = this;
        }
    }
}