# RiECalmingPlan
Mobile App that allows users to complete Calming Plan Survey

CURRENT CHANGES
1. Database now is local (Untested on IOS). 
It will move the database file from Assets/Resources folder and moves it to a location in app storage. A 'Reset Database' button can be clicked on to delete the existing copied database and replace it with a new blank database from the Assets folder.
Any changes to the database tables must also happen to the database models in the code.

2. All questions split apart over multiple pages are condensed into one page with multiple 'cards' that have a question and
appropriate responses. Checkbox and Stepper informaiton is saved as the user presses the respective controls, and text responses are saved when exiting out of the keyboard.

TODO
1. Fix presentation of the questionaire cards
2. Test Database code on IOS

NOTES
1. ListViews etc are not very good to have with a CarouselView, as swiping diagonally upwards or downwards will result 
in the carousel moving and not the ListView Scrolling
2. Carousel cards are differentiated by question type, which allows for different sizing and styles for all three current types
(Stepper, Text Response, Checkbox).
3. As cards are swiped and values are loaded in, it will trigger events that activate when a value is changed (E.G checkbox default is 0 but a loaded binding value could change it to 1 if checked before). Some events may be triggered on unrendered elements but do not affect the backend side of the app.
