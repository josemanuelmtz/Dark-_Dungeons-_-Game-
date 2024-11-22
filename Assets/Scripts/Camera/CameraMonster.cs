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
        if (other.CompareTag("Boss"))
        {
            // Llama al método para activar el ataque en el boss
            BossAI bossAI = other.GetComponent<BossAI>();
            if (bossAI != null)
            {
                 Debug.LogWarning("Sí hay jefe");
                bossAI.ActivateBoss();
            }
        }
    }
}
