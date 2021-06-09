package com.flotix.dto

import java.io.Serializable

class AlertaDTO(
    var tipoAlerta: String = "",
    var matricula: String = "",
    var nombreCliente: String = "",
    var vencimiento: Int = 0
) {
}