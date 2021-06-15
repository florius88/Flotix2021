package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.MetodoPagoDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.MetodoPago;
import com.flotix.firebase.service.MetodoPagoServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class MetodoPagoServiceImpl extends GenericServiceImpl<MetodoPago, MetodoPagoDTO> implements MetodoPagoServiceAPI {
	
	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("metodoPago");
	}
}
