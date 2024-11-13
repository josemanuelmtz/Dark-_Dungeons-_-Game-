using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    public float vidaIncremento = 20f;  // Cantidad de vida que aumenta

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;

            // Buscar el objeto con el script PlayerLife y aumentar la vida
            PlayerLife playerLife = FindObjectOfType<PlayerLife>();
            if (playerLife != null)
            {
                playerLife.AumentarVida(vidaIncremento);
                Debug.Log("Vida aumentada en " + vidaIncremento);
            }

            // Destruir el objeto una vez que ha sido recogido
            Destroy(gameObject);
        }
    }
}
