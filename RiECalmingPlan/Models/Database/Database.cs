﻿using System;
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
                        ng.AddRange(await GetAssociatedTextResponseAsync(q.CPQID));
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

        public async Task<List<Response>> GetDistressLevelViewModelList(int DistressLevelType) {
            /*
             * Returns all responses where their respective question's distress level type matches the 
             */
            List<Response> r = new List<Response>();
            r.AddRange(await db.QueryAsync<Label_Stepper>("SELECT * FROM [Questions] RIGHT JOIN [StepperLabels] ON Questions.DistressLevelType = ? ORDER BY RANDOM() LIMIT 5", DistressLevelType));
            r.AddRange(await db.QueryAsync<Label_CheckBox>("SELECT * FROM [Questions] RIGHT JOIN [CheckBoxLabels] ON Questions.DistressLevelType = ? ORDER BY RANDOM() LIMIT 5", DistressLevelType));
            r.AddRange(await db.QueryAsync<Label_TextResponse>("SELECT * FROM [Questions] RIGHT JOIN [TextResponseLabels] ON Questions.DistressLevelType = ? ORDER BY RANDOM() LIMIT 5", DistressLevelType));
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
                    textResponse.TextResponse, textResponse.CPQID, textResponse.TextResponseID);
                Console.WriteLine("\n CPQID:" + textResponse.CPQID + "\n textResponseID: " + textResponse.TextResponseID + "\n Text: " + textResponse.TextResponse);
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

        public async Task AppendUserInputDistressLevel(UserInputDistressLevel level) {
            await db.InsertAsync(level);
        }

        //------------------- DELETE --------------------------------
        /*
         * Deletes a specific row in a specific table
         * 
         */

    }
}
