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

        public ObservableCollection<string> observableCollectionClientes = new AsyncObservableCollection<string>();
        public static List<ClienteDTO> _listaClientes = null;

        public AlquilerDTO alquiler
        {
            get { return _alquiler; }
            set { _alquiler = value; }
        }

        public GestionAlquileresViewModel()
        {

        }

        public Array TipoKMArray
        {
            get { return Enum.GetValues(typeof(Constantes.TipoKM)); }
        }

        public GestionAlquileresViewModel(AlquilerDTO alquilerDTO)
        {
            _alquiler = alquilerDTO;
        }

        public void cargaComboMatriculas()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAllFilter("null","null","null","true");

                if (200 == serverResponseVehiculo.error.code)
                {
                    _listaVehiculos = serverResponseVehiculo.listaVehiculo;

                    foreach (var item in serverResponseVehiculo.listaVehiculo)
                    {
                        observableCollectionMatriculas.Add(item.matricula);
                    }
                }
            }));

            t.Start();
        }

        public void cargaComboClientes()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                ServerResponseCliente serverResponseCliente = serverServiceCliente.GetAll();

                if (200 == serverResponseCliente.error.code)
                {
                    _listaClientes = serverResponseCliente.listaCliente;

                    foreach (var item in serverResponseCliente.listaCliente)
                    {
                        if (null == _alquiler || !_alquiler.cliente.nif.Equals(item.nif))
                        {
                            observableCollectionClientes.Add(item.nombre);
                        }
                    }
                }
            }));

            t.Start();
        }

        public List<VehiculoDTO> ListaVehiculos
        {
            get
            {
                return _listaVehiculos;
            }
        }

        public List<ClienteDTO> ListaClientes
        {
            get
            {
                return _listaClientes;
            }
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
