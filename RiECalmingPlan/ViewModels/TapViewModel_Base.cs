using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;

namespace RiECalmingPlan.ViewModels {
    public class TapViewModel_Base : ViewModel_Base {
        // the base view model for implementing a tapGestureRecognizer
        // override the OnTapped method for unique implementation

        private ICommand _TapCommand;
        public TapViewModel_Base()
        {
            _TapCommand = new Command(OnTapped);
        }

        public ICommand TapCommand {
            get { return _TapCommand; }
        }

        public virtual void OnTapped(object s)
        {
        }

    }
}
