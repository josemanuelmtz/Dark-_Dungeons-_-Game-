using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cargar escenas

public class RegresarMenu : MonoBehaviour
{
    // Método para regresar al menú principal (escena 0)
    public void RegresarAlMenu()
    {
        SceneManager.LoadScene(0);  // Cambia "0" por el índice de tu escena o el nombre de la escena si lo prefieres
    }
}
