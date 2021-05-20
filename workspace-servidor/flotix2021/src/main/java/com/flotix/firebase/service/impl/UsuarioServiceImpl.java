package com.flotix.firebase.service.impl;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.flotix.dto.UsuarioDTO;
import com.flotix.firebase.commons.impl.GenericServiceImpl;
import com.flotix.firebase.model.Usuario;
import com.flotix.firebase.service.UsuarioServiceAPI;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.Firestore;

@Service
public class UsuarioServiceImpl extends GenericServiceImpl<Usuario, UsuarioDTO> implements UsuarioServiceAPI {
	
	@Autowired
	private Firestore firestore;

	@Override
	public CollectionReference getCollection() {
		return firestore.collection("usuario");
	}
}
