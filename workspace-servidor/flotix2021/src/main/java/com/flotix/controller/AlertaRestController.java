package com.flotix.controller;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.concurrent.TimeUnit;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.flotix.dto.AlertaDTO;
import com.flotix.dto.AlquilerDTO;
import com.flotix.dto.CaducidadDTO;
import com.flotix.dto.MantenimientoDTO;
import com.flotix.dto.TipoAlertaDTO;
import com.flotix.firebase.model.Alerta;
import com.flotix.firebase.service.AlertaServiceAPI;
import com.flotix.firebase.service.TipoAlertaServiceAPI;
import com.flotix.response.bean.ErrorBean;
import com.flotix.response.bean.ServerResponseAlerta;
import com.flotix.utils.MessageExceptions;
import com.flotix.utils.SpringUtils;

/**
 * Controlador que gestiona las alertas
 * 
 * @author Flor
 *
 */
@Controller
@RestController
@RequestMapping(value = "/api/alerta/")
@CrossOrigin("*")
public class AlertaRestController {

	@Autowired
	private AlertaServiceAPI alertaServiceAPI;

	@Autowired
	private TipoAlertaServiceAPI tipoAlertaServiceAPI;

        private enum EnumTipoAlerta {
		ITV, SEGURO, RUEDAS, REVISIÓN
	};

