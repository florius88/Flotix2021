using Flotix2021.Commands;
using Flotix2021.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionVehiculoView.xaml
    /// </summary>
    public partial class GestionVehiculoView : UserControl
    {
        private GestionVehiculoViewModel gestionVehiculoViewModel;

        private int modo;

        public GestionVehiculoView()
        {
            InitializeComponent();

            gestionVehiculoViewModel = (GestionVehiculoViewModel)this.DataContext;

            if (null == gestionVehiculoViewModel.vehiculo)
            {
                modo = 1;

                titulo.Text = "Nuevo vehiculo";

                txtMatricula.IsEnabled = true;

                btnNuevoVehiculo.Visibility = Visibility.Hidden;
                btnAceptarVehiculo.Visibility = Visibility.Visible;

            }
            else
            {
                modo = 2;

                string matricula = gestionVehiculoViewModel.vehiculo.matricula;

                titulo.Text = matricula;
                txtMatricula.Text = matricula;
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 3)
            {
                modo = 2;

                txtMatricula.IsEnabled = false;

                btnNuevoVehiculo.Visibility = Visibility.Visible;
                btnAceptarVehiculo.Visibility = Visibility.Hidden;

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

            txtMatricula.IsEnabled = true;

            btnNuevoVehiculo.Visibility = Visibility.Hidden;
            btnAceptarVehiculo.Visibility = Visibility.Visible;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (modo == 1)
            {
                MessageBox.Show("Se ha guardado correctamente");
            }
            else
            {
                MessageBox.Show("Se ha modificado correctamente");
            }

            UpdateViewCommand.viewModel.SelectedViewModel = new VehiculosViewModel();
        }
    }
}
