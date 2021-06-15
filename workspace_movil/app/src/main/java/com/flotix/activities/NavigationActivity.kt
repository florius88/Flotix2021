package com.flotix.activities

import android.os.Bundle
import android.view.Menu
import android.view.MenuItem
import android.view.View
import android.widget.TextView
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.appcompat.widget.Toolbar
import androidx.drawerlayout.widget.DrawerLayout
import androidx.fragment.app.Fragment
import androidx.fragment.app.FragmentManager
import androidx.navigation.findNavController
import androidx.navigation.fragment.NavHostFragment
import androidx.navigation.ui.AppBarConfiguration
import androidx.navigation.ui.navigateUp
import androidx.navigation.ui.setupActionBarWithNavController
import androidx.navigation.ui.setupWithNavController
import com.flotix.R
import com.flotix.activities.LoginActivity.Companion.USER
import com.flotix.activities.ui.alquileres.AlquilerFragment
import com.flotix.activities.ui.home.HomeFragment
import com.google.android.material.navigation.NavigationView
import java.util.*


class NavigationActivity : AppCompatActivity() {

    private lateinit var appBarConfiguration: AppBarConfiguration

    enum class FragmentCargado {
        Inicio, Alquileres
    }

    companion object {
        lateinit var navUsername: TextView
        lateinit var navUserRol: TextView
    }

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_navigation)
        val toolbar: Toolbar = findViewById(R.id.toolbar)
        setSupportActionBar(toolbar)

        val drawerLayout: DrawerLayout = findViewById(R.id.drawer_layout)
        val navView: NavigationView = findViewById(R.id.nav_view)
        val navController = findNavController(R.id.nav_host_fragment)
        // Passing each menu ID as a set of Ids because each
        // menu should be considered as top level destinations.
        appBarConfiguration = AppBarConfiguration(
            setOf(
                R.id.nav_home, R.id.nav_alquileres, R.id.nav_ajustes, R.id.nav_salir
            ), drawerLayout
        )
        setupActionBarWithNavController(navController, appBarConfiguration)
        navView.setupWithNavController(navController)

        cambiarDatos()
    }

    override fun onCreateOptionsMenu(menu: Menu): Boolean {
        // Inflate the menu; this adds items to the action bar if it is present.
        menuInflater.inflate(R.menu.navigation, menu)
        return true
    }

    override fun onSupportNavigateUp(): Boolean {
        val navController = findNavController(R.id.nav_host_fragment)
        return navController.navigateUp(appBarConfiguration) || super.onSupportNavigateUp()
    }

    /**
     * Funcion para manejar los eventos clic del menu derecho (tres puntitos)
     *
     * @param item
     */
    override fun onOptionsItemSelected(item: MenuItem): Boolean {

        return when (item.itemId) {

            R.id.action_fecha -> {

                var titulo:String = getSupportActionBar()!!.title.toString()

                if (null != titulo && titulo.isNotEmpty() && titulo.isNotBlank()){
                    if (FragmentCargado.Inicio.name.equals(titulo)){
                        var homeFragment:HomeFragment = getFunctionalFragment("nav_home") as HomeFragment
                        homeFragment.orderAlertaBy("vencimiento")

                    } else if (FragmentCargado.Alquileres.name.equals(titulo)){
                        var alquilerFragment:AlquilerFragment = getFunctionalFragment("nav_alquileres") as AlquilerFragment
                        alquilerFragment.orderAlquilerBy(1)
                    } else {
                        Toast.makeText(applicationContext,"Opción no disponible", Toast.LENGTH_SHORT).show();
                    }
                }

                true
            }

            R.id.action_matricula -> {

                var titulo:String = getSupportActionBar()!!.title.toString()

                if (null != titulo && titulo.isNotEmpty() && titulo.isNotBlank()){
                    if (FragmentCargado.Inicio.name.equals(titulo)){
                        var homeFragment:HomeFragment = getFunctionalFragment("nav_home") as HomeFragment
                        homeFragment.orderAlertaBy("matricula")

                    } else if (FragmentCargado.Alquileres.name.equals(titulo)){
                        var alquilerFragment:AlquilerFragment = getFunctionalFragment("nav_alquileres") as AlquilerFragment
                        alquilerFragment.orderAlquilerBy(2)
                    } else {
                        Toast.makeText(applicationContext,"Opción no disponible", Toast.LENGTH_SHORT).show();
                    }
                }

                true
            }

            R.id.action_cliente -> {

                var titulo:String = getSupportActionBar()!!.title.toString()

                if (null != titulo && titulo.isNotEmpty() && titulo.isNotBlank()){
                    if (FragmentCargado.Inicio.name.equals(titulo)){
                        var homeFragment:HomeFragment = getFunctionalFragment("nav_home") as HomeFragment
                        homeFragment.orderAlertaBy("nombreCliente")

                    } else if (FragmentCargado.Alquileres.name.equals(titulo)){
                        var alquilerFragment:AlquilerFragment = getFunctionalFragment("nav_alquileres") as AlquilerFragment
                        alquilerFragment.orderAlquilerBy(3)
                    } else {
                        Toast.makeText(applicationContext,"Opción no disponible", Toast.LENGTH_SHORT).show();
                    }
                }

                true
            }

            R.id.action_descripcion -> {

                var titulo:String = getSupportActionBar()!!.title.toString()

                if (null != titulo && titulo.isNotEmpty() && titulo.isNotBlank()){
                    if (FragmentCargado.Inicio.name.equals(titulo)){
                        var homeFragment:HomeFragment = getFunctionalFragment("nav_home") as HomeFragment
                        homeFragment.orderAlertaByDescripcion()

                    } else if (FragmentCargado.Alquileres.name.equals(titulo)){
                        Toast.makeText(applicationContext,"Opción no disponible", Toast.LENGTH_SHORT).show();
                    } else {
                        Toast.makeText(applicationContext,"Opción no disponible", Toast.LENGTH_SHORT).show();
                    }
                }

                true
            }

            else -> super.onOptionsItemSelected(item)
        }
    }

    /**
     * Metodo que devuelve la instancia del fragment solicitado
     */
    fun getFunctionalFragment(tag_name: String): Fragment? {
        val navHostFragment = supportFragmentManager.findFragmentById(R.id.nav_host_fragment) as NavHostFragment?
        val navHostManager: FragmentManager = Objects.requireNonNull(navHostFragment)!!.getChildFragmentManager()
        var fragment: Fragment? = null
        val fragment_list: List<*> = navHostManager.getFragments()
        for (i in fragment_list.indices) {
            if (fragment_list[i] is HomeFragment && tag_name == "nav_home") {
                fragment = fragment_list[i] as HomeFragment?
                break
            }
            if (fragment_list[i] is AlquilerFragment && tag_name == "nav_alquileres") {
                fragment = fragment_list[i] as AlquilerFragment?
                break
            }
        }
        return fragment
    }

    /**
     * Metodo que recoge los datos del user y los almacena en los datos del nav
     */
    private fun cambiarDatos(){
        val navigationView: NavigationView = findViewById(R.id.nav_view)
        val headerView: View = navigationView.getHeaderView(0)
        navUsername = headerView.findViewById(R.id.txtNavUser)
        navUserRol = headerView.findViewById(R.id.txtNavRol)

        //Pasamos la informacion del usuario
        navUsername.text = USER.nombre
        navUserRol.text = USER.nombreRol
    }

    override fun onBackPressed() {
        //Captura el retroceso para que no se salga de la aplicacion
    }
}