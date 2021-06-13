package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.dto.AlquilerDTO;
import com.flotix.response.bean.ServerResponseAlquiler;

@RunWith(SpringRunner.class)
@SpringBootTest
public class AlquilerRestControllerTest {

	@Autowired
	private AlquilerRestController alquilerRestController;

	@Test
	public void testGetAllFilter() {
		ServerResponseAlquiler result = alquilerRestController.getAllFilter("null", "null", "null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseAlquiler result = alquilerRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseAlquiler result = alquilerRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testGetFindByMatricula() {
		ServerResponseAlquiler result = alquilerRestController.getFindByMatricula("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseAlquiler result = alquilerRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testGetAlquilerByMatricula() {
		AlquilerDTO result = alquilerRestController.getAlquilerByMatricula("0");
		Assert.assertEquals(null, result);
	}

	@Test
	public void testGetAlquilerByCliente() {
		AlquilerDTO result = alquilerRestController.getAlquilerByCliente("0");
		Assert.assertEquals(null, result);
	}
}
