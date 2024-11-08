using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entra es un enemigo
        if (other.CompareTag("Enemy"))
        {
            // Llama al método para activar el ataque en el enemigo
            EnemyAI enemyAI = other.GetComponent<EnemyAI>();
            if (enemyAI != null)
            {
                enemyAI.ActivateAttack();
            }
        }
    }
}
