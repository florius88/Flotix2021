package com.flotix.utils

import android.content.Context

import android.util.Patterns
import android.widget.TextView
import com.flotix.R
import com.google.android.material.textfield.TextInputLayout

object UtilText {
    /**
     * Funcion que limpia una serie de TextInputLayout de errores
     */
    fun cleanErrors(vararg errors: TextInputLayout) {
        for (error in errors) error.error = null
    }

    /**
     * Metodo para validar el EMAIL
     */
    fun isMailValid(mail: String): Boolean {
        return Patterns.EMAIL_ADDRESS.matcher(mail).matches()
    }

    /**
     * Metodo para validar la PASSWORD
     */
    fun isPasswordValid(password: String): Boolean {
        return password.length > 5
    }

    /**
     * Metodo que comprueba si el TextView esta vacio y lanza un mensaje en TextInputLayout
     * @param txt TextView
     */
    fun empty(txt: TextView, txtLay: TextInputLayout, context: Context): Boolean {
        var empty = false
        if (txt.text.isEmpty()) {
            txtLay.error = context.resources.getString(R.string.isEmpty)
            empty = true
        } else{
            txtLay.error = ""
        }
        return empty
    }
}