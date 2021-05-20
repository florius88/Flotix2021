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

import com.flotix.dto.RolDTO;
import com.flotix.firebase.model.Rol;
import com.flotix.firebase.service.RolServiceAPI;

@RestController
@RequestMapping(value = "/api/rol/")
@CrossOrigin("*")
public class RolRestController {

	@Autowired
	private RolServiceAPI rolServiceAPI;
	
	@GetMapping(value = "/all")
	public List<RolDTO> getAll() throws Exception {
		return rolServiceAPI.getAll();
	}

	@GetMapping(value = "/find/{id}")
	public RolDTO find(@PathVariable String id) throws Exception {
		return rolServiceAPI.get(id);
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Rol rol, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = rolServiceAPI.save(rol);
		} else {
			rolServiceAPI.save(rol, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<RolDTO> delete(@PathVariable String id) throws Exception {
		RolDTO rol = rolServiceAPI.get(id);
		if (rol != null) {
			rolServiceAPI.delete(id);
		} else {
			return new ResponseEntity<RolDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<RolDTO>(rol, HttpStatus.OK);
	}
}
