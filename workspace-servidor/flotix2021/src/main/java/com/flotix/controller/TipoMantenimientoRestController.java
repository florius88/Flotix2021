package com.flotix.controller;

import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.dto.TipoMantenimientoDTO;
import com.flotix.firebase.model.TipoMantenimiento;
import com.flotix.firebase.service.TipoMantenimientoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseTipoMantenimiento;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/tipomantenimiento/")
@CrossOrigin("*")
public class TipoMantenimientoRestController {

	private static Logger logger = Logger.getLogger(AlquilerRestController.class);

	@Autowired
	private TipoMantenimientoServiceAPI tipoMantenimientoServiceAPI;

	@GetMapping(value = "/all")
	public ServerResponseTipoMantenimiento getAll() {

		logger.info("TipoMantenimientoRestController - getAll");

		ServerResponseTipoMantenimiento result = new ServerResponseTipoMantenimiento();

		try {

			result.setListaTipoMantenimiento(tipoMantenimientoServiceAPI.getAll("nombre"));
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.OK_CODE);
			error.setMessage(MessageExceptions.MSSG_OK);
			result.setError(error);

		} catch (Exception e) {
			// LOG
			logger.error("Se ha producido un error: " + e.getMessage());
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@GetMapping(value = "/find/{id}")
	public ServerResponseTipoMantenimiento find(@PathVariable String id) {

		logger.info("TipoMantenimientoRestController - find");

		ServerResponseTipoMantenimiento result = new ServerResponseTipoMantenimiento();

		try {

			TipoMantenimientoDTO tipoMantenimiento = tipoMantenimientoServiceAPI.get(id);

			if (tipoMantenimiento != null) {

				List<TipoMantenimientoDTO> lista = new ArrayList<TipoMantenimientoDTO>();
				lista.add(tipoMantenimiento);

				result.setListaTipoMantenimiento(lista);
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
			// LOG
			logger.error("Se ha producido un error: " + e.getMessage());
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@PostMapping(value = "/save/{id}")
	public ServerResponseTipoMantenimiento save(@RequestBody TipoMantenimiento tipoMantenimiento,
			@PathVariable String id) {

		logger.info("TipoMantenimientoRestController - save");

		ServerResponseTipoMantenimiento result = new ServerResponseTipoMantenimiento();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				tipoMantenimientoServiceAPI.save(tipoMantenimiento);

				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				TipoMantenimientoDTO tipoMantenimientoDTO = tipoMantenimientoServiceAPI.get(id);

				if (tipoMantenimientoDTO != null) {

					tipoMantenimientoServiceAPI.save(tipoMantenimiento, id);
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
			// LOG
			logger.error("Se ha producido un error: " + e.getMessage());
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}
}
