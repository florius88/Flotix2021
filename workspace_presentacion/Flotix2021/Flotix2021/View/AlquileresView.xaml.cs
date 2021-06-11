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
    /// Lógica de interacción para AlquileresView.xaml
    /// </summary>
    public partial class AlquileresView : UserControl
    {
        private ObservableCollection<AlquilerDTO> observableCollectionAlquiler = new AsyncObservableCollection<AlquilerDTO>();

        private AlquileresViewModel alquileresViewModel;

        public AlquileresView()
        {
            InitializeComponent();

            alquileresViewModel = (AlquileresViewModel)this.DataContext;

            panel.IsEnabled = false;
            alquileresViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();
                ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.GetAll();

                if (200 == serverResponseAlquiler.error.code)
                {
                    foreach (var item in serverResponseAlquiler.listaAlquiler)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionAlquiler.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseAlquiler.error.message, "Alquiler", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { alquileresViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstAlq.ItemsSource = observableCollectionAlquiler; }));
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
            UpdateViewCommand.viewModel.SelectedViewModel = new GestionAlquileresViewModel(null);
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            alquileresViewModel.PanelLoading = true;

            string cliente = "null";
            string matricula = "null";

            if (!txtCliente.Text.Equals(""))
            {
                cliente = txtCliente.Text.ToString();
            }

            if (!txtMatricula.Text.Equals(""))
            {
                matricula = txtMatricula.Text.ToString();
            }

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();
                ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.GetAllFilter(cliente, matricula, "null");

                if (200 == serverResponseAlquiler.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionAlquiler.Clear(); }));

                    foreach (var item in serverResponseAlquiler.listaAlquiler)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionAlquiler.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseAlquiler.error.message, "Alquiler", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { alquileresViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        private void listView_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                UpdateViewCommand.viewModel.SelectedViewModel = new GestionAlquileresViewModel(((AlquilerDTO)item));
            }
        }
    }
}
