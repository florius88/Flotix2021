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

import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Vehiculo;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseVehiculo;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/vehiculo/")
@CrossOrigin("*")
public class VehiculoRestController {

	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;

	// TODO Filtro: VARIABLE: Matricula, FIJO: Plazas. Tamaño y Dispoiblilidad
	@GetMapping(value = "/allFilter/{matricula}/{plazas}/{tam}/{dispoiblilidad}")
	public ServerResponseVehiculo getAllFilter(@PathVariable String matricula, @PathVariable String plazas,
			@PathVariable String tam, @PathVariable String dispoiblilidad) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			List<VehiculoDTO> listaResult = new ArrayList<VehiculoDTO>();

			List<VehiculoDTO> listaBD = null;

			boolean plazasVacio = false;
			boolean tamVacio = false;
			boolean dispoiblilidadVacio = false;
			int contFiltro = 0;

			if ("null".equalsIgnoreCase(plazas)) {
				plazasVacio = true;
			} else {
				contFiltro++;
			}
			if ("null".equalsIgnoreCase(tam)) {
				tamVacio = true;
			} else {
				contFiltro++;
			}
			if ("null".equalsIgnoreCase(dispoiblilidad)) {
				dispoiblilidadVacio = true;
			} else {
				contFiltro++;
			}

			String filtro1 = null;
			String valueFiltro1 = null;
			String filtro2 = null;
			String valueFiltro2 = null;

			switch (contFiltro) {
			case 0:
				listaBD = vehiculoServiceAPI.getAllNotBaja("matricula");
				break;
			case 1:

				if (!plazasVacio) {
					filtro1 = "plazas";
					valueFiltro1 = plazas;
				} else if (!tamVacio) {
					filtro1 = "capacidad";
					valueFiltro1 = tam;
				} else if (!dispoiblilidadVacio) {
					filtro1 = "disponibilidad";
					valueFiltro1 = dispoiblilidad;
				}

				listaBD = vehiculoServiceAPI.getAllFiltro1(filtro1, valueFiltro1, "matricula");

				break;
			case 2:

				if (!plazasVacio) {
					filtro1 = "plazas";
					valueFiltro1 = plazas;
				}
				if (!tamVacio) {
					if (null == filtro1) {
						filtro1 = "capacidad";
						valueFiltro1 = tam;
					} else {
						filtro2 = "capacidad";
						valueFiltro2 = tam;
					}
				}
				if (!dispoiblilidadVacio) {
					filtro2 = "disponibilidad";
					valueFiltro2 = dispoiblilidad;
				}

				listaBD = vehiculoServiceAPI.getAllFiltro2(filtro1, valueFiltro1, filtro2, valueFiltro2);

				break;
			case 3:

				listaBD = vehiculoServiceAPI.getAllFiltro3("plazas", plazas, "capacidad", tam, "disponibilidad",
						dispoiblilidad, "matricula");
				break;

			}

			if (!"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(vehiculo -> vehiculo.getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			result.setListaVehiculo(listaResult);
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

	@GetMapping(value = "/all")
	public ServerResponseVehiculo getAll() {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			result.setListaVehiculo(vehiculoServiceAPI.getAllNotBaja("matricula"));
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
	public ServerResponseVehiculo find(@PathVariable String id) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			VehiculoDTO vehiculo = vehiculoServiceAPI.get(id);

			if (vehiculo != null) {
				List<VehiculoDTO> lista = new ArrayList<VehiculoDTO>();
				lista.add(vehiculo);

				result.setListaVehiculo(lista);
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
	public ServerResponseVehiculo save(@RequestBody Vehiculo vehiculo, @PathVariable String id) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				vehiculo.setDisponibilidad(true);
				vehiculo.setBaja(false);
				id = vehiculoServiceAPI.save(vehiculo);

				result.setIdVehiculo(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(id);

				if (vehiculoDTO != null) {

					vehiculoServiceAPI.save(vehiculo, id);

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

	// TODO BAJA LOGICA
	@GetMapping(value = "/delete/{id}")
	public ServerResponseVehiculo delete(@PathVariable String id) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(id);

			if (vehiculoDTO != null) {
				// vehiculoServiceAPI.delete(id);
				Vehiculo vehiculo = transformVehiculoDTOToVehiculo(vehiculoDTO);
				vehiculo.setBaja(true);
				vehiculoServiceAPI.save(vehiculo, id);

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

	private Vehiculo transformVehiculoDTOToVehiculo(VehiculoDTO vehiculoDTO) {

		Vehiculo vehiculo = new Vehiculo();

		vehiculo.setCapacidad(vehiculoDTO.getCapacidad());
		vehiculo.setFechaMatriculacion(vehiculoDTO.getFechaMatriculacion());
		vehiculo.setKm(vehiculoDTO.getKm());
		vehiculo.setMatricula(vehiculoDTO.getMatricula());
		vehiculo.setModelo(vehiculoDTO.getModelo());
		vehiculo.setPlazas(vehiculoDTO.getPlazas());

		return vehiculo;
	}
}
