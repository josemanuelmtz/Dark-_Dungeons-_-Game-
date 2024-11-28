using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] private string escenaDestino; // Nombre de la escena destino

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el jugador interactúa con el portal
        if (other.CompareTag("Player"))
        {
            if (!string.IsNullOrEmpty(escenaDestino))
            {
                SceneManager.LoadScene(escenaDestino);
            }
            else
            {
                Debug.LogError("La escena de destino no está configurada.");
            }
        }
    }
}
