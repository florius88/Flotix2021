using Flotix2021.ModelResponse;
using Flotix2021.Services;
using System.ComponentModel;

namespace Flotix2021.Collection
{
    class AlquilerCollectionViewModel : INotifyPropertyChanged
    {
        private AlquilerCollection _alquileresList = new AlquilerCollection();

        public AlquilerCollection AlquileresList
        {
            get { return _alquileresList; }
            set { _alquileresList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AlquileresList"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public AlquilerCollectionViewModel()
        {
            ServerServiceAlquiler serverServiceAlquiler = new ServerServiceAlquiler();

            ServerResponseAlquiler serverResponseAlquiler = serverServiceAlquiler.GetAll();

            foreach (var item in serverResponseAlquiler.listaAlquiler)
            {
                _alquileresList.Add(item);
            }
        }
    }
}
