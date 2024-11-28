using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnTouch : MonoBehaviour
{
    [SerializeField] private string sceneName = "CreditsScene"; // Nombre de la escena de créditos
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("¡El jugador tocó a su vieja! cargando créditos...");

            // Cargar la siguiente escena
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("La escena de destino no está configurada.");
            }
        }
        else
        {
            Debug.Log("No lo toca");
        }
    }
}
