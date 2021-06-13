using Flotix2021.View;
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

        public void selectRb(int select)
        {
            switch (select)
            {
                case 0:
                    rbInicio.IsChecked = true;
                    break;
                case 1:
                    rbVehiculos.IsChecked = true;
                    break;
                case 2:
                    rbAlquileres.IsChecked = true;
                    break;
                case 3:
                    rbCaducidades.IsChecked = true;
                    break;
                case 4:
                    rbMantenimientos.IsChecked = true;
                    break;
                case 5:
                    rbClientes.IsChecked = true;
                    break;
                case 6:
                    rbAjustes.IsChecked = true;
                    break;
            }
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
            var dialog = new CustomMessageBox
            {
                Caption = "Salir",
                InstructionHeading = "¿Está seguro que quiere salir de la aplicación?",
                InstructionText = "Esta acción cerrará la aplicación",
            };
            dialog.SetButtonsPredefined(EnumPredefinedButtons.OkCancel);

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value && dialog.CustomCustomDialogResult == EnumDialogResults.Button1)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
