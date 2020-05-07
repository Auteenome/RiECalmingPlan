using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using RiECalmingPlan.SQLite;
using RiECalmingPlan.ViewModels;
using SQLite;
using Xamarin.Forms;

namespace RiECalmingPlan.Models {
    public class Database {

        private SQLiteAsyncConnection db;

        public Database() {
            GetConnection();
        }
        /*
         * To generate a question the following things must occur in order
         * 1. The Question row is checked to determine which question type it is (CheckBox, RadioBox, Text Response)
         * 2. CheckBox/RadioBox options require more database pulling, whereas text response will generate a textfield instead
         * 3. Fields that require the "Other" field in RadioBox/CheckBox options require an additional textfield alongside it
         * 
         */
        //------------------- INIT -------------------------------------
        private async void GetConnection() {
            db = await DependencyService.Get<ISQLite>().GetConnection();
        }


        //------------------- RETRIEVERS -------------------------------

        public async Task<ObservableCollection<DisplayQuestion>> GetDisplayQuestionList() {
            ObservableCollection<DisplayQuestion> list = new ObservableCollection<DisplayQuestion>();
            foreach (Question q in await db.Table<Question>().ToListAsync()) {
                List<Response> r = new List<Response>();
                switch (q.QuestionType) {
                    case ("CheckBox"):
                        r.AddRange(await GetAssociatedCheckBoxesAsync(q.CPQID));
                        break;
                    case ("Stepper"):
                        r.AddRange(await GetAssociatedStepperAsync(q.CPQID));
                        break;
                    case ("Text Response"):
                        r.AddRange(await GetAssociatedTextResponseAsync(q.CPQID));
                        break;
                    default:
                        r.AddRange(await GetAssociatedTextResponseAsync(q.CPQID));
                        break;
                }


                list.Add(new DisplayQuestion(q, r));
            }
            return list;
        }

        public async Task<List<Label_Stepper>> GetAssociatedStepperAsync(int CPQID) {
            return await db.QueryAsync<Label_Stepper>("SELECT * FROM [StepperLabels] WHERE [CPQID] = " + CPQID);
        }

        public async Task<List<Label_CheckBox>> GetAssociatedCheckBoxesAsync(int CPQID) {
            return await db.QueryAsync<Label_CheckBox>("SELECT * FROM [CheckBoxLabels] WHERE [CPQID] = " + CPQID);
        }

        public async Task<List<Label_TextResponse>> GetAssociatedTextResponseAsync(int CPQID) {
            return await db.QueryAsync<Label_TextResponse>("SELECT * FROM [TextResponseLabels] WHERE [CPQID] = " + CPQID);
        }

        //------------------- MUTATORS -------------------------------

        public async Task UpdateStepperResponse(Label_Stepper stepper) {
            if (stepper != null) {
                await db.QueryAsync<Label_Stepper>("UPDATE [StepperLabels] SET StepperValue = ? WHERE CPQID = ? AND StepperID = ?",
                    stepper.StepperValue, stepper.CPQID, stepper.StepperID);
                Console.WriteLine("\n CPQID:" + stepper.CPQID + "\n StepperID: " + stepper.StepperID + "\n StepperText: " + stepper.StepperText + "\n CheckBoxValue: " + stepper.StepperValue);
            }
            else {
                Console.WriteLine("\n STEPPER null");
            }
        }

        public async Task UpdateCheckBoxResponse(Label_CheckBox checkbox) {
            //db.Query<Label_CheckBox>("UPDATE [CheckBoxLabels] SET CheckBoxValue = " + 
            //    CheckBoxValue + " WHERE CPQID = " + CPQID + " AND CheckBoxID = " + CheckBoxID);
            if (checkbox != null) {
                await db.QueryAsync<Label_CheckBox>("UPDATE [CheckBoxLabels] SET CheckBoxValue = ? WHERE CPQID = ? AND CheckBoxID = ?",
                    checkbox.CheckBoxValue, checkbox.CPQID, checkbox.CheckBoxID);
                Console.WriteLine("\n CPQID:" + checkbox.CPQID + "\n CheckBoxID: " + checkbox.CheckBoxID + "\n CheckText: " + checkbox.CheckText + "\n CheckBoxValue: " + checkbox.CheckBoxValue);
            }
            else {
                Console.WriteLine("\n checkbox null");
            }
        }
    }
}
