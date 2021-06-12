package com.flotix.activities

import android.content.Intent
import android.os.Bundle
import android.os.Handler
import android.os.Looper
import android.util.Log
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.flotix.R
import com.google.firebase.auth.FirebaseAuth
import com.google.firebase.auth.ktx.auth
import com.google.firebase.ktx.Firebase
import java.lang.Exception

class SplashActivity : AppCompatActivity() {

    private val TAG = "SplashActivity"

    //autenticador
    private lateinit var auth: FirebaseAuth

    private var EMAIL_AUTH = "admin@flotix.com"
    private var PWD_AUTH = "admin_2021"

    private val TIME_SPLASH: Long = 4000

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_splash)

        // Initialize Firebase Auth
        auth = Firebase.auth

        //cargamos el login con un delay
        Handler(Looper.getMainLooper()).postDelayed({
            run {
                init()
            }
        }, TIME_SPLASH)
    }

    private fun init() {
        val currentUser = auth.currentUser
        if (currentUser != null) {
            Log.i(TAG, "SÍ hay sesión activa")
            toLogin()
        } else {
            Log.i(TAG, "NO hay sesión activa")
            toLogin()
        }
    }

    /**
     * metodo que viaja al login y da un mensaje en Toast de lo que ha ocurrido
     */
    private fun toLogin() {

        try {
            auth.signInWithEmailAndPassword(EMAIL_AUTH, PWD_AUTH)
                .addOnCompleteListener(this) { task ->
                    if (task.isSuccessful) {
                        // Sign in success, update UI with the signed-in user's information
                        Log.i(TAG, "signInWithEmail:success")
                        startActivity(Intent(this, LoginActivity::class.java))
                        finish()
                    } else {
                        // If sign in fails, display a message to the user.
                        Log.w(TAG, "signInWithEmail:failure", task.exception)
                        finish()
                    }
                }
        } catch (ex: Exception) {
            Toast.makeText(applicationContext,"Se ha producido un error.", Toast.LENGTH_SHORT).show();
        }
    }
}