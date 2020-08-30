using RiECalmingPlan.Models;
using System;
using System.Linq;
using Xamarin.Forms;

namespace RiECalmingPlan.Views {

    // code referenced from: https://xamgirl.com/step-bar-in-xamarin-forms/
    // modified by Mitchell Hedges
    public class StepProgressBar : StackLayout {
        /*
         * This StepProgressBar does the following things (To avoid confusion)
         * 
         * 1. Generates the unselected circles and lines based on the number of steps provided to the stacklayout
         * 2. Detects all the changes dealt to the property 'StepSelected' and toggles its unselected style to the selected style
         *    Since the default value of the StepSelectedProperty is -1 (Which is 1 less than the default value known in the database), this will always trigger on startup
         * 3. Clicking on an unselected circle will deselect it and select the new one, treating the new selected circle as the target to be unselected if another one were to
         *    be selected
         * 
         *
         * 
         */

        Button _lastStepSelected;
        public static BindableProperty StepsProperty = BindableProperty.Create(nameof(Steps), typeof(int), typeof(StepProgressBar), 0);
        public static BindableProperty StepSelectedProperty = BindableProperty.Create(nameof(StepSelected), typeof(int), typeof(StepProgressBar), -1, defaultBindingMode: BindingMode.OneWayToSource);
        public static BindableProperty StepColorProperty = BindableProperty.Create(nameof(StepColor), typeof(Color), typeof(StepProgressBar), Color.Black, defaultBindingMode: BindingMode.TwoWay);

        public Color StepColor {
            get { return (Color)GetValue(StepColorProperty); }
            set { SetValue(StepColorProperty, value); }
        }

        public int Steps {
            get { return (int)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public int StepSelected {
            get { return (int)GetValue(StepSelectedProperty); }
            set { SetValue(StepSelectedProperty, value); }
        }


        public StepProgressBar() {
            Orientation = StackOrientation.Horizontal;
            // VerticalOptions = LayoutOptions.EndAndExpand;
            HorizontalOptions = LayoutOptions.EndAndExpand;     // layoutoption to align stepbars to the right
            Padding = new Thickness(10, 0);
            Spacing = 0;
            AddStyles();
        }

        protected override void OnPropertyChanged(string propertyName = null) {
            base.OnPropertyChanged(propertyName);

            if (propertyName == StepsProperty.PropertyName) {
                for (int i = 0; i < Steps; i++) {
                    //Generates the circle button things, defaulting them all as unselected circles
                    var button = new Button() {
                        Text = $"{i}",
                        ClassId = $"{i}",
                        Style = Resources["unSelectedStyle"] as Style
                    };

                    button.Clicked += Handle_ClickedAsync;

                    this.Children.Add(button);

                    if (i < Steps - 1) {
                        //Generates the lines between each circle
                        var separatorLine = new BoxView() {
                            BackgroundColor = Color.Silver,
                            HeightRequest = 1,
                            WidthRequest = 5,
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        this.Children.Add(separatorLine);
                    }
                }
            } else if (propertyName == StepSelectedProperty.PropertyName) {
                var children = this.Children.First(p => (!string.IsNullOrEmpty(p.ClassId) && Convert.ToInt32(p.ClassId) == StepSelected));
                if (children != null) {
                    SelectElement(children as Button);
                }

            } else if (propertyName == StepColorProperty.PropertyName) {
                AddStyles();
            }
        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e) {
            Button b = (Button)sender;
            Label_Stepper stepperLabel = ((Label_Stepper)b.BindingContext);
            SelectElement(b);

            await App.database.UpdateStepperResponse(stepperLabel);
        }

        void SelectElement(Button elementSelected) {

            // Unselects the last button selected
            if (_lastStepSelected != null) {
                _lastStepSelected.Style = Resources["unSelectedStyle"] as Style;
            } 

            // Selects the new button selected
            elementSelected.Style = Resources["selectedStyle"] as Style;

            StepSelected = Convert.ToInt32(elementSelected.Text);

            // The last button is refered to the new button clicked 
            _lastStepSelected = elementSelected;
        }

        void AddStyles() {
            var unselectedStyle = new Style(typeof(Button)) {
                Setters = {
                    new Setter { Property = BackgroundColorProperty,   Value = Color.Transparent },
                    new Setter { Property = Button.BorderColorProperty,   Value = StepColor },
                    new Setter { Property = Button.TextColorProperty,   Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 }
                }
            };

            var selectedStyle = new Style(typeof(Button)) {
                Setters = {
                    new Setter { Property = BackgroundColorProperty, Value = StepColor },
                    new Setter { Property = Button.TextColorProperty, Value = Color.White },
                    new Setter { Property = Button.BorderColorProperty, Value = StepColor },
                    new Setter { Property = Button.BorderWidthProperty,   Value = 0.5 },
                    new Setter { Property = Button.CornerRadiusProperty,   Value = 20 },
                    new Setter { Property = HeightRequestProperty,   Value = 40 },
                    new Setter { Property = WidthRequestProperty,   Value = 40 },
                    new Setter { Property = Button.FontAttributesProperty,   Value = FontAttributes.Bold }
                }
            };

            Resources = new ResourceDictionary();
            Resources.Add("unSelectedStyle", unselectedStyle);
            Resources.Add("selectedStyle", selectedStyle);
        }
    }
}
