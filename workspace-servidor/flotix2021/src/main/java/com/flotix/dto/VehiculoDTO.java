package com.flotix.dto;

public class VehiculoDTO {

	private String id;
	private String matricula;
	private String fechaMatriculacion;
	private String modelo;
	private int plazas;
	private int capacidad;
	//TODO permiso de circulacion
	private int km;
	private String idCaducidad;
	private CaducidadDTO caducidad;

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

	public String getFechaMatriculacion() {
		return fechaMatriculacion;
	}

	public void setFechaMatriculacion(String fechaMatriculacion) {
		this.fechaMatriculacion = fechaMatriculacion;
	}

	public String getModelo() {
		return modelo;
	}

	public void setModelo(String modelo) {
		this.modelo = modelo;
	}

	public int getPlazas() {
		return plazas;
	}

	public void setPlazas(int plazas) {
		this.plazas = plazas;
	}

	public int getCapacidad() {
		return capacidad;
	}

	public void setCapacidad(int capacidad) {
		this.capacidad = capacidad;
	}

	public int getKm() {
		return km;
	}

	public void setKm(int km) {
		this.km = km;
	}
	
	public String getIdCaducidad() {
		return idCaducidad;
	}

	public void setIdCaducidad(String idCaducidad) {
		this.idCaducidad = idCaducidad;
	}

	public CaducidadDTO getCaducidad() {
		return caducidad;
	}

	public void setCaducidad(CaducidadDTO caducidad) {
		this.caducidad = caducidad;
	}
}
