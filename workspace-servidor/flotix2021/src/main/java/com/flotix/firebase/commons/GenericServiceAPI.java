package com.flotix.firebase.commons;

import java.util.List;
import java.util.Map;

public interface GenericServiceAPI<I, O> {

	public String save(I entity, String id) throws Exception;

	public String save(I entity) throws Exception;

	public void delete(String id) throws Exception;

	public O get(String id) throws Exception;

	public Map<String, Object> getAsMap(String id) throws Exception;

	public List<O> getAll(String fieldOrder) throws Exception;

	public List<O> getAllNotBaja(String fieldOrder) throws Exception;

	public List<O> getAllFiltro1(String filtro1, String valueFiltro1, String fieldOrder) throws Exception;

	public List<O> getAllFiltro2(String filtro1, String valueFiltro1, String filtro2, String valueFiltro2)
			throws Exception;

	public List<O> getAllFiltro3(String filtro1, String valueFiltro1, String filtro2, String valueFiltro2,
			String filtro3, String valueFiltro3, String fieldOrder) throws Exception;

	public List<O> getAllFiltro4(String filtro1, String valueFiltro1, String filtro2, String valueFiltro2,
			String filtro3, String valueFiltro3, String filtro4, String valueFiltro4, String fieldOrder)
			throws Exception;
}
