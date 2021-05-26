package com.flotix.response.bean;

public class ServerResponseInit {

	private String msg = "";

	private ErrorBean error = new ErrorBean();

	public ServerResponseInit() {
		super();
	}

	public String getMsg() {
		return msg;
	}

	public void setMsg(String msg) {
		this.msg = msg;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(final ErrorBean error) {
		this.error = error;
	}
}
