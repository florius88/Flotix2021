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
using System.Collections.ObjectModel;
using Flotix2021.Collection;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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

        private ObservableCollection<string> observableCollectionMatriculas = new AsyncObservableCollection<string>();
        private List<VehiculoDTO> listaVehiculos = null;

        private ObservableCollection<string> observableCollectionClientes = new AsyncObservableCollection<string>();
        private List<ClienteDTO> listaClientes = null;

        public GestionAlquileresView()
        {
            InitializeComponent();

            gestionAlquileresViewModel = (GestionAlquileresViewModel)this.DataContext;

            cmbCliente.ItemsSource = observableCollectionClientes;
            cmbMatricula.ItemsSource = observableCollectionMatriculas;

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
            txtError.Text = "";

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
            //panel.IsEnabled = false;
            //gestionAlquileresViewModel.PanelLoading = true;

            modo = Constantes.MODIFICA;
            ocultarMostrar(modo);

            observableCollectionClientes.Clear();
            observableCollectionClientes.Add(gestionAlquileresViewModel.alquiler.cliente.nombre);
            cmbCliente.SelectedIndex = 0;

            observableCollectionMatriculas.Clear();
            observableCollectionMatriculas.Add(gestionAlquileresViewModel.alquiler.vehiculo.matricula);
            cmbMatricula.SelectedIndex = 0;

            cargaComboClientes();
            cargaComboMatriculas();

            //Thread t = new Thread(new ThreadStart(() =>
            //{
            //    Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboClientes(); }));
            //    Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboMatriculas(); }));

            //    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
            //    Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
            //}));

            //t.Start();
        }

        private void btnVerCliente_Click(object sender, RoutedEventArgs e)
        {
            ClienteDTO clienteDTO = ((ClienteDTO)gestionAlquileresViewModel.alquiler.cliente);
            LoginWindow.mainWindow.selectRb(5);
            UpdateViewCommand.viewModel.SelectedViewModel = new GestionClientesViewModel(clienteDTO);
        }

        private void btnVerVehiculo_Click(object sender, RoutedEventArgs e)
        {
            VehiculoDTO vehiculoDTO = ((VehiculoDTO)gestionAlquileresViewModel.alquiler.vehiculo);

            if (vehiculoDTO.disponibilidad)
            {
                vehiculoDTO.urlImage = "/Images/ico_verde.png";
            }
            else
            {
                vehiculoDTO.urlImage = "/Images/ico_rojo.png";
            }

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

                        //panel.IsEnabled = false;
                        //gestionAlquileresViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                            Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = true; }));

                            ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();
                            ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.Save(alquilerModif, "null");

                            if (MessageExceptions.OK_CODE == serverResponseAlquiler.error.code)
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

                        //panel.IsEnabled = false;
                        //gestionAlquileresViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                            Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = true; }));

                            ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();
                            ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.Save(alquilerModif, alquilerModif.id);

                            if (MessageExceptions.OK_CODE == serverResponseAlquiler.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado el alquiler correctamente."); }));
                                
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
            if (null != listaClientes &&
                observableCollectionClientes.Count > 0)
            {
                foreach (var item in listaClientes)
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
            if (null != listaVehiculos && 
                observableCollectionMatriculas.Count > 0)
            {
                foreach (var item in listaVehiculos)
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

            observableCollectionClientes.Add(gestionAlquileresViewModel.alquiler.cliente.nombre);
            cmbCliente.SelectedIndex = 0;

            txtNif.Text = gestionAlquileresViewModel.alquiler.cliente.nif;

            DateTime dt = DateTime.ParseExact(gestionAlquileresViewModel.alquiler.fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpInicioContrato.SelectedDate = dt;

            DateTime dt2 = DateTime.ParseExact(gestionAlquileresViewModel.alquiler.fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpFinContrato.SelectedDate = dt2;

            txtKilometros.Text = gestionAlquileresViewModel.alquiler.km.ToString();

            cmbTipoKm.Text = gestionAlquileresViewModel.alquiler.tipoKm;

            txtImporte.Text = gestionAlquileresViewModel.alquiler.importe.ToString();

            observableCollectionMatriculas.Add(gestionAlquileresViewModel.alquiler.vehiculo.matricula);
            cmbMatricula.SelectedIndex = 0;

            txtModelo.Text = gestionAlquileresViewModel.alquiler.vehiculo.modelo;

            ocultarMostrar(modo);
        }

        private bool cambiosAlquiler()
        {
            bool sinError = true;

            if (null != listaClientes && 0 < cmbCliente.Items.Count && null != listaVehiculos && 0 < cmbMatricula.Items.Count)
            {
                //Cliente
                foreach (var item in listaClientes)
                {
                    if (cmbCliente.SelectedItem.ToString().Equals(item.nombre))
                    {
                        alquilerModif.idCliente = item.id;
                        alquilerModif.cliente = item;
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

                //Verifica el formato para el Importe
                if (!Regex.IsMatch(txtImporte.Text, @"^\-{0,1}\d+(,\d+){0,1}$"))
                {
                    txtError.Text = "* El campo Importe no es válido. El formato es: 0000,00";
                    txtImporte.Focus();
                    return false;
                }

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
                foreach (var item in listaVehiculos)
                {
                    if (cmbMatricula.SelectedItem.ToString().Equals(item.matricula))
                    {
                        alquilerModif.idVehiculo = item.id;
                        alquilerModif.vehiculo = item;
                        break;
                    }
                }
            }
            else
            {
                txtError.Text = "* No se puede gestionar el alquiler sin no hay clientes o vehiculos.";
                cmbCliente.Focus();
                return false;
            }

            return sinError;
        }

        private void ocultarMostrar(int modo)
        {
            switch (modo)
            {
                case 1:
                    //panel.IsEnabled = false;
                    //gestionAlquileresViewModel.PanelLoading = true;

                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    btnVerCliente.Visibility = Visibility.Hidden;
                    btnVerVehiculo.Visibility = Visibility.Hidden;

                    //Por defecto
                    cmbTipoKm.SelectedIndex = 0;
                    alquilerModif = new AlquilerDTO();

                    observableCollectionClientes.Clear();
                    observableCollectionMatriculas.Clear();

                    cargaComboClientes();
                    cargaComboMatriculas();

                    //Thread t = new Thread(new ThreadStart(() =>
                    //{
                    //    Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboClientes(); }));
                    //    Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.cargaComboMatriculas(); }));

                    //    Dispatcher.Invoke(new Action(() => { cmbCliente.SelectedIndex = 0; }));
                    //    Dispatcher.Invoke(new Action(() => { cmbMatricula.SelectedIndex = 0; }));
                        
                    //    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    //    Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
                    //}));

                    //t.Start();

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

        public void cargaComboMatriculas()
        {
            //panel.IsEnabled = false;
            //gestionAlquileresViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = true; }));

                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAllFilter("null", "null", "null", "true");

                if (MessageExceptions.OK_CODE == serverResponseVehiculo.error.code)
                {
                    if (null != serverResponseVehiculo.listaVehiculo)
                    {
                        Dispatcher.Invoke(new Action(() => { listaVehiculos = serverResponseVehiculo.listaVehiculo; }));

                        foreach (var item in serverResponseVehiculo.listaVehiculo)
                        {
                            Dispatcher.Invoke(new Action(() => { observableCollectionMatriculas.Add(item.matricula); }));
                        }

                        Dispatcher.Invoke(new Action(() => { cmbMatricula.SelectedIndex = 0; }));
                    }
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        public void cargaComboClientes()
        {
            //panel.IsEnabled = false;
            //gestionAlquileresViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = true; }));

                ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                ServerResponseCliente serverResponseCliente = serverServiceCliente.GetAll();

                if (MessageExceptions.OK_CODE == serverResponseCliente.error.code)
                {
                    if (null != serverResponseCliente.listaCliente)
                    {
                        Dispatcher.Invoke(new Action(() => { listaClientes = serverResponseCliente.listaCliente; }));

                        foreach (var item in serverResponseCliente.listaCliente)
                        {
                            if (null == gestionAlquileresViewModel.alquiler || !gestionAlquileresViewModel.alquiler.cliente.nif.Equals(item.nif))
                            {
                                Dispatcher.Invoke(new Action(() => { observableCollectionClientes.Add(item.nombre); }));
                            }
                        }

                        Dispatcher.Invoke(new Action(() => { cmbCliente.SelectedIndex = 0; }));
                    }
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionAlquileresViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }
    }
}
