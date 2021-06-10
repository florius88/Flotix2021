package com.flotix.model

import java.io.Serializable

class Alquiler(
    var idVehiculo: String = "",
    var idCliente: String = "",
    var fechaInicio: String = "",
    var fechaFin: String = "",
    var km: Int = 0,
    var tipoKm: String = "",
    val importe: Double = 0.0,
    var tipoImporte: String = ""
) : Serializable {
}