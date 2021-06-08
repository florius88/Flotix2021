using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.Utils;
using System.Text.RegularExpressions;
using System.Windows;

namespace Flotix2021.View
{
    /// <summary>
    /// Logica de interacción para LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        MainWindow mainWindow = new MainWindow();

        public LoginWindow()
        {
            InitializeComponent();
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
                //string email = txtUsuario.Text;
                //string password = txtPassword.Password;

                string email = "elias@elias.com";
                string password = "elias2";

                ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.GetLogin(email, password);

                if (serverResponseUsuario.error.code == MessageExceptions.OK_CODE)
                {
                    if (!serverResponseUsuario.listaUsuario[0].rol.nombre.Equals("COMERCIAL"))
                    {
                        MainWindow.usuarioDTO = serverResponseUsuario.listaUsuario[0];
                        mainWindow.Show();
                        Close();
                    }
                    else
                    {
                        txtError.Text = "No tienes permisos para acceder a la aplicación";
                        txtUsuario.Focus();
                    }

                }
                else
                {
                    txtError.Text = "Usuario o contraseña incorrectos";
                    txtUsuario.Focus();
                }
            //}
        }
    }
}
