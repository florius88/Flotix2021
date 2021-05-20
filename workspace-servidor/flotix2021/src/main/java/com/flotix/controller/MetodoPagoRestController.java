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

import com.flotix.dto.MetodoPagoDTO;
import com.flotix.firebase.model.MetodoPago;
import com.flotix.firebase.service.MetodoPagoServiceAPI;

@RestController
@RequestMapping(value = "/api/metodopago/")
@CrossOrigin("*")
public class MetodoPagoRestController {

	@Autowired
	private MetodoPagoServiceAPI metodoPagoServiceAPI;
	
	@GetMapping(value = "/all")
	public List<MetodoPagoDTO> getAll() throws Exception {
		return metodoPagoServiceAPI.getAll();
	}

	@GetMapping(value = "/find/{id}")
	public MetodoPagoDTO find(@PathVariable String id) throws Exception {
		return metodoPagoServiceAPI.get(id);
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody MetodoPago metodoPago, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = metodoPagoServiceAPI.save(metodoPago);
		} else {
			metodoPagoServiceAPI.save(metodoPago, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<MetodoPagoDTO> delete(@PathVariable String id) throws Exception {
		MetodoPagoDTO metodoPago = metodoPagoServiceAPI.get(id);
		if (metodoPago != null) {
			metodoPagoServiceAPI.delete(id);
		} else {
			return new ResponseEntity<MetodoPagoDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<MetodoPagoDTO>(metodoPago, HttpStatus.OK);
	}
}
