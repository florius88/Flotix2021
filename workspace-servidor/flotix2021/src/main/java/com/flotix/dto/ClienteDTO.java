package com.flotix.dto;

/**
 * Objeto que almacena la informacion de los clientes para su devolucion
 * 
 * @author Flor
 *
 */
public class ClienteDTO {

	private String id;
	private String nif;
	private String nombre;
	private String direccion;
	private String poblacion;
	private String personaContacto;
	private String tlfContacto;
	private String email;
	private String idMetodoPago;
	private MetodoPagoDTO metodoPago;
	private String cuentaBancaria;
	private boolean baja;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

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

	public MetodoPagoDTO getMetodoPago() {
		return metodoPago;
	}

	public void setMetodoPago(MetodoPagoDTO metodoPago) {
		this.metodoPago = metodoPago;
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
