package com.flotix.activities.ui.home

import android.os.Bundle
import android.util.Log
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import androidx.fragment.app.Fragment
import androidx.recyclerview.widget.LinearLayoutManager
import com.flotix.R
import com.flotix.adapter.ListAdapter
import com.flotix.dto.Alerta
import com.flotix.dto.AlertaDTO
import com.flotix.dto.TipoAlerta
import com.google.firebase.firestore.FirebaseFirestore
import kotlinx.android.synthetic.main.fragment_home.*

class HomeFragment : Fragment() {

    /*private lateinit var homeViewModel: HomeViewModel

    override fun onCreateView(
        inflater: LayoutInflater,
        container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        homeViewModel =
            ViewModelProvider(this).get(HomeViewModel::class.java)
        val root = inflater.inflate(R.layout.fragment_home, container, false)
        val textView: TextView = root.findViewById(R.id.text_home)
        homeViewModel.text.observe(viewLifecycleOwner, Observer {
            textView.text = it
        })
        return root
    }*/

    // Cloud Firestore
    private lateinit var db: FirebaseFirestore

    private var alertas = mutableListOf<AlertaDTO>()
    private lateinit var tipoAlerta: TipoAlerta
    private var mapTipoAlerta = HashMap<String,TipoAlerta>()

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        retainInstance = true

        //auth = Firebase.auth
        db = FirebaseFirestore.getInstance()

        db.collection("tipoAlerta").get()
            .addOnSuccessListener { result ->
                for (item in result) {
                    val tipoAlerta = item.toObject(TipoAlerta::class.java)
                    mapTipoAlerta.put(item.id,tipoAlerta)
                }
            }
            .addOnFailureListener { exception ->
                Log.i("fairebase", "error al cargar tipoAlerta")
                Toast.makeText(context!!, "service_error", Toast.LENGTH_SHORT).show()
            }
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? =
        inflater.inflate(R.layout.fragment_home, container, false)


    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        list_recycler_view.apply {
            layoutManager = LinearLayoutManager(activity)

            db.collection("alerta").get()
                .addOnSuccessListener { result ->
                    for (item in result) {
                        val alerta = item.toObject(Alerta::class.java)
                        var alertaDTO: AlertaDTO = AlertaDTO(mapTipoAlerta.get(alerta.idTipoAlerta)!!.nombre,alerta.matricula,alerta.nombreCliente,alerta.vencimiento)
                        alertas.add(alertaDTO)
                    }

                    adapter = ListAdapter(alertas)
                }
                .addOnFailureListener { exception ->
                    Log.i("fairebase", "error al cargar sitios")
                    Toast.makeText(context!!, "service_error", Toast.LENGTH_SHORT).show()
                }
        }
    }

    companion object {
        fun newInstance(): HomeFragment = HomeFragment()
    }
}