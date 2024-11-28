using UnityEngine;
using UnityEngine.UI; // Necesario para usar el componente Text

public class EnemyCounter : MonoBehaviour
{
    [SerializeField] private Text textoEnemigos; // Referencia al texto que mostrará la cantidad de enemigos restantes
    private LevelManager levelManager;  // Referencia al LevelManager para obtener el contador de enemigos

    private void Start()
    {
        // Obtén la referencia al LevelManager
        levelManager = FindObjectOfType<LevelManager>();

        // Asegúrate de que el texto esté sincronizado con la cantidad inicial de enemigos
        ActualizarEnemigos(levelManager.enemiesRemaining);
    }

    private void Update()
    {
        // Actualiza el texto cada cuadro con el número actual de enemigos restantes
        if (levelManager != null)
        {
            ActualizarEnemigos(levelManager.enemiesRemaining);
        }
    }

    // Método para actualizar el texto con el número de enemigos restantes
    private void ActualizarEnemigos(int enemigosRestantes)
    {
        // Si el texto no es nulo, actualiza su valor
        if (textoEnemigos != null)
        {
            textoEnemigos.text = $"{enemigosRestantes}";
        }
    }
}
