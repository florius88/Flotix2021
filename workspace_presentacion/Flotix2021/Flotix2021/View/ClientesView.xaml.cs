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
    /// Lógica de interacción para ClientesView.xaml
    /// </summary>
    public partial class ClientesView : UserControl
    {
        private ObservableCollection<ClienteDTO> observableCollectionCliente = new AsyncObservableCollection<ClienteDTO>();

        private ClientesViewModel clientesViewModel;

        public ClientesView()
        {
            InitializeComponent();

            clientesViewModel = (ClientesViewModel)this.DataContext;

            panel.IsEnabled = false;
            clientesViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                ServerResponseCliente serverResponseCliente = serverServiceCliente.GetAll();

                if (200 == serverResponseCliente.error.code)
                {
                    foreach (var item in serverResponseCliente.listaCliente)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionCliente.Add(item); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseCliente.error.message); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { clientesViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstCli.ItemsSource = observableCollectionCliente; }));
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
            UpdateViewCommand.viewModel.SelectedViewModel = new GestionClientesViewModel(null);
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            clientesViewModel.PanelLoading = true;

            string nif = "null";
            string cliente = "null";
            
            if (!txtNif.Text.Equals(""))
            {
                nif = txtNif.Text.ToString();
            }

            if (!txtCliente.Text.Equals(""))
            {
                cliente = txtCliente.Text.ToString();
            }

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                ServerResponseCliente serverResponseCliente = serverServiceCliente.GetAllFilter(nif, cliente);

                if (200 == serverResponseCliente.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionCliente.Clear(); }));

                    foreach (var item in serverResponseCliente.listaCliente)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionCliente.Add(item); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseCliente.error.message); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { clientesViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstCli.ItemsSource = observableCollectionCliente; }));
            }));

            t.Start();
        }

        private void listView_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                UpdateViewCommand.viewModel.SelectedViewModel = new GestionClientesViewModel(((ClienteDTO)item));
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
