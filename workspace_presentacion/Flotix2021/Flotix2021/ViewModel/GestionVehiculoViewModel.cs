using Flotix2021.ModelDTO;

namespace Flotix2021.ViewModel
{
    public class GestionVehiculoViewModel : BaseViewModel
    {

        private static VehiculoDTO _vehiculo;

        public VehiculoDTO vehiculo
        {
            get { return _vehiculo; }
            set { _vehiculo = value; }
        }

        public GestionVehiculoViewModel()
        {

        }

        public GestionVehiculoViewModel(VehiculoDTO vehiculoDTO)
        {
            _vehiculo = vehiculoDTO;
        }
    }
}
