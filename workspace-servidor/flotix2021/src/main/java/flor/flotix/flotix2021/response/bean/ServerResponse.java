package flor.flotix.flotix2021.response.bean;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;
import com.fasterxml.jackson.annotation.JsonInclude;
import com.fasterxml.jackson.annotation.JsonInclude.Include;

@JsonInclude(Include.NON_DEFAULT)
@JsonIgnoreProperties(ignoreUnknown = true)
public class ServerResponse {

	@JsonInclude(Include.NON_DEFAULT)
	private String nombre = "";

	@JsonInclude(Include.NON_DEFAULT)
	private ErrorBean error = new ErrorBean();

	public ServerResponse() {
		super();
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}

	public ErrorBean getError() {
		return error;
	}

	public void setError(final ErrorBean error) {
		this.error = error;
	}
}
