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

4/02/2021 (5hr)
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

6/02/2021 (4hrs)
- [User Diary] Leaving an EDITED Diary Entry will automatically save that frame, and refresh the Carousel View UI



