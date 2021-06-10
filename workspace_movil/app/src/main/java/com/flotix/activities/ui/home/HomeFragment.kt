package com.flotix.activities.ui.home

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.DefaultItemAnimator
import androidx.recyclerview.widget.LinearLayoutManager
import com.flotix.R
import com.flotix.adapter.ListAdapterAlertas
import com.flotix.model.Alerta
import com.flotix.dto.AlertaDTO
import com.flotix.model.TipoAlerta
import com.google.firebase.firestore.EventListener
import com.google.firebase.firestore.FirebaseFirestore
import com.google.firebase.firestore.ListenerRegistration
import kotlinx.android.synthetic.main.fragment_home.*

class HomeFragment : Fragment() {

    private val TAG = "HomeFragment"

    private var mAdapter: ListAdapterAlertas? = null

    private var firestoreDB: FirebaseFirestore? = null
    private var firestoreListener: ListenerRegistration? = null

    private var mapTipoAlerta = HashMap<String, TipoAlerta>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        firestoreDB = FirebaseFirestore.getInstance()

        //Carga el mapa con los tipos de alertas
        loadMapTipoAlerta()
        //Carga las alertas
        loadAlertasList()

        firestoreListener = firestoreDB!!.collection("alerta").orderBy("vencimiento")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }

                val alertaList = mutableListOf<AlertaDTO>()

                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val alerta = doc.toObject(Alerta::class.java)

                        var nombreAlerta = ""

                        if (null != mapTipoAlerta && mapTipoAlerta.isNotEmpty()){
                            nombreAlerta = mapTipoAlerta[alerta.idTipoAlerta]!!.nombre
                        }

                        var alertaDTO: AlertaDTO =
                            AlertaDTO(doc.id,nombreAlerta, alerta.matricula, alerta.nombreCliente, alerta.vencimiento)
                        alertaList.add(alertaDTO)
                    }
                }

                mAdapter = ListAdapterAlertas(alertaList)
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
        firestoreDB!!.collection("alerta").orderBy("vencimiento")
            .get()
            .addOnCompleteListener { task ->
                if (task.isSuccessful) {
                    val alertaList = mutableListOf<AlertaDTO>()

                    for (doc in task.result!!) {
                        val alerta = doc.toObject(Alerta::class.java)

                        var nombreAlerta = "No-Carga2"

                        if (null != mapTipoAlerta && mapTipoAlerta.isNotEmpty()){
                            nombreAlerta = mapTipoAlerta[alerta.idTipoAlerta]!!.nombre
                        }

                        var alertaDTO: AlertaDTO =
                            AlertaDTO(doc.id,nombreAlerta, alerta.matricula, alerta.nombreCliente, alerta.vencimiento)
                        alertaList.add(alertaDTO)
                    }

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