package com.flotix.firebase.commons;

import java.util.List;
import java.util.Map;

/**
 * Gestion de datos con Firebase
 * 
 * @author Flor
 *
 * @param <I> objeto de BD - entrada
 * @param <O> objeto DTO - salida
 */
public interface GenericServiceAPI<I, O> {

	/**
	 * Modifica un objeto
	 * 
	 * @param entity objeto de entrada
	 * @param id     identificador del objeto
	 * @return identificador del objeto
	 * @throws Exception
	 */
	public String save(I entity, String id) throws Exception;

	/**
	 * Crea un objeto
	 * 
	 * @param entity objeto de entrada
	 * @return identificador del objeto
	 * @throws Exception
	 */
	public String save(I entity) throws Exception;

	/**
	 * Borra un objeto
	 * 
	 * @param id identificador del objeto
	 * @throws Exception
	 */
	public void delete(String id) throws Exception;

	/**
	 * Obtiene un objeto
	 * 
	 * @param id identificador del objeto
	 * @return objeto DTO
	 * @throws Exception
	 */
	public O get(String id) throws Exception;

	/**
	 * Obtiene un mapa del objeto
	 * 
	 * @param id identificador del objeto
	 * @return mapa del objeto
	 * @throws Exception
	 */
	public Map<String, Object> getAsMap(String id) throws Exception;

	/**
	 * Obtiene una lista de objetos ordenada
	 * 
	 * @param fieldOrder campo de ordenacion
	 * @return lista de objetos ordenada
	 * @throws Exception
	 */
	public List<O> getAll(String fieldOrder) throws Exception;

	/**
	 * Obtiene una lista de objetos que no estan dados de baja ordenada
	 * 
	 * @param fieldOrder campo de ordenacion
	 * @return lista de objetos que no estan dados de baja ordenada
	 * @throws Exception
	 */
	public List<O> getAllNotBaja(String fieldOrder) throws Exception;

	/**
	 * Obtiene una lista de objetos con un filtro ordenada
	 * 
	 * @param filtro1      campo de filtro
	 * @param valueFiltro1 valor del filtro
	 * @param fieldOrder   campo de ordenacion
	 * @return lista de objetos con un filtro ordenada
	 * @throws Exception
	 */
	public List<O> getAllFiltro1(String filtro1, Object valueFiltro1, String fieldOrder) throws Exception;

	/**
	 * Obtiene una lista de objetos con dos filtros
	 * 
	 * @param filtro1      campo de filtro1
	 * @param valueFiltro1 valor del filtro1
	 * @param filtro2      campo de filtro2
	 * @param valueFiltro2 valor del filtro2
	 * @return lista de objetos con dos filtros
	 * @throws Exception
	 */
	public List<O> getAllFiltro2(String filtro1, Object valueFiltro1, String filtro2, Object valueFiltro2,
			String fieldOrder) throws Exception;

	/**
	 * Obtiene una lista de objetos con tres filtros ordenada
	 * 
	 * @param filtro1      campo de filtro1
	 * @param valueFiltro1 valor del filtro1
	 * @param filtro2      campo de filtro2
	 * @param valueFiltro2 valor del filtro2
	 * @param filtro3      campo de filtro3
	 * @param valueFiltro3 valor del filtro3
	 * @param fieldOrder   campo de ordenacion
	 * @return lista de objetos con tres filtros ordenada
	 * @throws Exception
	 */
	public List<O> getAllFiltro3(String filtro1, Object valueFiltro1, String filtro2, Object valueFiltro2,
			String filtro3, Object valueFiltro3, String fieldOrder) throws Exception;

}
