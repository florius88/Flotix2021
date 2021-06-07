package com.flotix.dto;

/**
 * Objeto que almacena la informacion de los metodos de pago para su devolucion
 * 
 * @author Flor
 *
 */
public class MetodoPagoDTO {

	private String id;
	private String nombre;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}
}
