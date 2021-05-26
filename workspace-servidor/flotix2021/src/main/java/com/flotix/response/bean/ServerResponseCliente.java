package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.ClienteDTO;

public class ServerResponseCliente {

	private String idCliente = null;
	private List<ClienteDTO> listaCliente = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseCliente() {
		super();
	}

	public String getIdCliente() {
		return idCliente;
	}

	public void setIdCliente(String idCliente) {
		this.idCliente = idCliente;
	}

	public List<ClienteDTO> getListaCliente() {
		return listaCliente;
	}

	public void setListaCliente(List<ClienteDTO> listaCliente) {
		this.listaCliente = listaCliente;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
