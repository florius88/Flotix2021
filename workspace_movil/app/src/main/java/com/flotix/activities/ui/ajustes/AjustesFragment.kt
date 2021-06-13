package com.flotix.activities.ui.ajustes

import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Toast
import com.flotix.R
import com.flotix.activities.LoginActivity.Companion.USER
import com.flotix.utils.UtilText
import com.google.firebase.firestore.FirebaseFirestore
import kotlinx.android.synthetic.main.fragment_ajustes.*
import java.lang.Exception

class AjustesFragment : Fragment() {

    private val TAG = "AjustesFragment"

    private var firestoreDB: FirebaseFirestore? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        firestoreDB = FirebaseFirestore.getInstance()
    }

    override fun onCreateView(inflater: LayoutInflater, container: ViewGroup?, savedInstanceState: Bundle?): View? =
        inflater.inflate(R.layout.fragment_ajustes, container, false)

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        buttonAceptar.setOnClickListener {
            updatePwd()
        }
    }

    /**
     * Actualiza la informacion de la pwd del usuario
     */
    private fun updatePwd() {
        try {
            if (anyEmpty()) {

                var pwd: String = editTextPwdActual.text.toString()

                if (pwd.equals(USER.pwd)) {

                    var pwdNew: String = editTextPwdNew.text.toString()
                    var pwdNew2: String = editTextPwdNew2.text.toString()

                    if (pwdNew.equals(pwdNew2)) {

                        val usuarioRef = firestoreDB?.collection("usuario")?.document(USER.id)

                        if (null != usuarioRef) {

                            usuarioRef
                                .update("pwd", pwdNew)
                                .addOnSuccessListener {
                                    Log.d(TAG, "Usuario successfully updated!")
                                    limpiar()
                                    Toast.makeText(requireContext(),"La información se actualizó correctamente",Toast.LENGTH_SHORT).show();

                                    USER.pwd = pwdNew;
                                }
                                .addOnFailureListener { e ->
                                    Log.w(TAG, "Error updating document", e)
                                    textError.text = "Se produjo un error al actualizar la información"
                                }
                        } else {
                            textError.text = "Se produjo un error al actualizar la información"
                        }
                    } else {
                        textError.text = "Las nuevas contraseñas no coinciden"
                    }

                } else {
                    textError.text = "La contraseña actual no coincide"
                }
            }
        } catch (ex: Exception) {
            Toast.makeText(requireContext(),"Se ha producido un error.", Toast.LENGTH_SHORT).show();
        }
    }

    /**
     * Método que devuelve false si alguno de los valores está vácio
     */
    private fun anyEmpty(): Boolean {
        var valid = true
        if (UtilText.empty(editTextPwdActual, txtInLaPwdActual, requireContext())) {
            valid = false
            Log.i(TAG, "alguno vacio")
        }
        if (UtilText.empty(editTextPwdNew, txtInLaPwdNew, requireContext())) {
            valid = false
            Log.i(TAG, "alguno vacio")
        }
        if (UtilText.empty(editTextPwdNew2, txtInLaPwdNew2, requireContext())) {
            valid = false
            Log.i(TAG, "alguno vacio")
        }
        return valid
    }

    private fun limpiar(){
        editTextPwdActual.text!!.clear()
        editTextPwdNew.text!!.clear()
        editTextPwdNew2.text!!.clear()
        textError.text = ""
    }
}