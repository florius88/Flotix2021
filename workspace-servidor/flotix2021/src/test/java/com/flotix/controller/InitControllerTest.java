package com.flotix.controller;

import org.junit.Assert;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import com.flotix.response.bean.ServerResponseInit;

@RunWith(SpringRunner.class)
@SpringBootTest
public class InitControllerTest {

	@Autowired
	private InitController initController;

	@Test
	public void testSave() {
		ServerResponseInit result = initController.save();
		Assert.assertEquals(200, result.getError().getCode());
	}

}
