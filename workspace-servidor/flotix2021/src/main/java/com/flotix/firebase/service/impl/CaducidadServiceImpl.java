package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.CaducidadDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.Caducidad;
import com.flotix.firebase.service.CaducidadServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class CaducidadServiceImpl extends GenericServiceImpl<Caducidad, CaducidadDTO> implements CaducidadServiceAPI {
	
	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("caducidad");
	}
}
