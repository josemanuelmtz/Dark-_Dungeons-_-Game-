using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float vida = 100f;
    [SerializeField] public float maxVida = 100f;

    public void TomarDano(float dano)
    {
        vida -= dano;
        if (vida <= 0)
        {
            Muerte();
        }
    }

    public void AumentarVida(float incremento)
    {
        vida += incremento;
        if (vida > maxVida)
        {
            vida = maxVida;  // Asegurarse de que la vida no exceda el máximo
        }
    }

    private void Muerte()
    {
        Debug.LogWarning("Se lo cargó la burger king");
        //Destroy(gameObject);
    }
}
