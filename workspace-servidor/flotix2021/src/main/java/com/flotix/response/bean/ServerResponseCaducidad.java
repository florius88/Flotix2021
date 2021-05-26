package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.CaducidadDTO;

public class ServerResponseCaducidad {

	private String idCaducidad = null;

	private List<CaducidadDTO> listaCaducidad = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseCaducidad() {
		super();
	}

	public String getIdCaducidad() {
		return idCaducidad;
	}

	public void setIdCaducidad(String idCaducidad) {
		this.idCaducidad = idCaducidad;
	}

	public List<CaducidadDTO> getListaCaducidad() {
		return listaCaducidad;
	}

	public void setListaCaducidad(List<CaducidadDTO> listaCaducidad) {
		this.listaCaducidad = listaCaducidad;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
