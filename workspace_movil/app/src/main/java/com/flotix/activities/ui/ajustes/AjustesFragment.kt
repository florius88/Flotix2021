package com.flotix.activities.ui.ajustes

import android.content.Context
import android.os.Bundle
import android.util.Log
import androidx.fragment.app.Fragment
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import com.flotix.R
import com.google.firebase.firestore.FirebaseFirestore
import kotlinx.android.synthetic.main.fragment_ajustes.*

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
        buttonAceptar.setOnClickListener{
            updatePwd()
        }
    }

    private fun updatePwd(){

        var pwd: String = editTextPwdActual.text.toString()
        var pwdNew: String = editTextPwdNew.text.toString()
        var pwdNew2: String = editTextPwdNew2.text.toString()

        Log.e(TAG, "pwd: " + pwd + " pwdNew: " + pwdNew + " pwdNew2: " + pwdNew2)
    }
}