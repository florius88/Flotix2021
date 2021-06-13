using Flotix2021.Commands;
using Flotix2021.ModelDTO;
using System.Windows.Input;

namespace Flotix2021.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static UsuarioDTO usuarioDTO;

        private BaseViewModel _selectedViewModel;
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }

        public ICommand UpdateViewCommand { get; set; }

        public MainViewModel()
        {
            UpdateViewCommand = new UpdateViewCommand(this);
        }
    }
}
