using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] private Text textoPuntuacion; // Referencia al texto de la puntuación en la UI
    private int puntuacion = 0;                    // Variable para almacenar la puntuación actual

    private void Start()
    {
        // Inicializa el texto de puntuación al inicio
        ActualizarPuntuacion();
    }

    // Método para aumentar la puntuación
    public void AumentarPuntuacion(int puntos)
    {
        puntuacion += puntos;
        ActualizarPuntuacion();
    }

    private void ActualizarPuntuacion()
    {
        // Actualiza el texto de la puntuación
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = "SCORE: " + puntuacion;
        }
    }
}
