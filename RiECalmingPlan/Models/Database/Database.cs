using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmHelpers;
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

        public async void ResetConnection() {
            await db.CloseAsync();
            db = await DependencyService.Get<ISQLite>().ResetDatabase();
        }


        //------------------- READ -------------------------------
        /*
         * Pulls the appropriate information from tables into tangible objects
         */

        public async Task<ObservableCollection<DisplayQuestion>> GetDisplayQuestionList() {
            ObservableCollection<DisplayQuestion> list = new ObservableCollection<DisplayQuestion>();
            foreach (Question q in await db.Table<Question>().ToListAsync()) {
                List<GeneratedResponse> g = new List<GeneratedResponse>();
                List<NonGeneratedResponse> ng = new List<NonGeneratedResponse>();
                switch (q.QuestionType) {
                    case ("CheckBox"):
                        g.AddRange(await GetAssociatedCheckBoxesAsync(q.CPQID));
                        break;
                    case ("Stepper"):
                        g.AddRange(await GetAssociatedStepperAsync(q.CPQID));
                        break;
                    case ("Text Response"):
                        ng.AddRange(await GetAssociatedTextResponseAsync(q.CPQID));
                        break;
                    default:
                        ng.AddRange(await GetAssociatedTextResponseAsync(q.CPQID));
                        break;
                }


                list.Add(new DisplayQuestion(q, g, ng));
            }
            return list;
        }

        public async Task<ObservableRangeCollection<Response>> GetDistressExpressions(string DistressLevelType) {
            /*
             * Returns all responses where their respective question's distress level type matches the input
             * 
             * The current problem with this is that it will select from the following tables in order,
             * and not a randomised sublist of the query. Pulling out a sublist from each one will make the returned list to be of size 3n,
             * which it really should be 5 or less regardless.
             * 
             *  ORDER BY RANDOM() LIMIT 5
             */
            ObservableRangeCollection<Response> r = new ObservableRangeCollection<Response>();
            r.AddRange(await db.QueryAsync<Label_CheckBox>("SELECT * FROM [CheckBoxLabels] LEFT JOIN [Questions] WHERE Questions.DistressLevelType = ? AND CheckBoxLabels.CheckBoxValue = 1 AND Questions.CPQID = CheckBoxLabels.CPQID", DistressLevelType));
            r.AddRange(await db.QueryAsync<Label_Stepper>("SELECT * FROM [StepperLabels] LEFT JOIN [Questions] WHERE Questions.DistressLevelType = ? AND StepperLabels.StepperValue > 0 AND Questions.CPQID = StepperLabels.CPQID", DistressLevelType));
            r.AddRange(await db.QueryAsync<Label_TextResponse>("SELECT * FROM [TextResponseLabels] LEFT JOIN [Questions] WHERE Questions.DistressLevelType = ? AND Questions.CPQID = TextResponseLabels.CPQID", DistressLevelType));
            return r;

        }

        public async Task<ObservableRangeCollection<Suggestion>> GetDistressSuggestions(string DistressLevelType) {
            /*
             * Returns all responses where their respective question's distress level type matches the input
             *
             * 
             *  ORDER BY RANDOM() LIMIT 5
             */
            ObservableRangeCollection<Suggestion> s = new ObservableRangeCollection<Suggestion>();
            s.AddRange(await db.QueryAsync<Suggestion>("SELECT * FROM [Suggestions] WHERE Level = ?", DistressLevelType));
            return s;
        }

        public async Task<List<Label_Stepper>> GetAssociatedStepperAsync(int CPQID) {
            return await db.QueryAsync<Label_Stepper>("SELECT * FROM [StepperLabels] WHERE [CPQID] = ?", CPQID);
        }

        public async Task<List<Label_CheckBox>> GetAssociatedCheckBoxesAsync(int CPQID) {
            return await db.QueryAsync<Label_CheckBox>("SELECT * FROM [CheckBoxLabels] WHERE [CPQID] = ?", CPQID);
        }

        public async Task<List<Label_TextResponse>> GetAssociatedTextResponseAsync(int CPQID) {
            return await db.QueryAsync<Label_TextResponse>("SELECT * FROM [TextResponseLabels] WHERE [CPQID] = ?", CPQID);
        }

        public async Task<List<UserInputDistressLevel>> GetUserInputDistressLevels() {
            return await db.QueryAsync<UserInputDistressLevel>("SELECT * FROM [UserInputDistressLevel]");
        }
        //------------------- UPDATE -------------------------------
        /*
         * Overwrites a row with the input given
         */
        public async Task UpdateStepperResponse(Label_Stepper stepper) {
            if (stepper != null) {
                await db.QueryAsync<Label_Stepper>("UPDATE [StepperLabels] SET StepperValue = ? WHERE CPQID = ? AND StepperID = ?",
                    stepper.StepperValue, stepper.CPQID, stepper.StepperID);
                Console.WriteLine("\n CPQID:" + stepper.CPQID + "\n StepperID: " + stepper.StepperID + "\n StepperText: " + stepper.Label + "\n StepperValue: " + stepper.StepperValue);
            } else {
                Console.WriteLine("\n STEPPER null");
            }
        }

        public async Task UpdateCheckBoxResponse(Label_CheckBox checkbox) {
            if (checkbox != null) {
                await db.QueryAsync<Label_CheckBox>("UPDATE [CheckBoxLabels] SET CheckBoxValue = ? WHERE CPQID = ? AND CheckBoxID = ?",
                    checkbox.CheckBoxValue, checkbox.CPQID, checkbox.CheckBoxID);
                Console.WriteLine("\n CPQID:" + checkbox.CPQID + "\n CheckBoxID: " + checkbox.CheckBoxID + "\n CheckText: " + checkbox.Label + "\n CheckBoxValue: " + checkbox.CheckBoxValue);
            } else {
                Console.WriteLine("\n checkbox null");
            }
        }

        public async Task UpdateTextResponse(Label_TextResponse textResponse) {
            if (textResponse != null) {
                await db.QueryAsync<Label_TextResponse>("UPDATE [TextResponseLabels] SET TextResponse = ? WHERE CPQID = ? AND TextResponseID = ?",
                    textResponse.Label, textResponse.CPQID, textResponse.TextResponseID);
                Console.WriteLine("\n CPQID:" + textResponse.CPQID + "\n textResponseID: " + textResponse.TextResponseID + "\n Text: " + textResponse.Label);
            } else {
                Console.WriteLine("\n textbox null");
            }
        }

        //------------------- CREATE -------------------------------
        /*
         * Appends the row to the end of the table
         */
        public async Task AppendCheckBoxResponse(Label_CheckBox checkbox) {
            await db.InsertAsync(checkbox);
        }

        public async Task AppendStepperResponse(Label_Stepper stepper) {
            await db.InsertAsync(stepper);
        }
        public async Task AppendTextResponse(Label_TextResponse textResponse) {
            await db.InsertAsync(textResponse);
        }

        public async Task AppendUserInputDistressLevel(UserInputDistressLevel level) {
            await db.InsertAsync(level);
        }

        //------------------- DELETE --------------------------------
        /*
         * Deletes a specific row in a specific table
         * 
         */

        public async Task DeleteStepperResponse(Label_Stepper stepper) {
            await db.QueryAsync<Label_Stepper>("DELETE FROM [StepperLabels] WHERE CPQID = ? AND StepperID = ?",
                    stepper.CPQID, stepper.StepperID);
        }

        public async Task DeleteCheckboxResponse(Label_CheckBox checkbox) {
            await db.QueryAsync<Label_CheckBox>("DELETE FROM [CheckBoxLabels] WHERE CPQID = ? AND CheckBoxID = ?",
                    checkbox.CPQID, checkbox.CheckBoxID);
        }

        public async Task DeleteTextResponse(Label_TextResponse textResponse) {
            await db.QueryAsync<Label_TextResponse>("DELETE FROM [TextResponseLabels] WHERE CPQID = ? AND TextResponseID = ?",
                    textResponse.CPQID, textResponse.TextResponseID);
        }

    }
}
