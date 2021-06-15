using Flotix2021.Collection;
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

            //panel.IsEnabled = false;
            //caducidadesViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { caducidadesViewModel.PanelLoading = true; }));

                ServerServiceCaducidad serverServiceCaducidad = new ServerServiceCaducidad();
                ServerResponseCaducidad serverResponseCaducidad = serverServiceCaducidad.GetAll();

                if (MessageExceptions.OK_CODE == serverResponseCaducidad.error.code)
                {
                    if (null != serverResponseCaducidad.listaCaducidad)
                    {
                        foreach (var item in serverResponseCaducidad.listaCaducidad)
                        {
                            Dispatcher.Invoke(new Action(() => { observableCollectionCaducidad.Add(item); }));
                        }
                    } else
                    {
                        Dispatcher.Invoke(new Action(() => { msgError("No hay información que cargar"); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseCaducidad.error.message); }));
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
            //panel.IsEnabled = false;
            //caducidadesViewModel.PanelLoading = true;

            string matricula = "null";

            if (!txtMatricula.Text.Equals(""))
            {
                matricula = txtMatricula.Text.ToString();
            }

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { caducidadesViewModel.PanelLoading = true; }));

                ServerServiceCaducidad serverServiceCaducidad = new ServerServiceCaducidad();
                ServerResponseCaducidad serverResponseCaducidad = serverServiceCaducidad.GetAllFilter(matricula);

                if (MessageExceptions.OK_CODE == serverResponseCaducidad.error.code)
                {
                    if (null != serverResponseCaducidad.listaCaducidad)
                    {
                        //Limpiar la lista para recuperar la informacion de la busqueda
                        Dispatcher.Invoke(new Action(() => { observableCollectionCaducidad.Clear(); }));

                        foreach (var item in serverResponseCaducidad.listaCaducidad)
                        {
                            Dispatcher.Invoke(new Action(() => { observableCollectionCaducidad.Add(item); }));
                        }
                    } else
                    {
                        Dispatcher.Invoke(new Action(() => { msgError("No hay información que cargar"); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseCaducidad.error.message); }));
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
                UpdateViewCommand.viewModel.SelectedViewModel = new GestionCaducidadesViewModel(((CaducidadDTO)item));
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
