using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    private Transform target; // Objeto al que el enemigo seguirá

    private void Start()
    {
        // Busca el objeto llamado "Player" en la jerarquía y obtiene su Transform
        GameObject playerObject = GameObject.Find("Player");
        
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el nombre 'Player' en la jerarquía.");
        }
    }

    private void Update()
    {
        // Si el target ha sido asignado correctamente
        if (target != null)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        // Calcula la dirección hacia el objetivo
        Vector3 direction = (target.position - transform.position).normalized;

        // Actualiza la posición del enemigo hacia el target en tiempo real
        transform.position += direction * speed * Time.deltaTime;
    }
}
