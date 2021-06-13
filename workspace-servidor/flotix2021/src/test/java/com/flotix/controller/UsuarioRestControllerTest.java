package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseUsuario;

@RunWith(SpringRunner.class)
@SpringBootTest
public class UsuarioRestControllerTest {

	@Autowired
	private UsuarioRestController usuarioRestController;

	@Test
	public void testGetLogin() {
		ServerResponseUsuario result = usuarioRestController.getLogin("null", "null");
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testGetAllFilter() {
		ServerResponseUsuario result = usuarioRestController.getAllFilter("null", "null", "null");
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testGetAll() {
		ServerResponseUsuario result = usuarioRestController.getAll();
		Assert.assertEquals(200, result.getError().getCode());
	}

	@Test
	public void testFind() {
		ServerResponseUsuario result = usuarioRestController.find("0");
		Assert.assertEquals(300, result.getError().getCode());
	}

	@Test
	public void testSave() {
		ServerResponseUsuario result = usuarioRestController.save(null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testChangePwd() {
		ServerResponseUsuario result = usuarioRestController.changePwd(null, null, null, null);
		Assert.assertEquals(500, result.getError().getCode());
	}

	@Test
	public void testDelete() {
		ServerResponseUsuario result = usuarioRestController.delete("0");
		Assert.assertEquals(300, result.getError().getCode());
	}
}
