using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ViewModel;
using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using Flotix2021.Utils;
using System.Threading;
using Flotix2021.Services;
using Flotix2021.ModelResponse;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionAlquileresView.xaml
    /// </summary>
    public partial class GestionAlquileresView : UserControl
    {
        private GestionAlquileresViewModel gestionAlquileresViewModel;

        private AlquilerDTO alquilerModif;
        private int modo;

        public GestionAlquileresView()
        {
            InitializeComponent();

            gestionAlquileresViewModel = (GestionAlquileresViewModel)this.DataContext;

            cmbCliente.ItemsSource = gestionAlquileresViewModel.observableCollectionClientes;
            cmbMatricula.ItemsSource = gestionAlquileresViewModel.observableCollectionMatriculas;

            if (null == gestionAlquileresViewModel.alquiler)
            {
                modo = Constantes.NUEVO;
                ocultarMostrar(modo);
            }
            else
            {
                modo = Constantes.CONSULTA;
                cargarDatos(modo);
            }

        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            volver();
        }

        private void volver()
        {
            if (modo == Constantes.MODIFICA)
            {
                modo = Constantes.CONSULTA;
                cargarDatos(modo);
            }
            else
            {
                gestionAlquileresViewModel.alquiler = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new AlquileresViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            panel.IsEnabled = false;
            gestionAlquileresViewModel.PanelLoading = true;

            modo = Constantes.MODIFICA;
            ocultarMostrar(modo);

            gestionAlquileresViewModel.observableCollectionClientes.Clear();
            gestionAlquileresViewModel.observableCollectionClientes.Add(gestionAlquileresViewModel.alquiler.cliente.nombre);
            cmbCliente.SelectedIndex = 0;

            gestionAlquileresViewModel.observableCollectionMatriculas.Clear();
            gestionAlquileresViewModel.observableCollectionMatriculas.Add(gestionAlquileresViewModel.alquiler.vehiculo.matricula);
            cmbMatricula.SelectedIndex = 0;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboClientes(); }));
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboMatriculas(); }));

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        private void btnVerCliente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerVehiculo_Click(object sender, RoutedEventArgs e)
        {
            VehiculoDTO vehiculoDTO = ((VehiculoDTO)gestionAlquileresViewModel.alquiler.vehiculo);
            vehiculoDTO.urlImage = "/Images/ico_rojo.png";
            LoginWindow.mainWindow.selectRb(1);
            UpdateViewCommand.viewModel.SelectedViewModel = new GestionVehiculoViewModel(vehiculoDTO);
        }

        private void btnAceptarAlquileres_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 1)
            {
                var dialog = new CustomMessageBox
                {
                    Caption = "Nuevo",
                    InstructionHeading = "¿Está seguro que quiere guardar el alquiler?",
                    InstructionText = "Esta acción guardará la información del alquiler",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosAlquiler())
                    {
                        //TODO REVISAR
                        alquilerModif.tipoImporte = "Pendiente";

                        txtError.Text = "";

                        panel.IsEnabled = false;
                        gestionAlquileresViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();
                            ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.Save(alquilerModif, "null");

                            if (200 == serverResponseAlquiler.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Nuevo", "Se ha guardado el alquiler correctamente."); }));

                                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.alquiler = alquilerModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseAlquiler.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
                        }));

                        t.Start();
                    }
                }
            }
            else
            {
                var dialog = new CustomMessageBox
                {
                    Caption = "Modificar",
                    InstructionHeading = "¿Está seguro que quiere modificar el alquiler?",
                    InstructionText = "Esta acción modificará la información del alquiler",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosAlquiler())
                    {
                        txtError.Text = "";

                        panel.IsEnabled = false;
                        gestionAlquileresViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();
                            ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.Save(alquilerModif, alquilerModif.id);

                            if (200 == serverResponseAlquiler.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado el vehiculo correctamente."); }));
                                
                                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.alquiler = alquilerModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseAlquiler.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
                        }));

                        t.Start();
                    }
                }
            }
        }

        private void cmbCliente_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != gestionAlquileresViewModel.ListaClientes &&
                gestionAlquileresViewModel.observableCollectionClientes.Count > 0)
            {
                foreach (var item in gestionAlquileresViewModel.ListaClientes)
                {
                    if (cmbCliente.SelectedItem.ToString().Equals(item.nombre))
                    {
                        txtNif.Text = item.nif;
                        break;
                    }
                }
            }
        }

        private void cmbMatricula_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != gestionAlquileresViewModel.ListaVehiculos && 
                gestionAlquileresViewModel.observableCollectionMatriculas.Count > 0)
            {
                foreach (var item in gestionAlquileresViewModel.ListaVehiculos)
                {
                    if (cmbMatricula.SelectedItem.ToString().Equals(item.matricula))
                    {
                        txtModelo.Text = item.modelo;
                        break;
                    }
                }
            }
        }

        private void cargarDatos(int modo)
        {
            alquilerModif = gestionAlquileresViewModel.alquiler;

            //cmbCliente.Text = gestionAlquileresViewModel.alquiler.cliente.nombre;
            gestionAlquileresViewModel.observableCollectionClientes.Add(gestionAlquileresViewModel.alquiler.cliente.nombre);
            cmbCliente.SelectedIndex = 0;

            txtNif.Text = gestionAlquileresViewModel.alquiler.cliente.nif;

            DateTime dt = DateTime.ParseExact(gestionAlquileresViewModel.alquiler.fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpInicioContrato.SelectedDate = dt;

            DateTime dt2 = DateTime.ParseExact(gestionAlquileresViewModel.alquiler.fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpFinContrato.SelectedDate = dt2;

            txtKilometros.Text = gestionAlquileresViewModel.alquiler.km.ToString();

            cmbTipoKm.Text = gestionAlquileresViewModel.alquiler.tipoKm;

            txtImporte.Text = gestionAlquileresViewModel.alquiler.importe.ToString();

            //cmbMatricula.Text = gestionAlquileresViewModel.alquiler.vehiculo.matricula;
            gestionAlquileresViewModel.observableCollectionMatriculas.Add(gestionAlquileresViewModel.alquiler.vehiculo.matricula);
            cmbMatricula.SelectedIndex = 0;

            txtModelo.Text = gestionAlquileresViewModel.alquiler.vehiculo.modelo;

            ocultarMostrar(modo);
        }

        private bool cambiosAlquiler()
        {
            bool sinError = true;

            //Cliente
            foreach (var item in gestionAlquileresViewModel.ListaClientes)
            {
                if (cmbCliente.SelectedItem.ToString().Equals(item.nombre))
                {
                    alquilerModif.idCliente = item.id;
                    break;
                }
            }

            //Inicio del Contrato
            try
            {
                alquilerModif.fechaInicio = Convert.ToDateTime(dtpInicioContrato.Text).ToString("dd/MM/yyyy");
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Inicio del Contrato tiene que tener una fecha correcta.";
                dtpInicioContrato.Focus();
                return false;
            }

            //Fin del Contrato
            try
            {
                alquilerModif.fechaFin = Convert.ToDateTime(dtpFinContrato.Text).ToString("dd/MM/yyyy");
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Fin del Contrato tiene que tener una fecha correcta.";
                dtpFinContrato.Focus();
                return false;
            }

            //Comprobar que la fecha de inicio del contrato no es mayor que la final
            try
            {
                int result = DateTime.Compare(Convert.ToDateTime(dtpInicioContrato.Text), Convert.ToDateTime(dtpFinContrato.Text));

                if (0 < result)
                {
                    txtError.Text = "* La fecha de inicio del contrato, no puede ser mayor que la final.";
                    dtpInicioContrato.Focus();
                    return false;
                }
            }
            catch (System.Exception)
            {
                txtError.Text = "* La fecha de inicio del contrato, no puede ser mayor que la final.";
                dtpInicioContrato.Focus();
                return false;
            }

            //Kilómetros
            try
            {
                alquilerModif.km = int.Parse(txtKilometros.Text);
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Kilómetros tiene que ser numérico y no estar vacío.";
                txtKilometros.Focus();
                return false;
            }

            //Tipo KM
            alquilerModif.tipoKm = cmbTipoKm.Text;

            //Importe
            try
            {
                alquilerModif.importe = double.Parse(txtImporte.Text);
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Importe tiene que ser numérico y no estar vacío.";
                txtImporte.Focus();
                return false;
            }

            //Vehiculo
            foreach (var item in gestionAlquileresViewModel.ListaVehiculos)
            {
                if (cmbMatricula.SelectedItem.ToString().Equals(item.matricula))
                {
                    alquilerModif.idVehiculo = item.id;
                    break;
                }
            }

            return sinError;
        }

        private void ocultarMostrar(int modo)
        {
            switch (modo)
            {
                case 1:
                    panel.IsEnabled = false;
                    gestionAlquileresViewModel.PanelLoading = true;

                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    btnVerCliente.Visibility = Visibility.Hidden;
                    btnVerVehiculo.Visibility = Visibility.Hidden;

                    //Por defecto
                    cmbTipoKm.SelectedIndex = 0;
                    alquilerModif = new AlquilerDTO();

                    gestionAlquileresViewModel.observableCollectionClientes.Clear();
                    gestionAlquileresViewModel.observableCollectionMatriculas.Clear();

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboClientes(); }));
                        Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboMatriculas(); }));

                        Dispatcher.Invoke(new Action(() => { cmbCliente.SelectedIndex = 0; }));
                        Dispatcher.Invoke(new Action(() => { cmbMatricula.SelectedIndex = 0; }));
                        
                        Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                        Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
                    }));

                    t.Start();

                    break;

                case 2:
                    //Ocultar
                    btnAceptarAlquileres.Visibility = Visibility.Hidden;
                    btnCancelarAlquileres.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnModificar.Visibility = Visibility.Visible;
                    btnVerCliente.Visibility = Visibility.Visible;
                    btnVerVehiculo.Visibility = Visibility.Visible;

                    //Deshabilitar
                    cmbCliente.IsEnabled = false;
                    dtpInicioContrato.IsEnabled = false;
                    dtpFinContrato.IsEnabled = false;
                    txtKilometros.IsEnabled = false;
                    txtImporte.IsEnabled = false;
                    cmbMatricula.IsEnabled = false;
                    cmbTipoKm.IsEnabled = false;
                    break;

                case 3:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    btnVerCliente.Visibility = Visibility.Hidden;
                    btnVerVehiculo.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnAceptarAlquileres.Visibility = Visibility.Visible;
                    btnCancelarAlquileres.Visibility = Visibility.Visible;

                    //Habilitar
                    cmbCliente.IsEnabled = true;
                    dtpInicioContrato.IsEnabled = true;
                    dtpFinContrato.IsEnabled = true;
                    txtKilometros.IsEnabled = true;
                    txtImporte.IsEnabled = true;
                    cmbMatricula.IsEnabled = true;
                    cmbTipoKm.IsEnabled = true;
                    break;
            }
        }

        private void mostrarAutoCloseMensaje(string titulo, string msg)
        {
            var dialog = new CustomMessageBox
            {
                Caption = titulo,
                InstructionHeading = msg,
                InstructionText = "",
                AutoCloseDialogTime = 3,
            };
            dialog.SetButtonsPredefined(EnumPredefinedButtons.No);
            dialog.ShowDialog();
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
