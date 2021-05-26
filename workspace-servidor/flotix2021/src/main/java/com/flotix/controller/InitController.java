package com.flotix.controller;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.firebase.model.MetodoPago;
import com.flotix.firebase.model.Rol;
import com.flotix.firebase.model.TipoAlerta;
import com.flotix.firebase.model.TipoMantenimiento;
import com.flotix.firebase.service.MetodoPagoServiceAPI;
import com.flotix.firebase.service.RolServiceAPI;
import com.flotix.firebase.service.TipoAlertaServiceAPI;
import com.flotix.firebase.service.TipoMantenimientoServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseInit;
import com.flotix.utils.MessageExceptions;

@RestController
@RequestMapping(value = "/api/init/")
@CrossOrigin("*")
public class InitController {

	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;

	@Autowired
	private TipoMantenimientoServiceAPI tipoMantenimientoServiceAPI;

	@Autowired
	private MetodoPagoServiceAPI metodoPagoServiceAPI;

	@Autowired
	private RolServiceAPI rolServiceAPI;

	private enum EnumTipoAlerta {
		ITV, SEGURO
	};

	private enum EnumTipoMantenimiento {
		RUEDAS, REVISIÓN
	};

	private enum EnumMetodoPago {
		TRANSFERENCIA, CONFIRMING, EFECTIVO, CHEQUE, PAGARÉ
	};

	private enum EnumRol {
		ADMINISTRADOR, ADMINISTRATIVO, COMERCIAL
	};

	@PostMapping(value = "/load")
	public ServerResponseInit save() {

		ServerResponseInit result = new ServerResponseInit();
		boolean cargado = true;

		try {

			if (null == tipoAlertaServiceAPI.getAll("nombre") || tipoAlertaServiceAPI.getAll("nombre").isEmpty()) {
				// Almacena todos los tipos de alerta
				for (EnumTipoAlerta eTipoAlerta : EnumTipoAlerta.values()) {
					TipoAlerta tipoAlerta = new TipoAlerta();
					tipoAlerta.setNombre(eTipoAlerta.name());
					tipoAlertaServiceAPI.save(tipoAlerta);
				}

				cargado = false;
			}

			if (null == tipoMantenimientoServiceAPI.getAll("nombre")
					|| tipoMantenimientoServiceAPI.getAll("nombre").isEmpty()) {
				// Almacena todos los tipos de mantenimiento
				for (EnumTipoMantenimiento eTipoMantenimiento : EnumTipoMantenimiento.values()) {
					TipoMantenimiento tipoMantenimiento = new TipoMantenimiento();
					tipoMantenimiento.setNombre(eTipoMantenimiento.name());
					tipoMantenimientoServiceAPI.save(tipoMantenimiento);
				}

				cargado = false;
			}

			if (null == metodoPagoServiceAPI.getAll("nombre") || metodoPagoServiceAPI.getAll("nombre").isEmpty()) {
				// Almacena todos los metodos de pago
				for (EnumMetodoPago eMetodoPago : EnumMetodoPago.values()) {
					MetodoPago metodoPago = new MetodoPago();
					metodoPago.setNombre(eMetodoPago.name());
					metodoPagoServiceAPI.save(metodoPago);
				}

				cargado = false;
			}

			if (null == rolServiceAPI.getAll("nombre") || rolServiceAPI.getAll("nombre").isEmpty()) {
				// Almacena todos los roles
				for (EnumRol eRol : EnumRol.values()) {
					Rol rol = new Rol();
					rol.setNombre(eRol.name());
					rolServiceAPI.save(rol);
				}

				cargado = false;
			}

			if (cargado) {
				result.setMsg("Todos los registros ya están cargados");
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			} else {
				result.setMsg("Se ha realizado la carga inicial correctamente");
				ErrorBean error = new ErrorBean();
				error.setCode(MessageExceptions.OK_CODE);
				error.setMessage(MessageExceptions.MSSG_OK);
				result.setError(error);
			}

		} catch (Exception e) {
			result.setMsg("Se ha producido un error al realizar la carga inicial");
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}
}