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
                        observableCollectionAlquiler.Add(item);
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

        private void lstAlq_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
