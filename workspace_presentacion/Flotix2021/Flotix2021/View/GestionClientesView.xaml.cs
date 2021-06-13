using Flotix2021.Collection;
using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using Flotix2021.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionClientesView.xaml
    /// </summary>
    public partial class GestionClientesView : UserControl
    {
        private GestionClientesViewModel gestionClientesViewModel;

        private ClienteDTO clienteModif;
        private int modo;

        private ObservableCollection<string> observableCollectionMetodoPago = new AsyncObservableCollection<string>();
        private List<MetodoPagoDTO> listaMetodoPagoDTO = null;

        public GestionClientesView()
        {
            InitializeComponent();

            gestionClientesViewModel = (GestionClientesViewModel)this.DataContext;

            cmbMetodoPago.ItemsSource = observableCollectionMetodoPago;

            if (null == gestionClientesViewModel.cliente)
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
                observableCollectionMetodoPago.Clear();
                cargarDatos(modo);
            }
            else
            {
                gestionClientesViewModel.cliente = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new ClientesViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modo = Constantes.MODIFICA;
            ocultarMostrar(modo);
        }

        private void btnBaja_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CustomMessageBox
            {
                Caption = "Baja",
                InstructionHeading = "¿Está seguro que quiere dar de baja el cliente?",
                InstructionText = "Esta acción dará de baja toda la información asociada a dicho cliente",
            };
            dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
            {
                panel.IsEnabled = false;
                gestionClientesViewModel.PanelLoading = true;

                Thread t = new Thread(new ThreadStart(() =>
                {
                    ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                    ServerResponseCliente serverResponseCliente = serverServiceCliente.Delete(gestionClientesViewModel.cliente.id);

                    if (200 == serverResponseCliente.error.code)
                    {
                        Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Baja", "Se ha dado de baja el cliente correctamente."); }));
                        Dispatcher.Invoke(new Action(() => { modo = Constantes.BAJA; }));
                        Dispatcher.Invoke(new Action(() => { volver(); }));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => { msgError(serverResponseCliente.error.message); }));
                    }

                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { gestionClientesViewModel.PanelLoading = false; }));
                }));

                t.Start();
            }
        }

        private void btnAceptarClientes_Click(object sender, RoutedEventArgs e)
        {
            if (modo == Constantes.NUEVO)
            {
                var dialog = new CustomMessageBox
                {
                    Caption = "Nuevo",
                    InstructionHeading = "¿Está seguro que quiere guardar el cliente?",
                    InstructionText = "Esta acción guardará la información del cliente",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosCliente())
                    {
                        txtError.Text = "";

                        panel.IsEnabled = false;
                        gestionClientesViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                            ServerResponseCliente serverResponseCliente = serverServiceCliente.Save(clienteModif, "null");

                            if (200 == serverResponseCliente.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Nuevo", "Se ha guardado el cliente correctamente."); }));

                                Dispatcher.Invoke(new Action(() => { gestionClientesViewModel.cliente = clienteModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseCliente.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionClientesViewModel.PanelLoading = false; }));
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
                    InstructionHeading = "¿Está seguro que quiere modificar el cliente?",
                    InstructionText = "Esta acción modificará la información del cliente",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosCliente())
                    {
                        txtError.Text = "";

                        panel.IsEnabled = false;
                        gestionClientesViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceCliente serverServiceCliente = new ServerServiceCliente();
                            ServerResponseCliente serverResponseCliente = serverServiceCliente.Save(clienteModif, clienteModif.id);

                            if (200 == serverResponseCliente.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado el cliente correctamente."); }));

                                Dispatcher.Invoke(new Action(() => { gestionClientesViewModel.cliente = clienteModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseCliente.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionClientesViewModel.PanelLoading = false; }));
                        }));

                        t.Start();
                    }
                }
            }
        }
        private void cargarDatos(int modo)
        {
            clienteModif = gestionClientesViewModel.cliente;

            txtNif.Text = gestionClientesViewModel.cliente.nif;

            txtNombreCliente.Text = gestionClientesViewModel.cliente.nombre;

            txtDireccion.Text = gestionClientesViewModel.cliente.direccion;

            txtPoblacion.Text = gestionClientesViewModel.cliente.poblacion;

            txtPersonaContacto.Text = gestionClientesViewModel.cliente.personaContacto;

            txtTlfContacto.Text = gestionClientesViewModel.cliente.tlfContacto;

            txtemailContacto.Text = gestionClientesViewModel.cliente.email;

            observableCollectionMetodoPago.Add(gestionClientesViewModel.cliente.metodoPago.nombre);

            txtCuentaBancaria.Text = gestionClientesViewModel.cliente.cuentaBancaria;

            ocultarMostrar(modo);
        }
        private bool cambiosCliente()
        {
            bool sinError = true;

            //NIF/NIE
            string nif = txtNif.Text;
            if (null == nif || 0 == nif.Length)
            {
                txtError.Text = "* El campo NIF/NIE no puede estar vacío.";
                txtNif.Focus();
                return false;
            }
            else
            {
                clienteModif.nif = nif;
            }

            //Validar NIF
            try
            {
                if (!NumeroNif.CompruebaNif(txtNif.Text).EsCorrecto)
                {
                    txtError.Text = "* El campo NIF/NIE no es correcto.";
                    txtNif.Focus();
                    return false;
                }
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo NIF/NIE no es correcto.";
                txtNif.Focus();
                return false;
            }

            //Nombre de la Empresa / Cliente
            string nombre = txtNombreCliente.Text;
            if (null == nombre || 0 == nombre.Length)
            {
                txtError.Text = "* El campo Nombre de la Empresa / Cliente no puede estar vacío.";
                txtNombreCliente.Focus();
                return false;
            }
            else
            {
                clienteModif.nombre = nombre;
            }

            //Direccion
            string direccion = txtDireccion.Text;
            if (null == direccion || 0 == direccion.Length)
            {
                txtError.Text = "* El campo Dirección no puede estar vacío.";
                txtDireccion.Focus();
                return false;
            }
            else
            {
                clienteModif.direccion = direccion;
            }

            //Poblacion
            string poblacion = txtPoblacion.Text;
            if (null == poblacion || 0 == poblacion.Length)
            {
                txtError.Text = "* El campo Población no puede estar vacío.";
                txtPoblacion.Focus();
                return false;
            }
            else
            {
                clienteModif.poblacion = poblacion;
            }

            //Persona de contacto
            string pContacto = txtPersonaContacto.Text;
            if (null == pContacto || 0 == pContacto.Length)
            {
                txtError.Text = "* El campo Persona de contacto no puede estar vacío.";
                txtPersonaContacto.Focus();
                return false;
            }
            else
            {
                clienteModif.personaContacto = pContacto;
            }

            //Telefono de contacto
            string tlfContacto = txtTlfContacto.Text;
            if (null == tlfContacto || 0 == tlfContacto.Length)
            {
                txtError.Text = "* El campo Teléfono de contacto no puede estar vacío.";
                txtTlfContacto.Focus();
                return false;
            }
            else
            {
                clienteModif.tlfContacto = tlfContacto;
            }

            //Verifica el formato del Telefono
            if (!Regex.IsMatch(txtTlfContacto.Text, @"\A[0-9]{7,10}\z"))
            {
                txtError.Text = "* El campo Teléfono no es válido.";
                txtTlfContacto.Focus();
                return false;
            }

            //Email de contacto
            string emailContacto = txtemailContacto.Text;
            if (null == emailContacto || 0 == emailContacto.Length)
            {
                txtError.Text = "* El campo Email de contacto no puede estar vacío.";
                txtemailContacto.Focus();
                return false;
            }
            else
            {
                clienteModif.email= emailContacto;
            }

            //Verifica el formato del email
            if (!Regex.IsMatch(txtemailContacto.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtError.Text = "* El campo Email no es válido. El formato es: XX@XX.XX";
                txtemailContacto.Focus();
                return false;
            }

            //Cuenta Bancaria
            string cuenta = txtCuentaBancaria.Text;
            if (null == cuenta || 0 == cuenta.Length)
            {
                txtError.Text = "* El campo Cuenta Bancaria no puede estar vacío.";
                txtCuentaBancaria.Focus();
                return false;
            }
            else
            {
                clienteModif.cuentaBancaria = cuenta;
            }

            //Validar Cuenta Bancaria
            try
            {
                if (!CuentasBancarias.ValidaCuentaBancaria(txtCuentaBancaria.Text))
                {
                    txtError.Text = "* El campo Cuenta Bancaria no tiene un formato correcto. El formato es: XXXXXXXXXXXXXXXXXXXX";
                    txtCuentaBancaria.Focus();
                    return false;
                }
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Cuenta Bancaria no tiene un formato correcto. El formato es: XXXXXXXXXXXXXXXXXXXX";
                txtNif.Focus();
                return false;
            }

            //Vehiculo
            foreach (var item in listaMetodoPagoDTO)
            {
                if (cmbMetodoPago.SelectedItem.ToString().Equals(item.nombre))
                {
                    clienteModif.idMetodoPago = item.id;
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
                    clienteModif = new ClienteDTO();
                    cargarCombo(null);

                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    btnBaja.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnAceptarClientes.Visibility = Visibility.Visible;
                    btnCancelarAlquileres.Visibility = Visibility.Visible;

                    //Habilitar
                    txtNif.IsEnabled = true;
                    txtNombreCliente.IsEnabled = true;
                    txtDireccion.IsEnabled = true;
                    txtPoblacion.IsEnabled = true;
                    txtPersonaContacto.IsEnabled = true;
                    txtTlfContacto.IsEnabled = true;
                    txtemailContacto.IsEnabled = true;
                    cmbMetodoPago.IsEnabled = true;
                    txtCuentaBancaria.IsEnabled = true;

                    break;

                case 2:
                    //Ocultar
                    btnAceptarClientes.Visibility = Visibility.Hidden;
                    btnCancelarAlquileres.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnModificar.Visibility = Visibility.Visible;
                    btnBaja.Visibility = Visibility.Visible;

                    //Deshabilitar
                    txtNif.IsEnabled = false;
                    txtNombreCliente.IsEnabled = false;
                    txtDireccion.IsEnabled = false;
                    txtPoblacion.IsEnabled = false;
                    txtPersonaContacto.IsEnabled = false;
                    txtTlfContacto.IsEnabled = false;
                    txtemailContacto.IsEnabled = false;
                    cmbMetodoPago.IsEnabled = false;
                    txtCuentaBancaria.IsEnabled = false;

                    cmbMetodoPago.SelectedIndex = 0;
                    break;

                case 3:
                    observableCollectionMetodoPago.Clear();
                    cargarCombo(gestionClientesViewModel.cliente.metodoPago.nombre);
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    
                    //Mostrar
                    btnAceptarClientes.Visibility = Visibility.Visible;
                    btnCancelarAlquileres.Visibility = Visibility.Visible;
                    btnBaja.Visibility = Visibility.Visible;

                    //Habilitar
                    txtNombreCliente.IsEnabled = true;
                    txtDireccion.IsEnabled = true;
                    txtPoblacion.IsEnabled = true;
                    txtPersonaContacto.IsEnabled = true;
                    txtTlfContacto.IsEnabled = true;
                    txtemailContacto.IsEnabled = true;
                    cmbMetodoPago.IsEnabled = true;
                    txtCuentaBancaria.IsEnabled = true;

                    //Deshabilitar
                    txtNif.IsEnabled = false;
                    break;
            }
        }
        private void cargarCombo(String metodoPago)
        {
            panel.IsEnabled = false;
            gestionClientesViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceMetodoPago serverServiceMetodoPago = new ServerServiceMetodoPago();
                ServerResponseMetodoPago serverResponseTipoMantenimiento = serverServiceMetodoPago.GetAll();

                if (200 == serverResponseTipoMantenimiento.error.code)
                {
                    Dispatcher.Invoke(new Action(() => { listaMetodoPagoDTO = serverResponseTipoMantenimiento.listaMetodoPago; }));

                    foreach (var item in serverResponseTipoMantenimiento.listaMetodoPago)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionMetodoPago.Add(item.nombre); }));
                    }
                }
                if (null != metodoPago)
                {
                    Dispatcher.Invoke(new Action(() => { cmbMetodoPago.SelectedItem = metodoPago; }));
                } 
                else
                {
                    Dispatcher.Invoke(new Action(() => { cmbMetodoPago.SelectedIndex = 0; }));
                }
                
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionClientesViewModel.PanelLoading = false; }));
            }));

            t.Start();
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
