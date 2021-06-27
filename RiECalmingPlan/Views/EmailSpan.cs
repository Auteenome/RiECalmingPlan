using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace RiECalmingPlan.Views {
    public class EmailSpan : Span {

        public static readonly BindableProperty EmailProperty = BindableProperty.Create(nameof(EmailAddress), typeof(string), typeof(EmailSpan), null);

        public string EmailAddress {
            get { return (string)GetValue(EmailProperty); }
            set { SetValue(EmailProperty, value); }
        }


        public EmailSpan() {
            TextDecorations = TextDecorations.Underline;
            TextColor = Color.Blue;
            GestureRecognizers.Add(new TapGestureRecognizer {
                // Launcher.OpenAsync is provided by Xamarin.Essentials.
                Command = new Command(async () =>
                    
                    await Email.ComposeAsync(new EmailMessage(to: EmailAddress, subject: "Nothing", body: "Sent from RiE Mobile App" ))

                )

            });
        }
    }
}
