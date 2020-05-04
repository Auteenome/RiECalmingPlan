using System.Collections.Generic;
using RiECalmingPlan.SQLite;
using RiECalmingPlan.ViewModels;
using SQLite;
using Xamarin.Forms;

namespace RiECalmingPlan.Models {
    public class Database {

        private SQLiteConnection db;

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
        public List<Question> GetQuestionList() {

            return db.Table<Question>().ToList();
        }

        public List<DisplayQuestion> GetDisplayQuestionList() {
            List<DisplayQuestion> list = new List<DisplayQuestion>();
            foreach (Question q in db.Table<Question>().ToList()) {
                List<Response> r = new List<Response>();
                switch (q.QuestionType) {
                    case ("CheckBox"):
                        r.AddRange(GetAssociatedCheckBoxes(q.CPQID));
                        break;
                    case ("RadioBox"):
                        r.AddRange(GetAssociatedRadioBoxes(q.CPQID));
                        break;
                    case ("Text Response"):
                        r.AddRange(GetAssociatedTextResponse(q.CPQID));
                        break;
                    default:
                        r.AddRange(GetAssociatedTextResponse(q.CPQID));
                        break;
                }


                list.Add(new DisplayQuestion(q, r));
            }
            return list;
        }

        public List<Label_RadioBox> GetAssociatedRadioBoxes(int CPQID) {
            return db.Query<Label_RadioBox>("SELECT * FROM [RadioBoxLabels] WHERE [CPQID] = " + CPQID);
        }

        public List<CheckBoxLabel> GetAssociatedCheckBoxes(int CPQID) {
            return db.Query<CheckBoxLabel>("SELECT * FROM [CheckBoxLabels] WHERE [CPQID] = " + CPQID);
        }

        public List<TextResponseLabel> GetAssociatedTextResponse(int CPQID) {
            return db.Query<TextResponseLabel>("SELECT * FROM [TextResponseLabels] WHERE [CPQID] = " + CPQID);
        }
    }
}
