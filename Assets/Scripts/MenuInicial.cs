using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // Método para empezar el juego, cargando la escena Dungeon1
    public void Jugar()
    {
        SceneManager.LoadScene("Dungeon1");
    }

    // Método para mostrar las reglas del juego
    public void Reglas()
    {
        SceneManager.LoadScene("Reglas");
    }

    // Método para mostrar los controles del juego
    public void Controles()
    {
        SceneManager.LoadScene("Controles");
    }

    // Método para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del Juego.......");
        Application.Quit();  // Sale de la aplicación
    }
}
