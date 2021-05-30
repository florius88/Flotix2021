package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.RolDTO;

/**
 * Objeto de respuesta con la informacion del rol
 * 
 * @author Flor
 *
 */
public class ServerResponseRol {

	private List<RolDTO> listaRol = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseRol() {
		super();
	}

	public List<RolDTO> getListaRol() {
		return listaRol;
	}

	public void setListaRol(List<RolDTO> listaRol) {
		this.listaRol = listaRol;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
