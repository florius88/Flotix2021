package com.flotix.dto;

public class MantenimientoDTO {

	private String id;
	private String ultimoMantenimiento;
	private String proximoMantenimiento;
	private int kmMantenimiento;
	private String idVehiculo;
	private VehiculoDTO vehiculo; 
	private String idTipoAlerta;
	private TipoAlertaDTO tipoAlerta;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getUltimoMantenimiento() {
		return ultimoMantenimiento;
	}

	public void setUltimoMantenimiento(String ultimoMantenimiento) {
		this.ultimoMantenimiento = ultimoMantenimiento;
	}

	public String getProximoMantenimiento() {
		return proximoMantenimiento;
	}

	public void setProximoMantenimiento(String proximoMantenimiento) {
		this.proximoMantenimiento = proximoMantenimiento;
	}

	public int getKmMantenimiento() {
		return kmMantenimiento;
	}

	public void setKmMantenimiento(int kmMantenimiento) {
		this.kmMantenimiento = kmMantenimiento;
	}
	
	public String getIdVehiculo() {
		return idVehiculo;
	}

	public void setIdVehiculo(String idVehiculo) {
		this.idVehiculo = idVehiculo;
	}

	public VehiculoDTO getVehiculo() {
		return vehiculo;
	}

	public void setVehiculo(VehiculoDTO vehiculo) {
		this.vehiculo = vehiculo;
	}

	public String getIdTipoAlerta() {
		return idTipoAlerta;
	}

	public void setIdTipoAlerta(String idTipoAlerta) {
		this.idTipoAlerta = idTipoAlerta;
	}

	public TipoAlertaDTO getTipoAlerta() {
		return tipoAlerta;
	}

	public void setTipoAlerta(TipoAlertaDTO tipoAlerta) {
		this.tipoAlerta = tipoAlerta;
	}
}
