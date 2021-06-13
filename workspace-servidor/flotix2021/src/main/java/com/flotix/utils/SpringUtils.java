package com.flotix.utils;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.context.ApplicationContext;
import org.springframework.stereotype.Component;

@Component
public class SpringUtils {

	public static ApplicationContext ctx;

	/**
	 * Haga que Spring inyecte el contexto de la aplicación y guardelo en una
	 * variable estatica, para que se pueda acceder a el desde cualquier punto de la
	 * aplicacion
	 */
	@Autowired
	private void setApplicationContext(ApplicationContext applicationContext) {
		ctx = applicationContext;
	}
}