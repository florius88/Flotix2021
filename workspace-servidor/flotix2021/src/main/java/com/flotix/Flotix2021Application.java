package com.flotix;

import org.apache.log4j.PropertyConfigurator;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityConfigurerAdapter;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.crypto.bcrypt.BCryptPasswordEncoder;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.provisioning.InMemoryUserDetailsManager;

/**
 * Main de la aplicacion Flotix 2021
 * 
 * @author Flor
 *
 */
@SpringBootApplication
@EnableWebSecurity
public class Flotix2021Application extends WebSecurityConfigurerAdapter {

	@Value("${security.user_name}")
	private String user_name;

	@Value("${security.user_pwd}")
	private String user_pwd;

	@Value("${security.user_role}")
	private String user_role;

	public static void main(String[] args) {

		// PropertiesConfigurator is used to configure logger from properties file
		PropertyConfigurator.configure("properties/log4j.properties");

		SpringApplication.run(Flotix2021Application.class, args);
	}

	@Bean
	@Override
	public AuthenticationManager authenticationManagerBean() throws Exception {
		return super.authenticationManagerBean();
	}

	@Bean
	@Override
	public UserDetailsService userDetailsService() {
		UserDetails userAdmin = User.builder().username(user_name).password(passwordEncoder().encode(user_pwd))
				.roles(user_role).build();
		return new InMemoryUserDetailsManager(userAdmin);
	}

	@Bean
	public PasswordEncoder passwordEncoder() {
		return new BCryptPasswordEncoder();
	}
}
