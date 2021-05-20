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

import com.flotix.dto.MantenimientoDTO;
import com.flotix.dto.TipoAlertaDTO;
import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.model.Mantenimiento;
import com.flotix.firebase.service.MantenimientoServiceAPI;
import com.flotix.firebase.service.TipoAlertaServiceAPI;
import com.flotix.firebase.service.VehiculoServiceAPI;

@RestController
@RequestMapping(value = "/api/mantenimiento/")
@CrossOrigin("*")
public class MantenimientoRestController {

	@Autowired
	private MantenimientoServiceAPI mantenimientoServiceAPI;
	
	@Autowired
	private VehiculoServiceAPI vehiculoServiceAPI;
	
	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;
	
	@GetMapping(value = "/all")
	public List<MantenimientoDTO> getAll() throws Exception {
		
		List<MantenimientoDTO> lista = mantenimientoServiceAPI.getAll();
		
		for (MantenimientoDTO mantenimiento: lista) {
			//Busca el vehiculo
			VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
			mantenimiento.setVehiculo(vehiculo);
			//Busca el tipo de alerta
			TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(mantenimiento.getIdTipoAlerta());
			mantenimiento.setTipoAlerta(tipoAlerta);
		}
		
		return lista;
	}

	@GetMapping(value = "/find/{id}")
	public MantenimientoDTO find(@PathVariable String id) throws Exception {
		
		MantenimientoDTO mantenimiento = mantenimientoServiceAPI.get(id);
		
		//Busca el vehiculo
		VehiculoDTO vehiculo = vehiculoServiceAPI.get(mantenimiento.getIdVehiculo());
		mantenimiento.setVehiculo(vehiculo);
		//Busca el tipo de alerta
		TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(mantenimiento.getIdTipoAlerta());
		mantenimiento.setTipoAlerta(tipoAlerta);
				
		return mantenimiento;
	}

	@PostMapping(value = "/save/{id}")
	public ResponseEntity<String> save(@RequestBody Mantenimiento mantenimiento, @PathVariable String id) throws Exception {
		if (id == null || id.length() == 0 || id.equals("null")) {
			id = mantenimientoServiceAPI.save(mantenimiento);
		} else {
			mantenimientoServiceAPI.save(mantenimiento, id);
		}
		return new ResponseEntity<String>(id, HttpStatus.OK);
	}

	@GetMapping(value = "/delete/{id}")
	public ResponseEntity<MantenimientoDTO> delete(@PathVariable String id) throws Exception {
		MantenimientoDTO mantenimiento = mantenimientoServiceAPI.get(id);
		if (mantenimiento != null) {
			mantenimientoServiceAPI.delete(id);
		} else {
			return new ResponseEntity<MantenimientoDTO>(HttpStatus.NO_CONTENT);
		}

		return new ResponseEntity<MantenimientoDTO>(mantenimiento, HttpStatus.OK);
	}
}
