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
import com.flotix.dto.UsuarioDTO;
import com.flotix.firebase.model.Usuario;
import com.flotix.firebase.service.RolServiceAPI;
import com.flotix.firebase.service.UsuarioServiceAPI;

@RestController
@RequestMapping(value = "/api/usuario/")
@CrossOrigin("*")
public class UsuarioRestController {

	@Autowired
	private UsuarioServiceAPI usuarioServiceAPI;
	
	@Autowired
	private RolServiceAPI rolServiceAPI;
	
	@GetMapping(value = "/all")
	public List<UsuarioDTO> getAll() throws Exception {
		
		List<UsuarioDTO> lista = usuarioServiceAPI.getAll();
		
		for (UsuarioDTO usuario: lista) {
			//Busca el metodo de pago
			RolDTO rol = rolServiceAPI.get(usuario.getIdRol());
			usuario.setRol(rol);
		}
		
		return lista;
	}

	@GetMapping(value = "/find/{id}")
	public UsuarioDTO find(@PathVariable String id) throws Exception {
		
		UsuarioDTO usuario = usuarioServiceAPI.get(id);
		
		//Busca el metodo de pago
		RolDTO rol = rolServiceAPI.get(usuario.getIdRol());
		usuario.setRol(rol);
		
		return usuario;
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Usuario usuario, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = usuarioServiceAPI.save(usuario);
		} else {
			usuarioServiceAPI.save(usuario, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<UsuarioDTO> delete(@PathVariable String id) throws Exception {
		UsuarioDTO usuario = usuarioServiceAPI.get(id);
		if (usuario != null) {
			usuarioServiceAPI.delete(id);
		} else {
			return new ResponseEntity<UsuarioDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<UsuarioDTO>(usuario, HttpStatus.OK);
	}
}
