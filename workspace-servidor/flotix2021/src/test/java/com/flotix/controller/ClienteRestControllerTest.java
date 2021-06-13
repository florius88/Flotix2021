package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseCliente;

@RunWith(SpringRunner.class)
@SpringBootTest
public class ClienteRestControllerTest {

	@Autowired
	private ClienteRestController clienteRestController;

	@Test
	public void testGetAllFilter() {
		ServerResponseCliente result = clienteRestController.getAllFilter("null", "null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseCliente result = clienteRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseCliente result = clienteRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseCliente result = clienteRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testDelete() {
		ServerResponseCliente result = clienteRestController.delete("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

}
