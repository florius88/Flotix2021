package com.flotix.activities.ui.alquileres

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.LinearLayoutManager
import com.flotix.R
import com.flotix.adapter.ListAdapterAlquileres
import com.flotix.dto.AlquilerDTO
import com.flotix.dto.ClienteDTO
import com.flotix.dto.VehiculoDTO
import com.flotix.model.*
import com.google.firebase.firestore.EventListener
import com.google.firebase.firestore.FirebaseFirestore
import com.google.firebase.firestore.ListenerRegistration
import kotlinx.android.synthetic.main.fragment_home.*
import java.text.SimpleDateFormat

class AlquilerFragment : Fragment() {

    private val TAG = "AlquilerFragment"

    private var mAdapter: ListAdapterAlquileres? = null

    private var firestoreDB: FirebaseFirestore? = null
    private var firestoreListener: ListenerRegistration? = null

    private var alquilerList = mutableListOf<AlquilerDTO>()

    private var mapCliente = HashMap<String, ClienteDTO>()
    private var mapVehiculo = HashMap<String, VehiculoDTO>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        firestoreDB = FirebaseFirestore.getInstance()

        //Carga el mapa con los clientes
        loadMapClientes()

        //Carga el mapa con los vehiculos
        loadMapVehiculos()

        //Carga los alquileres
        loadAlquileresList()

        firestoreListener = firestoreDB!!.collection("alquiler")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }

                var alquilerListDTO = mutableListOf<AlquilerDTO>()

                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val alquiler = doc.toObject(Alquiler::class.java)

                        var vehiculoDTO: VehiculoDTO = VehiculoDTO()

                        if (null != mapVehiculo && mapVehiculo.isNotEmpty()){
                            vehiculoDTO = mapVehiculo[alquiler.idVehiculo]!!
                        }

                        var clienteDTO: ClienteDTO = ClienteDTO()

                        if (null != mapCliente && mapCliente.isNotEmpty()){
                            clienteDTO = mapCliente[alquiler.idCliente]!!
                        }

                        var alquilerDTO: AlquilerDTO =
                            AlquilerDTO(doc.id, alquiler.idVehiculo,
                                vehiculoDTO,alquiler.idCliente, clienteDTO,alquiler.fechaInicio,alquiler.fechaFin,
                                alquiler.km,alquiler.tipoKm,alquiler.importe,alquiler.tipoImporte)

                        alquilerListDTO.add(alquilerDTO)
                    }
                }

                alquilerList = alquilerListDTO

                mAdapter = ListAdapterAlquileres(alquilerList)
                list_recycler_view.adapter = mAdapter
            })
    }

    /**
     * Ordena la lista por tipo
     */
    public fun orderAlquilerBy(pos: Int) {
        when (pos) {
            1 -> { // Order by Fecha
                this.alquilerList.sortWith() { uno: AlquilerDTO, dos: AlquilerDTO ->
                    SimpleDateFormat("dd/MM/yyyy").parse(uno.fechaFin)
                        .compareTo(SimpleDateFormat("dd/MM/yyyy").parse(dos.fechaFin))
                }
                mAdapter = ListAdapterAlquileres(alquilerList)
                list_recycler_view.adapter = mAdapter
            }

            2 -> { // Order by Matricula
                this.alquilerList.sortWith() { uno: AlquilerDTO, dos: AlquilerDTO ->
                    uno.vehiculo.matricula.toUpperCase().compareTo(dos.vehiculo.matricula.toUpperCase())
                }
                mAdapter = ListAdapterAlquileres(alquilerList)
                list_recycler_view.adapter = mAdapter
            }

            3 -> { // Order by Cliente
                this.alquilerList.sortWith() { uno: AlquilerDTO, dos: AlquilerDTO ->
                    uno.cliente.nombre.toUpperCase().compareTo(dos.cliente.nombre.toUpperCase())
                }
                mAdapter = ListAdapterAlquileres(alquilerList)
                list_recycler_view.adapter = mAdapter
            }
        }
    }

    override fun onDestroy() {
        super.onDestroy()
        firestoreListener!!.remove()
    }

    override fun onDestroyView() {
        super.onDestroyView()
        firestoreListener!!.remove()
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? =
        inflater.inflate(R.layout.fragment_home, container, false)

    private fun loadMapClientes() {
        firestoreDB!!.collection("cliente")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }
                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val cliente = doc.toObject(Cliente::class.java)
                        val clienteDTO: ClienteDTO = ClienteDTO(cliente.nif,cliente.nombre,cliente.direccion,cliente.poblacion,
                            cliente.personaContacto,cliente.tlfContacto,cliente.email,cliente.idMetodoPago,cliente.cuentaBancaria,cliente.baja)
                        mapCliente.put(doc.id, clienteDTO)
                    }
                }
            })
    }

    private fun loadMapVehiculos() {
        firestoreDB!!.collection("vehiculo")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }
                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val vehiculo = doc.toObject(Vehiculo::class.java)
                        var vehiculoDTO: VehiculoDTO = VehiculoDTO(vehiculo.matricula,vehiculo.fechaMatriculacion,vehiculo.modelo,
                            vehiculo.plazas,vehiculo.capacidad,vehiculo.km,vehiculo.disponibilidad,vehiculo.baja)
                        mapVehiculo.put(doc.id, vehiculoDTO)
                    }
                }
            })
    }

    private fun loadAlquileresList() {
        firestoreDB!!.collection("alquiler")
            .get()
            .addOnCompleteListener { task ->
                if (task.isSuccessful) {

                    val alquilerListDTO = mutableListOf<AlquilerDTO>()

                    for (doc in task.result!!) {
                        val alquiler = doc.toObject(Alquiler::class.java)

                        var vehiculoDTO: VehiculoDTO = VehiculoDTO()

                        if (null != mapVehiculo && mapVehiculo.isNotEmpty()){
                            vehiculoDTO = mapVehiculo[alquiler.idVehiculo]!!
                        }

                        var clienteDTO: ClienteDTO = ClienteDTO()

                        if (null != mapCliente && mapCliente.isNotEmpty()){
                            clienteDTO = mapCliente[alquiler.idCliente]!!
                        }

                        var alquilerDTO: AlquilerDTO =
                            AlquilerDTO(doc.id, alquiler.idVehiculo,
                                vehiculoDTO,alquiler.idCliente, clienteDTO,alquiler.fechaInicio,alquiler.fechaFin,
                                alquiler.km,alquiler.tipoKm,alquiler.importe,alquiler.tipoImporte)

                        alquilerListDTO.add(alquilerDTO)
                    }

                    alquilerList = alquilerListDTO

                    mAdapter = ListAdapterAlquileres(alquilerList)
                    val mLayoutManager = LinearLayoutManager(context!!.applicationContext)
                    list_recycler_view.layoutManager = mLayoutManager
                    list_recycler_view.itemAnimator = DefaultItemAnimator()
                    list_recycler_view.adapter = mAdapter
                } else {
                    Log.d(TAG, "Error getting documents: ", task.exception)
                }
            }
    }

    companion object {
        fun newInstance(): AlquilerFragment = AlquilerFragment()
    }
}