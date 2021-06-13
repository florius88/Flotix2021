package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseMetodoPago;

@RunWith(SpringRunner.class)
@SpringBootTest
public class MetodoPagoRestControllerTest {

	@Autowired
	private MetodoPagoRestController metodoPagoRestController;

	@Test
	public void testGetAll() {
		ServerResponseMetodoPago result = metodoPagoRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseMetodoPago result = metodoPagoRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseMetodoPago result = metodoPagoRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

}
