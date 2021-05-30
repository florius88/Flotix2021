package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.AlquilerDTO;

/**
 * Objeto de respuesta con la informacion del alquiler
 * 
 * @author Flor
 *
 */
public class ServerResponseAlquiler {

	private String idAlquiler = null;

	private List<AlquilerDTO> listaAlquiler = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseAlquiler() {
		super();
	}

	public String getIdAlquiler() {
		return idAlquiler;
	}

	public void setIdAlquiler(String idAlquiler) {
		this.idAlquiler = idAlquiler;
	}

	public List<AlquilerDTO> getListaAlquiler() {
		return listaAlquiler;
	}

	public void setListaAlquiler(List<AlquilerDTO> listaAlquiler) {
		this.listaAlquiler = listaAlquiler;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
