package com.flotix.model

import java.io.Serializable

class Alerta(
    var idTipoAlerta: String = "",
    var matricula: String = "",
    var nombreCliente: String = "",
    var vencimiento: Int = 0
) : Serializable {
}