using System;
using System.Collections.Generic;
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

        public async Task<List<DisplayQuestion>> GetDisplayQuestionList() {
            List<DisplayQuestion> list = new List<DisplayQuestion>();
            foreach (Question q in await db.Table<Question>().ToListAsync()) {
                List<Response> r = new List<Response>();
                switch (q.QuestionType) {
                    case ("CheckBox"):
                        r.AddRange(await GetAssociatedCheckBoxesAsync(q.CPQID));
                        break;
                    case ("RadioBox"):
                        r.AddRange(await GetAssociatedRadioBoxesAsync(q.CPQID));
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

        public async Task<List<Label_RadioBox>> GetAssociatedRadioBoxesAsync(int CPQID) {
            return await db.QueryAsync<Label_RadioBox>("SELECT * FROM [RadioBoxLabels] WHERE [CPQID] = " + CPQID);
        }

        public async Task<List<Label_CheckBox>> GetAssociatedCheckBoxesAsync(int CPQID) {
            return await db.QueryAsync<Label_CheckBox>("SELECT * FROM [CheckBoxLabels] WHERE [CPQID] = " + CPQID);
        }

        public async Task<List<Label_TextResponse>> GetAssociatedTextResponseAsync(int CPQID) {
            return await db.QueryAsync<Label_TextResponse>("SELECT * FROM [TextResponseLabels] WHERE [CPQID] = " + CPQID);
        }

        //------------------- MUTATORS -------------------------------

        public async void UpdateRadioBoxResponse(int CPQID, int RadioBoxID, int RadioBoxValue) {
            await db.QueryAsync<Label_RadioBox>("UPDATE [RadioBoxLabels] SET RadioBoxValue = ? WHERE CPQID = ? AND RadioBoxID = ?",
                RadioBoxValue, CPQID, RadioBoxID);
        }

        public async void UpdateCheckBoxResponse(int CPQID, int CheckBoxID, bool CheckBoxValue) {
            //db.Query<Label_CheckBox>("UPDATE [CheckBoxLabels] SET CheckBoxValue = " + 
            //    CheckBoxValue + " WHERE CPQID = " + CPQID + " AND CheckBoxID = " + CheckBoxID);

            await db.QueryAsync<Label_CheckBox>("UPDATE [CheckBoxLabels] SET CheckBoxValue = ? WHERE CPQID = ? AND CheckBoxID = ?", 
                CheckBoxValue, CPQID, CheckBoxID);

            Console.WriteLine("\n UPDATING DATABASE");
        }
    }
}
