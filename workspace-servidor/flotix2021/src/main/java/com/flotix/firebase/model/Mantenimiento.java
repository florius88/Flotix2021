package com.flotix.firebase.model;

public class Mantenimiento {

	private String ultimoMantenimiento;
	private String proximoMantenimiento;
	private int kmMantenimiento;
	private int idVehiculo; 
	private String idTipoAlerta;

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

	public int getIdVehiculo() {
		return idVehiculo;
	}

	public void setIdVehiculo(int idVehiculo) {
		this.idVehiculo = idVehiculo;
	}

	public String getIdTipoAlerta() {
		return idTipoAlerta;
	}

	public void setIdTipoAlerta(String idTipoAlerta) {
		this.idTipoAlerta = idTipoAlerta;
	}
}
