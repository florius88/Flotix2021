package com.flotix.dto;

public class CaducidadDTO {

	private String id;
	private String ultimaITV;
	private String proximaITV;
	private int kmMantenimiento;
	private String venciminetoVehiculo;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
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

	public int getKmMantenimiento() {
		return kmMantenimiento;
	}

	public void setKmMantenimiento(int kmMantenimiento) {
		this.kmMantenimiento = kmMantenimiento;
	}

	public String getVenciminetoVehiculo() {
		return venciminetoVehiculo;
	}

	public void setVenciminetoVehiculo(String venciminetoVehiculo) {
		this.venciminetoVehiculo = venciminetoVehiculo;
	}
}
