package com.flotix.controller;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.log4j.Logger;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.dto.TipoAlertaDTO;
import com.flotix.firebase.model.TipoAlerta;
import com.flotix.firebase.service.TipoAlertaServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseTipoAlerta;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/tipoalerta/")
@CrossOrigin("*")
public class TipoAlertaRestController {

	private static Logger logger = Logger.getLogger(TipoAlertaRestController.class);

	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;

	@GetMapping(value = "/all")
	public ServerResponseTipoAlerta getAll() {

		logger.info("TipoAlertaRestController - getAll");

		ServerResponseTipoAlerta result = new ServerResponseTipoAlerta();

		try {

			result.setListaTipoAlerta(tipoAlertaServiceAPI.getAll("nombre"));
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
	public ServerResponseTipoAlerta find(@PathVariable String id) {

		logger.info("TipoAlertaRestController - find");

		ServerResponseTipoAlerta result = new ServerResponseTipoAlerta();

		try {

			TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(id);

			if (tipoAlerta != null) {

				List<TipoAlertaDTO> lista = new ArrayList<TipoAlertaDTO>();
				lista.add(tipoAlerta);

				result.setListaTipoAlerta(lista);
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
	public ServerResponseTipoAlerta save(@RequestBody TipoAlerta tipoAlerta, @PathVariable String id) {

		logger.info("TipoAlertaRestController - save");

		ServerResponseTipoAlerta result = new ServerResponseTipoAlerta();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				tipoAlertaServiceAPI.save(tipoAlerta);

				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				TipoAlertaDTO tipoAlertaDTO = tipoAlertaServiceAPI.get(id);

				if (tipoAlertaDTO != null) {

					tipoAlertaServiceAPI.save(tipoAlerta, id);
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

	public Map<String, String> getListTipoAlertaDTO() {

		logger.info("TipoAlertaRestController - getListTipoAlertaDTO");

		Map<String, String> mapTipoAlerta = new HashMap<String, String>();

		try {

			List<TipoAlertaDTO> listTipoAlerta = tipoAlertaServiceAPI.getAll("nombre");

			for (TipoAlertaDTO tipoAlerta : listTipoAlerta) {
				mapTipoAlerta.put(tipoAlerta.getNombre(), tipoAlerta.getId());
			}

		} catch (Exception e) {
			// LOG
			logger.error("Se ha producido un error: " + e.getMessage());
			mapTipoAlerta = null;
		}

		return mapTipoAlerta;
	}
}
