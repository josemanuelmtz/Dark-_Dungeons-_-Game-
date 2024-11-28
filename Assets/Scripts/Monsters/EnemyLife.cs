using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] private float vida;
    private PlayerScore playerScore; // Referencia al script de puntuación
    public LevelManager levelManager;
    [SerializeField] private int scoreValue = 50;

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
        // Otorga 50 puntos al jugador al morir
        if (playerScore != null)
        {
            playerScore.AumentarPuntuacion(scoreValue);
        }

        if (levelManager != null)
        {
            levelManager.EnemyDefeated();
        }else{
            Debug.Log("No hay pelado");
        }

        // Destruye el objeto si la vida es 0 o menos
        Destroy(gameObject);
    }
}
