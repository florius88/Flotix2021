package com.flotix.controller;

import java.util.ArrayList;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.dto.RolDTO;
import com.flotix.dto.UsuarioDTO;
import com.flotix.firebase.model.Usuario;
import com.flotix.firebase.service.RolServiceAPI;
import com.flotix.firebase.service.UsuarioServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseUsuario;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/usuario/")
@CrossOrigin("*")
public class UsuarioRestController {

	@Autowired
	private UsuarioServiceAPI usuarioServiceAPI;

	@Autowired
	private RolServiceAPI rolServiceAPI;

	// TODO Filtro: VARIABLE: Nombre, Email y FIJO: Rol

	@GetMapping(value = "/all")
	public ServerResponseUsuario getAll() {

		ServerResponseUsuario result = new ServerResponseUsuario();

		try {

			List<UsuarioDTO> listaBD = usuarioServiceAPI.getAll("nombre");

			if (null != listaBD) {
				for (UsuarioDTO usuario : listaBD) {
					// Busca el metodo de pago
					if (null != usuario.getIdRol() && !usuario.getIdRol().isEmpty()) {
						RolDTO rol = rolServiceAPI.get(usuario.getIdRol());
						usuario.setRol(rol);
					}
				}
			}

			result.setListaUsuario(listaBD);
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.OK_CODE);
			error.setMessage(MessageExceptions.MSSG_OK);
			result.setError(error);

		} catch (Exception e) {
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@GetMapping(value = "/find/{id}")
	public ServerResponseUsuario find(@PathVariable String id) {

		ServerResponseUsuario result = new ServerResponseUsuario();

		try {

			UsuarioDTO usuario = usuarioServiceAPI.get(id);

			if (usuario != null) {

				// Busca el metodo de pago
				if (null != usuario.getIdRol() && !usuario.getIdRol().isEmpty()) {
					RolDTO rol = rolServiceAPI.get(usuario.getIdRol());
					usuario.setRol(rol);
				}

				List<UsuarioDTO> lista = new ArrayList<UsuarioDTO>();
				lista.add(usuario);

				result.setListaUsuario(lista);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);

			} else {
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.NOT_FOUND_CODE);
				error.setMessage(MessageExceptions.MSSG_NOT_FOUND);
				result.setError(error);
			}

		} catch (Exception e) {
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@PostMapping(value = "/save/{id}")
	public ServerResponseUsuario save(@RequestBody Usuario usuario, @PathVariable String id) {

		ServerResponseUsuario result = new ServerResponseUsuario();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				id = usuarioServiceAPI.save(usuario);

				result.setIdUsuario(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);

			} else {

				UsuarioDTO usuarioDTO = usuarioServiceAPI.get(id);

				if (usuarioDTO != null) {

					usuarioServiceAPI.save(usuario, id);

					ErrorBean error = new ErrorBean();
					error.setCode(MessageExceptions.OK_CODE);
					error.setMessage(MessageExceptions.MSSG_OK);
					result.setError(error);

				} else {
					ErrorBean error = new ErrorBean();
					error.setCode(MessageExceptions.NOT_FOUND_CODE);
					error.setMessage(MessageExceptions.MSSG_NOT_FOUND);
					result.setError(error);
				}
			}

		} catch (Exception e) {
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@GetMapping(value = "/delete/{id}")
	public ServerResponseUsuario delete(@PathVariable String id) {

		ServerResponseUsuario result = new ServerResponseUsuario();

		try {

			UsuarioDTO usuario = usuarioServiceAPI.get(id);
			if (usuario != null) {
				usuarioServiceAPI.delete(id);

				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.NOT_FOUND_CODE);
				error.setMessage(MessageExceptions.MSSG_NOT_FOUND);
				result.setError(error);
			}

		} catch (Exception e) {
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}
}
