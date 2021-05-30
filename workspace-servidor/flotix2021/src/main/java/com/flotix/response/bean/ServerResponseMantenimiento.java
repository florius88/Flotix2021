package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.MantenimientoDTO;

/**
 * Objeto de respuesta con la informacion del mantenimiento
 * 
 * @author Flor
 *
 */
public class ServerResponseMantenimiento {

	private String idMantenimiento = null;

	private List<MantenimientoDTO> listaMantenimiento = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseMantenimiento() {
		super();
	}

	public String getIdMantenimiento() {
		return idMantenimiento;
	}

	public void setIdMantenimiento(String idMantenimiento) {
		this.idMantenimiento = idMantenimiento;
	}

	public List<MantenimientoDTO> getListaMantenimiento() {
		return listaMantenimiento;
	}

	public void setListaMantenimiento(List<MantenimientoDTO> listaMantenimiento) {
		this.listaMantenimiento = listaMantenimiento;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
