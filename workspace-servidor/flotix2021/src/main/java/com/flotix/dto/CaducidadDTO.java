package com.flotix.dto;

/**
 * Objeto que almacena la informacion de las caducidades para su devolucion
 * 
 * @author Flor
 *
 */
public class CaducidadDTO {

	private String id;
	private String matricula;
	private VehiculoDTO vehiculo;
	private String ultimaITV;
	private String proximaITV;
	private int kmMantenimiento;
	private String venciminetoVehiculo;
	private boolean baja;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getMatricula() {
		return matricula;
	}

	public void setMatricula(String matricula) {
		this.matricula = matricula;
	}

	public VehiculoDTO getVehiculo() {
		return vehiculo;
	}

	public void setVehiculo(VehiculoDTO vehiculo) {
		this.vehiculo = vehiculo;
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

	public boolean isBaja() {
		return baja;
	}

	public void setBaja(boolean baja) {
		this.baja = baja;
	}
}
