package com.flotix.dto;

public class MantenimientoDTO {

	private String id;
	private String ultimoMantenimiento;
	private String proximoMantenimiento;
	private int kmMantenimiento;
	private String matricula;
	private VehiculoDTO vehiculo;
	private String idTipoMantenimiento;
	private TipoMantenimientoDTO tipoMantenimiento;
	private boolean baja;

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getUltimoMantenimiento() {
		return ultimoMantenimiento;
	}

	public void setUltimoMantenimiento(String ultimoMantenimiento) {
		this.ultimoMantenimiento = ultimoMantenimiento;
	}

	public String getProximoMantenimiento() {
		return proximoMantenimiento;
	}

	public void setProximoMantenimiento(String proximoMantenimiento) {
		this.proximoMantenimiento = proximoMantenimiento;
	}

	public int getKmMantenimiento() {
		return kmMantenimiento;
	}

	public void setKmMantenimiento(int kmMantenimiento) {
		this.kmMantenimiento = kmMantenimiento;
	}

	public String getMatricula() {
		return matricula;
	}

	public void setMatricula(String matricula) {
		this.matricula = matricula;
	}

	public VehiculoDTO getVehiculo() {
		return vehiculo;
	}

	public void setVehiculo(VehiculoDTO vehiculo) {
		this.vehiculo = vehiculo;
	}

	public String getIdTipoMantenimiento() {
		return idTipoMantenimiento;
	}

	public void setIdTipoMantenimiento(String idTipoMantenimiento) {
		this.idTipoMantenimiento = idTipoMantenimiento;
	}

	public TipoMantenimientoDTO getTipoMantenimiento() {
		return tipoMantenimiento;
	}

	public void setTipoMantenimiento(TipoMantenimientoDTO tipoMantenimiento) {
		this.tipoMantenimiento = tipoMantenimiento;
	}

	public boolean isBaja() {
		return baja;
	}

	public void setBaja(boolean baja) {
		this.baja = baja;
	}
}