	/**
	 * Devuelve los datos con los filtros: Tipo, Cliente y Matricula
	 * 
	 * @param tipo      de alerta
	 * @param cliente
	 * @param matricula
	 * @return ServerResponseAlerta
	 */
	@GetMapping(value = "/allFilter/{tipo}/{cliente}/{matricula}")
	public ServerResponseAlerta getAllFilter(@PathVariable String tipo, @PathVariable String cliente,
			@PathVariable String matricula) {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			List<AlertaDTO> listaResult = new ArrayList<AlertaDTO>();
			List<AlertaDTO> listaBD = null;

			if (!"null".equalsIgnoreCase(tipo)) {
				listaBD = alertaServiceAPI.getAllFiltro1("idTipoAlerta", tipo, "vencimiento");

				if (null != listaBD) {
					for (AlertaDTO alerta : listaBD) {
						// Busca el tipo de alerta
						if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
							TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
							alerta.setTipoAlerta(tipoAlerta);
						}
					}
				}
			} else {
				listaBD = alertaServiceAPI.getAll("vencimiento");

				if (null != listaBD) {
					for (AlertaDTO alerta : listaBD) {
						// Busca el tipo de alerta
						if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
							TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
							alerta.setTipoAlerta(tipoAlerta);
						}
					}
				}
			}

			if (!"null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alerta -> alerta.getNombreCliente().contains(cliente)
						&& alerta.getMatricula().contains(matricula)).collect(Collectors.toList());
			} else if (!"null".equalsIgnoreCase(cliente) && "null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alerta -> alerta.getNombreCliente().contains(cliente))
						.collect(Collectors.toList());
			} else if ("null".equalsIgnoreCase(cliente) && !"null".equalsIgnoreCase(matricula)) {
				listaResult = listaBD.stream().filter(alerta -> alerta.getMatricula().contains(matricula))
						.collect(Collectors.toList());
			} else {
				listaResult.addAll(listaBD);
			}

			result.setListaAlerta(listaResult);
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.OK_CODE);
			error.setMessage(MessageExceptions.MSSG_OK);
			result.setError(error);

		} catch (Exception e) {
			// LOG
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	/**
	 * Devuelve todos los datos
	 * 
	 * @return ServerResponseAlerta
	 */
	@GetMapping(value = "/all")
	public ServerResponseAlerta getAll() {

		ServerResponseAlerta result = new ServerResponseAlerta();

		try {

			List<AlertaDTO> listaBD = alertaServiceAPI.getAll("vencimiento");

			if (null != listaBD) {
				for (AlertaDTO alerta : listaBD) {
					// Busca el tipo de alerta
					if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
						TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
						alerta.setTipoAlerta(tipoAlerta);
					}
				}
			}

			result.setListaAlerta(listaBD);
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.OK_CODE);
			error.setMessage(MessageExceptions.MSSG_OK);
			result.setError(error);

		} catch (Exception e) {
			// LOG
			ErrorBean error = new ErrorBean();
			error.setCode(MessageExceptions.GENERIC_ERROR_CODE);
			error.setMessage(MessageExceptions.MSSG_GENERIC_ERROR);
			result.setError(error);
		}

		return result;
	}

	public void cargaAlertas() {

		try {

			List<Alerta> result = new ArrayList<Alerta>();

			TipoAlertaRestController tipoAlertaRestController = (TipoAlertaRestController) SpringUtils.ctx
					.getBean(TipoAlertaRestController.class);

			Map<String, String> mapTipoAlerta = tipoAlertaRestController.getListTipoAlertaDTO();

			CaducidadRestController caducidadRestController = (CaducidadRestController) SpringUtils.ctx
					.getBean(CaducidadRestController.class);

			MantenimientoRestController mantenimientoRestController = (MantenimientoRestController) SpringUtils.ctx
					.getBean(MantenimientoRestController.class);

			AlquilerRestController alquilerRestController = (AlquilerRestController) SpringUtils.ctx
					.getBean(AlquilerRestController.class);

			List<CaducidadDTO> listaCaducidad = caducidadRestController.getListCaducidadDTO();
			List<MantenimientoDTO> listaMantenimiento = mantenimientoRestController.getAllListMantenimientoDTO();

			if (null != listaCaducidad) {
				for (CaducidadDTO caducidad : listaCaducidad) {

					Date fecha1 = new Date();
					Date fecha2 = new SimpleDateFormat("dd/MM/yyyy").parse(caducidad.getProximaITV());

					Integer vencimiento = vencimiento(fecha1, fecha2);

					if (null != vencimiento) {
						if (30 >= vencimiento.intValue()) {

							Alerta alerta = new Alerta();

							String idTipoAlerta = mapTipoAlerta.get(EnumTipoAlerta.ITV.name());
							alerta.setIdTipoAlerta(idTipoAlerta);

							alerta.setMatricula(caducidad.getVehiculo().getMatricula());

							AlquilerDTO alquilerDTO = alquilerRestController
									.getAlquilerByMatricula(caducidad.getVehiculo().getMatricula());

							alerta.setNombreCliente("");
							alerta.setTlfContacto("");
							if (null != alquilerDTO) {
								alerta.setNombreCliente(alquilerDTO.getCliente().getNombre());
								alerta.setTlfContacto(alquilerDTO.getCliente().getTlfContacto());
				}

							alerta.setVencimiento(vencimiento);

							result.add(alerta);
						}
					}

					fecha1 = new Date();
					fecha2 = new SimpleDateFormat("dd/MM/yyyy").parse(caducidad.getVenciminetoVehiculo());

					vencimiento = vencimiento(fecha1, fecha2);

					if (null != vencimiento) {
						if (30 >= vencimiento) {

							Alerta alerta = new Alerta();

							String idTipoAlerta = mapTipoAlerta.get(EnumTipoAlerta.SEGURO.name());
							alerta.setIdTipoAlerta(idTipoAlerta);

							alerta.setMatricula(caducidad.getVehiculo().getMatricula());

							AlquilerDTO alquilerDTO = alquilerRestController
									.getAlquilerByMatricula(caducidad.getVehiculo().getMatricula());

							alerta.setNombreCliente("");
							alerta.setTlfContacto("");
							if (null != alquilerDTO) {
								alerta.setNombreCliente(alquilerDTO.getCliente().getNombre());
								alerta.setTlfContacto(alquilerDTO.getCliente().getTlfContacto());
			}

							alerta.setVencimiento(vencimiento);

							result.add(alerta);
						}
					}
				}
		}

			if (null != listaMantenimiento) {
				for (MantenimientoDTO mantenimiento : listaMantenimiento) {

					Date fecha1 = new Date();
					Date fecha2 = new SimpleDateFormat("dd/MM/yyyy").parse(mantenimiento.getProximoMantenimiento());

					Integer vencimiento = vencimiento(fecha1, fecha2);

					if (null != vencimiento) {
						if (30 >= vencimiento) {

							Alerta alerta = new Alerta();

							String idTipoAlerta = mapTipoAlerta.get(mantenimiento.getTipoMantenimiento().getNombre());
							alerta.setIdTipoAlerta(idTipoAlerta);

							alerta.setMatricula(mantenimiento.getVehiculo().getMatricula());

							AlquilerDTO alquilerDTO = alquilerRestController
									.getAlquilerByMatricula(mantenimiento.getVehiculo().getMatricula());

							alerta.setNombreCliente("");
							alerta.setTlfContacto("");
							if (null != alquilerDTO) {
								alerta.setNombreCliente(alquilerDTO.getCliente().getNombre());
								alerta.setTlfContacto(alquilerDTO.getCliente().getTlfContacto());
	}

							alerta.setVencimiento(vencimiento);

							result.add(alerta);
						}
					}
				}
			}

			if (!result.isEmpty()) {

				List<Alerta> listaNuevaAlerta = new ArrayList<Alerta>();
				Map<String, Alerta> mapaActualizarAlerta = new HashMap<String, Alerta>();
				List<String> listaBorrarAlerta = new ArrayList<String>();

				List<AlertaDTO> listaAlertaBD = getListAlertaDTO();

				if (null != listaAlertaBD && !listaAlertaBD.isEmpty()) {

					Map<String, AlertaDTO> mapAlertaBD = new HashMap<String, AlertaDTO>();

					for (AlertaDTO alertaBD : listaAlertaBD) {
						mapAlertaBD.put(alertaBD.getMatricula() + "-" + alertaBD.getIdTipoAlerta(), alertaBD);
					}

					for (Alerta alerta : result) {

						AlertaDTO alertaBD = mapAlertaBD.get(alerta.getMatricula() + "-" + alerta.getIdTipoAlerta());

						if (null != alertaBD) {
							mapaActualizarAlerta.put(alertaBD.getId(), alerta);
			} else {
							listaNuevaAlerta.add(alerta);
						}
					}

					Map<String, Alerta> mapAlertaResult = new HashMap<String, Alerta>();

					for (Alerta alerta : result) {
						mapAlertaResult.put(alerta.getMatricula() + "-" + alerta.getIdTipoAlerta(), alerta);
					}

					for (AlertaDTO alertaBD : listaAlertaBD) {

						if (null == mapAlertaResult.get(alertaBD.getMatricula() + "-" + alertaBD.getIdTipoAlerta())) {
							listaBorrarAlerta.add(alertaBD.getId());
						}
					}

				} else {
					listaNuevaAlerta = result;
				}

				for (Alerta alerta : listaNuevaAlerta) {
					save(alerta, null);
				}

				Iterator<?> iterator = mapaActualizarAlerta.entrySet().iterator();

				while (iterator.hasNext()) {
					@SuppressWarnings("rawtypes")
					Map.Entry mEntry = (Map.Entry) iterator.next();

					Alerta alerta = (Alerta) mEntry.getValue();
					String id = (String) mEntry.getKey();

					save(alerta, id);
				}

				for (String id : listaBorrarAlerta) {
					delete(id);
				}
			}

		} catch (Exception e) {
			// LOG
		}
	}

	private List<AlertaDTO> getListAlertaDTO() {

		List<AlertaDTO> result = new ArrayList<AlertaDTO>();

		try {

			result = alertaServiceAPI.getAll("vencimiento");

			if (null != result) {
				for (AlertaDTO alerta : result) {
					// Busca el tipo de alerta
					if (null != alerta.getIdTipoAlerta() && !alerta.getIdTipoAlerta().isEmpty()) {
						TipoAlertaDTO tipoAlerta = tipoAlertaServiceAPI.get(alerta.getIdTipoAlerta());
						alerta.setTipoAlerta(tipoAlerta);
					}
				}
			}

		} catch (Exception e) {
			// LOG
			result = null;
		}

		return result;
	}

	public boolean save(Alerta alerta, String id) {

		boolean result = true;

		try {

			if (id == null) {
				id = alertaServiceAPI.save(alerta);
			} else {
				alertaServiceAPI.save(alerta, id);
			}
		} catch (Exception e) {
			// LOG
			result = false;
		}

		return result;
	}

	private boolean delete(String id) {

		boolean result = true;

		try {

				alertaServiceAPI.delete(id);

		} catch (Exception e) {
			// LOG
			result = false;
		}

		return result;
			}

	private Integer vencimiento(Date fecha1, Date fecha2) {

		Integer vencimiento = null;

		try {

			long startTime = fecha1.getTime();
			long endTime = fecha2.getTime();
			long diffTime = endTime - startTime;
			vencimiento = (int) TimeUnit.DAYS.convert(diffTime, TimeUnit.MILLISECONDS);

		} catch (Exception e) {
			// LOG
			vencimiento = null;
		}

		return vencimiento;

	}
}
