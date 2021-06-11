using Flotix2021.Collection;
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
    /// Lógica de interacción para InicioView.xaml
    /// </summary>
    public partial class InicioView : UserControl
    {
        private ObservableCollection<AlertaDTO> observableCollectionAlerta = new AsyncObservableCollection<AlertaDTO>();

        private InicioViewModel inicioViewModel;

        public InicioView()
        {
            InitializeComponent();

            inicioViewModel = (InicioViewModel)this.DataContext;

            panel.IsEnabled = false;
            inicioViewModel.PanelLoading = true;

            cmbTipo.ItemsSource = inicioViewModel.observableCollectionTipoAlerta;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { inicioViewModel.cargaCombo(); }));

                ServerServiceAlerta serverServiceAlerta = new ServerServiceAlerta();
                ServerResponseAlerta serverResponseAlerta = serverServiceAlerta.GetAll();

                if (200 == serverResponseAlerta.error.code)
                {
                    foreach (var item in serverResponseAlerta.listaAlerta)
                    {
                        if (7 >= item.vencimiento)
                        {
                            item.urlImage = "/Images/ico_rojo.png";
                        } else
                        {
                            item.urlImage = "/Images/ico_amarillo.png";
                        }

                        Dispatcher.Invoke(new Action(() => { observableCollectionAlerta.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseAlerta.error.message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { inicioViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstAle.ItemsSource = observableCollectionAlerta; }));
            }));

            t.Start();
        }

        /**
        *------------------------------------------------------------------------------
        * Metodos para controlar los botones
        *------------------------------------------------------------------------------
        **/
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

            panel.IsEnabled = false;
            inicioViewModel.PanelLoading = true;

            Object selectedTipo = cmbTipo.SelectedItem;
            string tipo = "null";

            string cliente = "null";
            string matricula = "null";

            if (null != selectedTipo && 0 < cmbTipo.SelectedIndex)
            {
                tipo = selectedTipo.ToString();

                foreach (var item in inicioViewModel.ListaTipoAlerta)
                {
                    if (item.nombre.Equals(tipo))
                    {
                        tipo = item.id;
                    }
                }
            }
            
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
                ServerServiceAlerta serverServiceAlerta = new ServerServiceAlerta();
                ServerResponseAlerta serverResponseAlerta = serverServiceAlerta.GetAllFilter(tipo, cliente, matricula);

                if (200 == serverResponseAlerta.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionAlerta.Clear(); }));

                    foreach (var item in serverResponseAlerta.listaAlerta)
                    {
                        if (7 >= item.vencimiento)
                        {
                            item.urlImage = "/Images/ico_rojo.png";
                        }
                        else
                        {
                            item.urlImage = "/Images/ico_amarillo.png";
                        }

                        Dispatcher.Invoke(new Action(() => { observableCollectionAlerta.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseAlerta.error.message, "Alerta", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { inicioViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstAle.ItemsSource = observableCollectionAlerta; }));
            }));

            t.Start();
        }
    }
}
