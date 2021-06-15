package com.flotix;

import org.springframework.context.annotation.Configuration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.oauth2.config.annotation.web.configuration.EnableResourceServer;
import org.springframework.security.oauth2.config.annotation.web.configuration.ResourceServerConfigurer;
import org.springframework.security.oauth2.config.annotation.web.configurers.ResourceServerSecurityConfigurer;

/**
 * Impide el acceso a cualquier llamada no autorizada
 * 
 * @author Flor
 *
 */
@Configuration
@EnableResourceServer
public class ResourceServerConfiguration implements ResourceServerConfigurer {

	@Override
	public void configure(ResourceServerSecurityConfigurer resources) throws Exception {
	}

	@Override
	public void configure(HttpSecurity http) throws Exception {
		// Evita el acceso a las llamadas no autorizadas
		http.authorizeRequests().anyRequest().authenticated();
	}
}