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

import com.flotix.dto.CaducidadDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Caducidad;
import com.flotix.firebase.service.CaducidadServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseCaducidad;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/caducidad/")
@CrossOrigin("*")
public class CaducidadRestController {

	@Autowired
	private CaducidadServiceAPI caducidadServiceAPI;

	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;

	// TODO Filtro: VARIABLE: Matricula

	@GetMapping(value = "/all")
	public ServerResponseCaducidad getAll() {

		ServerResponseCaducidad result = new ServerResponseCaducidad();

		try {

			List<CaducidadDTO> listaResult = new ArrayList<CaducidadDTO>();
			List<CaducidadDTO> listaBD = caducidadServiceAPI.getAllNotBaja("venciminetoVehiculo");

			if (null != listaBD) {
				for (CaducidadDTO caducidad : listaBD) {
//					if (!caducidad.isBaja()) {
					// Busca el vehiculo
					if (null != caducidad.getMatricula() && !caducidad.getMatricula().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(caducidad.getMatricula());
						caducidad.setVehiculo(vehiculo);
					}

					listaResult.add(caducidad);
//					}
				}
			}

			result.setListaCaducidad(listaResult);
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
	public ServerResponseCaducidad find(@PathVariable String id) {

		ServerResponseCaducidad result = new ServerResponseCaducidad();

		try {

			CaducidadDTO caducidad = caducidadServiceAPI.get(id);

			if (caducidad != null) {

				// Busca el vehiculo
				if (null != caducidad.getMatricula() && !caducidad.getMatricula().isEmpty()) {
					VehiculoDTO vehiculo = vehiculoServiceAPI.get(caducidad.getMatricula());
					caducidad.setVehiculo(vehiculo);
				}

				List<CaducidadDTO> lista = new ArrayList<CaducidadDTO>();
				lista.add(caducidad);

				result.setListaCaducidad(lista);
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
	public ServerResponseCaducidad save(@RequestBody Caducidad caducidad, @PathVariable String id) {

		ServerResponseCaducidad result = new ServerResponseCaducidad();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				caducidad.setBaja(false);
				id = caducidadServiceAPI.save(caducidad);

				result.setIdCaducidad(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);

			} else {

				CaducidadDTO caducidadDTO = caducidadServiceAPI.get(id);

				if (caducidadDTO != null) {

					caducidadServiceAPI.save(caducidad, id);

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

	// TODO BAJA LOGICA?
	@GetMapping(value = "/delete/{id}")
	public ServerResponseCaducidad delete(@PathVariable String id) {

		ServerResponseCaducidad result = new ServerResponseCaducidad();

		try {

			CaducidadDTO caducidadDTO = caducidadServiceAPI.get(id);

			if (caducidadDTO != null) {
				// caducidadServiceAPI.delete(id);
				Caducidad caducidad = transformCaducidadDTOToCaducidad(caducidadDTO);
				caducidad.setBaja(true);
				caducidadServiceAPI.save(caducidad, id);

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

	private Caducidad transformCaducidadDTOToCaducidad(CaducidadDTO caducidadDTO) {

		Caducidad caducidad = new Caducidad();
		caducidad.setKmMantenimiento(caducidadDTO.getKmMantenimiento());
		caducidad.setProximaITV(caducidadDTO.getProximaITV());
		caducidad.setUltimaITV(caducidadDTO.getUltimaITV());
		caducidad.setVenciminetoVehiculo(caducidadDTO.getVenciminetoVehiculo());

		return caducidad;
	}
}
