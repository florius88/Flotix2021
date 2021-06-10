package com.flotix.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.Settings
import android.util.Log
import com.flotix.R
import com.flotix.dto.UserDTO
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

    enum class TipoRol {
        COMERCIAL, ADMINISTRADOR
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
                if (UtilText.isMailValid(email)){
                    userExists(email, pass)
                } else{
                    txtInLaLoginUsuario.error = resources.getString(R.string.userErrorEmail)
                }
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
        if (UtilText.empty(editTextLoginUsuario, txtInLaLoginUsuario, this)) {
            valid = false
            Log.i(TAG, "alguno vacio")
        }
        if (UtilText.empty(editTextLoginPwd,txtInLaLoginPwd,this)) {
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
            .addOnCompleteListener(this) { task ->
                if (task.isSuccessful) {
                    Log.i(TAG, "userExists:success")
                    var rolCorrecto: Boolean = true
                    for (doc in task.result!!) {
                        val user = doc.toObject(User::class.java)

                        val rol = mapRol[user.idRol]!!.nombre

                        if(TipoRol.COMERCIAL.name.equals(rol) || TipoRol.ADMINISTRADOR.name.equals(rol)){
                            USER.id = doc.id
                            USER.email = user.email
                            USER.nombre = user.nombre
                            USER.nombreRol = rol
                            USER.pwd = user.pwd

                            Log.i(TAG, user.toString())
                            toNavigation()
                        } else {
                            txtError.text = resources.getString(R.string.userNotRolCorrect)
                            rolCorrecto = false
                        }
                    }
                    if (rolCorrecto && (null == USER.email || USER.email.isEmpty() || USER.email.isBlank())){
                        txtError.text = resources.getString(R.string.userNotCorrect)
                    }
                } else {
                    // If sign in fails, display a message to the user.
                    Log.w(TAG, "userExists:failure", task.exception)
                    txtError.text = resources.getString(R.string.userNotCorrect)
                }
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