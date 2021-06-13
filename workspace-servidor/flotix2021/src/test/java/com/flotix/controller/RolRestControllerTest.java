package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseRol;

@RunWith(SpringRunner.class)
@SpringBootTest
public class RolRestControllerTest {

	@Autowired
	private RolRestController rolRestController;

	@Test
	public void testGetAll() {
		ServerResponseRol result = rolRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseRol result = rolRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseRol result = rolRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

}
