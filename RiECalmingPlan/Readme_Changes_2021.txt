Changelog 2021

9/1/2021 (4hr)
- Base Readme file added
- [Distress Pyramid] Timer reduced temporarily
- [Main Menu] About Page Added
- [Global] Minor text changes within app

19/01/2021 (3hr)
- [Distress Pyramid] Timer removed
- [Main Menu] Layout changed to include last diary entry date + T&C read-only page button
- [Terms and Conditions -> Main Menu] back button does not show on first entering app (The back button blinks when transitioning anyway)

23/01/2021 (4hr)
- [Calming Plan] Appended a last slide to the Questions Carousel to indicate the end of the Survey
- [Support Plan] Changed the flow of the Support Plan page. Will deal with the UI changes later.

25/01/2021 (4hr)
- [Support Plan] Tidied up the Suggestions frame to match the Expressions Frame

26/01/2021 (6hr)
- [User Diary] Started working on the layout and data updates

28/01/2021 (6hr)
- [User Diary] Continued working on the layout and data updates (Saving and loading from the JSON works, but more work needs to be done to make it work with images etc)

29/01/2021 (6hr)
- [User Diary] Continued working on the layout and data updates (Images now work correctly)

30/01/2021 (6hr)
- [User Diary] Continued working on the layout updates (UI is tidied up a bit and now it is suitable to be used functionally
	(Note, there is no function that allows the user to remove an image from the list when editing a diary entry. Might be added in eventually)

31/01/2021, 1/02/2021 (8hr)
- [User Diary] Continued working on UI, added new icons, vertical scrolling, diary starters now fully work 
	(Image Resources are added only in android side for now but also need to be updated so all icons are consistent in style before publishing to IOS store etc)

2/02/2021, 3/02/2021 (5hr)
- [Main Menu] Terms and Conditions button now takes you back to the Terms and Conditions page.
- [T&C Page] Checkbox recolour, T&C bottom controls are now only visible if the user hasn't already accepted the first time after the app is installed
- [User Diary] Vertical scrolling -> Horizontal Scrolling
- [Calming Plan, Distress Tracker, Distress Graph, User Diary] Upon entering the page for the first time, a popup occurs showing help text
- [Distress Tracker] Distress Input from Pyramid is now saved even without location being pulled (Will display a '-' if no value was saved at the time)

4/02/2021 (8hrs)
- Android Minimum SDK is now at v10
- [Nuget Packages] All packages updated to latest, except Xamarin Forms (See Notes)
- [Calming Plan, User Diary, Distress Tracker] Carousel Loop property set to false. Nuget Package update forced Carousel to loop as a default. REDACTED (See Notes)
- [Calming Plan] Main Menu button added at the last slide
Notes: Xamarin Forms [5.0.0.1931] (Newest) is not compatable with Carouselview for this project. This is because it has something to do with the new loop feature.
The last known version of it is Xamarin Forms [4.8.0.1821]. This will be kept until a more stable version is released. Also at this point the Loop property is taken back off.
- [User Diary, Main Menu] Last Edited and First Submitted fields are correctly changed and reflective in the views it belongs to.
- [Calming Plan, Distress Tracker, Distress Graph, User Diary] The user is given an option to permanently remove the respective dialog box after the first time after installation

5/02/2021 (1hr)
- [User Diary] Added a confirmation dialog box upon deleting a Diary Entry

6/02/2021 (8hrs)
- [User Diary] Leaving an EDITED Diary Entry will automatically save that frame, and refresh the Carousel View UI
- [Android Global] Pink Accent changed to RiE Dark Green for controls such as Editor, Entry, Dialog Boxes, etc
- [User Diary] Updated UI such that the positioning of buttons are fixed at the end and the content inside editors etc are height variable.
(Multiple lines in the Editor Control, also the 'Body' part of the Diary Entry, will squash the Pictures Collection downward, but only for a fixed amount of lines)
- [Calming Plan] "Add" button removed in the UI, its functionality is replaced by exiting the keyboard when typing in a new response.	

16/02/2021 (4hrs)
- [Distress History] Fixed a null reference exception for the Graph control. The 'text' property was compulsory for a ChartEntry object.

25/02/2021 (8hrs)
- [StepProgressBar (For Calming Plan)] 
Highlighted Circles start at value = 0, unless the user has entered a higher number before. If so, the "OnPropertyChanged" event occurs, and highlights the correct value before unselecting
the previous one.
Previously, Setting the default value for "StepSelectedProperty" to -1 as a quick fix will auto-highlight all zeros, and trigger "OnPropertyChanged", but swiping to the end was impossible
due to a pattern error.
Further testing lead to the Convert.Int32() function to result in a signed integer of a string (Used for the button Id's) but results in a 0, if the value was null. 
This hinders using 0 outright as in this case it is treated as the same as null.

Fix:
Iterator starts at 1 instead of 0, the text of the button generated in each iteration is -1 of the iterator's current index, which makes it look like it starts from 0
references will use the classID of the button instead, which will pretty much be the same value as the iterator. This is where we get [1, 2, 3, 4] for what seems to be [0, 1, 2, 3]
Using FirstOrDefault() instead of First() allows it to return a better result before being null checked, allowing the user to swipe through the entire carousel without crashing.

- [User Diary] 
Previous: The last index of the diary, upon deletion, triggers the "OnPositionChanged" Command, which checks the diary list of its previous position before moving the current carousel
position to the new last index. This results in an IndexOutOfBounds error.
Current: The "OnPositionChanged" command now proceeds to check the item in the array if it is below its item count.

Unfixable: For Android, the back button cannot be replaced by text

25/03/2021 (8hrs)
- [Questions.db] [DiaryStarter.cs removed] Diary Starters were removed from the database as they are one dimensional. The starters are hardcoded into the Picker element in the Page_UserDiary.xaml file instead.
- [User Diary] + button placed next to the Help button in the toolbar to allow the user to add a new page.
- [User Diary] Scrolling within the carousel does not enforce an entire Itemsource update. Instead, elements are now properly replaced in the viewmodel such that the UI will pick this up
independently of each element.
- [Distress Tracker] Graph as been updated as per Peter's configurations.

26/03/2021 (9hrs)
- [User Diary] Updated Image Collection padding.
- [User Diary] Started on infrastructure changes to allow for Diary Cover to be saved/edited alongside with diary entries.

27/03/2021, 28/03/2021 (8hrs)
- [User Diary] Finished infrastructure changes. 
The diary cover/entries can be edited/saved and when loading it treats all pages as "completed". It should automatically save upon swipe.
Cover background can be changed (preset ones are added in)

28/03/2021 (4hrs)
- [User Diary] Included icons that apply based on the entry's 'HappinessIndicator' variable
- [resources] Added icons that were in the Android variant but not in the IOS variant
- [User Diary] Moved the Edit to the Toolbar section of the Diary, allowing it to be used as a toggle button to change the state of the slide at will.

28/03/2021 (2hrs)
- [IOS] Splash screen now works

1/04/2021 (4hrs)
- [Distress History] UserInputDistressLevel table cleans up upon entering the Distress History page, removing all entries older than a month.
- [Distress Tracker] Updated the Suggestions/Responses frame (Just in case if no Suggestions are listed in the database)

6/04/2021 (7hrs)
- [Diary Page] Very minor changes here.
On IOS, the carousel scrolls to the end when a new entry is created. After reaching the end, the carousel moves back to the start of the list,
which means the individual IOS code's CarouselView implementation needs to be fixed.
When Scrolling, setting the animate parameter allows the carousel to move towards the end, opposed to setting it on and it only moving once.
Scrolling back on its own will trigger the PositionChanged event and therefore the last entry will mutate into a completed state.
Regardless, the Diary still will create an entry under the edit state, no problems, but scrolling to that position in IOS is troublesome for now.
On Android, no dramas.

8/05/2021 (7hrs)
- [Main Menu] Toolbar now shows "Options" instead of "Reset Database", as the "Reset Database" function is moved to a new page,
alongside with the newly added "Delete Diary" function. You can also see both file sizes for the Diary and Local database in this page

19/05/2021, 20/05/2021 - 21/05/2021 (8hrs)
- [User Diary] Allowed a completed diary entry's photos to be clicked on. This opens up a new page with that photo and lets you slide between them all.
You can also delete an image using the delete toolbar option.

TODO: Clicking on an image will still put you to the first image of the collection when it enters the enlargement page. I need to fix this by either:
1. Forcing the new carousel of images to scroll to the image clicked. The index of the image clicked needs to be referenced for the new carousel. Harder to do, but best outcome.
2. Force the new page to only house one image, and allow the user to delete just that one at will. Easier, but not great as we don't want to be stuck in 2010.

- Further commenting added

12/06/2021 - 13/06/2021 (4hrs)
- [Main Menu, Options] User Diary renamed to My Private Journal in the Main Menu. "Reset Diary" renamed to "Reset Journal" in Options.
- [Questions.db] The following changes have been made to the Questions Table:
CPQID ordering:
6 -> 6

13 -> 18
14 -> 19
15 -> 20
16 -> 21

18 -> 13
19 -> 14
20 -> 15
21 -> 16