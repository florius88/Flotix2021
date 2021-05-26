package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.VehiculoDTO;

public class ServerResponseVehiculo {

	private String idVehiculo = null;

	private List<VehiculoDTO> listaVehiculo = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseVehiculo() {
		super();
	}

	public String getIdVehiculo() {
		return idVehiculo;
	}

	public void setIdVehiculo(String idVehiculo) {
		this.idVehiculo = idVehiculo;
	}

	public List<VehiculoDTO> getListaVehiculo() {
		return listaVehiculo;
	}

	public void setListaVehiculo(List<VehiculoDTO> listaVehiculo) {
		this.listaVehiculo = listaVehiculo;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
