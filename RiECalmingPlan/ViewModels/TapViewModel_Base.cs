using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace RiECalmingPlan.ViewModels {
    public class TapViewModel_Base : ViewModel_Base {
        ICommand tapCommand;
        public TapViewModel_Base()
        {
            tapCommand = new Command(OnTapped);
        }

        public ICommand TapCommand {
            get { return tapCommand; }
        }

        public virtual void OnTapped(object s)
        {
        }

    }
}
