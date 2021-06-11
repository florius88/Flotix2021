using Flotix2021.Collection;
using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para GestionUsuariosView.xaml
    /// </summary>
    public partial class GestionUsuariosView : UserControl
    {
        private ObservableCollection<UsuarioDTO> observableCollectionUsuario = new AsyncObservableCollection<UsuarioDTO>();

        private GestionUsuariosViewModel gestionUsuariosViewModel;

        public GestionUsuariosView()
        {
            InitializeComponent();

            gestionUsuariosViewModel = (GestionUsuariosViewModel)this.DataContext;

            panel.IsEnabled = false;
            gestionUsuariosViewModel.PanelLoading = true;

            Thread t = new Thread(new ThreadStart(() =>
            {
                ServerServiceUsuario serverServiceUsuario = new ServerServiceUsuario();
                ServerResponseUsuario serverResponseUsuario = serverServiceUsuario.GetAll();

                if (200 == serverResponseUsuario.error.code)
                {
                    foreach (var item in serverResponseUsuario.listaUsuario)
                    {
                        Dispatcher.Invoke(new Action(() => { observableCollectionUsuario.Add(item); }));
                    }
                }
                else
                {
                    MessageBox.Show(serverResponseUsuario.error.message, "Usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                Dispatcher.Invoke(new Action(() => { panel.IsEnabled = true; }));
                Dispatcher.Invoke(new Action(() => { gestionUsuariosViewModel.PanelLoading = false; }));
                Dispatcher.Invoke(new Action(() => { lstUsuarios.ItemsSource = observableCollectionUsuario; }));
            }));

            t.Start();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                // BOTON AJUSTES
                case 0:
                    //gridAjustes.Visibility = Visibility.Visible;
                    //gridGestionUsuario.Visibility = Visibility.Hidden;
                    //gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    break;

                // BOTON ACEPTAR Cambiar Contrasenia
                case 1:
                    // TODO 

                    break;

                // BOTON GESTION USUARIOS
                case 2:
                    //gridGestionUsuario.Visibility = Visibility.Visible;
                    //gridAjustes.Visibility = Visibility.Hidden;
                    //gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    break;

                // BOTON NUEVO USUARIO
                case 3:
                    //gridGestionUsuario.Visibility = Visibility.Hidden;
                    //gridAjustes.Visibility = Visibility.Hidden;
                    //gridNuevoModificarUsuario.Visibility = Visibility.Visible;



                    break;

                // BOTON ELIMINAR USUARIO
                case 4:
                    //gridGestionUsuario.Visibility = Visibility.Hidden;
                    //gridAjustes.Visibility = Visibility.Hidden;
                    //gridNuevoModificarUsuario.Visibility = Visibility.Visible;


                    break;

                // BOTON MODIFICAR/ACEPTAR USUARIO
                case 5:
                    //gridGestionUsuario.Visibility = Visibility.Hidden;
                    //gridAjustes.Visibility = Visibility.Hidden;
                    //gridNuevoModificarUsuario.Visibility = Visibility.Visible;


                    break;

                // BOTON CANCELAR USUARIO
                case 6:
                    //gridGestionUsuario.Visibility = Visibility.Hidden;
                    //gridAjustes.Visibility = Visibility.Hidden;
                    //gridNuevoModificarUsuario.Visibility = Visibility.Visible;


                    break;
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listView_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as ListView).SelectedItem;
            if (item != null)
            {
                UpdateViewCommand.viewModel.SelectedViewModel = new GestionUsuariosViewModel(((UsuarioDTO)item));
            }
        }

    }
}
