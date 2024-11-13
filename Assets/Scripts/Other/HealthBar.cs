using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerLife playerLife;  // Referencia al script del jugador
    [SerializeField] public Image barraDeVida;      // La imagen de la barra de vida
    [SerializeField] public Text textoVida;         // El texto que muestra la vida actual

    private void Start()
    {
        // Configura la barra al inicio con la vida máxima
        ActualizarVida();
    }

    private void Update()
    {
        // Llama a ActualizarVida() cada cuadro para reflejar cambios en la vida
        ActualizarVida();
    }

    private void ActualizarVida()
    {
        // Asegura que la referencia no sea nula
        if (playerLife == null || barraDeVida == null || textoVida == null)
            return;

        // Calcula el porcentaje de vida actual
        float vidaActual = playerLife.vida; // Debes hacer pública la variable `vida` en PlayerLife o crear un método get
        float maxVida = playerLife.maxVida; // Lo mismo para maxVida

        // Actualiza el valor de la barra de vida (escala en 0 a 1)
        barraDeVida.fillAmount = vidaActual / maxVida;

        // Actualiza el texto de vida
        textoVida.text = $"HP: {vidaActual}";
    }
}
