using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.ResourceDictionaries
{
    public partial class Dictionary_Tablet : ResourceDictionary
    {
        public static Dictionary_Tablet SharedDictionary { get; } = new Dictionary_Tablet();
        public Dictionary_Tablet()
        {
            InitializeComponent();
        }
    }
}