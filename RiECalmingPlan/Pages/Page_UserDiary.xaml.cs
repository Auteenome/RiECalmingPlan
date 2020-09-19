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

        public ViewModel_DiaryStarters starters = new ViewModel_DiaryStarters();//starters are preloaded here so they are displayed during each ADD/UPDATE entry page

        public Page_UserDiary() {
            InitializeComponent();
            BindingContext = new ViewModel_DiaryEntries();
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            ((ViewModel_DiaryEntries)BindingContext).Refresh();
        }

        async void NewEntryButtonClicked(object sender, EventArgs e) {
            var page = new Page_NewDiaryEntry(starters) {
                BindingContext = new DiaryEntry()
            };
            page.Init();
            page.SaveHandler += ((ViewModel_DiaryEntries)BindingContext).AddEntry;
            await Navigation.PushAsync(page);
        }

        void ResetButtonClicked(object sender, EventArgs e) {
            ((ViewModel_DiaryEntries)BindingContext).Reset();
        }

        void DeleteSwipeItemClicked(object sender, EventArgs e) {
            SwipeItem item = sender as SwipeItem;
            DiaryEntry model = item.BindingContext as DiaryEntry;
            ((ViewModel_DiaryEntries)BindingContext).RemoveEntry(model);
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e) {
            DiaryEntry entry = (DiaryEntry)e.SelectedItem;
            ((ViewModel_DiaryEntries)BindingContext).Entries.Remove(entry);
            if (e.SelectedItem != null) {
                var page = new Page_NewDiaryEntry(starters) {
                    BindingContext = entry,
                    EntryType = "Update",
                    
                };
                page.Init();
                page.UpdateHandler += ((ViewModel_DiaryEntries)BindingContext).UpdateEntry;
                await Navigation.PushAsync(page);
            }
        }
    }
}