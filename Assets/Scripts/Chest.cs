using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;
    private int puntos;

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;

            // Genera un valor aleatorio de puntos entre 100 y 500
            puntos = Random.Range(100, 501);
            Debug.Log(puntos + " puntos adquiridos!");

            // Busca el objeto que contiene el script PlayerScore en la escena y aumenta la puntuación
            PlayerScore playerScore = FindObjectOfType<PlayerScore>();
            if (playerScore != null)
            {
                playerScore.AumentarPuntuacion(puntos);
            }
        }
    }
}
