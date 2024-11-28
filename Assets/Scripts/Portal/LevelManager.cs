using UnityEngine;
using UnityEngine.SceneManagement; // Para cargar escenas

public class LevelManager : MonoBehaviour
{
    public GameObject portal; // Referencia al portal
    public int enemiesRemaining; // Contador de enemigos restantes (ahora es público)

    void Start()
    {
        // Asegúrate de que el portal esté desactivado al iniciar el nivel
        if (portal != null)
        {
            portal.SetActive(false);
        }
    }

    // Método para reducir el contador de enemigos
    public void EnemyDefeated()
    {
        enemiesRemaining--;
        Debug.Log(enemiesRemaining);

        // Si todos los enemigos han sido eliminados, activa el portal
        if (enemiesRemaining <= 0)
        {
            if (portal != null)
            {
                portal.SetActive(true);
            }
        }
    }

    // Método para añadir enemigos manualmente
    public void AddEnemy()
    {
        enemiesRemaining++;
        Debug.Log("Enemigo añadido. Total enemigos restantes: " + enemiesRemaining);
    }
}
