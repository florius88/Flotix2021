
namespace Flotix2021.ModelDTO
{
	public class ClienteDTO
    {
        public ClienteDTO()
        {

        }

		public ClienteDTO(ClienteDTO clienteDTO)
		{
			_id = clienteDTO.id;
			_nif = clienteDTO.nif;
			_nombre = clienteDTO.nombre;
			_direccion = clienteDTO.direccion;
			_poblacion = clienteDTO.poblacion;
			_personaContacto = clienteDTO.personaContacto;
			_tlfContacto = clienteDTO.tlfContacto;
			_email = clienteDTO.email;
			_idMetodoPago = clienteDTO.idMetodoPago;
			_metodoPago = clienteDTO.metodoPago;
			_cuentaBancaria = clienteDTO.cuentaBancaria;
			_baja = clienteDTO.baja;
		}

		private string _id;

		public string id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _nif;

		public string nif
		{
			get { return _nif; }
			set { _nif = value; }
		}

		private string _nombre;

		public string nombre
		{
			get { return _nombre; }
			set { _nombre = value; }
		}

		private string _direccion;

		public string direccion
		{
			get { return _direccion; }
			set { _direccion = value; }
		}

		private string _poblacion;

		public string poblacion
		{
			get { return _poblacion; }
			set { _poblacion = value; }
		}
		
		private string _personaContacto;

		public string personaContacto
		{
			get { return _personaContacto; }
			set { _personaContacto = value; }
		}
		
		private string _tlfContacto;
		
		public string tlfContacto
		{
			get { return _tlfContacto; }
			set { _tlfContacto = value; }
		}
		
		private string _email;

		public string email
		{
			get { return _email; }
			set { _email = value; }
		}
		
		private string _idMetodoPago;

		public string idMetodoPago
		{
			get { return _idMetodoPago; }
			set { _idMetodoPago = value; }
		}
		
		private MetodoPagoDTO _metodoPago;

		public MetodoPagoDTO metodoPago
		{
			get { return _metodoPago; }
			set { _metodoPago = value; }
		}
		
		private string _cuentaBancaria;

		public string cuentaBancaria
		{
			get { return _cuentaBancaria; }
			set { _cuentaBancaria = value; }
		}
		
		private bool _baja;

		public bool baja
		{
			get { return _baja; }
			set { _baja = value; }
		}
	}
}
