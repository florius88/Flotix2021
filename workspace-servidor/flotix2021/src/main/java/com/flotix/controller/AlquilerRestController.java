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

import com.flotix.dto.AlquilerDTO;
import com.flotix.dto.ClienteDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Alquiler;
import com.flotix.firebase.service.AlquilerServiceAPI;
import com.flotix.firebase.service.ClienteServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;

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
	
	@GetMapping(value = "/all")
	public List<AlquilerDTO> getAll() throws Exception {
		
		List<AlquilerDTO> lista = alquilerServiceAPI.getAll();
		
		for (AlquilerDTO alquiler: lista) {
			//Busca el vehiculo
			VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
			alquiler.setVehiculo(vehiculo);
			//Busca el cliente
			ClienteDTO cliente = clienteServiceAPI.get(alquiler.getIdCliente());
			alquiler.setCliente(cliente);
		}
		
		return lista;
	}

	@GetMapping(value = "/find/{id}")
	public AlquilerDTO find(@PathVariable String id) throws Exception {
		
		AlquilerDTO alquiler = alquilerServiceAPI.get(id);
		
		//Busca el vehiculo
		VehiculoDTO vehiculo = vehiculoServiceAPI.get(alquiler.getIdVehiculo());
		alquiler.setVehiculo(vehiculo);
		//Busca el cliente
		ClienteDTO cliente = clienteServiceAPI.get(alquiler.getIdCliente());
		alquiler.setCliente(cliente);
		
		return alquiler;
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Alquiler alquiler, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = alquilerServiceAPI.save(alquiler);
		} else {
			alquilerServiceAPI.save(alquiler, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<AlquilerDTO> delete(@PathVariable String id) throws Exception {
		AlquilerDTO alquiler = alquilerServiceAPI.get(id);
		if (alquiler != null) {
			alquilerServiceAPI.delete(id);
		} else {
			return new ResponseEntity<AlquilerDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<AlquilerDTO>(alquiler, HttpStatus.OK);
	}
}
