using Flotix2021.Commands;
using Flotix2021.Model;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.ViewModel;
using Microsoft.Win32;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        private ImagenVehiculo imagenVehiculoModif;
        private ImagenVehiculo imagenPermisoVehiculoModif;
        private int modo;

        public GestionVehiculoView()
        {
            InitializeComponent();

            gestionVehiculoViewModel = (GestionVehiculoViewModel)this.DataContext;

            if (null == gestionVehiculoViewModel.vehiculo)
            {
                modo = 1;
                ocultarMostrar(modo);
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
                gestionVehiculoViewModel.imagenVehiculo = null;
                gestionVehiculoViewModel.imagenPermisoVehiculo = null;
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
                var dialog = new CustomMessageBox
                {
                    Caption = "Nuevo",
                    InstructionHeading = "¿Está seguro que quiere guardar el vehiculo?",
                    InstructionText = "Esta acción guardará la información del vehiculo",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosVehiculo())
                    {
                        txtError.Text = "";

                        panel.IsEnabled = false;
                        gestionVehiculoViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                            ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.Save(vehiculoModif, "null");

                            if (200 == serverResponseVehiculo.error.code)
                            {
                                string msgErrorImg = null;

                                if (null != imagenVehiculoModif)
                                {
                                    imagenVehiculoModif.nombreImagen = vehiculoModif.nombreImagen;
                                    ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.SaveDocument(imagenVehiculoModif);

                                    if (200 != serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                                    {
                                        msgErrorImg = serverResponseImagenVehiculo.error.message;
                                    }
                                }

                                if (null != imagenPermisoVehiculoModif)
                                {
                                    imagenPermisoVehiculoModif.nombreImagen = vehiculoModif.nombreImagenPermiso;
                                    ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.SaveDocument(imagenPermisoVehiculoModif);

                                    if (200 != serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                                    {
                                        msgErrorImg = serverResponseImagenVehiculo.error.message;
                                    }
                                }

                                if (null == msgErrorImg)
                                {
                                    Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Nuevo", "Se ha guardado el vehiculo correctamente."); }));
                                }
                                else
                                {
                                    Dispatcher.Invoke(new Action(() => { msgError(msgErrorImg); }));
                                }

                                Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.vehiculo = vehiculoModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseVehiculo.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.PanelLoading = false; }));
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
                    InstructionHeading = "¿Está seguro que quiere modificar el vehiculo?",
                    InstructionText = "Esta acción modificará la información del vehiculo",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosVehiculo())
                    {
                        txtError.Text = "";

                        panel.IsEnabled = false;
                        gestionVehiculoViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                            ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.Save(vehiculoModif, vehiculoModif.id);

                            if (200 == serverResponseVehiculo.error.code)
                            {
                                string msgErrorImg = null;

                                if (null != imagenVehiculoModif)
                                {
                                    imagenVehiculoModif.nombreImagen = vehiculoModif.nombreImagen;
                                    ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.SaveDocument(imagenVehiculoModif);

                                    if (200 != serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                                    {
                                        msgErrorImg = serverResponseImagenVehiculo.error.message;
                                    }
                                }

                                if (null != imagenPermisoVehiculoModif)
                                {
                                    imagenPermisoVehiculoModif.nombreImagen = vehiculoModif.nombreImagenPermiso;
                                    ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.SaveDocument(imagenPermisoVehiculoModif);

                                    if (200 != serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                                    {
                                        msgErrorImg = serverResponseImagenVehiculo.error.message;
                                    }
                                }

                                if (null == msgErrorImg)
                                {
                                    Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado el vehiculo correctamente."); }));
                                }
                                else
                                {
                                    Dispatcher.Invoke(new Action(() => { msgError(msgErrorImg); }));
                                }

                                Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.vehiculo = vehiculoModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseVehiculo.error.message); }));
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
            var dialog = new CustomMessageBox
            {
                Caption = "Baja",
                InstructionHeading = "¿Está seguro que quiere dar de baja el vehiculo?",
                InstructionText = "Esta acción dará de baja toda la información asociada a dicho vehiculo",
            };
            dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
            {
                panel.IsEnabled = false;
                gestionVehiculoViewModel.PanelLoading = true;

                Thread t = new Thread(new ThreadStart(() =>
                {
                    ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                    ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.Delete(gestionVehiculoViewModel.vehiculo.id);

                    if (200 == serverResponseVehiculo.error.code)
                    {
                        Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Baja", "Se ha dado de baja el vehiculo correctamente."); }));
                        Dispatcher.Invoke(new Action(() => { volver(); }));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => { msgError(serverResponseVehiculo.error.message); }));
                    }

                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.PanelLoading = false; }));
                }));

                t.Start();
            }
        }

        private void cargarDatos(int modo)
        {
            if (modo == 2)
            {
                if (null != gestionVehiculoViewModel.vehiculo.nombreImagenPermiso &&
                    0 != gestionVehiculoViewModel.vehiculo.nombreImagenPermiso.Length)
                {
                    txtPermiso.Text = gestionVehiculoViewModel.vehiculo.nombreImagenPermiso;
                    cargarFotoPermiso();
                }

                cargarFoto();
            }

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
                    panelDisp.Visibility = Visibility.Hidden;

                    //Por defecto
                    cmbPlazas.SelectedIndex = 0;
                    cmbTamanio.SelectedIndex = 0;
                    vehiculoModif = new VehiculoDTO();

                    break;
                case 2:
                    //Ocultar
                    btnAceptarVehiculo.Visibility = Visibility.Hidden;
                    btnCancelarVehiculo.Visibility = Visibility.Hidden;
                    btnSeleccionarPC.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnModificar.Visibility = Visibility.Visible;
                    panelDisp.Visibility = Visibility.Visible;

                    //Deshabilitar
                    txtMatricula.IsEnabled = false;
                    dtpFecha.IsEnabled = false;
                    txtModelo.IsEnabled = false;
                    cmbPlazas.IsEnabled = false;
                    cmbTamanio.IsEnabled = false;
                    txtKilometros.IsEnabled = false;
                    btnImgVehiculo.IsHitTestVisible = false;
                    break;
                case 3:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnAceptarVehiculo.Visibility = Visibility.Visible;
                    btnCancelarVehiculo.Visibility = Visibility.Visible;
                    btnSeleccionarPC.Visibility = Visibility.Visible;
                    panelDisp.Visibility = Visibility.Visible;

                    //Habilitar
                    txtMatricula.IsEnabled = true;
                    dtpFecha.IsEnabled = true;
                    txtModelo.IsEnabled = true;
                    cmbPlazas.IsEnabled = true;
                    cmbTamanio.IsEnabled = true;
                    txtKilometros.IsEnabled = true;
                    btnImgVehiculo.IsHitTestVisible = true;
                    break;
            }
        }

        private bool cambiosVehiculo()
        {
            bool sinError = true;

            //Matricula
            string matricula = txtMatricula.Text;
            if (null == matricula || 0 == matricula.Length)
            {
                txtError.Text = "* El campo Matricula no puede estar vacío.";
                txtMatricula.Focus();
                return false;
            }
            else
            {
                vehiculoModif.matricula = matricula;
            }

            //Fecha de matriculacion
            try
            {
                vehiculoModif.fechaMatriculacion = Convert.ToDateTime(dtpFecha.Text).ToString("dd/MM/yyyy");
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Fecha de Matriculación tiene que tener una fecha correcta.";
                dtpFecha.Focus();
                return false;
            }

            //Modelo
            string modelo = txtModelo.Text;
            if (null == modelo || 0 == modelo.Length)
            {
                txtError.Text = "* El campo Modelo no puede estar vacío.";
                txtModelo.Focus();
                return false;
            }
            else
            {
                vehiculoModif.modelo = modelo;
            }

            //Plazas
            if ("Seleccionar".Equals(cmbPlazas.Text))
            {
                txtError.Text = "* El campo Plazas tiene que estar seleccionado.";
                cmbPlazas.Focus();
                return false;
            }
            else
            {
                vehiculoModif.plazas = int.Parse(cmbPlazas.Text);
            }

            //Tamanio
            if ("Seleccionar".Equals(cmbTamanio.Text))
            {
                txtError.Text = "* El campo Tamaño tiene que estar seleccionado.";
                cmbTamanio.Focus();
                return false;
            }
            else
            {
                vehiculoModif.capacidad = int.Parse(cmbTamanio.Text);
            }

            //KM
            try
            {
                vehiculoModif.km = int.Parse(txtKilometros.Text);
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Kilómetros tiene que ser numérico y no estar vacío.";
                txtKilometros.Focus();
                return false;
            }

            //Imagen
            if (null != imagenVehiculoModif)
            {
                vehiculoModif.nombreImagen = vehiculoModif.matricula;
            }

            //Imagen
            if (null != imagenPermisoVehiculoModif)
            {
                vehiculoModif.nombreImagenPermiso = "Permiso-" + vehiculoModif.matricula;
            }

            return sinError;
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

        private void cargarFoto()
        {
            panel.IsEnabled = false;
            gestionVehiculoViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.FindDocument(
                    gestionVehiculoViewModel.vehiculo.nombreImagen);

                if (200 == serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                {
                    Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.imagenVehiculo = serverResponseImagenVehiculo.imagenVehiculo; }));
                    Dispatcher.Invoke(new Action(() => { imgVehiculo.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(serverResponseImagenVehiculo.imagenVehiculo.documento); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        private void cargarFotoPermiso()
        {
            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.FindDocument(
                    gestionVehiculoViewModel.vehiculo.nombreImagenPermiso);

                if (200 == serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                {
                    Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.imagenPermisoVehiculo = serverResponseImagenVehiculo.imagenVehiculo; }));
                }
                else
                {
                    Dispatcher.Invoke(new Action(() => { txtPermiso.Text = ""; }));
                }
            }));

            t.Start();
        }

        private void btnFoto_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccione una imagen";

            ofd.Filter = "Todas las imagenes|.jpg;.jpeg;*.png|" +
                "JPEG (jpg;.jpeg)|.jpg;.jpeg|" +
                "Portable Networl Graphic (.png)|.png";

            panel.IsEnabled = false;
            gestionVehiculoViewModel.PanelLoading = true;

            if (ofd.ShowDialog() == true)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    Dispatcher.Invoke(new Action(() => { bytes = File.ReadAllBytes(ofd.FileName); }));
                    Dispatcher.Invoke(new Action(() => { imagenVehiculoModif = new ImagenVehiculo(); }));
                    Dispatcher.Invoke(new Action(() => { imagenVehiculoModif.documento = bytes; }));

                    Dispatcher.Invoke(new Action(() => { imgVehiculo.Source = new BitmapImage(new Uri(ofd.FileName)); }));
                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.PanelLoading = false; }));
                }));

                t.Start();

            }
            else
            {
                panel.IsEnabled = true;
                gestionVehiculoViewModel.PanelLoading = false;
            }
        }

        private void btnPermisoFoto_Click(object sender, RoutedEventArgs e)
        {
            byte[] bytes = null;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Seleccione una imagen";

            ofd.Filter = "Todas las imagenes|.jpg;.jpeg;*.png|" +
                "JPEG (jpg;.jpeg)|.jpg;.jpeg|" +
                "Portable Networl Graphic (.png)|.png";

            panel.IsEnabled = false;
            gestionVehiculoViewModel.PanelLoading = true;

            if (ofd.ShowDialog() == true)
            {
                Thread t = new Thread(new ThreadStart(() =>
                {
                    Dispatcher.Invoke(new Action(() => { bytes = File.ReadAllBytes(ofd.FileName); }));
                    Dispatcher.Invoke(new Action(() => { imagenPermisoVehiculoModif = new ImagenVehiculo(); }));
                    Dispatcher.Invoke(new Action(() => { imagenPermisoVehiculoModif.documento = bytes; }));

                    if (null != gestionVehiculoViewModel.vehiculo)
                    {
                        Dispatcher.Invoke(new Action(() => { txtPermiso.Text = "Permiso-" + gestionVehiculoViewModel.vehiculo.matricula; }));
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => { txtPermiso.Text = "Permiso-" + txtMatricula.Text; }));
                    }

                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { gestionVehiculoViewModel.PanelLoading = false; }));
                }));

                t.Start();
            }
            else
            {
                panel.IsEnabled = true;
                gestionVehiculoViewModel.PanelLoading = false;
            }
        }

        private void BtnLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            if (null != imagenPermisoVehiculoModif)
            {
                var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(imagenPermisoVehiculoModif.documento);
                DialogImageView previewWindow = new DialogImageView(bitmap);
                previewWindow.ShowDialog();
            }
            else if (null != gestionVehiculoViewModel.imagenPermisoVehiculo)
            {
                var bitmap = (BitmapSource)new ImageSourceConverter().ConvertFrom(gestionVehiculoViewModel.imagenPermisoVehiculo.documento);
                DialogImageView previewWindow = new DialogImageView(bitmap);
                previewWindow.ShowDialog();
            }
        }
    }
}
