using Flotix2021.Collection;
using Flotix2021.HelperClasses;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    class MantenimientosViewModel : BaseViewModel
    {
        private static bool _panelLoading;
        private string _panelMainMessage = "Cargando la información necesaria para mostrar los mantenimientos.";
        private string _panelSubMessage = "Por favor, espere...";

        public ObservableCollection<string> observableCollectionTipoMantenimiento = new AsyncObservableCollection<string>();
        public static List<TipoMantenimientoDTO> _listaTipoMantenimiento = null;

        public MantenimientosViewModel()
        {
            
        }

        public void cargaCombo()
        {
            if (null == _listaTipoMantenimiento)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    observableCollectionTipoMantenimiento.Add("Seleccionar");

                    ServerServiceTipoMantenimiento serverServiceTipoMantenimiento = new ServerServiceTipoMantenimiento();
                    ServerResponseTipoMantenimiento serverResponseTipoMantenimiento = serverServiceTipoMantenimiento.GetAll();

                    if (MessageExceptions.OK_CODE == serverResponseTipoMantenimiento.error.code)
                    {
                        _listaTipoMantenimiento = serverResponseTipoMantenimiento.listaTipoMantenimiento;

                        if (null != serverResponseTipoMantenimiento.listaTipoMantenimiento)
                        {
                            foreach (var item in serverResponseTipoMantenimiento.listaTipoMantenimiento)
                            {
                                observableCollectionTipoMantenimiento.Add(item.nombre);
                            }
                        }
                    }
                    else
                    {
                        observableCollectionTipoMantenimiento.Add("Seleccionar");
                    }
                }));

                t.Start();
            }
            else
            {
                observableCollectionTipoMantenimiento.Add("Seleccionar");

                foreach (var item in _listaTipoMantenimiento)
                {
                    observableCollectionTipoMantenimiento.Add(item.nombre);
                }
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
        /// Gets or sets the panel main message.
        /// </summary>
        /// <value>The panel main message.</value>
        public string PanelMainMessage
        {
            get
            {
                return _panelMainMessage;
            }
            set
            {
                _panelMainMessage = value;
                OnPropertyChanged("PanelMainMessage");
            }
        }

        /// <summary>
        /// Gets or sets the panel sub message.
        /// </summary>
        /// <value>The panel sub message.</value>
        public string PanelSubMessage
        {
            get
            {
                return _panelSubMessage;
            }
            set
            {
                _panelSubMessage = value;
                OnPropertyChanged("PanelSubMessage");
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