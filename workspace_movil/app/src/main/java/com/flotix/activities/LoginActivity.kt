package com.flotix.activities

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.Settings
import android.util.Log
import android.widget.Toast
import com.flotix.R
import com.flotix.utils.UtilNet
import com.flotix.utils.UtilText
import com.google.android.material.snackbar.Snackbar
import com.google.firebase.firestore.FirebaseFirestore
import kotlinx.android.synthetic.main.activity_login.*

class LoginActivity : AppCompatActivity() {

    // Cloud Firestore
    private lateinit var db: FirebaseFirestore

    var email: String = ""
    var pass: String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_login)

        //auth = Firebase.auth
        db = FirebaseFirestore.getInstance()

        buttonLogin.setOnClickListener{
            login()
        }
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
                snackbar.setActionTextColor(getColor(R.color.purple_500))
                snackbar.setAction("Conectar") {
                    val intent = Intent(Settings.ACTION_WIFI_SETTINGS)
                    startActivity(intent)
                    finish()
                }
                snackbar.show()
            }
            Log.i("realm", "usuario logeado")
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
            Log.i("valido", "alguno vacio")
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

                    //texto.text = document.data.toString()
                    toNavigation()
                }
                Toast.makeText(applicationContext,"Usuario incorrecto", Toast.LENGTH_SHORT).show();
            }
            .addOnFailureListener { exception ->
                Log.w("TAG", "Error getting documents.", exception)
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