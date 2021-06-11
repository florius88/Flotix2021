﻿using Flotix2021.Collection;
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
                ServerServiceCliente serverServiceAlquiler = new ServerServiceCliente();
                ServerResponseCliente serverResponseAlquiler = serverServiceAlquiler.GetAll();

                if (200 == serverResponseAlquiler.error.code)
                {
                    foreach (var item in serverResponseAlquiler.listaCliente)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionCliente.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseAlquiler.error.message, "Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
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
                ServerServiceCliente serverServiceAlquiler = new ServerServiceCliente();
                ServerResponseCliente serverResponseAlquiler = serverServiceAlquiler.GetAllFilter(nif, cliente);

                if (200 == serverResponseAlquiler.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionCliente.Clear(); }));

                    foreach (var item in serverResponseAlquiler.listaCliente)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionCliente.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseAlquiler.error.message, "Cliente", MessageBoxButton.OK, MessageBoxImage.Error);
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
                MessageBox.Show(((ClienteDTO)item).id);
            }
        }
    }
}
