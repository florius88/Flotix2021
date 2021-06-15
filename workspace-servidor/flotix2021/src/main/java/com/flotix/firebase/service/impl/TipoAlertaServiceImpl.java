package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.TipoAlertaDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.TipoAlerta;
import com.flotix.firebase.service.TipoAlertaServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class TipoAlertaServiceImpl extends GenericServiceImpl<TipoAlerta, TipoAlertaDTO> implements TipoAlertaServiceAPI {
	
	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("tipoAlerta");
	}
}
