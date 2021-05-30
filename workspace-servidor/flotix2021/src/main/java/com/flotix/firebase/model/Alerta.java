package com.flotix.firebase.model;

/**
 * Objeto para gestionar la informacion de las alertas en la BD
 * 
 * @author Flor
 *
 */
public class Alerta {

	private String idTipoAlerta;
	private String matricula;
	private String nombreCliente;
	private int vencimiento;

	public String getIdTipoAlerta() {
		return idTipoAlerta;
	}

	public void setIdTipoAlerta(String idTipoAlerta) {
		this.idTipoAlerta = idTipoAlerta;
	}

	public String getMatricula() {
		return matricula;
	}

	public void setMatricula(String matricula) {
		this.matricula = matricula;
	}

	public String getNombreCliente() {
		return nombreCliente;
	}

	public void setNombreCliente(String nombreCliente) {
		this.nombreCliente = nombreCliente;
	}

	public int getVencimiento() {
		return vencimiento;
	}

	public void setVencimiento(int vencimiento) {
		this.vencimiento = vencimiento;
	}
}
