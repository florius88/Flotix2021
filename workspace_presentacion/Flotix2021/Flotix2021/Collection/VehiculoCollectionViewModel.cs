using Flotix2021.ModelResponse;
using Flotix2021.Services;
using System.ComponentModel;

namespace Flotix2021.Collection
{

    public class VehiculoCollectionViewModel : INotifyPropertyChanged
    {
        private VehiculoCollection _vehiculosList = new VehiculoCollection();

        public VehiculoCollection VehiculosList
        {
            get { return _vehiculosList; }
            set
            {
                _vehiculosList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("VehiculosList"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        public VehiculoCollectionViewModel()
        {
            ServerServiceVehiculo serverServiceVehiculo = new ServerServiceVehiculo();

            ServerResponseVehiculo serverResponseVehiculo = serverServiceVehiculo.GetAll();

            foreach (var item in serverResponseVehiculo.listaVehiculo)
            {
                _vehiculosList.Add(item);
            }
        }
    }
}