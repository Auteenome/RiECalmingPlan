# RiECalmingPlan
Mobile App that allows users to complete Calming Plan Survey

CURRENT FEATURES AND CHANGES
1. Database now is local (Untested on IOS). 
It will move the database file from Assets/Resources folder and moves it to a location in app storage. A 'Reset Database' button can be clicked on to delete the existing copied database and replace it with a new blank database from the Assets folder.
Any changes to the database tables must also happen to the database models in the code.

2. All questions split apart over multiple pages are condensed into one page with multiple 'cards' that have a question and
appropriate responses. Checkbox and Stepper informaiton is saved as the user presses the respective controls, and text responses are saved when exiting out of the keyboard.

3. The 'Distress Tracker' allows you to add distress responses into the same database file and review them in the 'Distress History' page. When selecting a Distress Level, a new timestamp is created with that level and the current time, and will only be logged into the database one minute after if the user is still in the same Distress Tracker Page.
   - The Distress Tracker will also show Expressions and Suggestions after selecting a Distress Level, where Suggestions also show Phone Number and URL links. Clicking on these links will allow you to use the Phone Dialer and Web Browser respectively.
   
4. This 'Distress History' page also allows filtering for four different filters for timestamps (All, Today, Week (7 days before today), and Month (1 month before this one)). This includes a small graph that shows the filtered list of timestamps that also change when the selected filter is changed.

5. The 'User Diary' allows users to CREATE and UPDATE Diary Entries. If storage permissions has not been granted through settings, the app will ask if you would like to turn on those permissions before proceeding to show the image chosen from the path.
   - On Android, If Permissions has not been granted once the initial alert is requested to the user, it will not ask again if the user has checked the 'Do not ask again' checkbox within the alert. If this was checked before the alert was confirmed, the user must turn on the permission through the settings.
   - On IOS, If Permissions has not been granted once the initial alert is requested to the user, it will not ask again. The user must toggle the permission through their settings.
   - Currently, the User Diary does not allow for removal of singular diary entries. The Reset button clears all Diary Entries.

TODO
1. Update Graph Presentation
2. Implement the full Terms and Conditions when given

NOTES
1. ListViews etc are not very good to have with a CarouselView, as swiping diagonally upwards or downwards will result 
in the carousel moving and not the ListView Scrolling
2. Carousel cards are differentiated by question type, which allows for different sizing and styles for all three current types
(Stepper, Text Response, Checkbox).
3. As cards are swiped and values are loaded in, it will trigger events that activate when a value is changed (E.G checkbox default is 0 but a loaded binding value could change it to 1 if checked before). Some events may be triggered on unrendered elements but do not affect the backend side of the app.

4. A little ambiguity is shown in the Month/Week filters, as Month could also mean "roughly 30 days ago", and Week can also mean "The full week Monday-Sunday before the current week". A good change to this is allowing the user to enter a specific date and any entries that pop up that is between that date and today will be shown.
