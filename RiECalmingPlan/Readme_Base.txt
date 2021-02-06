Version 1.0. Graeme Chew. 9/01/2021.


This is the documentation of Rest in Essence Mobile Application - Known as "Auteenome"

The app is currently split into multiple functionalities, which are explained separately along with the inclusion of which files are in its circle of influence.

You can use this as a guide to look within each of the files for a specific functionality, then look within commenting for further detail.
Most explanations are done in the ViewModel area, since Models are usually atomic data holding fields and Views are visual representations of that data.

The shared code contains most of the functions that can be run on both Android and IOS versions, with a couple of interfacing issues explained here:
File Pathing, Permissions, Database File Loading, LocalNotifications, and certain UI renderers may be different between these platforms.
Libraries and Third-Party APIs may help with bringing these closer together towards shared code, but are worth looking into if the above issues are causing problems or need to be edited.

0. Login System
Pages/Page_TermsAndConditions (.xaml + .cs)
Pages/Page_Login (.xaml + .cs)
Pages/Page_Menu (.xaml + .cs)
Pages/Register (.xaml + .cs)
ViewModels/ViewModel_Login.cs

1. Calming Plan
The Calming Plan is essentially a 'Survey' with different types of questions.
The bulk of these files are in the Models/Database folder, which makes up for the inheritance rules and type grouping needed for these to work in the app.
In the current version of the app, the Non-Generated questions are not shown in the survey, but files are still included to account for that.

Views/DisplayQuestionView (.xaml + .cs)
ViewModels/DisplayQuestion.cs
Pages/Page_Questions (.xaml + .cs)
Views/StepProgressBar.cs
Views/AnswerTemplateSelector.cs
Models/Database/RiEFeedback.cs
Models/Database/Question.cs
Models/Database/Response.cs
Models/Database/Label_Checkbox.cs
Models/Database/Label_Stepper.cs
Models/Database/Label_TextResponse.cs
Models/Database/Database.cs
Models/Database/GeneratedResponse.cs
Models/Database/NonGeneratedResponse.cs
Views/ResponseTypeToBoolConverter.cs

2. Distress Tracker
The Distress Tracker displays a scaled choice (Calm (0) -> Acute (3)) and then a generated list of Responses and Suggestions depending on the selected choice.
Selecting a choice stores back into the database and tracks location and time when clicked. This information is then used for Distress History.

Pages/Page_DistressTracker (.xaml + .cs)
ViewModels/ViewModel_DistressLevel.cs
Models/Database/Response.cs
Models/Database/Suggestion.cs
Models/Database/UserInputDistressLevel.cs
Models/Database/Database.cs
Models/Database/DistressType.cs

3. Distress History/Graph
The Distress History displays a graph and a list of selected choices in the distress tracker that is saved over time within the Distress Tracker section.

Pages/DistressHistory (.xaml + .cs)
ViewModel/ViewModel_DistressHistory.cs
Models/Database/UserInputDistressLevel.cs
Models/Database/Database.cs

4. User Diary
Pulls Diary information from a JSON file, and allows the user to create diary entries to store back into the JSON file.
It will also use the Database for starter context so the user can continue writing their diary entries.
This page is also FingerPrint Protected.

Pages/Page_UserDiary (.xaml + .cs)
[- REMOVED] Pages/Page_NewDiaryEntry (.xaml + .cs)
[- REMOVED] ViewModels/ViewModel_DiaryStarters.cs
[+ ADDED] ViewModels/ViewModel_UserDiary.cs
ViewModels/ViewModel_DiaryEntries.cs
Models/Database/DiaryStarter.cs
Models/Database/Database.cs
Models/JSON/DiaryEntry.cs
Models/JSON/UserDiaryFileController.cs

5. About Page
Basically a wall of text explaining the purpose of the app in relation to the binding company.

Page_About.xaml
Page_About.cs
