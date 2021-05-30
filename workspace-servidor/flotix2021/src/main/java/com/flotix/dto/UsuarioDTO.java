package com.flotix.dto;

/**
 * Objeto que almacena la informacion de los usuarios para su devolucion
 * 
 * @author Flor
 *
 */
public class UsuarioDTO {

	private String id;
	private String nombre;
	private String email;
	private String idRol;
	private RolDTO rol;

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

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getIdRol() {
		return idRol;
	}

	public void setIdRol(String idRol) {
		this.idRol = idRol;
	}

	public RolDTO getRol() {
		return rol;
	}

	public void setRol(RolDTO rol) {
		this.rol = rol;
	}
}
