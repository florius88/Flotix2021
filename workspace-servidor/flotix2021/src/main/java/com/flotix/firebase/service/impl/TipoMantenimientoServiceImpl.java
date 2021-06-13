package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.TipoMantenimientoDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.TipoMantenimiento;
import com.flotix.firebase.service.TipoMantenimientoServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class TipoMantenimientoServiceImpl extends GenericServiceImpl<TipoMantenimiento, TipoMantenimientoDTO>
		implements TipoMantenimientoServiceAPI {

	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("tipoMantenimiento");
	}
}
