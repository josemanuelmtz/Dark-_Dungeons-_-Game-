using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Necesario para cambiar escenas

public class PlayerLife : MonoBehaviour
{
    [SerializeField] public float vida = 100f;
    [SerializeField] public float maxVida = 100f;

    private void Start()
    {
        // Sincroniza la vida con el GameManager
        if (GameManager.Instance != null)
        {
            vida = GameManager.Instance.playerLife;
            maxVida = GameManager.Instance.playerMaxLife; // Si decides guardar el máximo también
        }
    }

    public void TomarDano(float dano)
    {
        vida -= dano;

        // Actualiza la vida en el GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerLife = vida;
        }

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

        // Actualiza la vida en el GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerLife = vida;
        }
    }

    private void Muerte()
    {
        Debug.LogWarning("Se lo cargó la burger king");


        // Destruir el objeto del jugador
        Destroy(gameObject);

        // Cargar la escena de derrota
        SceneManager.LoadScene("DerrotaScene");  // Asegúrate de que el nombre de la escena sea correcto
    }
}
