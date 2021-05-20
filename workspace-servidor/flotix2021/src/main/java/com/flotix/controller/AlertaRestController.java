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

import com.flotix.dto.AlertaDTO;
import com.flotix.dto.TipoAlertaDTO;
import com.flotix.firebase.model.Alerta;
import com.flotix.firebase.service.AlertaServiceAPI;
import com.flotix.firebase.service.TipoAlertaServiceAPI;

@RestController
@RequestMapping(value = "/api/alerta/")
@CrossOrigin("*")
public class AlertaRestController {

	@Autowired
	private AlertaServiceAPI alertaServiceAPI;
	
	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;
	
	@GetMapping(value = "/all")
	public List<AlertaDTO> getAll() throws Exception {
		
		List<AlertaDTO> lista = alertaServiceAPI.getAll();
		
		for (AlertaDTO alerta: lista) {
			//Busca el tipo de alerta
			TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
			alerta.setTipoAlerta(tipoAlerta);
		}
		
		return lista;
	}

	@GetMapping(value = "/find/{id}")
	public AlertaDTO find(@PathVariable String id) throws Exception {
		
		AlertaDTO alerta = alertaServiceAPI.get(id);
		
		//Busca el tipo de alerta
		TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
		alerta.setTipoAlerta(tipoAlerta);
		
		return alerta;
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Alerta alerta, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = alertaServiceAPI.save(alerta);
		} else {
			alertaServiceAPI.save(alerta, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<AlertaDTO> delete(@PathVariable String id) throws Exception {
		AlertaDTO alerta = alertaServiceAPI.get(id);
		if (alerta != null) {
			alertaServiceAPI.delete(id);
		} else {
			return new ResponseEntity<AlertaDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<AlertaDTO>(alerta, HttpStatus.OK);
	}
}
