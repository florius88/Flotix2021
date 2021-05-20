package com.flotix.response.bean;

public class ErrorBean {

	private int code = 0;
	private String message = "";
	
	public int getCode() {
		return code;
	}

	public void setCode(final int code) {
		this.code = code;
	}

	public String getMessage() {
		return message;
	}

	public void setMessage(final String message) {
		this.message = message;
	}
	
}
