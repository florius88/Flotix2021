package com.flotix.response.bean;

import java.util.List;

import com.flotix.dto.UsuarioDTO;

/**
 * Objeto de respuesta con la informacion del usuario
 * 
 * @author Flor
 *
 */
public class ServerResponseUsuario {

	private String idUsuario = null;

	private List<UsuarioDTO> listaUsuario = null;

	private ErrorBean error = new ErrorBean();

	public ServerResponseUsuario() {
		super();
	}

	public String getIdUsuario() {
		return idUsuario;
	}

	public void setIdUsuario(String idUsuario) {
		this.idUsuario = idUsuario;
	}

	public List<UsuarioDTO> getListaUsuario() {
		return listaUsuario;
	}

	public void setListaUsuario(List<UsuarioDTO> listaUsuario) {
		this.listaUsuario = listaUsuario;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(ErrorBean error) {
		this.error = error;
	}
}
