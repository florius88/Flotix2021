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
import com.flotix.firebase.model.Caducidad;
import com.flotix.firebase.service.CaducidadServiceAPI;

@RestController
@RequestMapping(value = "/api/caducidad/")
@CrossOrigin("*")
public class CaducidadRestController {

	@Autowired
	private CaducidadServiceAPI caducidadServiceAPI;
	
	@GetMapping(value = "/all")
	public List<CaducidadDTO> getAll() throws Exception {
		return caducidadServiceAPI.getAll();
	}

	@GetMapping(value = "/find/{id}")
	public CaducidadDTO find(@PathVariable String id) throws Exception {
		return caducidadServiceAPI.get(id);
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Caducidad caducidad, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = caducidadServiceAPI.save(caducidad);
		} else {
			caducidadServiceAPI.save(caducidad, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<CaducidadDTO> delete(@PathVariable String id) throws Exception {
		CaducidadDTO caducidad = caducidadServiceAPI.get(id);
		if (caducidad != null) {
			caducidadServiceAPI.delete(id);
		} else {
			return new ResponseEntity<CaducidadDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<CaducidadDTO>(caducidad, HttpStatus.OK);
	}
}
