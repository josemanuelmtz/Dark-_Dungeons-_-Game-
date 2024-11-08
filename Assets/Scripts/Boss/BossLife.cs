using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLife : MonoBehaviour
{
    [SerializeField] private float vida;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
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
        if (animator != null)
        {
            // Cambia el valor de "AnimationState" a 77 para reproducir la animación de muerte
            animator.SetInteger("AnimationState", 77);

            // Llama a la corutina para esperar que termine la animación y luego destruir el objeto
            StartCoroutine(EsperarYDestruir());
        }
        else
        {
            // Si no tiene Animator, destruye el objeto directamente
           Destroy(gameObject);
        }
    }

    private IEnumerator EsperarYDestruir()
    {
        // Espera la duración de la animación asociada al parámetro "AnimationState"
        float tiempoAnimacion = animator.GetCurrentAnimatorStateInfo(0).length;

        // Espera el tiempo de la animación
        yield return new WaitForSeconds(tiempoAnimacion);

        // Destruye el objeto una vez que la animación termine
        Destroy(gameObject);
    }
}
