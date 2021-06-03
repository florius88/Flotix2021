
namespace Flotix2021.Model
{
    class VehiculoDTO
    {
        private string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _matricula;

        public string Matricula
        {
            get { return _matricula; }
            set { _matricula = value; }
        }

        private string _fechaMatriculacion;

        public string FechaMatriculacion
        {
            get { return _fechaMatriculacion; }
            set { _fechaMatriculacion = value; }
        }

        private string _modelo;

        public string Modelo
        {
            get { return _modelo; }
            set { _modelo = value; }
        }

        private int _plazas;

        public int Plazas
        {
            get { return _plazas; }
            set { _plazas = value; }
        }

        private int _capacidad;

        public int Capacidad
        {
            get { return _capacidad; }
            set { _capacidad = value; }
        }

        private int _km;

        public int Km
        {
            get { return _km; }
            set { _km = value; }
        }

        private bool _disponibilidad;

        public bool Disponibilidad
        {
            get { return _disponibilidad; }
            set { _disponibilidad = value; }
        }

        private bool _baja;

        public bool Baja
        {
            get { return _baja; }
            set { _baja = value; }
        }

    }
}
