package com.flotix.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.Settings
import android.util.Log
import android.widget.Toast
import com.flotix.R
import com.flotix.dto.ClienteDTO
import com.flotix.dto.UserDTO
import com.flotix.model.Alquiler
import com.flotix.model.Cliente
import com.flotix.model.Rol
import com.flotix.model.User
import com.flotix.utils.UtilNet
import com.flotix.utils.UtilText
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.firestore.EventListener
import com.google.firebase.firestore.FirebaseFirestore
import kotlinx.android.synthetic.main.activity_login.*

class LoginActivity : AppCompatActivity() {

    private val TAG = "LoginActivity"

    companion object {
        var USER = UserDTO()
    }

    // Cloud Firestore
    private lateinit var db: FirebaseFirestore

    private var mapRol = HashMap<String, Rol>()

    var email: String = ""
    var pass: String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        db = FirebaseFirestore.getInstance()

        loadMapRoles()

        buttonLogin.setOnClickListener{
            login()
        }
    }

    private fun loadMapRoles() {
        db!!.collection("rol")
            .addSnapshotListener(EventListener { documentSnapshots, e ->
                if (e != null) {
                    Log.e(TAG, "Listen failed!", e)
                    return@EventListener
                }
                if (documentSnapshots != null) {
                    for (doc in documentSnapshots) {
                        val rol = doc.toObject(Rol::class.java)
                        mapRol.put(doc.id, rol)
                    }
                }
            })
    }

    /**
     * Método que cuando pulsa en en el boton si lo campos son correctos
     * logea al usuario
     */
    private fun login() {
        email = editTextLoginUsuario.text.toString()
        pass = /*UtilEncryptor.encrypt(*/editTextLoginPwd.text.toString()/*)!!*/

        if (anyEmpty()) {
            if (UtilNet.hasInternetConnection(this)) {
                userExists(email, pass)
            } else {//muestra una barra para pedir conexion a internet
                val snackbar = Snackbar.make(
                    findViewById(android.R.id.content),
                    R.string.no_net,
                    Snackbar.LENGTH_INDEFINITE
                )
                snackbar.setActionTextColor(getColor(R.color.primaryDarkColor))
                snackbar.setAction("Conectar") {
                    val intent = Intent(Settings.ACTION_WIFI_SETTINGS)
                    startActivity(intent)
                    finish()
                }
                snackbar.show()
            }
            Log.i(TAG, "usuario logeado")
        }
    }

    /**
     * Método que devuelve false si alguno de los valores está vácio
     */
    private fun anyEmpty(): Boolean {
        var valid = true
        if (UtilText.empty(editTextLoginUsuario, txtInLaLoginUsuario, this) || UtilText.empty(
                editTextLoginPwd,txtInLaLoginPwd,this
            )
        ) {
            valid = false
            Log.i(TAG, "alguno vacio")
        }
        return valid
    }

    /**
     * Método que busca por email y si lo encuentra
     * lo compara con la contraseña
     */
    private fun userExists(email: String, password: String) {

        db.collection("usuario").whereEqualTo("email",email).whereEqualTo("pwd",password).get()
            .addOnSuccessListener { result ->
                for (document in result) {
                    Toast.makeText(applicationContext,"Usuario correcto", Toast.LENGTH_SHORT).show();
                    Log.d("TAG", "${document.id} => ${document.data}")

                    val user = document.toObject(User::class.java)
                    USER.email = user.email
                    USER.nombre = user.nombre
                    USER.nombreRol = mapRol[user.idRol]!!.nombre
                    USER.pwd = user.pwd

                    //texto.text = document.data.toString()
                    toNavigation()
                }

                Toast.makeText(applicationContext,"Usuario incorrecto", Toast.LENGTH_SHORT).show();
            }
            .addOnFailureListener { exception ->
                Log.w(TAG, "Error getting documents.", exception)
            }
    }

    /**
     * Metodo que para ir al navigation
     */
    private fun toNavigation() {
        startActivity(Intent(this, NavigationActivity::class.java))
        finish()
    }
}