package com.flotix.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.firebase.model.MetodoPago;
import com.flotix.firebase.model.Rol;
import com.flotix.firebase.model.TipoAlerta;
import com.flotix.firebase.service.MetodoPagoServiceAPI;
import com.flotix.firebase.service.RolServiceAPI;
import com.flotix.firebase.service.TipoAlertaServiceAPI;

@RestController
@RequestMapping(value = "/api/init/")
@CrossOrigin("*")
public class InitController {

	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;

	@Autowired
	private MetodoPagoServiceAPI metodoPagoServiceAPI;

	@Autowired
	private RolServiceAPI rolServiceAPI;

	private enum EnumTipoAlerta {
		ITV, SEGURO, RUEDAS, REVISIÓN
	};

	private enum EnumMetodoPago {
		TRANSFERENCIA, CONFIRMING, EFECTIVO, CHEQUE, PAGARÉ
	};

	private enum EnumRol {
		ADMINISTRADOR, ADMINISTRATIVO, COMERCIAL
	};

	@PostMapping(value = "/load")
	public ResponseEntity<String> save() throws Exception {

		// Almacena todos los tipos de alerta
		for (EnumTipoAlerta eTipoAlerta : EnumTipoAlerta.values()) {
			TipoAlerta tipoAlerta = new TipoAlerta();
			tipoAlerta.setNombre(eTipoAlerta.name());
			tipoAlertaServiceAPI.save(tipoAlerta);
		}

		// Almacena todos los metodos de pago
		for (EnumMetodoPago eMetodoPago : EnumMetodoPago.values()) {
			MetodoPago metodoPago = new MetodoPago();
			metodoPago.setNombre(eMetodoPago.name());
			metodoPagoServiceAPI.save(metodoPago);
		}

		// Almacena todos los roles
		for (EnumRol eRol : EnumRol.values()) {
			Rol rol = new Rol();
			rol.setNombre(eRol.name());
			rolServiceAPI.save(rol);
		}

		return new ResponseEntity<String>("Carga inicial", HttpStatus.OK);
	}
}