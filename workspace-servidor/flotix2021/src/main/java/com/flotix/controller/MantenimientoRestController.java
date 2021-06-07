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

import com.flotix.dto.MantenimientoDTO;
import com.flotix.dto.TipoMantenimientoDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Mantenimiento;
import com.flotix.firebase.service.MantenimientoServiceAPI;
import com.flotix.firebase.service.TipoMantenimientoServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseMantenimiento;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/mantenimiento/")
@CrossOrigin("*")
public class MantenimientoRestController {

	@Autowired
	private MantenimientoServiceAPI mantenimientoServiceAPI;

	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;

	@Autowired
	private TipoMantenimientoServiceAPI tipoMantenimientoServiceAPI;

	// Filtro: FIJO: Tipo y VARIABLE: Matricula
	@GetMapping(value = "/allFilter/{tipo}/{matricula}")
	public ServerResponseMantenimiento getAllFilter(@PathVariable String tipo, @PathVariable String matricula) {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			List<MantenimientoDTO> listaResult = new ArrayList<MantenimientoDTO>();
			List<MantenimientoDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(tipo)) {
				listaBD = mantenimientoServiceAPI.getAllFiltro1("idTipoMantenimiento", tipo, "idVehiculo");

				if (null != listaBD) {
					for (MantenimientoDTO mantenimiento : listaBD) {
						// Busca el vehiculo
						if (null != mantenimiento.getIdVehiculo() && !mantenimiento.getIdVehiculo().isEmpty()) {
							VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
							mantenimiento.setVehiculo(vehiculo);
						}
						// Busca el tipo de mantenimiento
						if (null != mantenimiento.getIdTipoMantenimiento()
								&& !mantenimiento.getIdTipoMantenimiento().isEmpty()) {
							TipoMantenimientoDTO tipoMantenimiento = tipoMantenimientoServiceAPI
									.get(mantenimiento.getIdTipoMantenimiento());
							mantenimiento.setTipoMantenimiento(tipoMantenimiento);
						}
					}
				}
			} else {
				listaBD = mantenimientoServiceAPI.getAllNotBaja("idVehiculo");

				if (null != listaBD) {
					for (MantenimientoDTO mantenimiento : listaBD) {
						// Busca el vehiculo
						if (null != mantenimiento.getIdVehiculo() && !mantenimiento.getIdVehiculo().isEmpty()) {
							VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
							mantenimiento.setVehiculo(vehiculo);
						}
						// Busca el tipo de mantenimiento
						if (null != mantenimiento.getIdTipoMantenimiento()
								&& !mantenimiento.getIdTipoMantenimiento().isEmpty()) {
							TipoMantenimientoDTO tipoMantenimiento = tipoMantenimientoServiceAPI
									.get(mantenimiento.getIdTipoMantenimiento());
							mantenimiento.setTipoMantenimiento(tipoMantenimiento);
						}
					}
				}
			}

