using System;
using System.Collections;
using RiECalmingPlan.Models;
using Xamarin.Forms;
//using RiECalmingPlan.DBConnections;
using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RiECalmingPlan.Views
{

 /*       public class YPCheckbox
        {

            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            [Indexed]
            public int CPQID { get; set; }
            public string Label { get; set; }
            [Indexed]
            public int CHKID { get; set; }
            public bool ANS { get; set; }
        }
*/
        public partial class MyPage : ContentPage
        {
//            private SQLiteAsyncConnection _connection;
//            private ObservableCollection<CHKAnswer> _YPcheck;



            public MyPage()
            {
                InitializeComponent();
 //               _connection = DependencyService.Get<ISQLiteDB>().GetConnection();
            }
/*
            protected override async void OnAppearing()
            {
                // await _connection.CreateTableAsync<YPCheckbox>();
                var checkboxAnswers = await _connection.Table<CHKAnswer>().ToListAsync();
                _YPcheck = new ObservableCollection<CHKAnswer>(checkboxAnswers);

                MyCheckList.ItemsSource = _YPcheck;

                base.OnAppearing();
            }
*/


            void OnUpdate(object sender, System.EventArgs e)
            {
            }

            void OnDelete(object sender, System.EventArgs e)
            {
            }

            

        }
    }
