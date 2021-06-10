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

class AlquilerFragment : Fragment() {

    private val TAG = "AlquilerFragment"

    private var mAdapter: ListAdapterAlquileres? = null

    private var firestoreDB: FirebaseFirestore? = null
    private var firestoreListener: ListenerRegistration? = null

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

        firestoreListener = firestoreDB!!.collection("alquiler").orderBy("fechaFin")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }

                val alquilerList = mutableListOf<AlquilerDTO>()

                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val alquiler = doc.toObject(Alquiler::class.java)

                        var vehiculo: VehiculoDTO = mapVehiculo[alquiler.idVehiculo]!!
                        var cliente: ClienteDTO = mapCliente[alquiler.idCliente]!!

                        var alquilerDTO: AlquilerDTO =
                            AlquilerDTO(doc.id, alquiler.idVehiculo,
                                vehiculo,alquiler.idCliente, cliente,alquiler.fechaInicio,alquiler.fechaFin,
                                alquiler.km,alquiler.tipoKm,alquiler.importe,alquiler.tipoImporte)

                        alquilerList.add(alquilerDTO)
                    }
                }

                mAdapter = ListAdapterAlquileres(alquilerList)
                list_recycler_view.adapter = mAdapter
            })
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
        firestoreDB!!.collection("alquiler").orderBy("fechaFin")
            .get()
            .addOnCompleteListener { task ->
                if (task.isSuccessful) {
                    val alquilerList = mutableListOf<AlquilerDTO>()

                    for (doc in task.result!!) {
                        val alquiler = doc.toObject(Alquiler::class.java)

                        var vehiculo: VehiculoDTO = mapVehiculo[alquiler.idVehiculo]!!
                        var cliente: ClienteDTO = mapCliente[alquiler.idCliente]!!

                        var alquilerDTO: AlquilerDTO =
                            AlquilerDTO(doc.id, alquiler.idVehiculo,
                                vehiculo,alquiler.idCliente, cliente,alquiler.fechaInicio,alquiler.fechaFin,
                                alquiler.km,alquiler.tipoKm,alquiler.importe,alquiler.tipoImporte)

                        alquilerList.add(alquilerDTO)
                    }

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