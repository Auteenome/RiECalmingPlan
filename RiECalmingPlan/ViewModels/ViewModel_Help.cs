using System;
using System.Collections.Generic;
using System.Text;

namespace RiECalmingPlan.ViewModels {
    public class ViewModel_Help : ViewModel_Base {


        public string _Title;
        public string Title { get { return _Title; } set { SetProperty(ref _Title, value); } }

        public string _Text;
        public string Text { get { return _Text; } set { SetProperty(ref _Text, value); } }

        public ViewModel_Help(string pagelabel) {
            UpdatePageLabel(pagelabel);
        }

        public void UpdatePageLabel(string pagelabel) {
            switch (pagelabel) {
                case "DefaultHelp":
                    Title = "Welcome/About";
                    Text = "Welcome to RestinEssence (RiE). This app has been designed to help you build your coping and resilience. Complete your calming plan and understand how different levels of distress feel, and what you can do to cope, or how to reach out to others when you need them most. \nIf you are a parent using this App, then RiE will help you to recognise when your child needs help, and when they can manage on their own.\nFill out the Calming Plan and automatically build you own individualised Support Plan. This will show up on the Distress Tracker. Each time you log your distress on your Distress Tracker Support Plan it will keep a score of how you are doing. View the Distress History Chart to see how you are building those coping and resilience skills over time, or look for patterns that might show when you need help from others. We have even included you own private personal electronic journal where you can record your thoughts and feelings, and even upload a picture.\nGet started by clicking on Calming Plan. When you are finished this it will build your very own Support Plan.";
                    break;
                case "QuestionsPage":
                    Title = "Calming Plan";
                    Text = "Great, let’s get started.\nRead the following questions and pick all the answers that relate to you.\nSome of the questions ask you to rate on a scale of 0 - 3.\n0 means it does not relate to you, 1 = relates to mild distress.\nMild distress is when you feel a little bit distressed but you can manage yourself.\n2 = moderate distress, this is normally when you would need someone to start supporting you.\n3 = acute distress, this would be when you would need an adult’s support and may even start to feel unsafe or out of control.\nThere are a number of suggestions to choose from but don’t forget you can add your own answers by typing in the other section.\nWhen you have finished answering the Calming Plan questions your Support Plan will build automatically.\nGo to the Distress Tracker to view your support plan. Any time you want to make changes to your Calming Plan just click on the button in the Main Menu.";
                    break;
                case "DistressTrackerPage":
                    Title = "Distress Tracker";
                    Text = "At any time, you can try and guess how you are feeling.\nChoose any of the Calm, Mild, Moderate and Acute levels and based on your Calming Plan responses you will be provided with the Expressions (of distress), things you might be saying (or thinking), doing (actions), and feeling in your body. \n\nIf the Expressions are correct, select Yes and a list of Suggestions on how you could cope at this time will be offered based on your Calming Plan.If it is not quite right, select No and try again by deciding if you feel more or less distressed.\nIf you choose Acute distress you will be provided with suggestions to cope but also a list of important numbers and contacts you can reach out to for help.There is always an adult out there wants to support you.\n\nEach time you choose Yes, the Distress Tracker will log your level of distress, the time and location.\nYou can view this any time by going to the Main Menu and clicking on the Distress History Chart.";
                    break;
                case "DistressHistoryPage":
                    Title = "Distress History Chart";
                    Text = "Check out how you have been going. Use this chart to assess your progress. If things have been a bit difficult lately you can use the times, location and trends to look for patterns about when and where you might need help. This information would be a really great thing to talk about with a trusted adult. They might be able to help you reflect and come up with some ideas as you make progress.";
                    break;
                case "JournalPage":
                    Title = "My Private Journal";
                    Text = "This space is just for you! Use the private journal to record your experiences, thoughts and feelings over time. Journaling is a great way to store memories but is also an opportunity to get some of the mind clatter out and ‘close the book on it’. This is especially good to do after dinner as you start your nightly sleep routine. ";
                    break;
            }

        }
    }
}
