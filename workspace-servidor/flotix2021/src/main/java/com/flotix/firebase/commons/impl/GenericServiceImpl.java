package com.flotix.firebase.commons.impl;

import java.lang.reflect.ParameterizedType;
import java.util.ArrayList;
import java.util.List;
import java.util.Map;

import org.apache.commons.beanutils.PropertyUtils;

import com.flotix.firebase.commons.GenericServiceAPI;
import com.google.api.core.ApiFuture;
import com.google.cloud.firestore.CollectionReference;
import com.google.cloud.firestore.DocumentReference;
import com.google.cloud.firestore.DocumentSnapshot;
import com.google.cloud.firestore.QueryDocumentSnapshot;
import com.google.cloud.firestore.QuerySnapshot;

/**
 * Gestion de datos con Firebase
 * 
 * @author Flor
 *
 * @param <I>
 * @param <O>
 */
public abstract class GenericServiceImpl<I, O> implements GenericServiceAPI<I, O> {

	private String ID = "id";
	private String BAJA = "baja";

	public Class<O> clazz;

	@SuppressWarnings("unchecked")
	public GenericServiceImpl() {
		this.clazz = ((Class<O>) ((ParameterizedType) getClass().getGenericSuperclass()).getActualTypeArguments()[1]);
	}

	@Override
	public String save(I entity) throws Exception {
		return this.save(entity, null);
	}

	@Override
	public String save(I entity, String id) throws Exception {
		if (id == null || id.length() == 0) {
			return getCollection().add(entity).get().getId();
		}

		DocumentReference reference = getCollection().document(id);
		reference.set(entity);
		return reference.getId();
	}

	@Override
	public void delete(String id) throws Exception {
		getCollection().document(id).delete().get();
	}

	@Override
	public O get(String id) throws Exception {
		DocumentReference ref = getCollection().document(id);
		ApiFuture<DocumentSnapshot> futureDoc = ref.get();
		DocumentSnapshot document = futureDoc.get();
		if (document.exists()) {
			O object = document.toObject(clazz);
			PropertyUtils.setProperty(object, ID, document.getId());
			return object;
		}
		return null;
	}

	@Override
	public List<O> getAll(String fieldOrder) throws Exception {
		List<O> result = new ArrayList<O>();
		ApiFuture<QuerySnapshot> query = getCollection().orderBy(fieldOrder).get();
		List<QueryDocumentSnapshot> documents = query.get().getDocuments();
		for (QueryDocumentSnapshot doc : documents) {
			O object = doc.toObject(clazz);
			PropertyUtils.setProperty(object, ID, doc.getId());
			result.add(object);
		}
		return result;
	}

	@Override
	public List<O> getAllNotBaja(String fieldOrder) throws Exception {
		List<O> result = new ArrayList<O>();
		ApiFuture<QuerySnapshot> query = getCollection().whereEqualTo(BAJA, false).orderBy(fieldOrder).get();
		List<QueryDocumentSnapshot> documents = query.get().getDocuments();
		for (QueryDocumentSnapshot doc : documents) {
			O object = doc.toObject(clazz);
			PropertyUtils.setProperty(object, ID, doc.getId());
			result.add(object);
		}
		return result;
	}

	@Override
	public Map<String, Object> getAsMap(String id) throws Exception {
		DocumentReference reference = getCollection().document(id);
		ApiFuture<DocumentSnapshot> futureDoc = reference.get();
		DocumentSnapshot document = futureDoc.get();
		if (document.exists()) {
			return document.getData();
		}
		return null;
	}

	@Override
	public List<O> getAllFiltro1(String filtro1, String valueFiltro1, String fieldOrder) throws Exception {
		List<O> result = new ArrayList<O>();
		ApiFuture<QuerySnapshot> query = getCollection().whereEqualTo(filtro1, valueFiltro1).orderBy(fieldOrder).get();
		List<QueryDocumentSnapshot> documents = query.get().getDocuments();
		for (QueryDocumentSnapshot doc : documents) {
			O object = doc.toObject(clazz);
			PropertyUtils.setProperty(object, ID, doc.getId());
			result.add(object);
		}
		return result;
	}

	@Override
	public List<O> getAllFiltro2(String filtro1, String valueFiltro1, String filtro2, String valueFiltro2)
			throws Exception {
		List<O> result = new ArrayList<O>();
		ApiFuture<QuerySnapshot> query = getCollection().whereEqualTo(filtro1, valueFiltro1)
				.whereEqualTo(filtro2, valueFiltro2).get();
		List<QueryDocumentSnapshot> documents = query.get().getDocuments();
		for (QueryDocumentSnapshot doc : documents) {
			O object = doc.toObject(clazz);
			PropertyUtils.setProperty(object, ID, doc.getId());
			result.add(object);
		}
		return result;
	}

	@Override
	public List<O> getAllFiltro3(String filtro1, String valueFiltro1, String filtro2, String valueFiltro2,
			String filtro3, String valueFiltro3, String fieldOrder) throws Exception {
		List<O> result = new ArrayList<O>();
		ApiFuture<QuerySnapshot> query = getCollection().whereEqualTo(filtro1, valueFiltro1)
				.whereEqualTo(filtro2, valueFiltro2).whereEqualTo(filtro3, valueFiltro3).orderBy(fieldOrder).get();
		List<QueryDocumentSnapshot> documents = query.get().getDocuments();
		for (QueryDocumentSnapshot doc : documents) {
			O object = doc.toObject(clazz);
			PropertyUtils.setProperty(object, ID, doc.getId());
			result.add(object);
		}
		return result;
	}

	@Override
	public List<O> getAllFiltro4(String filtro1, String valueFiltro1, String filtro2, String valueFiltro2,
			String filtro3, String valueFiltro3, String filtro4, String valueFiltro4, String fieldOrder)
			throws Exception {
		List<O> result = new ArrayList<O>();
		ApiFuture<QuerySnapshot> query = getCollection().whereEqualTo(filtro1, valueFiltro1)
				.whereEqualTo(filtro2, valueFiltro2).whereEqualTo(filtro3, valueFiltro3)
				.whereEqualTo(filtro4, valueFiltro4).orderBy(fieldOrder).get();
		List<QueryDocumentSnapshot> documents = query.get().getDocuments();
		for (QueryDocumentSnapshot doc : documents) {
			O object = doc.toObject(clazz);
			PropertyUtils.setProperty(object, ID, doc.getId());
			result.add(object);
		}
		return result;
	}

	/**
	 * Se especifica el nombre de la coleccion
	 * 
	 * @return referencia de la coleccion
	 */
	public abstract CollectionReference getCollection();
}
