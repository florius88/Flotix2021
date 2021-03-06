package com.flotix.dto;

/**
 * Objeto que almacena la informacion de los mantenimientos para su devolucion
 * 
 * @author Flor
 *
 */
public class MantenimientoDTO {

	private String id;
	private String ultimoMantenimiento;
	private String proximoMantenimiento;
	private int kmMantenimiento;
	private String idVehiculo;
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

	public String getIdVehiculo() {
		return idVehiculo;
	}

	public void setIdVehiculo(String idVehiculo) {
		this.idVehiculo = idVehiculo;
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
