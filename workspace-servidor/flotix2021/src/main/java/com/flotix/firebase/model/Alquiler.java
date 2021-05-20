package com.flotix.firebase.model;

public class Alquiler {

	private String idVehiculo;
	private String idCliente;
	private String fechaInicio;
	private String fechaFin;
	private int km;
	private String tipoKm;
	private double importe;
	private String tipoImporte;
	
	public String getIdVehiculo() {
		return idVehiculo;
	}
	public void setIdVehiculo(String idVehiculo) {
		this.idVehiculo = idVehiculo;
	}
	public String getIdCliente() {
		return idCliente;
	}
	public void setIdCliente(String idCliente) {
		this.idCliente = idCliente;
	}
	public String getFechaInicio() {
		return fechaInicio;
	}
	public void setFechaInicio(String fechaInicio) {
		this.fechaInicio = fechaInicio;
	}
	public String getFechaFin() {
		return fechaFin;
	}
	public void setFechaFin(String fechaFin) {
		this.fechaFin = fechaFin;
	}
	public int getKm() {
		return km;
	}
	public void setKm(int km) {
		this.km = km;
	}
	public String getTipoKm() {
		return tipoKm;
	}
	public void setTipoKm(String tipoKm) {
		this.tipoKm = tipoKm;
	}
	public double getImporte() {
		return importe;
	}
	public void setImporte(double importe) {
		this.importe = importe;
	}
	public String getTipoImporte() {
		return tipoImporte;
	}
	public void setTipoImporte(String tipoImporte) {
		this.tipoImporte = tipoImporte;
	}
}
