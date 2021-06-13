package com.flotix.utils;

/**
 * Codigos y mensajes de la aplicacion
 * 
 * @author Flor
 *
 */
public class MessageExceptions {

	public static final int OK_CODE = 200;
	public static final int NOT_FOUND_CODE = 300;
	public static final int NOT_MODIF_VEHICULO_CODE = 301;
	public static final int NOT_MODIF_CLIENTE_CODE = 302;
	public static final int GENERIC_ERROR_CODE = 500;

	public static final String MSSG_OK = "OK";
	public static final String MSSG_NOT_FOUND = "KO: Registro no encontrado";
	public static final String MSSG_ERROR_SAVE_VEHICULO = "KO: Se ha producido un error al crear los objetos asociados";
	public static final String MSSG_ERROR_DELETE_VEHICULO = "KO: Se ha producido un error al borrar los objetos asociados";
	public static final String MSSG_ERROR_NOT_MODIF_VEHICULO = "KO: No se puede modificar la información del vehiculo, ya que está asociado a un alquiler en vigor";
	public static final String MSSG_ERROR_NOT_MODIF_CLIENTE = "KO: No se puede modificar la información del cliente, ya que está asociado a un alquiler en vigor";
	public static final String MSSG_GENERIC_ERROR = "KO: Se ha producido un error";
}
