package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseImagenVehiculo;
import com.flotix.response.bean.ServerResponseVehiculo;

@RunWith(SpringRunner.class)
@SpringBootTest
public class VehiculoRestControllerTest {

	@Autowired
	private VehiculoRestController vehiculoRestController;

	@Test
	public void testGetAllFilter() {
		ServerResponseVehiculo result = vehiculoRestController.getAllFilter("null", "null", "null", "null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseVehiculo result = vehiculoRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseVehiculo result = vehiculoRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testFindNoMantenimiento() {
		ServerResponseVehiculo result = vehiculoRestController.findNoMantenimiento();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseVehiculo result = vehiculoRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testSaveDocument() {
		ServerResponseImagenVehiculo result = vehiculoRestController.saveDocument(null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testFindDocument() {
		ServerResponseImagenVehiculo result = vehiculoRestController.findDocument("0");
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testDelete() {
		ServerResponseVehiculo result = vehiculoRestController.delete("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

}
