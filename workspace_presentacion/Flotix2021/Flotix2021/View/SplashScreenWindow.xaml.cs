using Flotix2021.ViewModel;
using System.Windows;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window
    {
        public SplashScreenWindow()
        {
            InitializeComponent();
            //Carga inicial de datos para la aplicacion
            LoginViewModel loginViewModel = new LoginViewModel();
            loginViewModel.cargaInformacionInicial();
        }
    }
}
