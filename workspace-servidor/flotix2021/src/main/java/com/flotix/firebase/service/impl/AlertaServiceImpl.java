package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.AlertaDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.Alerta;
import com.flotix.firebase.service.AlertaServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class AlertaServiceImpl extends GenericServiceImpl<Alerta, AlertaDTO> implements AlertaServiceAPI {
	
	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("alerta");
	}
}
