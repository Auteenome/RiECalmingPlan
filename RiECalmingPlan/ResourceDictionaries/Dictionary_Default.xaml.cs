using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.ResourceDictionaries
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dictionary_Default : ResourceDictionary
    {
        public static Dictionary_Default SharedDictionary { get; } = new Dictionary_Default();    // singleton for referencing dictionary
        public Dictionary_Default()
        {
            InitializeComponent();
        }
    }
}