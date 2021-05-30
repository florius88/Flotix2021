package com.flotix.controller;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.dto.AlertaDTO;
import com.flotix.dto.TipoAlertaDTO;
import com.flotix.firebase.model.Alerta;
import com.flotix.firebase.service.AlertaServiceAPI;
import com.flotix.firebase.service.TipoAlertaServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseAlerta;
import com.flotix.utils.MessageExceptions;

/**
 * Controlador que gestiona las alertas
 * 
 * @author Flor
 *
 */
@RestController
@RequestMapping(value = "/api/alerta/")
@CrossOrigin("*")
public class AlertaRestController {

	@Autowired
	private AlertaServiceAPI alertaServiceAPI;

	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;

	/**
	 * Devuelve los datos con los filtros: Tipo, Cliente y Matricula
	 * 
	 * @param tipo      de alerta
	 * @param cliente
	 * @param matricula
	 * @return ServerResponseAlerta
	 */
	@GetMapping(value = "/allFilter/{tipo}/{cliente}/{matricula}")
	public ServerResponseAlerta getAllFilter(@PathVariable String tipo, @PathVariable String cliente,
			@PathVariable String matricula) {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			List<AlertaDTO> listaResult = new ArrayList<AlertaDTO>();
			List<AlertaDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(tipo)) {
				listaBD = alertaServiceAPI.getAllFiltro1("idTipoAlerta", tipo, "vencimiento");
			} else {
				listaBD = alertaServiceAPI.getAll("vencimiento");
			}

			if (!"null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alerta -> alerta.getNombreCliente().contains(cliente)
						&& alerta.getMatricula().contains(matricula)).collect(Collectors.toList());
			} else if (!"null".equalsIgnoreCase(cliente) && "null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alerta -> alerta.getNombreCliente().contains(cliente))
						.collect(Collectors.toList());
			} else if ("null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alerta -> alerta.getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			if (null != listaResult) {
				for (AlertaDTO alerta : listaResult) {

					// Busca el tipo de alerta
					if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
						TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
						alerta.setTipoAlerta(tipoAlerta);
					}
				}
			}

			result.setListaAlerta(listaResult);
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

	/**
	 * Devuelve todos los datos
	 * 
	 * @return ServerResponseAlerta
	 */
	@GetMapping(value = "/all")
	public ServerResponseAlerta getAll() {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			List<AlertaDTO> listaBD = alertaServiceAPI.getAll("vencimiento");

			if (null != listaBD) {
				for (AlertaDTO alerta : listaBD) {
					// Busca el tipo de alerta
					if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
						TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
						alerta.setTipoAlerta(tipoAlerta);
					}
				}
			}

			result.setListaAlerta(listaBD);
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

	/**
	 * Devuelve los datos con un id
	 * 
	 * @param id
	 * @return ServerResponseAlerta
	 */
	@GetMapping(value = "/find/{id}")
	public ServerResponseAlerta find(@PathVariable String id) {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			AlertaDTO alerta = alertaServiceAPI.get(id);

			if (alerta != null) {
				// Busca el tipo de alerta
				if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
					TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
					alerta.setTipoAlerta(tipoAlerta);
				}

				List<AlertaDTO> lista = new ArrayList<AlertaDTO>();
				lista.add(alerta);

				result.setListaAlerta(lista);
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

	/**
	 * Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el
	 * objeto de la BD
	 * 
	 * @param alerta objeto de BD
	 * @param id
	 * @return ServerResponseAlerta
	 */
	@PostMapping(value = "/save/{id}")
	public ServerResponseAlerta save(@RequestBody Alerta alerta, @PathVariable String id) {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				id = alertaServiceAPI.save(alerta);

				result.setIdAlerta(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);

			} else {

				AlertaDTO alertaDTO = alertaServiceAPI.get(id);

				if (alertaDTO != null) {
					alertaServiceAPI.save(alerta, id);

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

	// TODO METODO DE SERVIDOR
	@GetMapping(value = "/delete/{id}")
	public ServerResponseAlerta delete(@PathVariable String id) {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			AlertaDTO alerta = alertaServiceAPI.get(id);

			if (alerta != null) {
				alertaServiceAPI.delete(id);

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
