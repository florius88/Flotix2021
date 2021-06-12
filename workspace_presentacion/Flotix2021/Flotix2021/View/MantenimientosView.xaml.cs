using Flotix2021.Collection;
using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para MantenimientosView.xaml
    /// </summary>
    public partial class MantenimientosView : UserControl
    {
        private ObservableCollection<MantenimientoDTO> observableCollectionMantenimiento = new AsyncObservableCollection<MantenimientoDTO>();
        private MantenimientosViewModel mantenimientosViewModel;

        public MantenimientosView()
        {
            InitializeComponent();

            mantenimientosViewModel = (MantenimientosViewModel)this.DataContext;

            panel.IsEnabled = false;
            mantenimientosViewModel.PanelLoading = true;

            cmbTipo.ItemsSource = mantenimientosViewModel.observableCollectionTipoMantenimiento;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { mantenimientosViewModel.cargaCombo(); }));

                ServerServiceMantenimiento serverServiceMantenimiento = new ServerServiceMantenimiento();
                ServerResponseMantenimiento serverResponseMantenimiento = serverServiceMantenimiento.GetAll();

                if (200 == serverResponseMantenimiento.error.code)
                {
                    foreach (var item in serverResponseMantenimiento.listaMantenimiento)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionMantenimiento.Add(item); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseMantenimiento.error.message); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { mantenimientosViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstMant.ItemsSource = observableCollectionMantenimiento; }));
            }));

            t.Start();
        }

        /**
        *------------------------------------------------------------------------------
        * Metodos para controlar los botones
        *------------------------------------------------------------------------------
        **/
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            UpdateViewCommand.viewModel.SelectedViewModel = new GestionMantenimientosViewModel(null);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            mantenimientosViewModel.PanelLoading = true;

            string matricula = "null";

            Object selectedTipo = cmbTipo.SelectedItem;
            string tipo = "null";

            if (!txtMatricula.Text.Equals(""))
            {
                matricula = txtMatricula.Text.ToString();
            }

            if (null != selectedTipo && 0 < cmbTipo.SelectedIndex)
            {
                tipo = selectedTipo.ToString();

                foreach (var item in mantenimientosViewModel.ListaTipoMantenimiento)
                {
                    if (item.nombre.Equals(tipo))
                    {
                        tipo = item.id;
                    }
                }
            }

            if (!txtMatricula.Text.Equals(""))
            {
                matricula = txtMatricula.Text.ToString();
            }

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceMantenimiento serverServiceMantenimiento = new ServerServiceMantenimiento();
                ServerResponseMantenimiento serverResponseMantenimiento = serverServiceMantenimiento.GetAllFilter(tipo, matricula);

                if (200 == serverResponseMantenimiento.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionMantenimiento.Clear(); }));

                    foreach (var item in serverResponseMantenimiento.listaMantenimiento)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionMantenimiento.Add(item); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseMantenimiento.error.message); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { mantenimientosViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstMant.ItemsSource = observableCollectionMantenimiento; }));
            }));

            t.Start();
        }

        private void listView_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                UpdateViewCommand.viewModel.SelectedViewModel = new GestionMantenimientosViewModel(((MantenimientoDTO)item));
            }
        }
        private void msgError(string msg)
        {
            var dialog = new CustomMessageBox
            {
                Caption = "Error",
                InstructionHeading = msg,
                InstructionText = "",
            };
            dialog.SetButtonsPredefined(EnumPredefinedButtons.Ok);
            dialog.ShowDialog();
        }
    }
}
