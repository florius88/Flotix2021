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
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionMantenimientosView.xaml
    /// </summary>
    public partial class GestionMantenimientosView : UserControl
    {
        private GestionMantenimientosViewModel gestionMantenimientosViewModel;

        private MantenimientoDTO mantenimientoModif;
        private int modo;

        private ObservableCollection<string> observableCollectionMatriculas = new AsyncObservableCollection<string>();
        private static List<VehiculoDTO> listaVehiculos = null;

        private ObservableCollection<string> observableCollectionTipoMantenimiento = new AsyncObservableCollection<string>();
        private static List<TipoMantenimientoDTO> listaTipoMantenimiento = null;

        public GestionMantenimientosView()
        {
            InitializeComponent();

            gestionMantenimientosViewModel = (GestionMantenimientosViewModel)this.DataContext;

            cmbMatricula.ItemsSource = observableCollectionMatriculas;
            cmbTipoManten.ItemsSource = observableCollectionTipoMantenimiento;

            if (null == gestionMantenimientosViewModel.mantenimiento)
            {
                modo = Constantes.NUEVO;

                //panel.IsEnabled = false;
                //gestionMantenimientosViewModel.PanelLoading = true;

                //Thread t = new Thread(new ThreadStart(() =>
                //{
                //    Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.cargaComboMatriculas(); }));
                //    Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.cargaCombo(); }));

                //    Dispatcher.Invoke(new Action(() => { ocultarMostrar(modo); }));

                //    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                //    Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = false; }));
                //}));

                //t.Start();

                cargaComboMatriculas();
                cargaCombo();
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
                gestionMantenimientosViewModel.mantenimiento = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new MantenimientosViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modo = Constantes.MODIFICA;
            ocultarMostrar(modo);
        }

        private void btnAceptarMantenim_Click(object sender, RoutedEventArgs e)
        {
            if (modo == Constantes.NUEVO)
            {
                var dialog = new CustomMessageBox
                {
                    Caption = "Nuevo",
                    InstructionHeading = "¿Está seguro que quiere guardar el mantenimiento?",
                    InstructionText = "Esta acción guardará la información del mantenimiento",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosMantenimiento())
                    {
                        txtError.Text = "";

                        //panel.IsEnabled = false;
                        //gestionMantenimientosViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                            Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = true; }));

                            ServerServiceMantenimiento serverServiceMantenimiento = new ServerServiceMantenimiento();
                            ServerResponseMantenimiento serverResponseMantenimiento = serverServiceMantenimiento.Save(mantenimientoModif, "null");

                            if (MessageExceptions.OK_CODE == serverResponseMantenimiento.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Nuevo", "Se ha guardado el mantenimiento correctamente."); }));

                                Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.mantenimiento = mantenimientoModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseMantenimiento.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = false; }));
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
                    InstructionHeading = "¿Está seguro que quiere modificar el mantenimiento?",
                    InstructionText = "Esta acción modificará la información del mantenimiento",
                };
                dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

                var result = dialog.ShowDialog();
                if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
                {
                    if (cambiosMantenimiento())
                    {
                        txtError.Text = "";

                        //panel.IsEnabled = false;
                        //gestionMantenimientosViewModel.PanelLoading = true;

                        Thread t = new Thread(new ThreadStart(() =>
                        {
                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                            Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = true; }));

                            ServerServiceMantenimiento serverServiceMantenimiento = new ServerServiceMantenimiento();
                            ServerResponseMantenimiento serverResponseMantenimiento = serverServiceMantenimiento.Save(mantenimientoModif, mantenimientoModif.id);

                            if (MessageExceptions.OK_CODE == serverResponseMantenimiento.error.code)
                            {
                                Dispatcher.Invoke(new Action(() => { mostrarAutoCloseMensaje("Modificar", "Se ha modificado el mantenimiento correctamente."); }));

                                Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.mantenimiento = mantenimientoModif; }));
                                Dispatcher.Invoke(new Action(() => { volver(); }));
                            }
                            else
                            {
                                Dispatcher.Invoke(new Action(() => { msgError(serverResponseMantenimiento.error.message); }));
                            }

                            Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                            Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = false; }));
                        }));

                        t.Start();
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
                        //Si tiene un mantenimiento creado, lo bloquea
                        if (null != item.mantenimientoNoCreado && 0 < item.mantenimientoNoCreado.Length)
                        {
                            cmbTipoManten.Text = item.mantenimientoNoCreado;
                            cmbTipoManten.IsEnabled = false;
                        } else
                        {
                            cmbTipoManten.IsEnabled = true;
                            cmbTipoManten.SelectedIndex = 0;
                        }

                        txtModelo.Text = item.modelo;
                        DateTime dt = DateTime.ParseExact(item.fechaMatriculacion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dtpFecha.SelectedDate = dt;
                        txtKilometrosActuales.Text = item.km.ToString();
                        cargarFoto(item.nombreImagen);
                        break;
                    }
                }
            }
        }
        private void cmbTipoManten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (modo != Constantes.CONSULTA)
            {
                if ("RUEDAS".Equals(cmbTipoManten.SelectedItem.ToString()))
                {
                    //Ocultar
                    gRevision.Visibility = Visibility.Hidden;

                    //Mostrar
                    gRuedas.Visibility = Visibility.Visible;

                    //Deshabilitar
                    dtpUltimoRuedas.IsEnabled = true;
                    dtpProximaRuedas.IsEnabled = true;
                    txtKilometrosRuedas.IsEnabled = true;
                }
                else
                {
                    //Ocultar
                    gRuedas.Visibility = Visibility.Hidden;

                    //Mostrar
                    gRevision.Visibility = Visibility.Visible;

                    //Deshabilitar
                    dtpUltimaRevision.IsEnabled = true;
                    dtpProximaRevision.IsEnabled = true;
                    txtKilometrosRevision.IsEnabled = true;
                }
            }
        }
        private void cargarDatos(int modo)
        {
            if (modo == Constantes.CONSULTA)
            {
                cargarFoto(gestionMantenimientosViewModel.mantenimiento.vehiculo.nombreImagen);
            }

            mantenimientoModif = gestionMantenimientosViewModel.mantenimiento;

            observableCollectionMatriculas.Add(gestionMantenimientosViewModel.mantenimiento.vehiculo.matricula);

            DateTime dt = DateTime.ParseExact(gestionMantenimientosViewModel.mantenimiento.vehiculo.fechaMatriculacion, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpFecha.SelectedDate = dt;

            txtModelo.Text = gestionMantenimientosViewModel.mantenimiento.vehiculo.modelo;

            if ("RUEDAS".Equals(gestionMantenimientosViewModel.mantenimiento.tipoMantenimiento.nombre))
            {
                dtpUltimoRuedas.Text = gestionMantenimientosViewModel.mantenimiento.ultimoMantenimiento;
                dtpProximaRuedas.Text = gestionMantenimientosViewModel.mantenimiento.proximoMantenimiento;
                txtKilometrosRuedas.Text = gestionMantenimientosViewModel.mantenimiento.kmMantenimiento.ToString();
            }
            else
            {
                dtpUltimaRevision.Text = gestionMantenimientosViewModel.mantenimiento.ultimoMantenimiento;
                dtpProximaRevision.Text = gestionMantenimientosViewModel.mantenimiento.proximoMantenimiento;
                txtKilometrosRevision.Text = gestionMantenimientosViewModel.mantenimiento.kmMantenimiento.ToString();
            }

            txtKilometrosActuales.Text = gestionMantenimientosViewModel.mantenimiento.vehiculo.km.ToString();

            observableCollectionTipoMantenimiento.Add(gestionMantenimientosViewModel.mantenimiento.tipoMantenimiento.nombre);

            ocultarMostrar(modo);
        }

        private void ocultarMostrar(int modo)
        {
            switch (modo)
            {
                case 1:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;

                    //Habilitar
                    cmbMatricula.IsEnabled = true;
                    cmbMatricula.SelectedIndex = 0;
                    cmbTipoManten.IsEnabled = true;
                    cmbTipoManten.SelectedItem = "RUEDAS";
                    mantenimientoModif = new MantenimientoDTO();

                    //Ocultar
                    gRevision.Visibility = Visibility.Hidden;

                    //Mostrar
                    gRuedas.Visibility = Visibility.Visible;

                    //Deshabilitar
                    dtpUltimoRuedas.IsEnabled = true;
                    dtpProximaRuedas.IsEnabled = true;
                    txtKilometrosRuedas.IsEnabled = true;

                    //Mostrar
                    btnAceptarMantenim.Visibility = Visibility.Visible;
                    btnCancelarMantenim.Visibility = Visibility.Visible;

                    break;

                case 2:

                    if ("RUEDAS".Equals(gestionMantenimientosViewModel.mantenimiento.tipoMantenimiento.nombre)){
                        //Ocultar
                        gRevision.Visibility = Visibility.Hidden;

                        //Mostrar
                        gRuedas.Visibility = Visibility.Visible;

                        //Deshabilitar
                        dtpUltimoRuedas.IsEnabled = false;
                        dtpProximaRuedas.IsEnabled = false;
                        txtKilometrosRuedas.IsEnabled = false;
                    }
                    else
                    {
                        //Ocultar
                        gRuedas.Visibility = Visibility.Hidden;

                        //Mostrar
                        gRevision.Visibility = Visibility.Visible;

                        //Deshabilitar
                        dtpUltimaRevision.IsEnabled = false;
                        dtpProximaRevision.IsEnabled = false;
                        txtKilometrosRevision.IsEnabled = false;
                    }

                    //Ocultar
                    btnAceptarMantenim.Visibility = Visibility.Hidden;
                    btnCancelarMantenim.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnModificar.Visibility = Visibility.Visible;

                    //Deshabilitar
                    cmbMatricula.IsEnabled = false;
                    cmbTipoManten.IsEnabled = false;

                    cmbMatricula.SelectedIndex = 0;
                    cmbTipoManten.SelectedIndex = 0;
                    break;
                case 3:

                    if ("RUEDAS".Equals(gestionMantenimientosViewModel.mantenimiento.tipoMantenimiento.nombre))
                    {
                        //Ocultar
                        gRevision.Visibility = Visibility.Hidden;

                        //Mostrar
                        gRuedas.Visibility = Visibility.Visible;

                        //Deshabilitar
                        dtpUltimoRuedas.IsEnabled = true;
                        dtpProximaRuedas.IsEnabled = true;
                        txtKilometrosRuedas.IsEnabled = true;
                    }
                    else
                    {
                        //Ocultar
                        gRuedas.Visibility = Visibility.Hidden;

                        //Mostrar
                        gRevision.Visibility = Visibility.Visible;

                        //Deshabilitar
                        dtpUltimaRevision.IsEnabled = true;
                        dtpProximaRevision.IsEnabled = true;
                        txtKilometrosRevision.IsEnabled = true;
                    }

                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;

                    //Mostrar
                    btnAceptarMantenim.Visibility = Visibility.Visible;
                    btnCancelarMantenim.Visibility = Visibility.Visible;

                    //Deshabilitar
                    cmbMatricula.IsEnabled = false;
                    cmbTipoManten.IsEnabled = false;

                    cmbMatricula.SelectedIndex = 0;
                    cmbTipoManten.SelectedIndex = 0;
                    break;
            }
        }
        private bool cambiosMantenimiento()
        {
            bool sinError = true;

            if (0 == cmbMatricula.Items.Count || 0 == cmbTipoManten.Items.Count)
            {
                txtError.Text = "* No se puede gestionar el mantenimiento sin no hay matriculas o tipo.";
                cmbMatricula.Focus();
                return false;
            }
            else if("RUEDAS".Equals(cmbTipoManten.SelectedItem.ToString()))
                //||  (null != gestionMantenimientosViewModel.mantenimiento && "RUEDAS".Equals(gestionMantenimientosViewModel.mantenimiento.tipoMantenimiento.nombre)))
            {
                //Ultimo Cambio Ruedas
                try
                {
                    mantenimientoModif.ultimoMantenimiento = Convert.ToDateTime(dtpUltimoRuedas.Text).ToString("dd/MM/yyyy");
                }
                catch (System.Exception)
                {
                    txtError.Text = "* El campo Último Cambio Ruedas tiene que tener una fecha correcta.";
                    dtpUltimoRuedas.Focus();
                    return false;
                }

                //Proximo Cambio Ruedas
                try
                {
                    mantenimientoModif.proximoMantenimiento = Convert.ToDateTime(dtpProximaRuedas.Text).ToString("dd/MM/yyyy");
                }
                catch (System.Exception)
                {
                    txtError.Text = "* El campo Próximo Cambio Ruedas tiene que tener una fecha correcta.";
                    dtpProximaRuedas.Focus();
                    return false;
                }

                //Comprobar que la fecha de inicio del contrato no es mayor que la final
                try
                {
                    int result = DateTime.Compare(Convert.ToDateTime(dtpUltimoRuedas.Text), Convert.ToDateTime(dtpProximaRuedas.Text));

                    if (0 < result)
                    {
                        txtError.Text = "* La fecha del último cambio de ruedas, no puede ser mayor que el próximo.";
                        dtpUltimoRuedas.Focus();
                        return false;
                    }
                }
                catch (System.Exception)
                {
                    txtError.Text = "* La fecha del último cambio de ruedas, no puede ser mayor que el próximo.";
                    dtpUltimoRuedas.Focus();
                    return false;
                }

                //Kms. Cambio Ruedas
                try
                {
                    mantenimientoModif.kmMantenimiento = int.Parse(txtKilometrosRuedas.Text);
                }
                catch (System.Exception)
                {
                    txtError.Text = "* El campo Kms. Cambio Ruedas tiene que ser numérico y no estar vacío.";
                    txtKilometrosRuedas.Focus();
                    return false;
                }

            } else
            {
                //Ultima Revision
                try
                {
                    mantenimientoModif.ultimoMantenimiento = Convert.ToDateTime(dtpUltimaRevision.Text).ToString("dd/MM/yyyy");
                }
                catch (System.Exception)
                {
                    txtError.Text = "* El campo Última Revisión tiene que tener una fecha correcta.";
                    dtpUltimaRevision.Focus();
                    return false;
                }

                //Proxima Revision
                try
                {
                    mantenimientoModif.proximoMantenimiento = Convert.ToDateTime(dtpProximaRevision.Text).ToString("dd/MM/yyyy");
                }
                catch (System.Exception)
                {
                    txtError.Text = "* El campo Próxima Revisión tiene que tener una fecha correcta.";
                    dtpProximaRevision.Focus();
                    return false;
                }

                //Comprobar que la fecha de inicio del contrato no es mayor que la final
                try
                {
                    int result = DateTime.Compare(Convert.ToDateTime(dtpUltimaRevision.Text), Convert.ToDateTime(dtpProximaRevision.Text));

                    if (0 < result)
                    {
                        txtError.Text = "* La fecha de la última revisión, no puede ser mayor que la próxima.";
                        dtpUltimoRuedas.Focus();
                        return false;
                    }
                }
                catch (System.Exception)
                {
                    txtError.Text = "* La fecha de la última revisión, no puede ser mayor que la próxima.";
                    dtpUltimoRuedas.Focus();
                    return false;
                }

                //Kms. Revision
                try
                {
                    mantenimientoModif.kmMantenimiento = int.Parse(txtKilometrosRevision.Text);
                }
                catch (System.Exception)
                {
                    txtError.Text = "* El campo Kms. Revisión tiene que ser numérico y no estar vacío.";
                    txtKilometrosRevision.Focus();
                    return false;
                }
            }

            //Vehiculo
            if (null != listaVehiculos)
            {
                foreach (var item in listaVehiculos)
                {
                    if (cmbMatricula.SelectedItem.ToString().Equals(item.matricula))
                    {
                        mantenimientoModif.idVehiculo = item.id;
                        mantenimientoModif.vehiculo = item;
                        break;
                    }
                }
            }
            
            //Tipo Mantenimiento
            if (null != listaTipoMantenimiento)
            {
                foreach (var item in listaTipoMantenimiento)
                {
                    if (cmbTipoManten.SelectedItem.ToString().Equals(item.nombre))
                    {
                        mantenimientoModif.idTipoMantenimiento = item.id;
                        mantenimientoModif.tipoMantenimiento = item;
                        break;
                    }
                }
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

        private void cargarFoto(string imagen)
        {
            //panel.IsEnabled = false;
            //gestionMantenimientosViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = true; }));

                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseImagenVehiculo serverResponseImagenVehiculo = serverServiceVehiculo.FindDocument(imagen);

                if (MessageExceptions.OK_CODE == serverResponseImagenVehiculo.error.code && null != serverResponseImagenVehiculo.imagenVehiculo)
                {
                    Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.imagenVehiculo = serverResponseImagenVehiculo.imagenVehiculo; }));
                    Dispatcher.Invoke(new Action(() => { imgVehiculo.Source = (BitmapSource)new ImageSourceConverter().ConvertFrom(serverResponseImagenVehiculo.imagenVehiculo.documento); }));
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        public void cargaComboMatriculas()
        {
            //panel.IsEnabled = false;
            //gestionMantenimientosViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = true; }));

                ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();
                ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAllNoMantenimiento();

                if (MessageExceptions.OK_CODE == serverResponseVehiculo.error.code)
                {
                    listaVehiculos = serverResponseVehiculo.listaVehiculo;

                    if (null != serverResponseVehiculo.listaVehiculo)
                    {
                        foreach (var item in serverResponseVehiculo.listaVehiculo)
                        {
                            observableCollectionMatriculas.Add(item.matricula);
                        }
                    }
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = false; }));
            }));

            t.Start();
        }

        public void cargaCombo()
        {
            if (null == listaTipoMantenimiento)
            {
                //panel.IsEnabled = false;
                //gestionMantenimientosViewModel.PanelLoading = true;

                Thread t = new Thread(new ThreadStart(() =>
                {
                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = false; }));
                    Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = true; }));

                    ServerServiceTipoMantenimiento serverServiceTipoMantenimiento = new ServerServiceTipoMantenimiento();
                    ServerResponseTipoMantenimiento serverResponseTipoMantenimiento = serverServiceTipoMantenimiento.GetAll();

                    if (MessageExceptions.OK_CODE == serverResponseTipoMantenimiento.error.code)
                    {
                        listaTipoMantenimiento = serverResponseTipoMantenimiento.listaTipoMantenimiento;

                        if (null != serverResponseTipoMantenimiento.listaTipoMantenimiento)
                        {
                            foreach (var item in serverResponseTipoMantenimiento.listaTipoMantenimiento)
                            {
                                observableCollectionTipoMantenimiento.Add(item.nombre);
                            }
                        }
                    }

                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { gestionMantenimientosViewModel.PanelLoading = false; }));
                }));

                t.Start();
            }
            else
            {
                foreach (var item in listaTipoMantenimiento)
                {
                    observableCollectionTipoMantenimiento.Add(item.nombre);
                }
            }
        }
    }
}
