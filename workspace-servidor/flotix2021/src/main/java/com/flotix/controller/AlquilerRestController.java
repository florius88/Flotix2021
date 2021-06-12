package com.flotix.controller;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.Date;
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

import com.flotix.dto.AlquilerDTO;
import com.flotix.dto.ClienteDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Alquiler;
import com.flotix.firebase.model.Vehiculo;
import com.flotix.firebase.service.AlquilerServiceAPI;
import com.flotix.firebase.service.ClienteServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseAlquiler;
import com.flotix.utils.MessageExceptions;

/**
 * Controlador que gestiona los alquileres
 * 
 * @author Flor
 *
 */
@RestController
@RequestMapping(value = "/api/alquiler/")
@CrossOrigin("*")
public class AlquilerRestController {

	@Autowired
	private AlquilerServiceAPI alquilerServiceAPI;

	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;

	@Autowired
	private ClienteServiceAPI clienteServiceAPI;

	private boolean ASC = true;
	private boolean DESC = false;

	/**
	 * Devuelve los datos con los filtros: Cliente, Matricula y Periodo
	 * 
	 * @param cliente
	 * @param matricula
	 * @param periodo
	 * @return ServerResponseAlquiler
	 */
	@GetMapping(value = "/allFilter/{cliente}/{matricula}/{periodo}")
	public ServerResponseAlquiler getAllFilter(@PathVariable String cliente, @PathVariable String matricula,
			@PathVariable String periodo) {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			List<AlquilerDTO> listaResult = new ArrayList<AlquilerDTO>();
			List<AlquilerDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(periodo)) {
				// TODO PERIODO?????????????
				// listaBD = alquilerServiceAPI.getAllFiltro1("", periodo, "fechaFin");
				listaBD = alquilerServiceAPI.getAll("fechaFin");

				listaBD = orderByFechaFin(listaBD, ASC);

				if (null != listaBD) {
					for (AlquilerDTO alquiler : listaBD) {
						// Busca el vehiculo
						if (null != alquiler.getIdVehiculo() && !alquiler.getIdVehiculo().isEmpty()) {
							VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
							alquiler.setVehiculo(vehiculo);
						}
						// Busca el cliente
						if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
							ClienteDTO clienteBD = clienteServiceAPI.get(alquiler.getIdCliente());
							alquiler.setCliente(clienteBD);
						}
					}
				}

			} else {
				listaBD = alquilerServiceAPI.getAll("fechaFin");

				listaBD = orderByFechaFin(listaBD, ASC);

				if (null != listaBD) {
					for (AlquilerDTO alquiler : listaBD) {
						// Busca el vehiculo
						if (null != alquiler.getIdVehiculo() && !alquiler.getIdVehiculo().isEmpty()) {
							VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
							alquiler.setVehiculo(vehiculo);
						}
						// Busca el cliente
						if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
							ClienteDTO clienteBD = clienteServiceAPI.get(alquiler.getIdCliente());
							alquiler.setCliente(clienteBD);
						}
					}
				}
			}

			if (!"null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream()
						.filter(alquiler -> alquiler.getCliente().getNombre().contains(cliente)
								&& alquiler.getVehiculo().getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else if (!"null".equalsIgnoreCase(cliente) && "null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alquiler -> alquiler.getCliente().getNombre().contains(cliente))
						.collect(Collectors.toList());
			} else if ("null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream()
						.filter(alquiler -> alquiler.getVehiculo().getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			result.setListaAlquiler(listaResult);
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
	 * @return ServerResponseAlquiler
	 */
	@GetMapping(value = "/all")
	public ServerResponseAlquiler getAll() {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			List<AlquilerDTO> listaBD = alquilerServiceAPI.getAll("fechaFin");

			listaBD = orderByFechaFin(listaBD, ASC);

			if (null != listaBD) {
				for (AlquilerDTO alquiler : listaBD) {
					// Busca el vehiculo
					if (null != alquiler.getIdVehiculo() && !alquiler.getIdVehiculo().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
						alquiler.setVehiculo(vehiculo);
					}
					// Busca el cliente
					if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
						ClienteDTO cliente = clienteServiceAPI.get(alquiler.getIdCliente());
						alquiler.setCliente(cliente);
					}
				}
			}

			result.setListaAlquiler(listaBD);
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
	 * @return ServerResponseAlquiler
	 */
	@GetMapping(value = "/find/{id}")
	public ServerResponseAlquiler find(@PathVariable String id) {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			AlquilerDTO alquiler = alquilerServiceAPI.get(id);

			if (alquiler != null) {
				// Busca el vehiculo
				if (null != alquiler.getIdVehiculo() && !alquiler.getIdVehiculo().isEmpty()) {
					VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
					alquiler.setVehiculo(vehiculo);
				}
				// Busca el cliente
				if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
					ClienteDTO cliente = clienteServiceAPI.get(alquiler.getIdCliente());
					alquiler.setCliente(cliente);
				}

				List<AlquilerDTO> lista = new ArrayList<AlquilerDTO>();
				lista.add(alquiler);

				result.setListaAlquiler(lista);
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

	@GetMapping(value = "/findvehiculo/{id}")
	public ServerResponseAlquiler getFindByMatricula(@PathVariable String id) {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			List<AlquilerDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(id)) {
				listaBD = alquilerServiceAPI.getAllFiltro1("idVehiculo", id, "fechaFin");

				listaBD = orderByFechaFin(listaBD, ASC);

				if (null != listaBD && !listaBD.isEmpty()) {
					for (AlquilerDTO alquiler : listaBD) {
						// Busca el vehiculo
						if (null != alquiler.getIdVehiculo() && !alquiler.getIdVehiculo().isEmpty()) {
							VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
							alquiler.setVehiculo(vehiculo);
						}
						// Busca el cliente
						if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
							ClienteDTO clienteBD = clienteServiceAPI.get(alquiler.getIdCliente());
							alquiler.setCliente(clienteBD);
						}
					}

					result.setListaAlquiler(listaBD);
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

	/**
	 * Con el id "null" guarda un nuevo objeto y, en caso contrario, modifica el
	 * objeto de la BD
	 * 
	 * @param alquiler
	 * @param id
	 * @return ServerResponseAlquiler
	 */
	@PostMapping(value = "/save/{id}")
	public ServerResponseAlquiler save(@RequestBody Alquiler alquiler, @PathVariable String id) {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				id = alquilerServiceAPI.save(alquiler);

				// Cambia la disponibilidad del vehiculo
				VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
				vehiculoDTO.setDisponibilidad(false);
				vehiculoServiceAPI.save(transformVehiculoDTOToVehiculo(vehiculoDTO), vehiculoDTO.getId());

				result.setIdAlquiler(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				AlquilerDTO alquilerDTO = alquilerServiceAPI.get(id);

				if (alquilerDTO != null) {
					alquilerServiceAPI.save(alquiler, id);

					// Se comrprueba si el vehiculo cambia, para ponerlo disponible
					if (!alquilerDTO.getIdVehiculo().equals(alquiler.getIdVehiculo())) {
						// Cambia la disponibilidad del vehiculo
						VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(alquilerDTO.getIdVehiculo());
						vehiculoDTO.setDisponibilidad(true);
						vehiculoServiceAPI.save(transformVehiculoDTOToVehiculo(vehiculoDTO), vehiculoDTO.getId());
					}

					// Cambia la disponibilidad del vehiculo
					VehiculoDTO vehiculoDTO = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
					vehiculoDTO.setDisponibilidad(false);
					vehiculoServiceAPI.save(transformVehiculoDTOToVehiculo(vehiculoDTO), vehiculoDTO.getId());

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

	public AlquilerDTO getAlquilerByMatricula(String idVehiculo) {

		AlquilerDTO result = null;

		try {

			List<AlquilerDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(idVehiculo)) {
				listaBD = alquilerServiceAPI.getAllFiltro1("idVehiculo", idVehiculo, "fechaFin");

				listaBD = orderByFechaFin(listaBD, DESC);

				SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");

				if (null != listaBD && !listaBD.isEmpty()) {

					AlquilerDTO alquiler = listaBD.get(0);

					if (simpleDateFormat.parse(alquiler.getFechaFin()).after(new Date())) {

						// Busca el cliente
						if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
							ClienteDTO clienteBD = clienteServiceAPI.get(alquiler.getIdCliente());
							alquiler.setCliente(clienteBD);
						}

						result = alquiler;
					}

				} else {
					// LOG
					result = null;
				}
			} else {
				// LOG
				result = null;
			}

		} catch (Exception e) {
			// LOG
			result = null;
		}

		return result;
	}

	public AlquilerDTO getAlquilerByCliente(String idCliente) {

		AlquilerDTO result = null;

		try {

			List<AlquilerDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(idCliente)) {
				listaBD = alquilerServiceAPI.getAllFiltro1("idCliente", idCliente, "fechaFin");

				listaBD = orderByFechaFin(listaBD, DESC);

				SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");

				if (null != listaBD && !listaBD.isEmpty()) {

					AlquilerDTO alquiler = listaBD.get(0);

					if (simpleDateFormat.parse(alquiler.getFechaFin()).after(new Date())) {
						result = alquiler;
					}

				} else {
					// LOG
					result = null;
				}
			} else {
				// LOG
				result = null;
			}

		} catch (Exception e) {
			// LOG
			result = null;
		}

		return result;
	}

	private List<AlquilerDTO> orderByFechaFin(List<AlquilerDTO> lista, boolean asc) {
		SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy");

		if (asc) {
			Comparator<AlquilerDTO> comparatortWorkflowDate = new Comparator<AlquilerDTO>() {
				@Override
				public int compare(final AlquilerDTO obj1, final AlquilerDTO obj2) {
					// Ordeno por fecha de inicio de estado
					try {
						return (simpleDateFormat.parse(obj1.getFechaFin()))
								.compareTo((simpleDateFormat.parse(obj2.getFechaFin())));
					} catch (ParseException e) {
						return 0;
					}
				}
			};
			Collections.sort(lista, comparatortWorkflowDate);
		} else {
			Comparator<AlquilerDTO> comparatortWorkflowDate = new Comparator<AlquilerDTO>() {
				@Override
				public int compare(final AlquilerDTO obj1, final AlquilerDTO obj2) {
					// Ordeno por fecha de inicio de estado
					try {
						return (simpleDateFormat.parse(obj2.getFechaFin()))
								.compareTo((simpleDateFormat.parse(obj1.getFechaFin())));
					} catch (ParseException e) {
						return 0;
					}
				}
			};
			Collections.sort(lista, comparatortWorkflowDate);
		}

		return lista;
	}

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
