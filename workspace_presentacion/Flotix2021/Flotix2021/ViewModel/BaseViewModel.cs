using Flotix2021.Helpers;
using Flotix2021.Interfaces;
using System;

namespace Flotix2021.ViewModel
{
    class BaseViewModel : ChangeNotifier, IChangeViewModel
    {
        IChangeViewModel _viewModelChanger;

        public BaseViewModel(IChangeViewModel viewModelChanger)
        {
            ViewModelChanger = viewModelChanger;
        }

        public IChangeViewModel ViewModelChanger
        {
            get { return _viewModelChanger; }
            set { _viewModelChanger = value; }
        }

        #region IChangeViewModel

        public void PushViewModel(BaseViewModel model)
        {
            _viewModelChanger?.PushViewModel(model);
        }

        #endregion
    }
}
