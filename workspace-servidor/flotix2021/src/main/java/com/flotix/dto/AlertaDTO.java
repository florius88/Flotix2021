package com.flotix.dto;

public class AlertaDTO {

	private String id;
	private String idTipoAlerta;
	private TipoAlertaDTO tipoAlerta;
	private String matricula;
	private String nombreCliente;
	private int vencimiento;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}
	
	public String getIdTipoAlerta() {
		return idTipoAlerta;
	}

	public void setIdTipoAlerta(String idTipoAlerta) {
		this.idTipoAlerta = idTipoAlerta;
	}

	public TipoAlertaDTO getTipoAlerta() {
		return tipoAlerta;
	}

	public void setTipoAlerta(TipoAlertaDTO tipoAlerta) {
		this.tipoAlerta = tipoAlerta;
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
