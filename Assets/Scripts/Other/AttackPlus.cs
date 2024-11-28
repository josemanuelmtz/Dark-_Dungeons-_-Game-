using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlus : Collectable
{
    public float attackIncremento = 10f;  // Cantidad de vida que aumenta

    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;

            // Buscar el objeto con el script PlayerLife y aumentar la vida
            PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.AumentarAtaque(attackIncremento);
                Debug.Log("Ataque aumentado en " + attackIncremento);
            }

            // Destruir el objeto una vez que ha sido recogido
            Destroy(gameObject);
        }
    }
}
