using Flotix2021.Collection;
using Flotix2021.Helpers;
using Flotix2021.Interfaces;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    public class VehiculosViewModel : BaseViewModel, INotifyPropertyChanged
    {

        public VehiculosViewModel(IChangeViewModel viewModelChanger) : base(viewModelChanger)
        {

        }

    }
}