using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ViewModel;
using System;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;


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

            if (null == gestionAlquileresViewModel.alquiler)
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
                gestionAlquileresViewModel.alquiler = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new AlquileresViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modo = 3;
            ocultarMostrar(modo);
        }

        private void btnVerCliente_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnVerVehiculo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAceptarAlquileres_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cargarDatos(int modo)
        {
            alquilerModif = gestionAlquileresViewModel.alquiler;

            txtCliente.Text = gestionAlquileresViewModel.alquiler.cliente.nombre;

            txtNif.Text = gestionAlquileresViewModel.alquiler.cliente.nif;

            DateTime dt = DateTime.ParseExact(gestionAlquileresViewModel.alquiler.fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpInicioContrato.SelectedDate = dt;

            DateTime dt2 = DateTime.ParseExact(gestionAlquileresViewModel.alquiler.fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            dtpFinContrato.SelectedDate = dt2;

            txtKilometros.Text = gestionAlquileresViewModel.alquiler.km.ToString() + " /" + gestionAlquileresViewModel.alquiler.tipoKm;

            txtImporte.Text = gestionAlquileresViewModel.alquiler.importe.ToString() + " €";

            cmbMatricula.Text = gestionAlquileresViewModel.alquiler.vehiculo.matricula;

            txtModelo.Text = gestionAlquileresViewModel.alquiler.vehiculo.modelo;

            ocultarMostrar(modo);
        }

        private void ocultarMostrar(int modo)
        {
            switch (modo)
            {
                case 1:
                    //Ocultar
                    btnModificar.Visibility = Visibility.Hidden;
                    btnVerCliente.Visibility = Visibility.Hidden;
                    btnVerVehiculo.Visibility = Visibility.Hidden;
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
                    txtCliente.IsEnabled = false;
                    txtNif.IsEnabled = false;
                    dtpInicioContrato.IsEnabled = false;
                    dtpFinContrato.IsEnabled = false;
                    txtKilometros.IsEnabled = false;
                    txtImporte.IsEnabled = false;
                    cmbMatricula.IsEnabled = false;
                    txtModelo.IsEnabled = false;
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
                    txtCliente.IsEnabled = true;
                    txtNif.IsEnabled = true;
                    dtpInicioContrato.IsEnabled = true;
                    dtpFinContrato.IsEnabled = true;
                    txtKilometros.IsEnabled = true;
                    txtImporte.IsEnabled = true;
                    cmbMatricula.IsEnabled = true;
                    txtModelo.IsEnabled = true;
                    break;
            }
        }
    }
}
