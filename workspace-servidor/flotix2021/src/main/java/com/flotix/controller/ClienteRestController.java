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

import com.flotix.dto.ClienteDTO;
import com.flotix.dto.MetodoPagoDTO;
import com.flotix.firebase.model.Cliente;
import com.flotix.firebase.service.ClienteServiceAPI;
import com.flotix.firebase.service.MetodoPagoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseCliente;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/cliente/")
@CrossOrigin("*")
public class ClienteRestController {

	@Autowired
	private ClienteServiceAPI clienteServiceAPI;

	@Autowired
	private MetodoPagoServiceAPI metodoPagoServiceAPI;

	// TODO Filtro: VARIABLE: NIF y Cliente
	@GetMapping(value = "/allFilter/{nif}/{empresa}")
	public ServerResponseCliente getAllFilter(@PathVariable String nif, @PathVariable String empresa) {

		ServerResponseCliente result = new ServerResponseCliente();

		try {

			List<ClienteDTO> listaResult = new ArrayList<ClienteDTO>();
			List<ClienteDTO> listaBD = clienteServiceAPI.getAllNotBaja("nif");

			if (!"null".equalsIgnoreCase(nif) && !"null".equalsIgnoreCase(empresa)) {
				listaResult = listaBD.stream()
						.filter(cliente -> cliente.getNif().contains(nif) && cliente.getNombre().contains(empresa))
						.collect(Collectors.toList());
			} else if (!"null".equalsIgnoreCase(nif) && "null".equalsIgnoreCase(empresa)) {
				listaResult = listaBD.stream().filter(cliente -> cliente.getNif().contains(nif))
						.collect(Collectors.toList());
			} else if ("null".equalsIgnoreCase(nif) && !"null".equalsIgnoreCase(empresa)) {
				listaResult = listaBD.stream().filter(cliente -> cliente.getNombre().contains(empresa))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			if (null != listaResult) {
				for (ClienteDTO cliente : listaResult) {

					// Busca el metodo de pago
					if (null != cliente.getIdMetodoPago() && !cliente.getIdMetodoPago().isEmpty()) {
						MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(cliente.getIdMetodoPago());
						cliente.setMetodoPago(metodoPago);
					}
				}
			}

			result.setListaCliente(listaResult);
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
	public ServerResponseCliente getAll() {

		ServerResponseCliente result = new ServerResponseCliente();

		try {

			List<ClienteDTO> listaResult = new ArrayList<ClienteDTO>();
			List<ClienteDTO> listaBD = clienteServiceAPI.getAllNotBaja("nif");

			if (null != listaBD) {
				for (ClienteDTO cliente : listaBD) {
//					if (!cliente.isBaja()) {
					// Busca el metodo de pago
					if (null != cliente.getIdMetodoPago() && !cliente.getIdMetodoPago().isEmpty()) {
						MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(cliente.getIdMetodoPago());
						cliente.setMetodoPago(metodoPago);
					}

					listaResult.add(cliente);
//					}
				}
			}

			result.setListaCliente(listaResult);
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
	public ServerResponseCliente find(@PathVariable String id) {

		ServerResponseCliente result = new ServerResponseCliente();

		try {

			ClienteDTO cliente = clienteServiceAPI.get(id);

			if (cliente != null) {

				// Busca el metodo de pago
				if (null != cliente.getIdMetodoPago() && !cliente.getIdMetodoPago().isEmpty()) {
					MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(cliente.getIdMetodoPago());
					cliente.setMetodoPago(metodoPago);
				}

				List<ClienteDTO> lista = new ArrayList<ClienteDTO>();
				lista.add(cliente);

				result.setListaCliente(lista);
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
	public ServerResponseCliente save(@RequestBody Cliente cliente, @PathVariable String id) {

		ServerResponseCliente result = new ServerResponseCliente();

		try {

			if (id == null || id.length() == 0 || id.equals("null")) {
				cliente.setBaja(false);
				id = clienteServiceAPI.save(cliente);

				result.setIdCliente(id);
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {

				ClienteDTO clienteDTO = clienteServiceAPI.get(id);

				if (clienteDTO != null) {
					clienteServiceAPI.save(cliente, id);

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
	public ServerResponseCliente delete(@PathVariable String id) {

		ServerResponseCliente result = new ServerResponseCliente();

		try {

			ClienteDTO clienteDTO = clienteServiceAPI.get(id);
			if (clienteDTO != null) {
				// clienteServiceAPI.delete(id);
				Cliente cliente = transformClienteDTOToCliente(clienteDTO);
				cliente.setBaja(true);
				clienteServiceAPI.save(cliente, id);

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

	private Cliente transformClienteDTOToCliente(ClienteDTO clienteDTO) {

		Cliente cliente = new Cliente();

		cliente.setCuentaBancaria(clienteDTO.getCuentaBancaria());
		cliente.setDireccion(clienteDTO.getDireccion());
		cliente.setEmail(clienteDTO.getEmail());
		cliente.setIdMetodoPago(clienteDTO.getIdMetodoPago());
		cliente.setNif(clienteDTO.getNif());
		cliente.setNombre(clienteDTO.getNombre());
		cliente.setPersonaContacto(clienteDTO.getPersonaContacto());
		cliente.setPoblacion(clienteDTO.getPoblacion());
		cliente.setTlfContacto(clienteDTO.getTlfContacto());

		return cliente;
	}
}
