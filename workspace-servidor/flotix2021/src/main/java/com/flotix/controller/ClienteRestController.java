package com.flotix.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
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

@RestController
@RequestMapping(value = "/api/cliente/")
@CrossOrigin("*")
public class ClienteRestController {

	@Autowired
	private ClienteServiceAPI clienteServiceAPI;
	
	@Autowired
	private MetodoPagoServiceAPI metodoPagoServiceAPI;
	
	@GetMapping(value = "/all")
	public List<ClienteDTO> getAll() throws Exception {
		
		List<ClienteDTO> lista = clienteServiceAPI.getAll();
		
		for (ClienteDTO cliente: lista) {
			//Busca el metodo de pago
			MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(cliente.getIdMetodoPago());
			cliente.setMetodoPago(metodoPago);
		}
		
		return lista;
	}

	@GetMapping(value = "/find/{id}")
	public ClienteDTO find(@PathVariable String id) throws Exception {
		
		ClienteDTO cliente = clienteServiceAPI.get(id);
		
		//Busca el metodo de pago
		MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(cliente.getIdMetodoPago());
		cliente.setMetodoPago(metodoPago);
		
		return cliente;
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Cliente cliente, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = clienteServiceAPI.save(cliente);
		} else {
			//TODO Comprobar si existe el ID
			clienteServiceAPI.save(cliente, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<ClienteDTO> delete(@PathVariable String id) throws Exception {
		ClienteDTO cliente = clienteServiceAPI.get(id);
		if (cliente != null) {
			clienteServiceAPI.delete(id);
		} else {
			return new ResponseEntity<ClienteDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<ClienteDTO>(cliente, HttpStatus.OK);
	}
}
