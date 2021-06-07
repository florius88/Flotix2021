package com.flotix.firebase.model;

/**
 * Objeto para gestionar la informacion de los mantenimientos en la BD
 * 
 * @author Flor
 *
 */
public class Mantenimiento {

	private String ultimoMantenimiento;
	private String proximoMantenimiento;
	private int kmMantenimiento;
	private String idVehiculo;
	private String idTipoMantenimiento;
	private boolean baja;

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

	public String getIdTipoMantenimiento() {
		return idTipoMantenimiento;
	}

	public void setIdTipoMantenimiento(String idTipoMantenimiento) {
		this.idTipoMantenimiento = idTipoMantenimiento;
	}

	public boolean isBaja() {
		return baja;
	}

	public void setBaja(boolean baja) {
		this.baja = baja;
	}
}
