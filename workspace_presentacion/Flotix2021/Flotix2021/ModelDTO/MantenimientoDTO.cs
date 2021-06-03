using System;
using System.Collections.Generic;
using System.Text;

namespace Flotix2021.ModelDTO
{
    class MantenimientoDTO
    {
		private string _id;

		public string id
		{
			get { return _id; }
			set { _id = value; }
		}

		private string _ultimoMantenimiento;

		public string ultimoMantenimiento
		{
			get { return _ultimoMantenimiento; }
			set { _ultimoMantenimiento = value; }
		}
		
		private string _proximoMantenimiento;

		public string proximoMantenimiento
		{
			get { return _proximoMantenimiento; }
			set { _proximoMantenimiento = value; }
		}
		
		private int _kmMantenimiento;

		public int kmMantenimiento
		{
			get { return _kmMantenimiento; }
			set { _kmMantenimiento = value; }
		}
		
		private string _idVehiculo;

		public string idVehiculo
		{
			get { return _idVehiculo; }
			set { _idVehiculo = value; }
		}
		
		private VehiculoDTO _vehiculo;

		public VehiculoDTO vehiculo
		{
			get { return _vehiculo; }
			set { _vehiculo = value; }
		}
		
		private string _idTipoMantenimiento;

		public string idTipoMantenimiento
		{
			get { return _idTipoMantenimiento; }
			set { _idTipoMantenimiento = value; }
		}
		
		private TipoMantenimientoDTO _tipoMantenimiento;

		public TipoMantenimientoDTO tipoMantenimiento
		{
			get { return _tipoMantenimiento; }
			set { _tipoMantenimiento = value; }
		}
		
		private bool _baja;

		public bool baja
		{
			get { return _baja; }
			set { _baja = value; }
		}
	}
}
