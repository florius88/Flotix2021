package com.flotix.controller;

import org.apache.log4j.Logger;

import com.flotix.utils.SpringUtils;

public class AlertaSegundoPlano extends Thread {

	private static Logger logger = Logger.getLogger(AlertaSegundoPlano.class);

	public AlertaSegundoPlano() {
		super();
	}

	public void run() {
		try {
			AlertaRestController alertaRestController = (AlertaRestController) SpringUtils.ctx
					.getBean(AlertaRestController.class);
			alertaRestController.cargaAlertas();
			logger.info("Se recargan las alertas");
		} catch (Exception e) {
			// LOG
			logger.error("Se ha producido un error al cargar las alertas: " + e.getMessage());
		}
	}
}