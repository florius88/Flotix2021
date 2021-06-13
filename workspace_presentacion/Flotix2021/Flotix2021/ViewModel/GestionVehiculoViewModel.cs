using Flotix2021.HelperClasses;
using Flotix2021.Model;
using Flotix2021.ModelDTO;
using Flotix2021.Utils;
using System;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    public class GestionVehiculoViewModel : BaseViewModel
    {
        private static bool _panelLoading;
        private static VehiculoDTO _vehiculo;
        private static ImagenVehiculo _imagenVehiculo;
        private static ImagenVehiculo _imagenPermisoVehiculo;

        public VehiculoDTO vehiculo
        {
            get { return _vehiculo; }
            set { _vehiculo = value; }
        }

        public ImagenVehiculo imagenVehiculo
        {
            get { return _imagenVehiculo; }
            set { _imagenVehiculo = value; }
        }

        public ImagenVehiculo imagenPermisoVehiculo
        {
            get { return _imagenPermisoVehiculo; }
            set { _imagenPermisoVehiculo = value; }
        }

        public GestionVehiculoViewModel()
        {
            imagenVehiculo = null;
            imagenPermisoVehiculo = null;
        }

        public GestionVehiculoViewModel(VehiculoDTO vehiculoDTO)
        {
            _vehiculo = vehiculoDTO;
        }

        public Array PlazasArray
        {
            get { return Constantes.arrayPlazas; }
        }

        public Array TamArray
        {
            get { return Constantes.arrayTam; }
        }


        /**
        *------------------------------------------------------------------------------
        * Metodos para controlar el panel de transicion
        *------------------------------------------------------------------------------
        **/

        /// <summary>
        /// Gets or sets a value indicating whether [panel loading].
        /// </summary>
        /// <value>
        /// <c>true</c> if [panel loading]; otherwise, <c>false</c>.
        /// </value>
        public bool PanelLoading
        {
            get
            {
                return _panelLoading;
            }
            set
            {
                _panelLoading = value;
                OnPropertyChanged("PanelLoading");
            }
        }

        /// <summary>
        /// Gets the panel close command.
        /// </summary>
        public ICommand PanelCloseCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    // Your code here.
                    // You may want to terminate the running thread etc.
                    PanelLoading = false;
                });
            }
        }

        /// <summary>
        /// Gets the show panel command.
        /// </summary>
        public ICommand ShowPanelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PanelLoading = true;
                });
            }
        }

        /// <summary>
        /// Gets the hide panel command.
        /// </summary>
        public ICommand HidePanelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PanelLoading = false;
                });
            }
        }
    }
}
