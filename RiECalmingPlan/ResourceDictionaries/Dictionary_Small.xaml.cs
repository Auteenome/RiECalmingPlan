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
    public partial class Dictionary_Small : ResourceDictionary
    {
        public static Dictionary_Small SharedDictionary { get; } = new Dictionary_Small();    // singleton for referencing dictionary

        public Dictionary_Small()
        {
            InitializeComponent();
        }
    }
}