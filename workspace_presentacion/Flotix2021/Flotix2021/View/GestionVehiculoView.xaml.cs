using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.ViewModel;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionVehiculoView.xaml
    /// </summary>
    public partial class GestionVehiculoView : UserControl
    {
        private GestionVehiculoViewModel gestionVehiculoViewModel;

        private VehiculoDTO vehiculoModif;
        private int modo;

        public GestionVehiculoView()
        {
            InitializeComponent();

            gestionVehiculoViewModel = (GestionVehiculoViewModel)this.DataContext;

            if (null == gestionVehiculoViewModel.vehiculo)
            {
                modo = 1;

                ocultarMostrar(modo);

                //titulo.Text = "Nuevo vehiculo";

                //txtMatricula.IsEnabled = true;

                //btnNuevoVehiculo.Visibility = Visibility.Hidden;
                //btnAceptarVehiculo.Visibility = Visibility.Visible;

            }
            else
            {
                modo = 2;
                cargarDatos(modo);
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            volver();
        }

        private void volver()
        {
            if (modo == 3)
            {
                modo = 2;
                cargarDatos(modo);
            }
            else
            {
                gestionVehiculoViewModel.vehiculo = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new VehiculosViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modo = 3;
            ocultarMostrar(modo);
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 1)
            {
                MessageBox.Show("Se ha guardado correctamente");
                UpdateViewCommand.viewModel.SelectedViewModel = new VehiculosViewModel();
            }
            else
            {
                var response = MessageBox.Show("¿Seguro que quiere modificar la información del vehiculo?", "Modificar...",
                                   MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                if (response == MessageBoxResult.Yes)
                {
                    if (cambiosVehiculo())
                    {
                        panel.IsEnabled = false;
                        gestionVehiculoViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                            ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.Save(vehiculoModif, vehiculoModif.id);

                            if (200 == serverResponseVehiculo.error.code)
                            {
                                //TODO MODIFICAR
                                MessageBox.Show("Se ha modificado el vehiculo correctamente");
                                //UpdateViewCommand.viewModel.SelectedViewModel = new VehiculosViewModel();
                                Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.vehiculo = vehiculoModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                MessageBox.Show(serverResponseVehiculo.error.message, "Vehiculo", MessageBoxButton.OK, MessageBoxImage.Error);
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.PanelLoading = false; }));
                        }));

                        t.Start();
                    }
                }
            }
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("¿Seguro que quiere dar de baja el vehiculo?", "Baja...",
                                   MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.Yes)
            {
                //TODO ELIMINAR
                MessageBox.Show("Se ha eliminado el vehiculo correctamente");
                UpdateViewCommand.viewModel.SelectedViewModel = new VehiculosViewModel();
            }
        }

        private void cargarDatos(int modo)
        {
            vehiculoModif = gestionVehiculoViewModel.vehiculo;

            string matricula = gestionVehiculoViewModel.vehiculo.matricula;

            txtTitulo.Text = matricula;
            txtMatricula.Text = matricula;

            DateTime dt = DateTime.ParseExact(gestionVehiculoViewModel.vehiculo.fechaMatriculacion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpFecha.SelectedDate = dt;

            txtModelo.Text = gestionVehiculoViewModel.vehiculo.modelo;

            txtKilometros.Text = gestionVehiculoViewModel.vehiculo.km.ToString();

            if (gestionVehiculoViewModel.vehiculo.disponibilidad)
            {
                txtDisponible.Text = "Disponible";
            } 
            else
            {
                txtDisponible.Text = "Alquilado";
            }

            Uri resourceUri = new Uri(gestionVehiculoViewModel.vehiculo.urlImage, UriKind.Relative);
            imgDisponible.Source = new BitmapImage(resourceUri);

            cmbPlazas.Text = gestionVehiculoViewModel.vehiculo.plazas.ToString();
            cmbTamanio.Text = gestionVehiculoViewModel.vehiculo.capacidad.ToString();

            //TODO IMG
            txtPermiso.Text = gestionVehiculoViewModel.vehiculo.nombreImagenPermiso;

            ocultarMostrar(modo);
        }

        private void ocultarMostrar(int modo)
        {
            switch (modo)
            {
                case 1:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    btnBaja.Visibility = Visibility.Hidden;

                    break;
                case 2:
                    //Ocultar
                    btnAceptarVehiculo.Visibility = Visibility.Hidden;
                    btnCancelarVehiculo.Visibility = Visibility.Hidden;
                    btnSeleccionarPC.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnModificar.Visibility = Visibility.Visible;

                    //Deshabilitar
                    txtMatricula.IsEnabled = false;
                    dtpFecha.IsEnabled = false;
                    txtModelo.IsEnabled = false;
                    cmbPlazas.IsEnabled = false;
                    cmbTamanio.IsEnabled = false;
                    txtKilometros.IsEnabled = false;
                    break;
                case 3:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnAceptarVehiculo.Visibility = Visibility.Visible;
                    btnCancelarVehiculo.Visibility = Visibility.Visible;
                    btnSeleccionarPC.Visibility = Visibility.Visible;

                    //Habilitar
                    txtMatricula.IsEnabled = true;
                    dtpFecha.IsEnabled = true;
                    txtModelo.IsEnabled = true;
                    cmbPlazas.IsEnabled = true;
                    cmbTamanio.IsEnabled = true;
                    txtKilometros.IsEnabled = true;
                    break;
            }
        }
        
        private bool cambiosVehiculo()
        {
            bool sinError = true;

            vehiculoModif.matricula = txtMatricula.Text;
            vehiculoModif.fechaMatriculacion = Convert.ToDateTime(dtpFecha.Text).ToString("dd/MM/yyyy");
            vehiculoModif.modelo = txtModelo.Text;

            if ("Seleccionar".Equals(cmbPlazas.Text))
            {
                //TODO ERRORES
                MessageBox.Show("Tiene que escoger un valor para el numero de plazas");
                sinError = false;
            } else
            {
                vehiculoModif.plazas = int.Parse(cmbPlazas.Text);
            }

            if ("Seleccionar".Equals(cmbTamanio.Text))
            {
                //TODO ERRORES
                MessageBox.Show("Tiene que escoger un valor para el tamaño");
                sinError = false;
            }
            else
            {
                vehiculoModif.capacidad = int.Parse(cmbTamanio.Text);
            }

            vehiculoModif.km = int.Parse(txtKilometros.Text);


            return sinError;
        }
    }
}
