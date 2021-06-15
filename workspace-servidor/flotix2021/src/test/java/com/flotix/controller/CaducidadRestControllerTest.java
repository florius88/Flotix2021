package com.flotix.controller;

import java.util.List;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.dto.CaducidadDTO;
import com.flotix.response.bean.ServerResponseCaducidad;

@RunWith(SpringRunner.class)
@SpringBootTest
public class CaducidadRestControllerTest {

	@Autowired
	private CaducidadRestController caducidadRestController;

	@Test
	public void testGetAllFilter() {
		ServerResponseCaducidad result = caducidadRestController.getAllFilter("null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseCaducidad result = caducidadRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseCaducidad result = caducidadRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSaveCaducidadString() {
		ServerResponseCaducidad result = caducidadRestController.save(null, null);
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testGetListCaducidadDTO() {
		List<CaducidadDTO> result = caducidadRestController.getListCaducidadDTO();
		Assert.assertEquals(false, result.isEmpty());
	}

	@Test
	public void testSaveString() {
		String result = caducidadRestController.save(null);
		Assert.assertEquals(null, result);
	}

	@Test
	public void testDelete() {
		boolean result = caducidadRestController.delete("0");
		Assert.assertEquals(false, result);
	}
}
