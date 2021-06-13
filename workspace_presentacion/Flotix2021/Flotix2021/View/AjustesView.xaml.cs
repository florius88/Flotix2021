using Flotix2021.Collection;
using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using Flotix2021.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para AjustesView.xaml
    /// </summary>
    public partial class AjustesView : UserControl
    {
        private ObservableCollection<UsuarioDTO> observableCollectionUsuario = new AsyncObservableCollection<UsuarioDTO>();

        private AjustesViewModel ajustesViewModel;
        private UsuarioDTO usuarioModif;
        private int modo;

        public AjustesView()
        {
            InitializeComponent();

            // Mostrar
            gridAjustes.Visibility = Visibility.Visible;

            // Ocultar
            gridGestionUsuario.Visibility = Visibility.Hidden;
            gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

            ajustesViewModel = (AjustesViewModel)this.DataContext;

            //Oculta la gestion de usuarios
            if ("ADMINISTRATIVO".Equals(MainViewModel.usuarioDTO.rol.nombre))
            {
                btnGestionUsuarios.Visibility = Visibility.Hidden;
            }
            else
            {
                cmbRol.ItemsSource = ajustesViewModel.observableCollectionRol;

                panel.IsEnabled = false;
                ajustesViewModel.PanelLoading = true;

                Thread t = new Thread(new ThreadStart(() =>
                {
                    Dispatcher.Invoke(new Action(() => { ajustesViewModel.cargaCombo(); }));

                    ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                    ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.GetAll();

                    if (200 == serverResponseUsuario.error.code)
                    {
                        foreach (var item in serverResponseUsuario.listaUsuario)
                        {
                            Dispatcher.Invoke(new Action(() => { observableCollectionUsuario.Add(item); }));
                        }
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => { msgError(serverResponseUsuario.error.message); }));
                    }

                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
                    Dispatcher.Invoke(new Action(() => { lstUsuarios.ItemsSource = observableCollectionUsuario; }));
                }));

                t.Start();
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                // BOTON AJUSTES
                case 0:
                    // Mostrar
                    gridAjustes.Visibility = Visibility.Visible;

                    // Ocultar
                    gridGestionUsuario.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    break;

                // BOTON ACEPTAR Cambiar Contrasenia
                case 1:
                    // Mostrar
                    gridAjustes.Visibility = Visibility.Visible;

                    // Ocultar
                    gridGestionUsuario.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    if (verificarPwd())
                    {
                        txtErrorCambio.Text = "";

                        panel.IsEnabled = false;
                        ajustesViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                            ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.ChangePwd(MainViewModel.usuarioDTO.nombre, txtPwdActual.Password, txtPwdNueva.Password, txtPwdNuevaRepetir.Password);

                            if (200 == serverResponseUsuario.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Contraseña", "Se ha actualizado la contraseña correctamente."); }));
                                Dispatcher.Invoke(new Action(() => { MainViewModel.usuarioDTO.pwd = txtPwdNueva.Password; }));
                                Dispatcher.Invoke(new Action(() => { limpiarPwd(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseUsuario.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
                        }));

                        t.Start();
                    }

                    break;

                // BOTON GESTION USUARIOS
                case 2:
                    // Mostrar
                    gridGestionUsuario.Visibility = Visibility.Visible;

                    // Ocultar
                    gridAjustes.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    //Limpia la informacion
                    limpiarInfUsuario();
                    break;

                // BOTON NUEVO USUARIO
                case 3:
                    limpiarInfUsuario();

                    // Mostrar
                    gridNuevoModificarUsuario.Visibility = Visibility.Visible;

                    btnModifAceptar.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;

                    // Ocultar
                    gridAjustes.Visibility = Visibility.Hidden;
                    gridGestionUsuario.Visibility = Visibility.Hidden;

                    btnEliminar.Visibility = Visibility.Hidden;

                    cmbRolNuevoModif.ItemsSource = ajustesViewModel.observableCollectionRol;

                    modo = Constantes.NUEVO;
                    usuarioModif = new UsuarioDTO();
                    break;

                // BOTON ELIMINAR USUARIO
                case 4:
                    var dialogEliminar = new CustomMessageBox
                    {
                        Caption = "Eliminar",
                        InstructionHeading = "¿Está seguro que quiere eliminar el usuario?",
                        InstructionText = "Esta acción será irreversible",
                    };
                    dialogEliminar.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                    var resultEliminar = dialogEliminar.ShowDialog();
                    if (resultEliminar.HasValue && resultEliminar.Value && dialogEliminar.CustomCustomDialogResult == EnumDialogResults.Button1)
                    {
                        //El usuario Administrador no puede cambiar su informacion
                        if (null != ajustesViewModel.usuario && MainViewModel.usuarioDTO.id.Equals(ajustesViewModel.usuario.id))
                        {
                            txtErrorNuevo.Text = "* No puede cambiar su usuario, contacte con el servicio técnico.";
                            btnCancelar.Focus();

                        } else
                        {
                            panel.IsEnabled = false;
                            ajustesViewModel.PanelLoading = true;

                            Thread t = new Thread(new ThreadStart(() =>
                            {
                                ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                                ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.Delete(ajustesViewModel.usuario.id);

                                if (200 == serverResponseUsuario.error.code)
                                {
                                    Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Eliminar", "Se ha eliminado el usuario correctamente."); }));
                                }
                                else
                                {
                                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseUsuario.error.message); }));
                                }

                                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                                Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }));

                            t.Start();
                        }
                    }
                    break;

                // BOTON MODIFICAR/ACEPTAR USUARIO
                case 5:
                    //modificar(null);
                    if (modo == Constantes.NUEVO)
                    {
                        var dialog = new CustomMessageBox
                        {
                            Caption = "Nuevo",
                            InstructionHeading = "¿Está seguro que quiere guardar el usuario?",
                            InstructionText = "Esta acción guardará la información del usuario",
                        };
                        dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                        var result = dialog.ShowDialog();
                        if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                        {
                            if (cambiosUsuario())
                            {
                                txtErrorNuevo.Text = "";

                                panel.IsEnabled = false;
                                ajustesViewModel.PanelLoading = true;

                                Thread t = new Thread(new ThreadStart(() =>
                                {
                                    ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                                    ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.Save(usuarioModif, "null");

                                    if (200 == serverResponseUsuario.error.code)
                                    {
                                        Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Nuevo", "Se ha guardado el usuario correctamente."); }));
                                        Dispatcher.Invoke(new Action(() => { ajustesViewModel.usuario = usuarioModif; }));
                                    }
                                    else
                                    {
                                        Dispatcher.Invoke(new Action(() => { msgError(serverResponseUsuario.error.message); }));
                                    }

                                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                                    Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
                                    Dispatcher.Invoke(new Action(() => { volver(); }));
                                }));

                                t.Start();
                            }
                        }
                    } 
                    else if (modo == Constantes.MODIFICA)
                    {
                        var dialog = new CustomMessageBox
                        {
                            Caption = "Modificar",
                            InstructionHeading = "¿Está seguro que quiere modificar el usuario?",
                            InstructionText = "Esta acción modificará la información del usuario",
                        };
                        dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                        var result = dialog.ShowDialog();
                        if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                        {
                            if (cambiosUsuario())
                            {
                                txtErrorNuevo.Text = "";

                                panel.IsEnabled = false;
                                ajustesViewModel.PanelLoading = true;

                                Thread t = new Thread(new ThreadStart(() =>
                                {
                                    ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                                    ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.Save(usuarioModif, usuarioModif.id);

                                    if (200 == serverResponseUsuario.error.code)
                                    {
                                        Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado el usuario correctamente."); }));
                                        Dispatcher.Invoke(new Action(() => { ajustesViewModel.usuario = usuarioModif; }));
                                    }
                                    else
                                    {
                                        Dispatcher.Invoke(new Action(() => { msgError(serverResponseUsuario.error.message); }));
                                    }

                                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                                    Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
                                    Dispatcher.Invoke(new Action(() => { volver(); }));
                                }));

                                t.Start();
                            }
                        }
                    }
                    break;

                // BOTON CANCELAR USUARIO
                case 6:
                    // Mostrar
                    gridGestionUsuario.Visibility = Visibility.Visible;

                    // Ocultar
                    gridAjustes.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    //Limpia la informacion
                    limpiarInfUsuario();
                    break;
            }
        }
        private void volver()
        {
            // Mostrar
            gridGestionUsuario.Visibility = Visibility.Visible;

            // Ocultar
            gridAjustes.Visibility = Visibility.Hidden;
            gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

            //Limpia la informacion
            limpiarInfUsuario();

            filtrar();
        }
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            filtrar();
        }

        private void filtrar()
        {
            panel.IsEnabled = false;
            ajustesViewModel.PanelLoading = true;

            string nombre = "null";

            if (!txtNombre.Text.Equals(""))
            {
                nombre = txtNombre.Text.ToString();
            }

            string email = "null";

            if (!txtEmail.Text.Equals(""))
            {
                email = txtEmail.Text.ToString();
            }

            Object selectedTipo = cmbRol.SelectedItem;
            string tipo = "null";

            if (null != selectedTipo && 0 < cmbRol.SelectedIndex)
            {
                tipo = selectedTipo.ToString();

                foreach (var item in ajustesViewModel.ListaRol)
                {
                    if (item.nombre.Equals(tipo))
                    {
                        tipo = item.id;
                    }
                }
            }

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.GetAllFilter(nombre, email, tipo);

                if (200 == serverResponseUsuario.error.code)
                {
                    //Limpiar la lista para recuperar la informacion de la busqueda
                    Dispatcher.Invoke(new Action(() => { observableCollectionUsuario.Clear(); }));

                    foreach (var item in serverResponseUsuario.listaUsuario)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionUsuario.Add(item); }));
                    }
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { msgError(serverResponseUsuario.error.message); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstUsuarios.ItemsSource = observableCollectionUsuario; }));
            }));

            t.Start();
        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                modificar(((UsuarioDTO)item));
            }
        }

        private void modificar(UsuarioDTO usuario)
        {
            // Mostrar
            gridNuevoModificarUsuario.Visibility = Visibility.Visible;

            btnEliminar.Visibility = Visibility.Visible;
            btnModifAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            // Ocultar
            gridAjustes.Visibility = Visibility.Hidden;
            gridGestionUsuario.Visibility = Visibility.Hidden;

            ajustesViewModel.usuario = usuario;

            cmbRolNuevoModif.ItemsSource = ajustesViewModel.observableCollectionRol;

            modo = Constantes.MODIFICA;
            cargarDatos(modo);
        }
        private bool verificarPwd()
        {
            bool correcto = true;

            if (txtPwdActual.Password.Equals(""))
            {
                txtErrorCambio.Text = "* Debe introducir la contraseña actual.";
                txtPwdActual.Focus();
                return false;
            }

            if (!MainViewModel.usuarioDTO.pwd.Equals(txtPwdActual.Password))
            {
                txtErrorCambio.Text = "* La contraseña actual no es correcta.";
                txtPwdActual.Focus();
                return false;
            }

            if (txtPwdNueva.Password.Equals(""))
            {
                txtErrorCambio.Text = "* Debe introducir la contraseña nueva.";
                txtPwdNueva.Focus();
                return false;
            }

            if (txtPwdNuevaRepetir.Password.Equals(""))
            {
                txtErrorCambio.Text = "* Debe introducir la confirmación de la contraseña nueva.";
                txtPwdNuevaRepetir.Focus();
                return false;
            }

            if (!txtPwdNueva.Password.Equals(txtPwdNuevaRepetir.Password))
            {
                txtErrorCambio.Text = "* Las nuevas contraseñas no coinciden.";
                txtPwdNueva.Focus();
                return false;
            }

            return correcto;
        }
        private bool cambiosUsuario()
        {
            bool sinError = true;

            //El usuario Administrador no puede cambiar su informacion
            if (null != ajustesViewModel.usuario && MainViewModel.usuarioDTO.id.Equals(ajustesViewModel.usuario.id))
            {
                txtErrorNuevo.Text = "* No puede cambiar su usuario, contacte con el servicio técnico.";
                btnCancelar.Focus();
                return false;
            }

            //Nombre
            string nombre = txtNombreNuevoModif.Text;
            if (null == nombre || 0 == nombre.Length)
            {
                txtErrorNuevo.Text = "* El campo Nombre no puede estar vacío.";
                txtNombreNuevoModif.Focus();
                return false;
            }
            else
            {
                usuarioModif.nombre = nombre;
            }

            //Email
            string email = txtEmailNuevoModif.Text;
            if (null == email || 0 == email.Length)
            {
                txtErrorNuevo.Text = "* El campo Email no puede estar vacío.";
                txtEmailNuevoModif.Focus();
                return false;
            }
            else
            {
                usuarioModif.email = email;
            }

            //Verifica el formato del email
            if (!Regex.IsMatch(txtEmailNuevoModif.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                txtErrorNuevo.Text = "* El campo Email no es válido. El formato es: XX@XX.XX";
                txtEmailNuevoModif.Focus();
                return false;
            }

            //Rol
            if ("Seleccionar".Equals(cmbRolNuevoModif.Text))
            {
                txtErrorNuevo.Text = "* El campo Rol tiene que estar seleccionado.";
                cmbRolNuevoModif.Focus();
                return false;
            }
            else
            {
                foreach (var item in ajustesViewModel.ListaRol)
                {
                    if (cmbRolNuevoModif.SelectedItem.ToString().Equals(item.nombre))
                    {
                        usuarioModif.idRol = item.id;
                        break;
                    }
                }
            }

            if (null == ajustesViewModel.usuario)
            {
                usuarioModif.pwd = nombre;
            }

            return sinError;
        }
        private void cargarDatos(int modo)
        {
            usuarioModif = ajustesViewModel.usuario;

            txtNombreNuevoModif.Text = ajustesViewModel.usuario.nombre;
            txtEmailNuevoModif.Text = ajustesViewModel.usuario.email;
            cmbRolNuevoModif.SelectedItem = ajustesViewModel.usuario.rol.nombre;
        }
        private void limpiarPwd()
        {
            txtPwdActual.Password = "";
            txtPwdNueva.Password = "";
            txtPwdNuevaRepetir.Password = "";
        }
        private void limpiarInfUsuario()
        {
            //Limpia la informacion
            ajustesViewModel.usuario = null;
            usuarioModif = null;

            txtNombreNuevoModif.Text = "";
            txtEmailNuevoModif.Text = "";
            cmbRolNuevoModif.SelectedIndex = 0;

            txtErrorNuevo.Text = "";
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
