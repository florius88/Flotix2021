using Flotix2021.Collection;
using Flotix2021.HelperClasses;
using Flotix2021.Model;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    class GestionMantenimientosViewModel : BaseViewModel
    {
        private static bool _panelLoading;
        private static MantenimientoDTO _mantenimiento;
        private static ImagenVehiculo _imagenVehiculo;

        public ObservableCollection<string> observableCollectionMatriculas = new AsyncObservableCollection<string>();
        public static List<VehiculoDTO> _listaVehiculos = null;

        public ObservableCollection<string> observableCollectionTipoMantenimiento = new AsyncObservableCollection<string>();
        public static List<TipoMantenimientoDTO> _listaTipoMantenimiento = null;

        public MantenimientoDTO mantenimiento
        {
            get { return _mantenimiento; }
            set { _mantenimiento = value; }
        }

        public ImagenVehiculo imagenVehiculo
        {
            get { return _imagenVehiculo; }
            set { _imagenVehiculo = value; }
        }

        public GestionMantenimientosViewModel()
        {
            imagenVehiculo = null;
        }

        public GestionMantenimientosViewModel(MantenimientoDTO mantenimientoDTO)
        {
            _mantenimiento = mantenimientoDTO;
        }

        public void cargaComboMatriculas()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
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
            }));

            t.Start();
        }

        public void cargaCombo()
        {
            if (null == _listaTipoMantenimiento)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    ServerServiceTipoMantenimiento serverServiceTipoMantenimiento = new ServerServiceTipoMantenimiento();
                    ServerResponseTipoMantenimiento serverResponseTipoMantenimiento = serverServiceTipoMantenimiento.GetAll();

                    if (200 == serverResponseTipoMantenimiento.error.code)
                    {
                        _listaTipoMantenimiento = serverResponseTipoMantenimiento.listaTipoMantenimiento;

                        foreach (var item in serverResponseTipoMantenimiento.listaTipoMantenimiento)
                        {
                            observableCollectionTipoMantenimiento.Add(item.nombre);
                        }
                    }
                }));

                t.Start();
            }
            else
            {
                foreach (var item in _listaTipoMantenimiento)
                {
                    observableCollectionTipoMantenimiento.Add(item.nombre);
                }
            }
        }

        public List<VehiculoDTO> ListaVehiculos
        {
            get
            {
                return _listaVehiculos;
            }
        }

        public List<TipoMantenimientoDTO> ListaTipoMantenimiento
        {
            get
            {
                return _listaTipoMantenimiento;
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
