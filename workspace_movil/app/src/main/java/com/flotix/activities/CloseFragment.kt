package com.flotix.activities

import android.app.AlertDialog
import android.content.DialogInterface
import android.content.Intent
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.fragment.app.Fragment
import com.flotix.R
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.auth.ktx.auth
import com.google.firebase.ktx.Firebase
import kotlin.system.exitProcess

class CloseFragment : Fragment() {
    //autenticador
    private lateinit var auth: FirebaseAuth
    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View? {
        auth = Firebase.auth
        salir()
        return inflater.inflate(R.layout.fragment_home, container, false)
    }

    /**
     * Funcion para salir de la aplicacion
     */
    private fun salir() {

        val dialogo: AlertDialog.Builder = AlertDialog.Builder(activity)
        dialogo.setTitle("Salir")
        dialogo.setMessage("¿Seguro que quiere salir de la aplicación?")
        dialogo.setCancelable(false)
        dialogo.setPositiveButton("Aceptar",
            DialogInterface.OnClickListener { dialogo1, id -> acceptExit() })
        dialogo.setNegativeButton("Cancelar",
            DialogInterface.OnClickListener { dialogo1, id -> cancelExit()})
        dialogo.show()
    }

    /**
     * Se sale de la sesion y cierra la aplicacion
     */
    private fun acceptExit() {
        auth.signOut()
        activity!!.finish()
        exitProcess(0)
    }

    /**
     * Redirige al home
     */
    private fun cancelExit() {
        startActivity(Intent(context, NavigationActivity::class.java))
        activity!!.finish()
    }
}