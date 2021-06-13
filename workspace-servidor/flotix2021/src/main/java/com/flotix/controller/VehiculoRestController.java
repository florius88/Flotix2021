package com.flotix.controller;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
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
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.ImagenVehiculo;
import com.flotix.firebase.model.Vehiculo;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseImagenVehiculo;
import com.flotix.response.bean.ServerResponseVehiculo;
import com.flotix.utils.MessageExceptions;
import com.flotix.utils.SpringUtils;

/**
 * Controlador que gestiona los vehiculos
 * 
 * @author Flor
 *
 */
@RestController
@RequestMapping(value = "/api/vehiculo/")
@CrossOrigin("*")
public class VehiculoRestController {

	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;

	/**
	 * Devuelve los datos con los filtros: Matricula, plazas, capacidad y
	 * disponibilidad
	 * 
	 * @param matricula
	 * @param plazas
	 * @param capacidad
	 * @param dispoiblilidad
	 * @return
	 */
	@GetMapping(value = "/allFilter/{matricula}/{plazas}/{capacidad}/{dispoiblilidad}")
	public ServerResponseVehiculo getAllFilter(@PathVariable String matricula, @PathVariable String plazas,
			@PathVariable String capacidad, @PathVariable String dispoiblilidad) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			List<VehiculoDTO> listaResult = new ArrayList<VehiculoDTO>();

			List<VehiculoDTO> listaBD = null;

			boolean plazasVacio = false;
			boolean capacidadVacio = false;
			boolean dispoiblilidadVacio = false;
			int contFiltro = 0;

			if ("null".equalsIgnoreCase(plazas)) {
				plazasVacio = true;
			} else {
				contFiltro++;
			}
			if ("null".equalsIgnoreCase(capacidad)) {
				capacidadVacio = true;
			} else {
				contFiltro++;
			}
			if ("null".equalsIgnoreCase(dispoiblilidad)) {
				dispoiblilidadVacio = true;
			} else {
				contFiltro++;
			}

			String filtro1 = null;
			Object valueFiltro1 = null;
			String filtro2 = null;
			Object valueFiltro2 = null;

