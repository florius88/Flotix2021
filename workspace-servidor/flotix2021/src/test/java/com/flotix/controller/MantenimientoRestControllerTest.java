package com.flotix.controller;

import java.util.List;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.dto.MantenimientoDTO;
import com.flotix.response.bean.ServerResponseMantenimiento;

@RunWith(SpringRunner.class)
@SpringBootTest
public class MantenimientoRestControllerTest {

	@Autowired
	private MantenimientoRestController mantenimientoRestController;

	@Test
	public void testGetAllFilter() {
		ServerResponseMantenimiento result = mantenimientoRestController.getAllFilter("null", "null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseMantenimiento result = mantenimientoRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseMantenimiento result = mantenimientoRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseMantenimiento result = mantenimientoRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testGetAllListMantenimientoDTO() {
		List<MantenimientoDTO> result = mantenimientoRestController.getAllListMantenimientoDTO();
		Assert.assertEquals(false, result.isEmpty());
	}

	@Test
	public void testDelete() {
		boolean result = mantenimientoRestController.delete("0");
		Assert.assertEquals(true, result);
	}

}
