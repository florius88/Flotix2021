using Flotix2021.ViewModel;
using System.Windows;

namespace Flotix2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            mainViewModel = (MainViewModel)this.DataContext;
        }

        public void cargarUsuario()
        {
            if (null != MainViewModel.usuarioDTO)
            {
                txtNomUsuario.Text = MainViewModel.usuarioDTO.nombre;
                txtRolUsuario.Text = MainViewModel.usuarioDTO.rol.nombre;
            }            
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            var response = MessageBox.Show("¿Seguro que quiere salir de la aplicación?", "Salir...",
                                    MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

            if (response == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
