package com.flotix.dto

class VehiculoDTO(
    var matricula: String = "",
    var fechaMatriculacion: String = "",
    var modelo: String = "",
    var plazas: Int = 0,
    var capacidad: Int = 0,
    // TODO permiso de circulacion
    var km: Int = 0,
    var disponibilidad: Boolean = false,
    var baja: Boolean = false
) {
}