using Flotix2021.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Flotix2021.Commands
{
    /// <summary>
    /// Gestion de cambios entre vistas
    /// </summary>
    class UpdateViewCommand : ICommand
    {
        public static MainViewModel viewModel;

        /// <summary>
        /// Inicializa la clase con la ventana InicioViewModel
        /// </summary>
        /// <param name="viewModel"></param>
        public UpdateViewCommand(MainViewModel viewModel)
        {
            UpdateViewCommand.viewModel = viewModel;
            //Carga inicial
            viewModel.SelectedViewModel = new InicioViewModel();
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Inicio")
            {
                viewModel.SelectedViewModel = new InicioViewModel();
            }
            else if (parameter.ToString() == "Vehiculos")
            {
                viewModel.SelectedViewModel = new VehiculosViewModel();
            }
            else if (parameter.ToString() == "Alquileres")
            {
                viewModel.SelectedViewModel = new AlquileresViewModel();
            }
            else if (parameter.ToString() == "Caducidades")
            {
                viewModel.SelectedViewModel = new CaducidadesViewModel();
            }
            else if (parameter.ToString() == "Mantenimientos")
            {
                viewModel.SelectedViewModel = new MantenimientosViewModel();
            }
            else if (parameter.ToString() == "Clientes")
            {
                viewModel.SelectedViewModel = new ClientesViewModel();
            }
            else if (parameter.ToString() == "Ajustes")
            {
                viewModel.SelectedViewModel = new AjustesViewModel();
            }
        }
    }
}