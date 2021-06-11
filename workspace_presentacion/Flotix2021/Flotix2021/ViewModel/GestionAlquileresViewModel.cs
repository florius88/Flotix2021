using Flotix2021.Collection;
using Flotix2021.HelperClasses;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    class GestionAlquileresViewModel : BaseViewModel
    {
        private static bool _panelLoading;
        private static AlquilerDTO _alquiler;
        public ObservableCollection<string> observableCollectionMatriculas = new AsyncObservableCollection<string>();
        public static List<VehiculoDTO> _listaVehiculos = null;

        public AlquilerDTO alquiler
        {
            get { return _alquiler; }
            set { _alquiler = value; }
        }

        public GestionAlquileresViewModel()
        {

        }

        public GestionAlquileresViewModel(AlquilerDTO alquilerDTO)
        {
            _alquiler = alquilerDTO;
        }

        public void cargaComboMatriculas()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                observableCollectionMatriculas.Add("Seleccionar");

                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAll();

                if (200 == serverResponseVehiculo.error.code)
                {
                    _listaVehiculos = serverResponseVehiculo.listaVehiculo;

                    foreach (var item in serverResponseVehiculo.listaVehiculo)
                    {
                        observableCollectionMatriculas.Add(item.matricula);
                    }
                }
                else
                {
                    observableCollectionMatriculas.Add("Seleccionar");
                }
            }));

            t.Start();
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
