using Flotix2021.Collection;
using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using Flotix2021.ModelResponse;
using Flotix2021.Services;
using Flotix2021.ViewModel;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace Flotix2021.View
{
    /// <summary>
    /// Lógica de interacción para AjustesView.xaml
    /// </summary>
    public partial class AjustesView : UserControl
    {
        private ObservableCollection<UsuarioDTO> observableCollectionUsuario = new AsyncObservableCollection<UsuarioDTO>();

        private AjustesViewModel ajustesViewModel;

        public AjustesView()
        {
            InitializeComponent();

            // Mostrar
            gridAjustes.Visibility = Visibility.Visible;

            // Ocultar
            gridGestionUsuario.Visibility = Visibility.Hidden;
            gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

            ajustesViewModel = (AjustesViewModel)this.DataContext;

            panel.IsEnabled = false;
            ajustesViewModel.PanelLoading = true;

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
                Dispatcher.Invoke(new Action(() => { ajustesViewModel.PanelLoading = false; }));
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
                    // Mostrar
                    gridAjustes.Visibility = Visibility.Visible;

                    // Ocultar
                    gridGestionUsuario.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    break;

                // BOTON ACEPTAR Cambiar Contrasenia
                case 1:
                    // Mostrar
                    gridAjustes.Visibility = Visibility.Visible;

                    // Ocultar
                    gridGestionUsuario.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    break;

                // BOTON GESTION USUARIOS
                case 2:
                    // Mostrar
                    gridGestionUsuario.Visibility = Visibility.Visible;

                    // Ocultar
                    gridAjustes.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;

                    break;

                // BOTON NUEVO USUARIO
                case 3:
                    // Mostrar
                    gridNuevoModificarUsuario.Visibility = Visibility.Visible;

                    btnModifAceptar.Visibility = Visibility.Visible;
                    btnCancelar.Visibility = Visibility.Visible;

                    // Ocultar
                    gridAjustes.Visibility = Visibility.Hidden;
                    gridGestionUsuario.Visibility = Visibility.Hidden;

                    btnEliminar.Visibility = Visibility.Hidden;

                    break;

                // BOTON ELIMINAR USUARIO
                case 4:
                    // TODO 

                    break;

                // BOTON MODIFICAR/ACEPTAR USUARIO
                case 5:
                    modificar(null);

                    break;

                // BOTON CANCELAR USUARIO
                case 6:
                    // Mostrar
                    gridGestionUsuario.Visibility = Visibility.Visible;

                    // Ocultar
                    gridAjustes.Visibility = Visibility.Hidden;
                    gridNuevoModificarUsuario.Visibility = Visibility.Hidden;


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
                modificar(item);
            }
        }

        private void modificar(object item)
        {
            // Mostrar
            gridNuevoModificarUsuario.Visibility = Visibility.Visible;

            btnEliminar.Visibility = Visibility.Visible;
            btnModifAceptar.Visibility = Visibility.Visible;
            btnCancelar.Visibility = Visibility.Visible;

            // Ocultar
            gridAjustes.Visibility = Visibility.Hidden;
            gridGestionUsuario.Visibility = Visibility.Hidden;
        }
    }
}
