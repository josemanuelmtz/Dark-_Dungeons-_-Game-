using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    [SerializeField] private float vida;
    private PlayerScore playerScore; // Referencia al script de puntuación

    private void Start()
    {
        // Busca el objeto que contiene el script PlayerScore en la escena
        playerScore = FindObjectOfType<PlayerScore>();
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
        // Otorga 200 puntos al jugador al morir
        if (playerScore != null)
        {
            playerScore.AumentarPuntuacion(200);
        }

        // Destruye el objeto si la vida es 0 o menos
        Destroy(gameObject);
    }
}
