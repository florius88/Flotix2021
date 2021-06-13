package com.flotix.controller;

import java.util.Map;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseTipoAlerta;

@RunWith(SpringRunner.class)
@SpringBootTest
public class TipoAlertaRestControllerTest {

	@Autowired
	private TipoAlertaRestController tipoAlertaRestController;

	@Test
	public void testGetAll() {
		ServerResponseTipoAlerta result = tipoAlertaRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseTipoAlerta result = tipoAlertaRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseTipoAlerta result = tipoAlertaRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testGetListTipoAlertaDTO() {
		Map<String, String> result = tipoAlertaRestController.getListTipoAlertaDTO();
		Assert.assertEquals(false, result.isEmpty());
	}
}
