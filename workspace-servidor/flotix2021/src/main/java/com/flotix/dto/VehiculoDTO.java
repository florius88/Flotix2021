package com.flotix.dto;

/**
 * Objeto que almacena la informacion de los vehiculos para su devolucion
 * 
 * @author Flor
 *
 */
public class VehiculoDTO {

	private String id;
	private String matricula;
	private String fechaMatriculacion;
	private String modelo;
	private int plazas;
	private int capacidad;
	private int km;
	private boolean disponibilidad;
	private boolean baja;
	private String nombreImagen;
	private String nombreImagenPermiso;
	private String mantenimientoNoCreado;

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

	public boolean isDisponibilidad() {
		return disponibilidad;
	}

	public void setDisponibilidad(boolean disponibilidad) {
		this.disponibilidad = disponibilidad;
	}

	public boolean isBaja() {
		return baja;
	}

	public void setBaja(boolean baja) {
		this.baja = baja;
	}

	public String getNombreImagen() {
		return nombreImagen;
	}

	public void setNombreImagen(String nombreImagen) {
		this.nombreImagen = nombreImagen;
	}

	public String getNombreImagenPermiso() {
		return nombreImagenPermiso;
	}

	public void setNombreImagenPermiso(String nombreImagenPermiso) {
		this.nombreImagenPermiso = nombreImagenPermiso;
	}

	public String getMantenimientoNoCreado() {
		return mantenimientoNoCreado;
	}

	public void setMantenimientoNoCreado(String mantenimientoNoCreado) {
		this.mantenimientoNoCreado = mantenimientoNoCreado;
	}
}
