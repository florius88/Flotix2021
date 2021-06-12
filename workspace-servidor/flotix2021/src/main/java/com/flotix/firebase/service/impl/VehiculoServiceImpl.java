package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.VehiculoDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.ImagenVehiculo;
import com.flotix.firebase.model.Vehiculo;
import com.flotix.firebase.service.VehiculoServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;
import com.google.cloud.storage.Blob;
import com.google.cloud.storage.Bucket;
import com.google.firebase.cloud.StorageClient;

@Service
public class VehiculoServiceImpl extends GenericServiceImpl<Vehiculo, VehiculoDTO> implements VehiculoServiceAPI {

	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("vehiculo");
	}

	@Override
	public String saveDocument(ImagenVehiculo imagenVehiculo) throws Exception {

		Bucket bucket = StorageClient.getInstance().bucket();

		String nombre = imagenVehiculo.getNombreImagen();

		Blob documento = bucket.create(nombre, imagenVehiculo.getDocumento());

		return documento.getBlobId().getName();
	}

	@Override
	public byte[] getDocument(String id) throws Exception {

		Bucket bucket = StorageClient.getInstance().bucket();

		Blob blob = bucket.get(id);
		byte[] content = blob.getContent();

		return content;
	}
}
