# RiECalmingPlan
Mobile App that allows users to complete Calming Plan Survey

CURRENT CHANGES
1. Database now is local (Untested on IOS). 
It will move the database file from Assets/Resources folder and moves it to a location in app storage.
Any changes to the database tables must also happen to the database models in the code.
Changes to the fields do not need a database model change.

2. All questions split apart over multiple pages are condensed into one page with multiple 'cards' that have a question and
appropriate responses. Text Response does not show as no items are in that respective table

TODO
1. Show proper methods of answering the DisplayQuestion, as well as storing this information to the database
2. Test Database code on IOS, and include a way to overwrite the base database over the editted database in the shared code

NOTES
1. ListViews etc are not very good to have with a CarouselView, as swiping diagonally upwards or downwards will result 
in the carousel moving and not the ListView Scrolling
2. Carousel cards are differentiated by question type, which allows for different sizing and styles for all three current types
(Radio Box, Text Response, Checkbox).
