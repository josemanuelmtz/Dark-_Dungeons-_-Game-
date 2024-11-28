using UnityEngine;
using UnityEngine.UI;  // Necesario para usar Text

public class FinalStatsDisplay : MonoBehaviour
{
    [SerializeField] private Text scoreText;      // Texto para mostrar el puntaje
    [SerializeField] private Text lifeText;       // Texto para mostrar la vida restante
    [SerializeField] private Text damageText;     // Texto para mostrar el daño del jugador

    private void Start()
    {
        // Verificar que los Texts estén asignados para evitar errores
        if (scoreText != null && lifeText != null && damageText != null)
        {
            // Mostrar las estadísticas en los textos correspondientes
            scoreText.text = $"{GameManager.Instance.playerScore}";
            lifeText.text = $"{GameManager.Instance.playerLife}/{GameManager.Instance.playerMaxLife}";
            damageText.text = $"{GameManager.Instance.playerDamage}";
        }
        else
        {
            Debug.LogError("Uno o más textos no están asignados en el inspector.");
        }
    }
}
