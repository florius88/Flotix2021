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

	// Filtro: VARIABLE: Matricula
	@GetMapping(value = "/allFilter/{matricula}")
	public ServerResponseCaducidad getAllFilter(@PathVariable String matricula) {

		ServerResponseCaducidad result = new ServerResponseCaducidad();

		try {

			List<CaducidadDTO> listaResult = new ArrayList<CaducidadDTO>();
			List<CaducidadDTO> listaBD = caducidadServiceAPI.getAllNotBaja("vencimientoVehiculo");

			if (null != listaBD) {
				for (CaducidadDTO caducidad : listaBD) {
					// Busca el vehiculo
					if (null != caducidad.getIdVehiculo() && !caducidad.getIdVehiculo().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(caducidad.getIdVehiculo());
						caducidad.setVehiculo(vehiculo);
					}
				}
			}

			if (!"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream()
						.filter(caducidad -> caducidad.getVehiculo().getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			result.setListaCaducidad(listaResult);
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.OK_CODE);
			error.setMessage(MessageExceptions.MSSG_OK);
			result.setError(error);

		} catch (Exception e) {
			// LOG
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@GetMapping(value = "/all")
	public ServerResponseCaducidad getAll() {

		ServerResponseCaducidad result = new ServerResponseCaducidad();

		try {

			List<CaducidadDTO> listaBD = caducidadServiceAPI.getAllNotBaja("vencimientoVehiculo");

			if (null != listaBD) {
				for (CaducidadDTO caducidad : listaBD) {
					// Busca el vehiculo
					if (null != caducidad.getIdVehiculo() && !caducidad.getIdVehiculo().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(caducidad.getIdVehiculo());
						caducidad.setVehiculo(vehiculo);
					}
				}
			}

			result.setListaCaducidad(listaBD);
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.OK_CODE);
			error.setMessage(MessageExceptions.MSSG_OK);
			result.setError(error);

		} catch (Exception e) {
			// LOG
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
				if (null != caducidad.getIdVehiculo() && !caducidad.getIdVehiculo().isEmpty()) {
					VehiculoDTO vehiculo = vehiculoServiceAPI.get(caducidad.getIdVehiculo());
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
			// LOG
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

			if (id != null && id.length() != 0) {

				CaducidadDTO caducidadDTO = caducidadServiceAPI.get(id);

				if (caducidadDTO != null) {

					caducidadServiceAPI.save(caducidad, id);

					new AlertaSegundoPlano().start();

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
			} else {
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.NOT_FOUND_CODE);
				error.setMessage(MessageExceptions.MSSG_NOT_FOUND);
				result.setError(error);
			}

		} catch (Exception e) {
			// LOG
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	public List<CaducidadDTO> getListCaducidadDTO() {

		List<CaducidadDTO> listaResult = null;

		try {

			listaResult = caducidadServiceAPI.getAllNotBaja("vencimientoVehiculo");

			if (null != listaResult) {
				for (CaducidadDTO caducidad : listaResult) {
					// Busca el vehiculo
					if (null != caducidad.getIdVehiculo() && !caducidad.getIdVehiculo().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(caducidad.getIdVehiculo());
						caducidad.setVehiculo(vehiculo);
					}
				}
			}

		} catch (Exception e) {
			// LOG
			listaResult = null;
		}

		return listaResult;
	}

	public String save(String idVehiculo) {

		String id = null;

		try {

			if (idVehiculo != null && !idVehiculo.isEmpty()) {

				Caducidad caducidad = new Caducidad();
				caducidad.setIdVehiculo(idVehiculo);
				caducidad.setBaja(false);
				id = caducidadServiceAPI.save(caducidad);
			}

		} catch (Exception e) {
			// LOG
			id = null;
		}

		return id;
	}

	// TODO BAJA LOGICA?
	public boolean delete(String idVehiculo) {

		boolean result = true;

		try {

			List<CaducidadDTO> listaCaducidadBD = caducidadServiceAPI.getAllFiltro1("idVehiculo", idVehiculo,
					"idVehiculo");

			if (listaCaducidadBD != null && !listaCaducidadBD.isEmpty()) {

				CaducidadDTO caducidadDTO = listaCaducidadBD.get(0);

				Caducidad caducidad = transformCaducidadDTOToCaducidad(caducidadDTO);
				caducidad.setBaja(true);
				caducidadServiceAPI.save(caducidad, caducidadDTO.getId());

			} else {
				result = false;
			}

		} catch (Exception e) {
			// LOG
			result = false;
		}

		return result;
	}

	private Caducidad transformCaducidadDTOToCaducidad(CaducidadDTO caducidadDTO) {

		Caducidad caducidad = new Caducidad();
		caducidad.setIdVehiculo(caducidadDTO.getIdVehiculo());
		caducidad.setProximaITV(caducidadDTO.getProximaITV());
		caducidad.setUltimaITV(caducidadDTO.getUltimaITV());
		caducidad.setVencimientoVehiculo(caducidadDTO.getVencimientoVehiculo());
		caducidad.setBaja(caducidadDTO.isBaja());

		return caducidad;
	}
}
