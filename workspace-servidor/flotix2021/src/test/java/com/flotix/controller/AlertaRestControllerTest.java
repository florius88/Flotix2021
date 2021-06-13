package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseAlerta;

@RunWith(SpringRunner.class)
@SpringBootTest
public class AlertaRestControllerTest {

	@Autowired
	private AlertaRestController alertaRestController;

	@Test
	public void testGetAllFilter() {
		ServerResponseAlerta result = alertaRestController.getAllFilter("null", "null", "null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseAlerta result = alertaRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testCargaAlertas() {
		alertaRestController.cargaAlertas();
	}

	@Test
	public void testSave() {
		boolean result = alertaRestController.save(null, null);
		Assert.assertEquals(false, result);
	}

}
