package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.TipoAlertaDTO;

/**
 * Objeto de respuesta con la informacion del tipo de alerta
 * 
 * @author Flor
 *
 */
public class ServerResponseTipoAlerta {

	private List<TipoAlertaDTO> listaTipoAlerta = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseTipoAlerta() {
		super();
	}

	public List<TipoAlertaDTO> getListaTipoAlerta() {
		return listaTipoAlerta;
	}

	public void setListaTipoAlerta(List<TipoAlertaDTO> listaTipoAlerta) {
		this.listaTipoAlerta = listaTipoAlerta;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
