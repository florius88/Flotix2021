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

import com.flotix.dto.CaducidadDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Vehiculo;
import com.flotix.firebase.service.CaducidadServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;

@RestController
@RequestMapping(value = "/api/vehiculo/")
@CrossOrigin("*")
public class VehiculoRestController {

	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;
	
	@Autowired
	private CaducidadServiceAPI caducidadServiceAPI;
	
	@GetMapping(value = "/all")
	public List<VehiculoDTO> getAll() throws Exception {
		
		List<VehiculoDTO> lista = vehiculoServiceAPI.getAll();
		
		for (VehiculoDTO vehiculo: lista) {
			//Busca la caducidad
			CaducidadDTO caducidad = caducidadServiceAPI.get(vehiculo.getIdCaducidad());
			vehiculo.setCaducidad(caducidad);
		}
		
		return lista;
	}

	@GetMapping(value = "/find/{id}")
	public VehiculoDTO find(@PathVariable String id) throws Exception {
		
		VehiculoDTO vehiculo = vehiculoServiceAPI.get(id);
		
		//Busca la caducidad
		CaducidadDTO caducidad = caducidadServiceAPI.get(vehiculo.getIdCaducidad());
		vehiculo.setCaducidad(caducidad);
		
		return vehiculo;
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Vehiculo vehiculo, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = vehiculoServiceAPI.save(vehiculo);
		} else {
			vehiculoServiceAPI.save(vehiculo, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<VehiculoDTO> delete(@PathVariable String id) throws Exception {
		VehiculoDTO vehiculo = vehiculoServiceAPI.get(id);
		if (vehiculo != null) {
			vehiculoServiceAPI.delete(id);
		} else {
			return new ResponseEntity<VehiculoDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<VehiculoDTO>(vehiculo, HttpStatus.OK);
	}
}
