using Flotix2021.ModelResponse;
using Flotix2021.Services;
using System.ComponentModel;

namespace Flotix2021.Collection
{
    class CaducidadCollectionViewModel : INotifyPropertyChanged
    {
        private CaducidadCollection _caducidadesList = new CaducidadCollection();

        public CaducidadCollection CaducidadesList
        {
            get { return _caducidadesList; }
            set
            {
                _caducidadesList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CaducidadesList"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public CaducidadCollectionViewModel()
        {
            ServerServiceCaducidad serverServiceCaducidad = new ServerServiceCaducidad();

            ServerResponseCaducidad serverResponseCaducidad = serverServiceCaducidad.GetAll();

            foreach (var item in serverResponseCaducidad.listaCaducidad)
            {
                _caducidadesList.Add(item);
            }
        }
    }

}