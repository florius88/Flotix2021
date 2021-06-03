using Flotix2021.Helpers;
using Flotix2021.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    class MainWindowViewModel : ChangeNotifier, IChangeViewModel
    {
        BaseViewModel _currentViewModel;

        public IChangeViewModel ViewModelChanger { get; private set; }

        public MainWindowViewModel()
        {
            var initialViewModel = new InicioViewModel(this);
            CurrentViewModel = initialViewModel;
        }

        public BaseViewModel CurrentViewModel
        {
            get { return _currentViewModel; }
            set { _currentViewModel = value; NotifyPropertyChanged(); }
        }

        #region IChangeViewModel

        public void PushViewModel(BaseViewModel model)
        {
            CurrentViewModel = model;
        }

        #endregion

        #region MoveToInicio

        public ICommand MoveToInicio
        {
            get { return new RelayCommand(LoadInicioView); }
        }

        private void LoadInicioView()
        {
            PushViewModel(new InicioViewModel(ViewModelChanger));
        }

        #endregion

        #region MoveToVehiculo

        public ICommand MoveToVehiculo
        {
            get { return new RelayCommand(LoadVehiculoView); }
        }

        private void LoadVehiculoView()
        {
            PushViewModel(new VehiculosViewModel(ViewModelChanger));
        }

        #endregion

        #region MoveToAlquileres

        public ICommand MoveToAlquileres
        {
            get { return new RelayCommand(LoadAlquileresView); }
        }

        private void LoadAlquileresView()
        {
            PushViewModel(new AlquileresViewModel(ViewModelChanger));
        }

        #endregion

        #region MoveToCaducidades

        public ICommand MoveToCaducidades
        {
            get { return new RelayCommand(LoadCaducidadesView); }
        }

        private void LoadCaducidadesView()
        {
            PushViewModel(new CaducidadesViewModel(ViewModelChanger));
        }

        #endregion

        #region MoveToMantenimientos

        public ICommand MoveToMantenimientos
        {
            get { return new RelayCommand(LoadMantenimientosView); }
        }

        private void LoadMantenimientosView()
        {
            PushViewModel(new MantenimientosViewModel(ViewModelChanger));
        }

        #endregion

        #region MoveToClientes

        public ICommand MoveToClientes
        {
            get { return new RelayCommand(LoadClientesView); }
        }

        private void LoadClientesView()
        {
            PushViewModel(new ClientesViewModel(ViewModelChanger));
        }

        #endregion

        #region MoveToAjustes

        public ICommand MoveToAjustes
        {
            get { return new RelayCommand(LoadAjustesView); }
        }

        private void LoadAjustesView()
        {
            PushViewModel(new AjustesViewModel(ViewModelChanger));
        }

        #endregion

        #region Salir

        

        #endregion

    }
}
