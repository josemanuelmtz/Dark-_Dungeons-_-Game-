using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalStatsMenu : MonoBehaviour
{
    // Método para volver al menú principal
    public void VolverAlMenu()
    {
        // Restablecer las estadísticas de GameManager
        GameManager.Instance.playerScore = 0;
        GameManager.Instance.playerLife = GameManager.Instance.playerMaxLife;
        GameManager.Instance.playerDamage = 10;

        SceneManager.LoadScene(0);
    }

    // Método para salir del juego
    public void Salir()
    {
        Debug.Log("Saliendo del Juego.......");
        Application.Quit(); // Sale de la aplicación
    }
}