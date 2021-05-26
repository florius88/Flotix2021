package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.AlertaDTO;

public class ServerResponseAlerta {

	private String idAlerta = null;

	private List<AlertaDTO> listaAlerta = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseAlerta() {
		super();
	}

	public String getIdAlerta() {
		return idAlerta;
	}

	public void setIdAlerta(String idAlerta) {
		this.idAlerta = idAlerta;
	}

	public List<AlertaDTO> getListaAlerta() {
		return listaAlerta;
	}

	public void setListaAlerta(List<AlertaDTO> listaAlerta) {
		this.listaAlerta = listaAlerta;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
