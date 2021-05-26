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
import com.flotix.firebase.model.Rol;
import com.flotix.firebase.service.RolServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseRol;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/rol/")
@CrossOrigin("*")
public class RolRestController {

	@Autowired
	private RolServiceAPI rolServiceAPI;

	@GetMapping(value = "/all")
	public ServerResponseRol getAll() {

		ServerResponseRol result = new ServerResponseRol();

		try {

			result.setListaRol(rolServiceAPI.getAll("nombre"));
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
	public ServerResponseRol find(@PathVariable String id) {
		ServerResponseRol result = new ServerResponseRol();

		try {

			RolDTO rol = rolServiceAPI.get(id);

			if (rol != null) {

				List<RolDTO> lista = new ArrayList<RolDTO>();
				lista.add(rol);

				result.setListaRol(lista);
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
	public ServerResponseRol save(@RequestBody Rol rol, @PathVariable String id) {

		ServerResponseRol result = new ServerResponseRol();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				rolServiceAPI.save(rol);

				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				RolDTO rolDTO = rolServiceAPI.get(id);

				if (rolDTO != null) {

					rolServiceAPI.save(rol, id);

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
}
