package com.flotix.firebase.model;

/**
 * Objeto para gestionar la informacion de los usuarios en la BD
 * 
 * @author Flor
 *
 */
public class Usuario {

	private String nombre;
	private String email;
	private String idRol;
	private String pwd;

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

	public String getPwd() {
		return pwd;
	}

	public void setPwd(String pwd) {
		this.pwd = pwd;
	}
}
