package com.flotix.controller;

import java.text.SimpleDateFormat;
import java.util.Date;

import org.apache.log4j.Logger;
import org.springframework.scheduling.annotation.Scheduled;
import org.springframework.stereotype.Component;

@Component
public class AlertasScheduled {

	private static Logger logger = Logger.getLogger(AlertasScheduled.class);

	private static final SimpleDateFormat dateFormat = new SimpleDateFormat("HH:mm:ss");

	@Scheduled(cron = "0 0 7 ? * * ")
	public void reportCurrentTime() {
		logger.info("AlertasScheduled - " + dateFormat.format(new Date()));
		new AlertaSegundoPlano().start();
	}
}
