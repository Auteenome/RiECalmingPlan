using RiECalmingPlan.PhotoPicker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiECalmingPlan.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RiECalmingPlan.Models.JSON;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_UserDiary : ContentPage {

        
        public Page_UserDiary() {
            InitializeComponent();
            BindingContext = new ViewModel_DiaryEntries();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            ((ViewModel_DiaryEntries)BindingContext).Refresh();
        }

        async void NewEntryButtonClicked(object sender, EventArgs e) {
            var page = new Page_NewDiaryEntry {
                BindingContext = new DiaryEntry()
        };
            page.SaveHandler += ((ViewModel_DiaryEntries)BindingContext).AddEntry;
            await Navigation.PushAsync(page);
        }

        void ResetButtonClicked(object sender, EventArgs e) {
            ((ViewModel_DiaryEntries)BindingContext).Reset();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e) {
            DiaryEntry entry = (DiaryEntry)e.SelectedItem;
            ((ViewModel_DiaryEntries)BindingContext).Entries.Remove(entry);
            if (e.SelectedItem != null) {
                var page = new Page_NewDiaryEntry {
                    BindingContext = entry,
                    EntryType = "Update"
                };
                page.UpdateHandler += ((ViewModel_DiaryEntries)BindingContext).UpdateEntry;
                await Navigation.PushAsync(page);
            }
        }
    }
}