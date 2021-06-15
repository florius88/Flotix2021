using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using Flotix2021.ViewModel;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionCaducidadesView.xaml
    /// </summary>
    public partial class GestionCaducidadesView : UserControl
    {
        private GestionCaducidadesViewModel gestionCaducidadesViewModel;

        private CaducidadDTO caducidaModif;
        private int modo;

        public GestionCaducidadesView()
        {
            InitializeComponent();

            gestionCaducidadesViewModel = (GestionCaducidadesViewModel)this.DataContext;

            modo = Constantes.CONSULTA;
            cargarDatos(modo);
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
                gestionCaducidadesViewModel.caducidad = null;
                gestionCaducidadesViewModel.imagenVehiculo = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new CaducidadesViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modo = Constantes.MODIFICA;
            ocultarMostrar(modo);
        }

        private void btnAceptarCaducidades_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CustomMessageBox
            {
                Caption = "Modificar",
                InstructionHeading = "¿Está seguro que quiere modificar la caducidad?",
                InstructionText = "Esta acción modificará la información de la caducidad",
            };
            dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
            {
                if (cambiosCaducidad())
                {
                    txtError.Text = "";

                    //panel.IsEnabled = false;
                    //gestionCaducidadesViewModel.PanelLoading = true;

                    Thread t = new Thread(new ThreadStart(() =>
                    {
                        Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                        Dispatcher.Invoke(new Action(() => { gestionCaducidadesViewModel.PanelLoading = true; }));

                        ServerServiceCaducidad serverServiceCaducidad = new ServerServiceCaducidad();
                        ServerResponseCaducidad serverResponseCaducidad = serverServiceCaducidad.Save(caducidaModif, caducidaModif.id);

                        if (MessageExceptions.OK_CODE == serverResponseCaducidad.error.code)
                        {
                            Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado la caducidad correctamente."); }));
                            
                            Dispatcher.Invoke(new Action(() => { gestionCaducidadesViewModel.caducidad = caducidaModif; }));
                            Dispatcher.Invoke(new Action(() => { volver(); }));
                        }
                        else
                        {
                            Dispatcher.Invoke(new Action(() => { msgError(serverResponseCaducidad.error.message); }));
                        }

                        Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                        Dispatcher.Invoke(new Action(() => { gestionCaducidadesViewModel.PanelLoading = false; }));
                    }));

                    t.Start();
                }
            }
        }

        private void cargarDatos(int modo)
        {
            if (modo == Constantes.CONSULTA)
            {
                cargarFoto();
            }

            caducidaModif = gestionCaducidadesViewModel.caducidad;

            txtMatricula.Text = gestionCaducidadesViewModel.caducidad.vehiculo.matricula;

            DateTime dt = DateTime.ParseExact(gestionCaducidadesViewModel.caducidad.vehiculo.fechaMatriculacion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpFechaMatr.SelectedDate = dt;

            txtModelo.Text = gestionCaducidadesViewModel.caducidad.vehiculo.modelo;

            if (null != gestionCaducidadesViewModel.caducidad.ultimaITV && 0 < gestionCaducidadesViewModel.caducidad.ultimaITV.Length)
            {
                DateTime dtUlt = DateTime.ParseExact(gestionCaducidadesViewModel.caducidad.ultimaITV, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtpUltimaITV.SelectedDate = dtUlt;
            }

            if (null != gestionCaducidadesViewModel.caducidad.proximaITV && 0 < gestionCaducidadesViewModel.caducidad.proximaITV.Length)
            {
                DateTime dtProx = DateTime.ParseExact(gestionCaducidadesViewModel.caducidad.proximaITV, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtpProximaITV.SelectedDate = dtProx;
            }

            if (null != gestionCaducidadesViewModel.caducidad.vencimientoVehiculo && 0 < gestionCaducidadesViewModel.caducidad.vencimientoVehiculo.Length)
            {
                DateTime dtVen = DateTime.ParseExact(gestionCaducidadesViewModel.caducidad.vencimientoVehiculo, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dtpVencimientoSeguro.SelectedDate = dtVen;
            }

            ocultarMostrar(modo);
        }

        private void ocultarMostrar(int modo)
        {
            switch (modo)
            {
                case 2:
                    //Ocultar
                    btnAceptarCaducidades.Visibility = Visibility.Hidden;
                    btnCancelarCaducidades.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnModificar.Visibility = Visibility.Visible;

                    //Deshabilitar
                    btnCancelarCaducidades.IsEnabled = false;
                    dtpUltimaITV.IsEnabled = false;
                    dtpProximaITV.IsEnabled = false;
                    dtpVencimientoSeguro.IsEnabled = false;
                    break;
                case 3:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnAceptarCaducidades.Visibility = Visibility.Visible;
                    btnCancelarCaducidades.Visibility = Visibility.Visible;

                    //Habilitar
                    btnCancelarCaducidades.IsEnabled = true;
                    dtpUltimaITV.IsEnabled = true;
                    dtpProximaITV.IsEnabled = true;
                    dtpVencimientoSeguro.IsEnabled = true;
                    break;
            }
        }

        private bool cambiosCaducidad()
        {
            bool sinError = true;

            //Ultima ITV
            try
            {
                caducidaModif.ultimaITV = Convert.ToDateTime(dtpUltimaITV.Text).ToString("dd/MM/yyyy");
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Última ITV tiene que tener una fecha correcta.";
                dtpUltimaITV.Focus();
                return false;
            }

            //Proxima ITV
            try
            {
                caducidaModif.proximaITV = Convert.ToDateTime(dtpProximaITV.Text).ToString("dd/MM/yyyy");
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Próxima ITV tiene que tener una fecha correcta.";
                dtpProximaITV.Focus();
                return false;
            }

            //Comprobar que la fecha de la ultima revision no es mayor que la proxima
            try
            {
                int result = DateTime.Compare(Convert.ToDateTime(dtpUltimaITV.Text), Convert.ToDateTime(dtpProximaITV.Text));

                if (0 < result)
                {
                    txtError.Text = "* La fecha de la última revisión de la ITV, no puede ser mayor que la próxima.";
                    dtpUltimaITV.Focus();
                    return false;
                }
            }
            catch (System.Exception)
            {
                txtError.Text = "* La fecha de la última revisión de la ITV, no puede ser mayor que la próxima.";
                dtpUltimaITV.Focus();
                return false;
            }

            //Vencimiento Seguro
            try
            {
                caducidaModif.vencimientoVehiculo = Convert.ToDateTime(dtpVencimientoSeguro.Text).ToString("dd/MM/yyyy");
            }
            catch (System.Exception)
            {
                txtError.Text = "* El campo Vencimiento Seguro tiene que tener una fecha correcta.";
                dtpVencimientoSeguro.Focus();
                return false;
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
            //panel.IsEnabled = false;
            //gestionCaducidadesViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { gestionCaducidadesViewModel.PanelLoading = true; }));

                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.FindDocument(
                    gestionCaducidadesViewModel.caducidad.vehiculo.nombreImagen);

                if (MessageExceptions.OK_CODE == serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                {
                    Dispatcher.Invoke(new Action(() => { gestionCaducidadesViewModel.imagenVehiculo = serverResponseImagenVehiculo.imagenVehiculo; }));
                    Dispatcher.Invoke(new Action(() => { imgVehiculo.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(serverResponseImagenVehiculo.imagenVehiculo.documento); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionCaducidadesViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }
    }
}
