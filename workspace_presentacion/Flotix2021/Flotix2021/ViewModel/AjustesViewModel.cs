
using Flotix2021.Collection;
using Flotix2021.Commands;
using Flotix2021.HelperClasses;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    class AjustesViewModel : BaseViewModel
    {
        private static bool _panelLoading;
        private string _panelMainMessage = "Cargando la información necesaria para mostrar los usuarios.";
        private string _panelSubMessage = "Por favor, espere...";

        public ObservableCollection<string> observableCollectionRol = new AsyncObservableCollection<string>();
        public static List<RolDTO> _listaRol = null;

        private static UsuarioDTO _usuario;

        public UsuarioDTO usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public AjustesViewModel()
        {

        }

        public void cargaCombo()
        {
            if (null == _listaRol)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    observableCollectionRol.Add("Seleccionar");

                    ServerServiceRol serverServiceRol = new ServerServiceRol();
                    ServerResponseRol serverResponseRol = serverServiceRol.GetAll();

                    if (MessageExceptions.OK_CODE == serverResponseRol.error.code)
                    {
                        _listaRol = serverResponseRol.listaRol;

                        if (null != serverResponseRol.listaRol)
                        {
                            foreach (var item in serverResponseRol.listaRol)
                            {
                                observableCollectionRol.Add(item.nombre);
                            }
                        }
                    }
                    else
                    {
                        observableCollectionRol.Add("Seleccionar");
                    }
                }));

                t.Start();
            }
            else
            {
                observableCollectionRol.Add("Seleccionar");

                foreach (var item in _listaRol)
                {
                    observableCollectionRol.Add(item.nombre);
                }
            }
        }

        public List<RolDTO> ListaRol
        {
            get
            {
                return _listaRol;
            }
        }

        public AjustesViewModel(UsuarioDTO usuarioDTO)
        {
            _usuario = usuarioDTO;
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
