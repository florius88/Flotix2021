using Flotix2021.HelperClasses;
using Flotix2021.ModelDTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    class GestionMantenimientosViewModel : BaseViewModel
    {
        private static bool _panelLoading;
        private static MantenimientoDTO _mantenimiento;

        public MantenimientoDTO mantenimiento
        {
            get { return _mantenimiento; }
            set { _mantenimiento = value; }
        }

        public GestionMantenimientosViewModel()
        {

        }

        public GestionMantenimientosViewModel(MantenimientoDTO mantenimientoDTO)
        {
            _mantenimiento = mantenimientoDTO;
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
