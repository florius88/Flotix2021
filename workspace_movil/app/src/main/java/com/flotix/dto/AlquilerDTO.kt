package com.flotix.dto

class AlquilerDTO(
    var id: String = "",
    var idVehiculo: String = "",
    var vehiculo: VehiculoDTO,
    var idCliente: String = "",
    var cliente: ClienteDTO,
    var fechaInicio: String = "",
    var fechaFin: String = "",
    var km: Int = 0,
    var tipoKm: String = "",
    val importe: Double = 0.0,
    var tipoImporte: String = ""
) {
}