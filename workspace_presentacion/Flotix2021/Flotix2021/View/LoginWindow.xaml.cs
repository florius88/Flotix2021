using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using Flotix2021.ViewModel;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;

namespace Flotix2021.View
{
    /// <summary>
    /// Logica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static MainWindow mainWindow = new MainWindow();

        private LoginViewModel loginViewModel;

        public LoginWindow()
        {
            InitializeComponent();

            loginViewModel = (LoginViewModel)this.DataContext;
        }

        /// <summary>
        /// Logica del Boton Salir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /// <summary>
        /// Logica del Boton Entrar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            //if (txtUsuario.Text.Length == 0 || txtPassword.Password.Length == 0)
            //{
            //    txtError.Text = "El email y la constraseña no pueden estar vacíos";
            //    txtUsuario.Focus();
            //}
            //else if (!Regex.IsMatch(txtUsuario.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            //{
            //    txtError.Text = "Introduzca un email válido";
            //    txtUsuario.Select(0, txtUsuario.Text.Length);
            //    txtUsuario.Focus();
            //}
            //else
            //{
                string email = "elias@elias.com";// txtUsuario.Text;
                string password = "elias2";// txtPassword.Password;

                panel.IsEnabled = false;
                loginViewModel.PanelLoading = true;

                Thread t = new Thread(new ThreadStart(() =>
                {
                    ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                    ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.GetLogin(email, password);

                    Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                    Dispatcher.Invoke(new Action(() => { loginViewModel.PanelLoading = false; }));

                    if (serverResponseUsuario.error.code == MessageExceptions.OK_CODE)
                    {
                        if (!serverResponseUsuario.listaUsuario[0].rol.nombre.Equals("COMERCIAL"))
                        {
                            Dispatcher.Invoke(new Action(() => { MainViewModel.usuarioDTO = serverResponseUsuario.listaUsuario[0]; }));
                            Dispatcher.Invoke(new Action(() => { mainWindow.cargarUsuario(); }));
                            Dispatcher.Invoke(new Action(() => { mainWindow.Show(); }));
                            Dispatcher.Invoke(new Action(() => { Close(); }));
                        }
                        else
                        {
                            Dispatcher.Invoke(new Action(() => { txtError.Text = "No tienes permisos para acceder a la aplicación"; }));
                            Dispatcher.Invoke(new Action(() => { txtUsuario.Focus(); }));
                        }
                    }
                    else
                    {
                        Dispatcher.Invoke(new Action(() => { txtError.Text = "Usuario o contraseña incorrectos"; }));
                        Dispatcher.Invoke(new Action(() => { txtUsuario.Focus(); }));
                    }
                }));

                t.Start();
            //}
        }
    }
}
