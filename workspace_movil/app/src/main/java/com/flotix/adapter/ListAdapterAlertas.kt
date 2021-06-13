package com.flotix.adapter

import android.view.LayoutInflater
import android.view.ViewGroup
import android.widget.ImageView
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView
import com.flotix.R
import com.flotix.dto.AlertaDTO

class ListAdapterAlertas(private val list: List<AlertaDTO>)
    : RecyclerView.Adapter<AlertaViewHolder>() {

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): AlertaViewHolder {
        val inflater = LayoutInflater.from(parent.context)
        return AlertaViewHolder(inflater, parent)
    }

    override fun onBindViewHolder(holder: AlertaViewHolder, position: Int) {
        val alerta: AlertaDTO = list[position]
        holder.bind(alerta)
    }

    override fun getItemCount(): Int = list.size

}

class AlertaViewHolder(inflater: LayoutInflater, parent: ViewGroup) :
    RecyclerView.ViewHolder(inflater.inflate(R.layout.item_list_alerta, parent, false)) {

    enum class TipoAlerta {
        ITV, RUEDAS, REVISIÓN, SEGURO
    }

    private var imgAlerta: ImageView? = null
    private var textMatricula: TextView? = null
    private var textCliente: TextView? = null
    private var textDescripcion: TextView? = null
    private var textTlf: TextView? = null
    private var textVencimiento: TextView? = null
    private var imgVencimiento: ImageView? = null

    init {
        imgAlerta = itemView.findViewById(R.id.imageAlerta)
        textMatricula = itemView.findViewById(R.id.txtMatriculaAlq)
        textCliente = itemView.findViewById(R.id.txtClienteAlq)
        textDescripcion = itemView.findViewById(R.id.txtDescripcion)
        textTlf = itemView.findViewById(R.id.txtTlfAlq)
        textVencimiento = itemView.findViewById(R.id.txtVencimiento)
        imgVencimiento = itemView.findViewById(R.id.imageVencimiento)
    }

    fun bind(alerta: AlertaDTO) {

        if(TipoAlerta.ITV.name.equals(alerta.tipoAlerta) || TipoAlerta.SEGURO.name.equals(alerta.tipoAlerta)){
            //Calendario
            imgAlerta?.setImageResource(R.drawable.icon_calendar)
        } else {
            //Revision
            imgAlerta?.setImageResource(R.drawable.icon_car_service)
        }
        textMatricula?.text = alerta.matricula
        textCliente?.text = alerta.nombreCliente
        textDescripcion?.text = alerta.tipoAlerta
        textTlf?.text = alerta.tlfContacto
        textVencimiento?.text = alerta.vencimiento.toString() + " días"

        //Segun los dias de vencimiento se pone un color u otro
        if(7 >= alerta.vencimiento){
            //Color rojo
            imgVencimiento?.setImageResource(R.drawable.ico_rojo)
        } else {
            //Color amarillo
            imgVencimiento?.setImageResource(R.drawable.ico_amarillo)
        }
    }
}