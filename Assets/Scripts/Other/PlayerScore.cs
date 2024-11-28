using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text textoPuntuacion; // Referencia al texto de la puntuación en la UI

    private void Start()
    {
        // Al iniciar, sincroniza la puntuación desde el GameManager
        ActualizarPuntuacion(GameManager.Instance.playerScore);
    }

    // Método para aumentar la puntuación
    public void AumentarPuntuacion(int puntos)
    {
        // Actualiza la puntuación en el GameManager
        GameManager.Instance.playerScore += puntos;

        // Sincroniza la UI con el nuevo score
        ActualizarPuntuacion(GameManager.Instance.playerScore);
    }

    private void ActualizarPuntuacion(int scoreActual)
    {
        // Actualiza el texto de la puntuación
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "SCORE: " + scoreActual;
        }
    }
}
