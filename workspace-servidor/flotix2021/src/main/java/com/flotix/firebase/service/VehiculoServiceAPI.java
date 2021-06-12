package com.flotix.firebase.service;

import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.commons.GenericServiceAPI;
import com.flotix.firebase.model.ImagenVehiculo;
import com.flotix.firebase.model.Vehiculo;

public interface VehiculoServiceAPI extends GenericServiceAPI<Vehiculo, VehiculoDTO> {

	public String saveDocument(ImagenVehiculo imagenVehiculo) throws Exception;

	public byte[] getDocument(String id) throws Exception;
}
