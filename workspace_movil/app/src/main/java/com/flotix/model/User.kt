package com.flotix.model

import java.io.Serializable

class User(
    var email: String = "",
    var idRol: String = "",
    var nombre: String = "",
    var pwd: String = ""
) : Serializable {
}