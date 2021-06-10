package com.flotix.model

import java.io.Serializable

class Cliente(
    var nif: String = "",
    var nombre: String = "",
    var direccion: String = "",
    var poblacion: String = "",
    var personaContacto: String = "",
    var tlfContacto: String = "",
    var email: String = "",
    var idMetodoPago: String = "",
    var cuentaBancaria: String = "",
    var baja: Boolean = false
) : Serializable {
}