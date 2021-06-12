package com.flotix.response.bean;

import com.flotix.firebase.model.ImagenVehiculo;

public class ServerResponseImagenVehiculo {

	private String idImagenVehiculo = null;

	private ImagenVehiculo imagenVehiculo = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseImagenVehiculo() {
		super();
	}

	public String getIdImagenVehiculo() {
		return idImagenVehiculo;
	}

	public void setIdImagenVehiculo(String idImagenVehiculo) {
		this.idImagenVehiculo = idImagenVehiculo;
	}

	public ImagenVehiculo getImagenVehiculo() {
		return imagenVehiculo;
	}

	public void setImagenVehiculo(ImagenVehiculo imagenVehiculo) {
		this.imagenVehiculo = imagenVehiculo;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
