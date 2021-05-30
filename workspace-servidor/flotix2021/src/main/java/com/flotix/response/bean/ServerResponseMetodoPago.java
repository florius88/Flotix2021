package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.MetodoPagoDTO;

/**
 * Objeto de respuesta con la informacion del metodo de pago
 * 
 * @author Flor
 *
 */
public class ServerResponseMetodoPago {

	private List<MetodoPagoDTO> listaMetodoPago = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseMetodoPago() {
		super();
	}

	public List<MetodoPagoDTO> getListaMetodoPago() {
		return listaMetodoPago;
	}

	public void setListaMetodoPago(List<MetodoPagoDTO> listaMetodoPago) {
		this.listaMetodoPago = listaMetodoPago;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
