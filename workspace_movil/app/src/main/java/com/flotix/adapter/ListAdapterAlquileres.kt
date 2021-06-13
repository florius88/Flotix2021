package com.flotix.adapter

import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.flotix.R
import com.flotix.dto.AlquilerDTO

class ListAdapterAlquileres(private val list: List<AlquilerDTO>)
    : RecyclerView.Adapter<AlquilerViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlquilerViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        return AlquilerViewHolder(inflater, parent)
    }

    override fun onBindViewHolder(holder: AlquilerViewHolder, position: Int) {
        val alquiler: AlquilerDTO = list[position]
        holder.bind(alquiler)
    }

    override fun getItemCount(): Int = list.size

}

class AlquilerViewHolder(inflater: LayoutInflater, parent: ViewGroup) :
    RecyclerView.ViewHolder(inflater.inflate(R.layout.item_list_alquiler, parent, false)) {

    private var textCliente: TextView? = null
    private var textTlf: TextView? = null
    private var textMatricula: TextView? = null
    private var textFinContrato: TextView? = null
    private var textImporte: TextView? = null

    init {
        textCliente = itemView.findViewById(R.id.txtClienteAlq)
        textTlf = itemView.findViewById(R.id.txtTlfAlq)
        textMatricula = itemView.findViewById(R.id.txtMatriculaAlq)
        textFinContrato = itemView.findViewById(R.id.txtFinContrato)
        textImporte = itemView.findViewById(R.id.txtImporte)
    }

    fun bind(alquiler: AlquilerDTO) {

        textCliente?.text = alquiler.cliente.nombre
        textTlf?.text = alquiler.cliente.tlfContacto
        textMatricula?.text = alquiler.vehiculo.matricula
        textFinContrato?.text = alquiler.fechaFin
        textImporte?.text = alquiler.importe.toString() + "€"
    }
}