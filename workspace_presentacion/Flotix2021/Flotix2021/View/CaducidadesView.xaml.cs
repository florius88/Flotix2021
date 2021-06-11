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
    /// Lógica de interacción para CaducidadesView.xaml
    /// </summary>
    public partial class CaducidadesView : UserControl
    {
        private ObservableCollection<CaducidadDTO> observableCollectionCaducidad = new AsyncObservableCollection<CaducidadDTO>();

        private CaducidadesViewModel caducidadesViewModel;

        public CaducidadesView()
        {
            InitializeComponent();

            caducidadesViewModel = (CaducidadesViewModel)this.DataContext;

            panel.IsEnabled = false;
            caducidadesViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceCaducidad serverServiceCaducidad = new ServerServiceCaducidad();
                ServerResponseCaducidad serverResponseCaducidad = serverServiceCaducidad.GetAll();

                if (200 == serverResponseCaducidad.error.code)
                {
                    foreach (var item in serverResponseCaducidad.listaCaducidad)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionCaducidad.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseCaducidad.error.message, "Caducidad", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { caducidadesViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstCaduc.ItemsSource = observableCollectionCaducidad; }));
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
            caducidadesViewModel.PanelLoading = true;

            string matricula = "null";

            if (!txtMatricula.Text.Equals(""))
            {
                matricula = txtMatricula.Text.ToString();
            }

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceCaducidad serverServiceCaducidad = new ServerServiceCaducidad();
                ServerResponseCaducidad serverResponseCaducidad = serverServiceCaducidad.GetAllFilter(matricula);

                if (200 == serverResponseCaducidad.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionCaducidad.Clear(); }));

                    foreach (var item in serverResponseCaducidad.listaCaducidad)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionCaducidad.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseCaducidad.error.message, "Caducidad", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { caducidadesViewModel.PanelLoading = false; }));
            }));

            t.Start();

        }
        private void listView_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                MessageBox.Show(((CaducidadDTO)item).id);
            }
        }
    }
}
