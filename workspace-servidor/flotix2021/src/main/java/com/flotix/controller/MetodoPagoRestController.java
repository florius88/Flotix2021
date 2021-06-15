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

import com.flotix.dto.MetodoPagoDTO;
import com.flotix.firebase.model.MetodoPago;
import com.flotix.firebase.service.MetodoPagoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseMetodoPago;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/metodopago/")
@CrossOrigin("*")
public class MetodoPagoRestController {

	private static Logger logger = Logger.getLogger(MetodoPagoRestController.class);

	@Autowired
	private MetodoPagoServiceAPI metodoPagoServiceAPI;

	@GetMapping(value = "/all")
	public ServerResponseMetodoPago getAll() {

		logger.info("MetodoPagoRestController - getAll");

		ServerResponseMetodoPago result = new ServerResponseMetodoPago();

		try {

			result.setListaMetodoPago(metodoPagoServiceAPI.getAll("nombre"));
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
	public ServerResponseMetodoPago find(@PathVariable String id) {

		logger.info("MetodoPagoRestController - find");

		ServerResponseMetodoPago result = new ServerResponseMetodoPago();

		try {

			MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(id);

			if (metodoPago != null) {

				List<MetodoPagoDTO> lista = new ArrayList<MetodoPagoDTO>();
				lista.add(metodoPago);

				result.setListaMetodoPago(lista);
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
	public ServerResponseMetodoPago save(@RequestBody MetodoPago metodoPago, @PathVariable String id) {

		logger.info("MetodoPagoRestController - save");

		ServerResponseMetodoPago result = new ServerResponseMetodoPago();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				metodoPagoServiceAPI.save(metodoPago);

				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				MetodoPagoDTO metodoPagoDTO = metodoPagoServiceAPI.get(id);

				if (metodoPagoDTO != null) {

					metodoPagoServiceAPI.save(metodoPago, id);

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
