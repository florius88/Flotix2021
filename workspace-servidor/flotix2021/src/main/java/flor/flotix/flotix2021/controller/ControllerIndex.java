package flor.flotix.flotix2021.controller;

import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

@Controller
public class ControllerIndex {

	@RequestMapping(value = "/flotix", method = RequestMethod.GET)
	public String index() {
		return "Bienvenido a Flotix2021";
	}
}