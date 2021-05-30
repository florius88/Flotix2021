package com.flotix.firebase.model;

/**
 * Objeto para gestionar la informacion de los clientes en la BD
 * 
 * @author Flor
 *
 */
public class Cliente {

	private String nif;
	private String nombre;
	private String direccion;
	private String poblacion;
	private String personaContacto;
	private String tlfContacto;
	private String email;
	private String idMetodoPago;
	private String cuentaBancaria;
	private boolean baja;

	public String getNif() {
		return nif;
	}

	public void setNif(String nif) {
		this.nif = nif;
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}

	public String getDireccion() {
		return direccion;
	}

	public void setDireccion(String direccion) {
		this.direccion = direccion;
	}

	public String getPoblacion() {
		return poblacion;
	}

	public void setPoblacion(String poblacion) {
		this.poblacion = poblacion;
	}

	public String getPersonaContacto() {
		return personaContacto;
	}

	public void setPersonaContacto(String personaContacto) {
		this.personaContacto = personaContacto;
	}

	public String getTlfContacto() {
		return tlfContacto;
	}

	public void setTlfContacto(String tlfContacto) {
		this.tlfContacto = tlfContacto;
	}

	public String getEmail() {
		return email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getIdMetodoPago() {
		return idMetodoPago;
	}

	public void setIdMetodoPago(String idMetodoPago) {
		this.idMetodoPago = idMetodoPago;
	}

	public String getCuentaBancaria() {
		return cuentaBancaria;
	}

	public void setCuentaBancaria(String cuentaBancaria) {
		this.cuentaBancaria = cuentaBancaria;
	}

	public boolean isBaja() {
		return baja;
	}

	public void setBaja(boolean baja) {
		this.baja = baja;
	}
}
