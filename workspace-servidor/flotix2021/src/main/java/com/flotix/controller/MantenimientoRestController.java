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

	// TODO Filtro: FIJO: Tipo y VARIABLE: Matricula

	@GetMapping(value = "/all")
	public ServerResponseMantenimiento getAll() {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			List<MantenimientoDTO> listaResult = new ArrayList<MantenimientoDTO>();
			List<MantenimientoDTO> listaBD = mantenimientoServiceAPI.getAllNotBaja("matricula");

			if (null != listaBD) {
				for (MantenimientoDTO mantenimiento : listaBD) {
//					if (!mantenimiento.isBaja()) {
					// Busca el vehiculo
					if (null != mantenimiento.getMatricula() && !mantenimiento.getMatricula().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getMatricula());
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
//					}
				}
			}

			result.setListaMantenimiento(listaResult);
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
	public ServerResponseMantenimiento find(@PathVariable String id) {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			MantenimientoDTO mantenimiento = mantenimientoServiceAPI.get(id);

			if (mantenimiento != null) {

				// Busca el vehiculo
				if (null != mantenimiento.getMatricula() && !mantenimiento.getMatricula().isEmpty()) {
					VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getMatricula());
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
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	// TODO BAJA LOGICA?
	@GetMapping(value = "/delete/{id}")
	public ServerResponseMantenimiento delete(@PathVariable String id) {

		ServerResponseMantenimiento result = new ServerResponseMantenimiento();

		try {

			MantenimientoDTO mantenimientoDTO = mantenimientoServiceAPI.get(id);
			if (mantenimientoDTO != null) {
				// mantenimientoServiceAPI.delete(id);
				Mantenimiento mantenimiento = transformMantenimientoDTOToMantenimiento(mantenimientoDTO);
				mantenimiento.setBaja(true);
				mantenimientoServiceAPI.save(mantenimiento, id);

				result.setIdMantenimiento(id);
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

	private Mantenimiento transformMantenimientoDTOToMantenimiento(MantenimientoDTO mantenimientoDTO) {

		Mantenimiento mantenimiento = new Mantenimiento();
		mantenimiento.setIdTipoMantenimiento(mantenimientoDTO.getIdTipoMantenimiento());
		mantenimiento.setMatricula(mantenimientoDTO.getMatricula());
		mantenimiento.setKmMantenimiento(mantenimientoDTO.getKmMantenimiento());
		mantenimiento.setProximoMantenimiento(mantenimientoDTO.getProximoMantenimiento());
		mantenimiento.setUltimoMantenimiento(mantenimientoDTO.getUltimoMantenimiento());

		return mantenimiento;
	}
}
