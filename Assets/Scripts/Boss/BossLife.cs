using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    [SerializeField] private float vida;
    private PlayerScore playerScore; // Referencia al script de puntuación
    public LevelManager levelManager;

    [SerializeField] private int scoreValue = 200; // Puntos otorgados al morir
    [SerializeField] private GameObject falseBorder; // Referencia a la pared específica que se destruirá

    private void Start()
    {
        // Busca el objeto que contiene el script PlayerScore en la escena
        playerScore = FindObjectOfType<PlayerScore>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void TomarDano(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        // Otorga puntos configurados al jugador
        if (playerScore != null)
        {
            playerScore.AumentarPuntuacion(scoreValue);
        }

        // Notifica al LevelManager si es necesario
        if (levelManager != null)
        {
            levelManager.EnemyDefeated();
        }

        // Destruye la pared si está configurada
        if (falseBorder != null)
        {
            Destroy(falseBorder);
            Debug.Log("La pared ha sido destruida.");
        }

        // Destruye el objeto del jefe
        Destroy(gameObject);
    }
}
