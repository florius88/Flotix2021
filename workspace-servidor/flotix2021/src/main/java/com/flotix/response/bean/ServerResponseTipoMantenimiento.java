package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.TipoMantenimientoDTO;

/**
 * Objeto de respuesta con la informacion del tipo de mantenimiento
 * 
 * @author Flor
 *
 */
public class ServerResponseTipoMantenimiento {

	private List<TipoMantenimientoDTO> listaTipoMantenimiento = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseTipoMantenimiento() {
		super();
	}

	public List<TipoMantenimientoDTO> getListaTipoMantenimiento() {
		return listaTipoMantenimiento;
	}

	public void setListaTipoMantenimiento(List<TipoMantenimientoDTO> listaTipoMantenimiento) {
		this.listaTipoMantenimiento = listaTipoMantenimiento;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
