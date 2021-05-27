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

import com.flotix.dto.AlquilerDTO;
import com.flotix.dto.ClienteDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Alquiler;
import com.flotix.firebase.service.AlquilerServiceAPI;
import com.flotix.firebase.service.ClienteServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseAlquiler;
import com.flotix.utils.MessageExceptions;

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

	// TODO Filtro: VARIABLE: Cliente, Matricula y FIJO: Periodo
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
			} else {
				listaBD = alquilerServiceAPI.getAll("fechaFin");
			}

			if (!"null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alquiler -> alquiler.getCliente().getNombre().contains(cliente)
						&& alquiler.getMatricula().contains(matricula)).collect(Collectors.toList());
			} else if (!"null".equalsIgnoreCase(cliente) && "null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alquiler -> alquiler.getCliente().getNombre().contains(cliente))
						.collect(Collectors.toList());
			} else if ("null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alquiler -> alquiler.getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			if (null != listaResult) {
				for (AlquilerDTO alquiler : listaResult) {
					// Busca el vehiculo
					if (null != alquiler.getMatricula() && !alquiler.getMatricula().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getMatricula());
						alquiler.setVehiculo(vehiculo);
					}
					// Busca el cliente
					if (null != alquiler.getIdCliente() && !alquiler.getIdCliente().isEmpty()) {
						ClienteDTO clienteBD = clienteServiceAPI.get(alquiler.getIdCliente());
						alquiler.setCliente(clienteBD);
					}
				}
			}

			result.setListaAlquiler(listaResult);
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
	public ServerResponseAlquiler getAll() {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			List<AlquilerDTO> listaBD = alquilerServiceAPI.getAll("fechaFin");

			if (null != listaBD) {
				for (AlquilerDTO alquiler : listaBD) {
					// Busca el vehiculo
					if (null != alquiler.getMatricula() && !alquiler.getMatricula().isEmpty()) {
						VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getMatricula());
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
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@GetMapping(value = "/find/{id}")
	public ServerResponseAlquiler find(@PathVariable String id) {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			AlquilerDTO alquiler = alquilerServiceAPI.get(id);

			if (alquiler != null) {
				// Busca el vehiculo
				if (null != alquiler.getMatricula() && !alquiler.getMatricula().isEmpty()) {
					VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getMatricula());
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
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	@PostMapping(value = "/save/{id}")
	public ServerResponseAlquiler save(@RequestBody Alquiler alquiler, @PathVariable String id) {

		ServerResponseAlquiler result = new ServerResponseAlquiler();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				id = alquilerServiceAPI.save(alquiler);

				result.setIdAlquiler(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				AlquilerDTO alquilerDTO = alquilerServiceAPI.get(id);

				if (alquilerDTO != null) {
					alquilerServiceAPI.save(alquiler, id);

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
}
