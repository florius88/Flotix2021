using Flotix2021.Commands;
using Flotix2021.Utils;
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
                modo = Constantes.NUEVO;

                txtMatricula.IsEnabled = true;

                btnModificar.Visibility = Visibility.Hidden;
                btnBaja.Visibility = Visibility.Hidden;
                btnAceptarVehiculo.Visibility = Visibility.Visible;

            }
            else
            {
                modo = Constantes.CONSULTA;

                string matricula = gestionVehiculoViewModel.vehiculo.matricula;

                txtTitulo.Text = matricula;
                txtMatricula.Text = matricula;
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            if (modo == Constantes.MODIFICA)
            {
                modo = Constantes.CONSULTA;

                txtMatricula.IsEnabled = false;

                btnModificar.Visibility = Visibility.Visible;
                btnAceptarVehiculo.Visibility = Visibility.Hidden;

                // TODO -> Reiniciar los datos 
            }
            else
            {
                gestionVehiculoViewModel.vehiculo = null;
                UpdateViewCommand.viewModel.SelectedViewModel = new VehiculosViewModel();
            }
        }

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            modo = Constantes.MODIFICA;

            txtMatricula.IsEnabled = true;

            btnModificar.Visibility = Visibility.Hidden;
            btnBaja.Visibility = Visibility.Visible;
            btnAceptarVehiculo.Visibility = Visibility.Visible;
        }

        private void btnAceptar_Click(object sender, RoutedEventArgs e)
        {
            if (modo == Constantes.NUEVO)
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
