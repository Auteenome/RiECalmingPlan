using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RiECalmingPlan.ResourceDictionaries {
    public partial class Dictionary_Base : ResourceDictionary {
        public static Dictionary_Base SharedDictionary { get; } = new Dictionary_Base();    // singleton for referencing dictionary
        public Dictionary_Base()
        {
            InitializeComponent();
        }
    }
}