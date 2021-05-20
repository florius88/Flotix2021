package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.MantenimientoDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.Mantenimiento;
import com.flotix.firebase.service.MantenimientoServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class MantenimientoServiceImpl extends GenericServiceImpl<Mantenimiento, MantenimientoDTO> implements MantenimientoServiceAPI {
	
	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("mantenimiento");
	}
}