			switch (contFiltro) {
			case 0:
				listaBD = vehiculoServiceAPI.getAllNotBaja("matricula");
				break;
			case 1:

				if (!plazasVacio) {
					filtro1 = "plazas";
					valueFiltro1 = Integer.valueOf(plazas);
				} else if (!capacidadVacio) {
					filtro1 = "capacidad";
					valueFiltro1 = Integer.valueOf(capacidad);
				} else if (!dispoiblilidadVacio) {
					filtro1 = "disponibilidad";
					valueFiltro1 = Boolean.valueOf(dispoiblilidad);
				}

				listaBD = vehiculoServiceAPI.getAllFiltro1(filtro1, valueFiltro1, "matricula");

				break;
			case 2:

				if (!plazasVacio) {
					filtro1 = "plazas";
					valueFiltro1 = Integer.valueOf(plazas);
				}
				if (!capacidadVacio) {
					if (null == filtro1) {
						filtro1 = "capacidad";
						valueFiltro1 = Integer.valueOf(capacidad);
					} else {
						filtro2 = "capacidad";
						valueFiltro2 = Integer.valueOf(capacidad);
					}
				}
				if (!dispoiblilidadVacio) {
					filtro2 = "disponibilidad";
					valueFiltro2 = Boolean.valueOf(dispoiblilidad);
				}

				listaBD = vehiculoServiceAPI.getAllFiltro2(filtro1, valueFiltro1, filtro2, valueFiltro2, "matricula");

				break;
			case 3:

				listaBD = vehiculoServiceAPI.getAllFiltro3("plazas", Integer.valueOf(plazas), "capacidad",
						Integer.valueOf(capacidad), "disponibilidad", Boolean.valueOf(dispoiblilidad), "matricula");
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
			// LOG
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
	 * @return
	 */
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
			// LOG
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
	 * @return
	 */
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
			// LOG
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	/**
	 * Devuelve los datos sin mantenimiento
	 * 
	 * @return
	 */
	@GetMapping(value = "/findnomantenimiento")
	public ServerResponseVehiculo findNoMantenimiento() {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			List<VehiculoDTO> listaResult = new ArrayList<VehiculoDTO>();

			MantenimientoRestController mantenimientoRestController = (MantenimientoRestController) SpringUtils.ctx
					.getBean(MantenimientoRestController.class);

			List<MantenimientoDTO> listaMantenimiento = mantenimientoRestController.getAllListMantenimientoDTO();

			List<VehiculoDTO> listaVehiculoBD = vehiculoServiceAPI.getAllNotBaja("matricula");

			if (null != listaMantenimiento && !listaMantenimiento.isEmpty()) {

				Map<String, MantenimientoDTO> mapMantenimiento = new HashMap<String, MantenimientoDTO>();

				for (MantenimientoDTO mantenimiento : listaMantenimiento) {
					mapMantenimiento.put(mantenimiento.getIdVehiculo(), mantenimiento);
				}

				for (VehiculoDTO vehiculo : listaVehiculoBD) {
					if (null == mapMantenimiento.get(vehiculo.getId())) {
						listaResult.add(vehiculo);
					}
				}

			} else {
				listaResult = listaVehiculoBD;
			}

			result.setListaVehiculo(listaResult);
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

	/**
	 * Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el
	 * objeto de la BD
	 * 
	 * @param vehiculo
	 * @param id
	 * @return
	 */
	@PostMapping(value = "/save/{id}")
	public ServerResponseVehiculo save(@RequestBody Vehiculo vehiculo, @PathVariable String id) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				vehiculo.setDisponibilidad(true);
				vehiculo.setBaja(false);
				id = vehiculoServiceAPI.save(vehiculo);

				CaducidadRestController caducidadRestController = (CaducidadRestController) SpringUtils.ctx
						.getBean(CaducidadRestController.class);
				String idCaducidad = caducidadRestController.save(id);

				if (null == idCaducidad) {
					ErrorBean error = new ErrorBean();
					error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
					error.setMessage(MessageExceptions.MSSG_ERROR_SAVE_VEHICULO);
					result.setError(error);
				} else {
					result.setIdVehiculo(id);
					ErrorBean error = new ErrorBean();
					error.setCode(MessageExceptions.OK_CODE);
					error.setMessage(MessageExceptions.MSSG_OK);
					result.setError(error);
				}
			} else {

				VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(id);

				if (vehiculoDTO != null) {

					AlquilerRestController alquilerRestController = (AlquilerRestController) SpringUtils.ctx
							.getBean(AlquilerRestController.class);

					if (null == alquilerRestController.getAlquilerByMatricula(id)) {

						vehiculoServiceAPI.save(vehiculo, id);

						ErrorBean error = new ErrorBean();
						error.setCode(MessageExceptions.OK_CODE);
						error.setMessage(MessageExceptions.MSSG_OK);
						result.setError(error);

					} else {
						ErrorBean error = new ErrorBean();
						error.setCode(MessageExceptions.NOT_MODIF_VEHICULO_CODE);
						error.setMessage(MessageExceptions.MSSG_ERROR_NOT_MODIF_VEHICULO);
						result.setError(error);
					}
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

	/**
	 * Guarda una imagen
	 * 
	 * @param imagenVehiculo
	 * @return
	 */
	@PostMapping(value = "/savedoc")
	public ServerResponseImagenVehiculo saveDocument(@RequestBody ImagenVehiculo imagenVehiculo) {

		ServerResponseImagenVehiculo result = new ServerResponseImagenVehiculo();

		try {

			String id = vehiculoServiceAPI.saveDocument(imagenVehiculo);

			result.setIdImagenVehiculo(id);
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

	/**
	 * Devuelve los datos con el id de la imagen
	 * 
	 * @param id
	 * @return
	 */
	@GetMapping(value = "/finddoc/{id}")
	public ServerResponseImagenVehiculo findDocument(@PathVariable String id) {

		ServerResponseImagenVehiculo result = new ServerResponseImagenVehiculo();

		try {

			byte[] docVehiculo = vehiculoServiceAPI.getDocument(id);

			if (docVehiculo != null) {

				ImagenVehiculo imagenVehiculo = new ImagenVehiculo();
				imagenVehiculo.setNombreImagen(id);
				imagenVehiculo.setDocumento(docVehiculo);

				result.setImagenVehiculo(imagenVehiculo);
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

	/**
	 * Baja logica del objeto y sus asociados
	 * 
	 * @param id
	 * @return
	 */
	@GetMapping(value = "/delete/{id}")
	public ServerResponseVehiculo delete(@PathVariable String id) {

		ServerResponseVehiculo result = new ServerResponseVehiculo();

		try {

			VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(id);

			if (vehiculoDTO != null) {

				AlquilerRestController alquilerRestController = (AlquilerRestController) SpringUtils.ctx
						.getBean(AlquilerRestController.class);

				if (null == alquilerRestController.getAlquilerByMatricula(id)) {

					Vehiculo vehiculo = transformVehiculoDTOToVehiculo(vehiculoDTO);
					vehiculo.setBaja(true);
					vehiculoServiceAPI.save(vehiculo, id);

					CaducidadRestController caducidadRestController = (CaducidadRestController) SpringUtils.ctx
							.getBean(CaducidadRestController.class);
					MantenimientoRestController mantenimientoRestController = (MantenimientoRestController) SpringUtils.ctx
							.getBean(MantenimientoRestController.class);

					boolean resultDeleteCaducidad = caducidadRestController.delete(id);
					boolean resultDeleteMantenimiento = mantenimientoRestController.delete(id);

					if (!resultDeleteCaducidad || !resultDeleteMantenimiento) {
						ErrorBean error = new ErrorBean();
						error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
						error.setMessage(MessageExceptions.MSSG_ERROR_DELETE_VEHICULO);
						result.setError(error);
					} else {

						new AlertaSegundoPlano().start();

						ErrorBean error = new ErrorBean();
						error.setCode(MessageExceptions.OK_CODE);
						error.setMessage(MessageExceptions.MSSG_OK);
						result.setError(error);
					}

				} else {
					ErrorBean error = new ErrorBean();
					error.setCode(MessageExceptions.NOT_MODIF_VEHICULO_CODE);
					error.setMessage(MessageExceptions.MSSG_ERROR_NOT_MODIF_VEHICULO);
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

	/**
	 * Parseo de DTO a modelo
	 * 
	 * @param vehiculoDTO
	 * @return
	 */
	private Vehiculo transformVehiculoDTOToVehiculo(VehiculoDTO vehiculoDTO) {

		Vehiculo vehiculo = new Vehiculo();

		vehiculo.setCapacidad(vehiculoDTO.getCapacidad());
		vehiculo.setFechaMatriculacion(vehiculoDTO.getFechaMatriculacion());
		vehiculo.setKm(vehiculoDTO.getKm());
		vehiculo.setMatricula(vehiculoDTO.getMatricula());
		vehiculo.setModelo(vehiculoDTO.getModelo());
		vehiculo.setPlazas(vehiculoDTO.getPlazas());
		vehiculo.setDisponibilidad(vehiculoDTO.isDisponibilidad());
		vehiculo.setBaja(vehiculoDTO.isBaja());
		vehiculo.setNombreImagen(vehiculoDTO.getNombreImagen());
		vehiculo.setNombreImagenPermiso(vehiculoDTO.getNombreImagenPermiso());

		return vehiculo;
	}
}
