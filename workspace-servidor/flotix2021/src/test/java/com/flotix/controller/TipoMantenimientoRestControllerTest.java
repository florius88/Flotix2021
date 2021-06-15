package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseTipoMantenimiento;

@RunWith(SpringRunner.class)
@SpringBootTest
public class TipoMantenimientoRestControllerTest {

	@Autowired
	private TipoMantenimientoRestController tipoMantenimientoRestController;

	@Test
	public void testGetAll() {
		ServerResponseTipoMantenimiento result = tipoMantenimientoRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseTipoMantenimiento result = tipoMantenimientoRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseTipoMantenimiento result = tipoMantenimientoRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

}
