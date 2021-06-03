
namespace Flotix2021.ModelDTO
{
    class AlertaDTO
    {
		private string _id;

		public string id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _idTipoAlerta;

		public string idTipoAlerta
		{
			get { return _idTipoAlerta; }
			set { _idTipoAlerta = value; }
		}

		private TipoAlertaDTO _tipoAlerta;

		public TipoAlertaDTO tipoAlerta
		{
			get { return _tipoAlerta; }
			set { _tipoAlerta = value; }
		}

		private string _matricula;

		public string matricula
		{
			get { return _matricula; }
			set { _matricula = value; }
		}

		private string _nombreCliente;

		public string nombreCliente
		{
			get { return _nombreCliente; }
			set { _nombreCliente = value; }
		}

		private string _tlfContacto;

		public string tlfContacto
		{
			get { return _tlfContacto; }
			set { _tlfContacto = value; }
		}

		private int _vencimiento;

		public int vencimiento
		{
			get { return _vencimiento; }
			set { _vencimiento = value; }
		}
	}
}
