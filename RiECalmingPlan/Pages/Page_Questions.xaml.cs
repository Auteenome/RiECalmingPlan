using RiECalmingPlan.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.Pages {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_Questions : ContentPage {


        private readonly Database database;


        public Page_Questions() {
            InitializeComponent();
            database = new Database();

        }

        private void RefreshListView() {
            try {
                Questions.ItemsSource = database.GetDisplayQuestionList();
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }

        }
        protected override void OnAppearing() {
            base.OnAppearing();
            RefreshListView();
        }


    }
}