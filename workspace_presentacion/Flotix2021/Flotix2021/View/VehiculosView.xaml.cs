using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using Flotix2021.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para VehiculosView.xaml
    /// </summary>
    public partial class VehiculosView : UserControl
    {
        private ObservableCollection<VehiculoDTO> observableCollectionVehiculo = new AsyncObservableCollection<VehiculoDTO>();

        private VehiculosViewModel vehiculosViewModel;

        public VehiculosView()
        {
            InitializeComponent();

            vehiculosViewModel = (VehiculosViewModel)this.DataContext;

            panel.IsEnabled = false;
            vehiculosViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAll();

                foreach (var item in serverResponseVehiculo.listaVehiculo)
                {
                    observableCollectionVehiculo.Add(item);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { vehiculosViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstVehic.ItemsSource = observableCollectionVehiculo; }));
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
            UpdateViewCommand.viewModel.SelectedViewModel = new GestionVehiculoViewModel(null);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            vehiculosViewModel.PanelLoading = true;

            string matricula = "null";

            Object selectedPlazas = cmbPlazas.SelectedItem;
            string plazas = "null";

            Object selectedTam = cmbTamanio.SelectedItem;
            string tam = "null";

            Object selectedDisp = cmbDisponibilidad.SelectedItem;
            string disp = "null";

            if (!txtMatricula.Text.Equals(""))
            {
                matricula = txtMatricula.Text.ToString();
            }

            if (null != selectedPlazas && 0 < cmbPlazas.SelectedIndex)
            {
                plazas = selectedPlazas.ToString();
            }

            if (null != selectedTam && 0 < cmbTamanio.SelectedIndex)
            {
                tam = selectedTam.ToString();
            }

            if (null != selectedDisp && 0 < cmbDisponibilidad.SelectedIndex)
            {
                if (Constantes.Disponibilidad.Si.Equals(selectedDisp))
                {
                    disp = "true";

                }
                else
                {
                    disp = "false";
                }
            }

            //Limpiar la lista para recuperar la informacion de la busqueda
            observableCollectionVehiculo.Clear();

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAllFilter(matricula, plazas, tam, disp);

                foreach (var item in serverResponseVehiculo.listaVehiculo)
                {
                    Dispatcher.Invoke(new Action(() => { observableCollectionVehiculo.Add(item); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { vehiculosViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                //MessageBox.Show(((VehiculoDTO)item).matricula);
                UpdateViewCommand.viewModel.SelectedViewModel = new GestionVehiculoViewModel(((VehiculoDTO)item));
            }
        }
    }
}
