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
         * Follows the CRUD Database method structure.
         * 
         * 
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
                if (q.Enabled)
                {
                    List<GeneratedResponse> g = new List<GeneratedResponse>();
                    List<NonGeneratedResponse> ng = new List<NonGeneratedResponse>();

                    string fbText = await GetAssociatedRiEFeedback(q.CPQID);    // added by mh

                    switch (q.QuestionType)
                    {
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

                    list.Add(new DisplayQuestion(q, g, ng, fbText));    // modifed by mh
                }
            }
            return list;
        }

        public async Task<ObservableRangeCollection<DiaryStarter>> GetDiaryStarterOptionsAsync() {
            ObservableRangeCollection<DiaryStarter> r = new ObservableRangeCollection<DiaryStarter>(); 
            r.AddRange(await db.QueryAsync<DiaryStarter>("SELECT * From DiaryStarters"));
            return r;
        }

        public async Task<ObservableRangeCollection<Response>> GetDistressExpressions(string DistressLevelType) {
            /*
             * This function pulls the Top Half of the Support Plan.
             */

            ObservableRangeCollection<Response> r = new ObservableRangeCollection<Response>();
            if (DistressLevelType.Equals("Calm")) {
                //Calm Request, pull every response that is checked with a "Calm" override OR if the Question associated with the checked response has a QuestionCarePlanArea containing "Positive"
                //This will also pull Stepper Responses if the value is >0 and the QuestionCarePlanArea contains "Positive"
                //When using UNION in a query, the ORDER BY clause is restricted to a specific column
                
                r.AddRange(await db.QueryAsync<Response>(
                    "SELECT *, RANDOM() As Random FROM [CheckBoxLabels] LEFT JOIN [Questions] WHERE (Questions.CPQID = CheckBoxLabels.CPQID AND CheckBoxLabels.Value = 1) AND (Questions.QuestionCarePlanArea LIKE '%Positive%' OR CheckBoxLabels.Override = ?)"
                    + "UNION SELECT *, RANDOM() As Random FROM [StepperLabels] LEFT JOIN [Questions] WHERE (Questions.CPQID = StepperLabels.CPQID AND StepperLabels.Value > 1) AND (Questions.QuestionCarePlanArea LIKE '%Positive%' OR StepperLabels.Override = ?)"
                    + "ORDER BY Random LIMIT 5", DistressLevelType, DistressLevelType));
                
            } else {
                //Non Calm Request, pull every response with DistressLevelType override OR if the Question associated with the checked response has a QuestionCarePlanArea containing "Distress Actions"
                //Since Override takes priority over the actual Value of the stepper, the tuples that will be added that have the correct value will only be added if the Override is empty
                r.AddRange(await db.QueryAsync<Response>(
                    "SELECT *, RANDOM() As Random FROM [CheckBoxLabels] LEFT JOIN [Questions] WHERE (Questions.CPQID = CheckBoxLabels.CPQID AND CheckBoxLabels.Value = 1) AND (Questions.QuestionCarePlanArea LIKE '%Distress Actions%' OR CheckBoxLabels.Override = ?)"
                    + "UNION SELECT *, RANDOM() As Random FROM [StepperLabels] LEFT JOIN [Questions] WHERE (Questions.CPQID = StepperLabels.CPQID AND (StepperLabels.Value = ? AND StepperLabels.Override IS NULL)) AND (Questions.QuestionCarePlanArea LIKE '%Distress Actions%' OR StepperLabels.Override = ?)"
                    + "ORDER BY Random LIMIT 5", DistressLevelType, DistressType.DistressTypeValue(DistressLevelType), DistressLevelType));
                if (DistressLevelType.Equals("Acute")) {
                    //Acute Request, in addition to pulling every response associated with it, it will also append all responses that have an Override value of "LT-Acute", regardless of value above 0
                    //This will also go above the previous 5 limit, which will also ensure that this partitiion of the query always is appended
                    r.AddRange(await db.QueryAsync<Response>("SELECT * FROM [StepperLabels] WHERE (StepperLabels.Override = 'LT-Acute' AND Value > 0 AND Value IS NOT NULL)"));
                }
            }
            return r;

        }

        public async Task<ObservableRangeCollection<Suggestion>> GetDistressSuggestions(string DistressLevelType) {
            /*
             * This function pulls the Second Half of the Support Plan from Suggestions (Usually Calm Acute and LT-Acute)
             */
            ObservableRangeCollection<Suggestion> s = new ObservableRangeCollection<Suggestion>();
            if (DistressLevelType.Equals("LT-Acute")) {
                s.AddRange(await db.QueryAsync<Suggestion>("SELECT * FROM [Suggestions] WHERE Level = ?", "Acute"));
                s.AddRange(await db.QueryAsync<Suggestion>("SELECT * FROM [Suggestions] WHERE Level = ?", DistressLevelType));
            } else {
                s.AddRange(await db.QueryAsync<Suggestion>("SELECT * FROM [Suggestions] WHERE Level = ?", DistressLevelType));
            }
            return s;
        }

        public async Task<ObservableRangeCollection<Response>> GetDistressInterventions(string DistressLevelType) {
            /*
             * This function pulls the Second Half of the Support Plan from the Responses Table (Mild Moderate and Acute)
             */
            ObservableRangeCollection<Response> r = new ObservableRangeCollection<Response>();
            if (!DistressLevelType.Equals("Calm")) {
                //Pull every response with DistressLevelType override OR if the Question associated with the checked response has a QuestionCarePlanArea containing "Intervention"
                //Since Override takes priority over the actual Value of the stepper, the tuples that will be added that have the correct value will only be added if the Override is empty
                r.AddRange(await db.QueryAsync<Response>(
                "SELECT *, RANDOM() As Random FROM [CheckBoxLabels] LEFT JOIN [Questions] WHERE (Questions.CPQID = CheckBoxLabels.CPQID AND CheckBoxLabels.Value = 1) AND (Questions.QuestionCarePlanArea LIKE '%Intervention%' OR CheckBoxLabels.Override = ?)"
                + "UNION SELECT *, RANDOM() As Random FROM [StepperLabels] LEFT JOIN [Questions] WHERE (Questions.CPQID = StepperLabels.CPQID AND (StepperLabels.Value = ? AND StepperLabels.Override IS NULL)) AND (Questions.QuestionCarePlanArea LIKE '%Intervention%' OR StepperLabels.Override = ?)"
                + "ORDER BY Random LIMIT 5"
                , DistressLevelType, DistressType.DistressTypeValue(DistressLevelType), DistressLevelType));

            }
            return r;
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

        public async Task<string> GetAssociatedRiEFeedback(int CPQID)
        {
            List<RiEFeedback> fbList = new List<RiEFeedback>();
            fbList.AddRange(await db.QueryAsync<RiEFeedback>("SELECT * FROM [RiEFeedback] WHERE [CPQID] = ?", CPQID));
            return fbList[0].FeedbackText; // not sure how to return just a string from a query, so simply returns the first entry
        }

        //------------------- UPDATE -------------------------------
        /*
         * Overwrites a row with the input given
         */
        public async Task UpdateStepperResponse(Label_Stepper stepper) {
            if (stepper != null) {
                await db.QueryAsync<Label_Stepper>("UPDATE [StepperLabels] SET Value = ? WHERE CPQID = ? AND QID = ?",
                    stepper.Value, stepper.CPQID, stepper.QID);
                Console.WriteLine("\n CPQID:" + stepper.CPQID + "\n StepperID: " + stepper.QID + "\n StepperText: " + stepper.Label + "\n StepperValue: " + stepper.Value);
            } else {
                Console.WriteLine("\n STEPPER null");
            }
        }

        public async Task UpdateCheckBoxResponse(Label_CheckBox checkbox) {
            if (checkbox != null) {
                await db.QueryAsync<Label_CheckBox>("UPDATE [CheckBoxLabels] SET Value = ? WHERE CPQID = ? AND QID = ?",
                    checkbox.Value, checkbox.CPQID, checkbox.QID);
                Console.WriteLine("\n CPQID:" + checkbox.CPQID + "\n CheckBoxID: " + checkbox.QID + "\n CheckText: " + checkbox.Label + "\n Value: " + checkbox.Value);
            } else {
                Console.WriteLine("\n checkbox null");
            }
        }

        public async Task UpdateTextResponse(Label_TextResponse textResponse) {
            if (textResponse != null) {
                await db.QueryAsync<Label_TextResponse>("UPDATE [TextResponseLabels] SET Label = ? WHERE CPQID = ? AND QID = ?",
                    textResponse.Label, textResponse.CPQID, textResponse.QID);
                Console.WriteLine("\n CPQID:" + textResponse.CPQID + "\n textResponseID: " + textResponse.CPQID + "\n Text: " + textResponse.QID);
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
            await db.QueryAsync<Label_Stepper>("DELETE FROM [StepperLabels] WHERE CPQID = ? AND QID = ?",
                    stepper.CPQID, stepper.QID);
        }

        public async Task DeleteCheckboxResponse(Label_CheckBox checkbox) {
            await db.QueryAsync<Label_CheckBox>("DELETE FROM [CheckBoxLabels] WHERE CPQID = ? AND QID = ?",
                    checkbox.CPQID, checkbox.QID);
        }

        public async Task DeleteTextResponse(Label_TextResponse textResponse) {
            await db.QueryAsync<Label_TextResponse>("DELETE FROM [TextResponseLabels] WHERE CPQID = ? AND QID = ?",
                    textResponse.CPQID, textResponse.QID);
        }

    }
}
