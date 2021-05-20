package com.flotix.controller;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.GetMapping;

@Controller
public class ControllerTest {
	
	@GetMapping(value = "/test")
	public ResponseEntity<String> test() throws Exception{
		return new ResponseEntity<String>("Bienvenido a Flotix2021", HttpStatus.OK);
	}
}