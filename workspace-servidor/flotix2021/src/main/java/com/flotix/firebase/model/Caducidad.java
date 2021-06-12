package com.flotix.firebase.model;

/**
 * Objeto para gestionar la informacion de las caducidades en la BD
 * 
 * @author Flor
 *
 */
public class Caducidad {

	private String idVehiculo;
	private String ultimaITV;
	private String proximaITV;
	private String vencimientoVehiculo;
	private boolean baja;

	public String getIdVehiculo() {
		return idVehiculo;
	}

	public void setIdVehiculo(String idVehiculo) {
		this.idVehiculo = idVehiculo;
	}

	public String getUltimaITV() {
		return ultimaITV;
	}

	public void setUltimaITV(String ultimaITV) {
		this.ultimaITV = ultimaITV;
	}

	public String getProximaITV() {
		return proximaITV;
	}

	public void setProximaITV(String proximaITV) {
		this.proximaITV = proximaITV;
	}

	public String getVencimientoVehiculo() {
		return vencimientoVehiculo;
	}

	public void setVencimientoVehiculo(String vencimientoVehiculo) {
		this.vencimientoVehiculo = vencimientoVehiculo;
	}

	public boolean isBaja() {
		return baja;
	}

	public void setBaja(boolean baja) {
		this.baja = baja;
	}
}
