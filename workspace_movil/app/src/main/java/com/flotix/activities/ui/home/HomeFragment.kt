package com.flotix.activities.ui.home

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.LinearLayoutManager
import com.flotix.R
import com.flotix.adapter.ListAdapterAlertas
import com.flotix.dto.AlertaDTO
import com.flotix.model.Alerta
import com.flotix.model.TipoAlerta
import com.google.firebase.firestore.EventListener
import com.google.firebase.firestore.FirebaseFirestore
import com.google.firebase.firestore.ListenerRegistration
import kotlinx.android.synthetic.main.fragment_home.*
import java.lang.Exception

class HomeFragment : Fragment() {

    private val TAG = "HomeFragment"

    private var mAdapter: ListAdapterAlertas? = null

    private var firestoreDB: FirebaseFirestore? = null
    private var firestoreListener: ListenerRegistration? = null

    private var alertaList = mutableListOf<AlertaDTO>()

    private var mapTipoAlerta = HashMap<String, TipoAlerta>()

    private var order : String = "vencimiento"

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        try {
            firestoreDB = FirebaseFirestore.getInstance()

            //Carga el mapa con los tipos de alertas
            loadMapTipoAlerta()
            //Carga las alertas
            loadAlertasList()

            firestoreListener = firestoreDB!!.collection("alerta").orderBy(order)
                .addSnapshotListener(EventListener { documentSnapshots, e ->
                    if (e != null) {
                        Log.e(TAG, "Listen failed!", e)
                        return@EventListener
                    }

                    val alertaListDTO = mutableListOf<AlertaDTO>()

                    if (documentSnapshots != null) {
                        for (doc in documentSnapshots) {
                            val alerta = doc.toObject(Alerta::class.java)

                            var nombreAlerta = ""

                            if (null != mapTipoAlerta && mapTipoAlerta.isNotEmpty()){
                                nombreAlerta = mapTipoAlerta[alerta.idTipoAlerta]!!.nombre
                            }

                            var alertaDTO: AlertaDTO =
                                AlertaDTO(doc.id,nombreAlerta, alerta.matricula, alerta.nombreCliente, alerta.tlfContacto, alerta.vencimiento)
                            alertaListDTO.add(alertaDTO)
                        }
                    }

                    if(alertaListDTO.size == 0) {
                        listaVacia();
                    } else {
                        listaConDatos();
                    }

                    alertaList = alertaListDTO

                    mAdapter = ListAdapterAlertas(alertaList)
                    list_recycler_view.adapter = mAdapter
                })
        } catch (ex: Exception) {
            Toast.makeText(requireContext(),"Se ha producido un error.", Toast.LENGTH_SHORT).show();
        }
    }

    /**
     * Cambia el campo a ordenar y solicita de nuevo a la BD la informacion ordenada
     */
    public fun orderAlertaBy(ordenar: String){
        order = ordenar
        //Carga las alertas
        loadAlertasList()
    }

    /**
     * Ordena la lista por tipo de alerta
     */
    public fun orderAlertaByDescripcion() {
        // Order by Descripcion
        this.alertaList.sortWith() { uno: AlertaDTO, dos: AlertaDTO ->
            uno.tipoAlerta.toUpperCase().compareTo(dos.tipoAlerta.toUpperCase())
        }
        mAdapter = ListAdapterAlertas(alertaList)
        list_recycler_view.adapter = mAdapter
    }

    /**
     * Cargo esta función si la lista no contiene datos
     */
    fun listaVacia() {
        cLayout_no_dato.setVisibility(View.VISIBLE)
        list_recycler_view.setVisibility(View.GONE)
    }

    /**
     * Cargo esta función si la lista contiene datos
     */
    fun listaConDatos() {
        cLayout_no_dato.setVisibility(View.GONE)
        list_recycler_view.setVisibility(View.VISIBLE)
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

    private fun loadMapTipoAlerta() {
        firestoreDB!!.collection("tipoAlerta")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }
                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val tipoAlerta = doc.toObject(TipoAlerta::class.java)
                        mapTipoAlerta.put(doc.id, tipoAlerta)
                    }
                }
            })
    }

    private fun loadAlertasList() {
        firestoreDB!!.collection("alerta").orderBy(order)
            .get()
            .addOnCompleteListener { task ->
                if (task.isSuccessful) {
                    val alertaListDTO = mutableListOf<AlertaDTO>()

                    for (doc in task.result!!) {
                        val alerta = doc.toObject(Alerta::class.java)

                        var nombreAlerta = "No-Carga2"

                        if (null != mapTipoAlerta && mapTipoAlerta.isNotEmpty()){
                            nombreAlerta = mapTipoAlerta[alerta.idTipoAlerta]!!.nombre
                        }

                        var alertaDTO: AlertaDTO =
                            AlertaDTO(doc.id,nombreAlerta, alerta.matricula, alerta.nombreCliente, alerta.tlfContacto, alerta.vencimiento)
                        alertaListDTO.add(alertaDTO)
                    }

                    alertaList = alertaListDTO

                    mAdapter = ListAdapterAlertas(alertaList)
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
        fun newInstance(): HomeFragment = HomeFragment()
    }
}