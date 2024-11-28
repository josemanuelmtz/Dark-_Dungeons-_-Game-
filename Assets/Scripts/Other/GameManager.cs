using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton

    public int playerScore;     // Puntaje del jugador
    public float playerLife = 100;    // Vida del jugador
    public float playerMaxLife = 100;
    public float playerDamage = 10;    // Daño del jugador

    private void Awake()
    {
        // Implementación Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persistir entre escenas
        }
        else
        {
            Destroy(gameObject); // Asegurarse de que solo haya uno
        }
    }
}
