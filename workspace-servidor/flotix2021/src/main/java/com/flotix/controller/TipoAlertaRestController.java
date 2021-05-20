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

import com.flotix.dto.TipoAlertaDTO;
import com.flotix.firebase.model.TipoAlerta;
import com.flotix.firebase.service.TipoAlertaServiceAPI;

@RestController
@RequestMapping(value = "/api/tipoalerta/")
@CrossOrigin("*")
public class TipoAlertaRestController {

	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;
	
	@GetMapping(value = "/all")
	public List<TipoAlertaDTO> getAll() throws Exception {
		return tipoAlertaServiceAPI.getAll();
	}

	@GetMapping(value = "/find/{id}")
	public TipoAlertaDTO find(@PathVariable String id) throws Exception {
		return tipoAlertaServiceAPI.get(id);
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody TipoAlerta tipoAlerta, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = tipoAlertaServiceAPI.save(tipoAlerta);
		} else {
			tipoAlertaServiceAPI.save(tipoAlerta, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<TipoAlertaDTO> delete(@PathVariable String id) throws Exception {
		TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(id);
		if (tipoAlerta != null) {
			tipoAlertaServiceAPI.delete(id);
		} else {
			return new ResponseEntity<TipoAlertaDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<TipoAlertaDTO>(tipoAlerta, HttpStatus.OK);
	}
}