			if (!"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream()
						.filter(mantenimiento -> mantenimiento.getVehiculo().getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			result.setListaMantenimiento(listaResult);
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
	public ServerResponseMantenimiento getAll() {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			List<MantenimientoDTO> listaBD = mantenimientoServiceAPI.getAllNotBaja("idVehiculo");

			if (null != listaBD) {
				for (MantenimientoDTO mantenimiento : listaBD) {
					// Busca el vehiculo
					if (null != mantenimiento.getIdVehiculo() && !mantenimiento.getIdVehiculo().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
						mantenimiento.setVehiculo(vehiculo);
					}
					// Busca el tipo de mantenimiento
					if (null != mantenimiento.getIdTipoMantenimiento()
							&& !mantenimiento.getIdTipoMantenimiento().isEmpty()) {
						TipoMantenimientoDTO tipoMantenimiento = tipoMantenimientoServiceAPI
								.get(mantenimiento.getIdTipoMantenimiento());
						mantenimiento.setTipoMantenimiento(tipoMantenimiento);
					}
				}
			}

			result.setListaMantenimiento(listaBD);
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
	public ServerResponseMantenimiento find(@PathVariable String id) {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			MantenimientoDTO mantenimiento = mantenimientoServiceAPI.get(id);

			if (mantenimiento != null) {

				// Busca el vehiculo
				if (null != mantenimiento.getIdVehiculo() && !mantenimiento.getIdVehiculo().isEmpty()) {
					VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
					mantenimiento.setVehiculo(vehiculo);
				}
				// Busca el tipo de mantenimiento
				if (null != mantenimiento.getIdTipoMantenimiento()
						&& !mantenimiento.getIdTipoMantenimiento().isEmpty()) {
					TipoMantenimientoDTO tipoMantenimiento = tipoMantenimientoServiceAPI
							.get(mantenimiento.getIdTipoMantenimiento());
					mantenimiento.setTipoMantenimiento(tipoMantenimiento);
				}

				List<MantenimientoDTO> lista = new ArrayList<MantenimientoDTO>();
				lista.add(mantenimiento);

				result.setListaMantenimiento(lista);
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
	public ServerResponseMantenimiento save(@RequestBody Mantenimiento mantenimiento, @PathVariable String id) {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				mantenimiento.setBaja(false);
				id = mantenimientoServiceAPI.save(mantenimiento);

				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);

			} else {

				MantenimientoDTO mantenimientoDTO = mantenimientoServiceAPI.get(id);

				if (mantenimientoDTO != null) {

					mantenimientoServiceAPI.save(mantenimiento, id);

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
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	public List<MantenimientoDTO> getAllListMantenimientoDTO() {

		List<MantenimientoDTO> listaResult = new ArrayList<MantenimientoDTO>();

		try {

			List<MantenimientoDTO> listaBD = mantenimientoServiceAPI.getAllNotBaja("idVehiculo");

			if (null != listaBD) {
				for (MantenimientoDTO mantenimiento : listaBD) {
					// Busca el vehiculo
					if (null != mantenimiento.getIdVehiculo() && !mantenimiento.getIdVehiculo().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
						mantenimiento.setVehiculo(vehiculo);
					}
					// Busca el tipo de mantenimiento
					if (null != mantenimiento.getIdTipoMantenimiento()
							&& !mantenimiento.getIdTipoMantenimiento().isEmpty()) {
						TipoMantenimientoDTO tipoMantenimiento = tipoMantenimientoServiceAPI
								.get(mantenimiento.getIdTipoMantenimiento());
						mantenimiento.setTipoMantenimiento(tipoMantenimiento);
					}

					listaResult.add(mantenimiento);
				}
			}

		} catch (Exception e) {
			// LOG
			listaResult = null;
		}

		return listaResult;
	}

	// TODO BAJA LOGICA?
	public boolean delete(String idVehiculo) {

		boolean result = true;

		try {

			List<MantenimientoDTO> listaMantenimientoBD = mantenimientoServiceAPI.getAllFiltro1("idVehiculo",
					idVehiculo, "idVehiculo");

			if (listaMantenimientoBD != null && !listaMantenimientoBD.isEmpty()) {

				MantenimientoDTO mantenimientoDTO = listaMantenimientoBD.get(0);

				Mantenimiento mantenimiento = transformMantenimientoDTOToMantenimiento(mantenimientoDTO);
				mantenimiento.setBaja(true);
				mantenimientoServiceAPI.save(mantenimiento, mantenimientoDTO.getId());

			} else {
				result = false;
			}

		} catch (Exception e) {
			// LOG
			result = false;
		}

		return result;
	}

	private Mantenimiento transformMantenimientoDTOToMantenimiento(MantenimientoDTO mantenimientoDTO) {

		Mantenimiento mantenimiento = new Mantenimiento();
		mantenimiento.setIdTipoMantenimiento(mantenimientoDTO.getIdTipoMantenimiento());
		mantenimiento.setIdVehiculo(mantenimientoDTO.getIdVehiculo());
		mantenimiento.setKmMantenimiento(mantenimientoDTO.getKmMantenimiento());
		mantenimiento.setProximoMantenimiento(mantenimientoDTO.getProximoMantenimiento());
		mantenimiento.setUltimoMantenimiento(mantenimientoDTO.getUltimoMantenimiento());

		return mantenimiento;
	}
}
