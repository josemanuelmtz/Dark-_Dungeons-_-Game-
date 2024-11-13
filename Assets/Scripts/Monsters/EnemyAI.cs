using System.Collections;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f; // Velocidad de movimiento del enemigo
    private Transform target; // Objeto al que el enemigo seguirá
    private bool hasBeenSeen = false; // Marca si el enemigo ha sido visto por la cámara

    private Animator animator;
    private Vector2 moveDirection;

    private string animationState = "AnimationState";

    // Enum para los estados de animación
    private enum EnemyStates
    {
        idleSouth = 0,
        walkSouth = 1,
        walkNorth = 2,
        walkWest = 3,
        walkEast = 4
    }

    private EnemyStates currentState = EnemyStates.idleSouth; // Estado inicial por defecto

    public float damageAmount = 5f; // Daño al tocar al jugador
    public float damageInterval = 1f; // Intervalo de tiempo en segundos entre cada daño

    private float damageTimer = 0f; // Temporizador para contar el tiempo entre cada daño

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");

        if (playerObject != null)
        {
            target = playerObject.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con el nombre 'Player' en la jerarquía.");
        }

        animator = GetComponent<Animator>();
        animator.SetInteger(animationState, (int)currentState);
    }

    private void Update()
    {
        // Solo se mueve hacia el jugador si ha sido visto una vez
        if (hasBeenSeen && target != null)
        {
            MoveTowardsTarget();
        }
        else
        {
            SetIdleState();
        }

        // Actualiza el temporizador para aplicar daño en intervalos regulares
        if (damageTimer > 0f)
        {
            damageTimer -= Time.deltaTime;
        }
    }

    private void MoveTowardsTarget()
    {
        // Calcula la dirección hacia el objetivo
        moveDirection = (target.position - transform.position).normalized;

        // Actualiza la posición del enemigo hacia el target en tiempo real
        transform.position += (Vector3)moveDirection * speed * Time.deltaTime;

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        // Cambia el estado dependiendo de la dirección del movimiento
        if (Mathf.Abs(moveDirection.x) > Mathf.Abs(moveDirection.y))
        {
            // Movimiento en eje horizontal
            if (moveDirection.x > 0)
            {
                currentState = EnemyStates.walkEast;
            }
            else
            {
                currentState = EnemyStates.walkWest;
            }
        }
        else
        {
            // Movimiento en eje vertical
            if (moveDirection.y > 0)
            {
                currentState = EnemyStates.walkNorth;
            }
            else
            {
                currentState = EnemyStates.walkSouth;
            }
        }

        animator.SetInteger(animationState, (int)currentState);
    }

    private void SetIdleState()
    {
        // Establece el estado idle (idle al sur) cuando el enemigo no se mueve
        currentState = EnemyStates.idleSouth;
        animator.SetInteger(animationState, (int)currentState);
    }

    // Método que activa el ataque cuando el enemigo entra en el campo de visión de la cámara
    public void ActivateAttack()
    {
        hasBeenSeen = true;
    }

    // Detecta cuando el enemigo toca al jugador y aplica daño cada cierto intervalo
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();

            // Solo aplica daño si ha pasado el tiempo suficiente
            if (damageTimer <= 0f)
            {
                // Aplica el daño de 5 en 5 y reinicia el temporizador
                playerLife?.TomarDano(damageAmount); // Aplica el daño
                damageTimer = damageInterval; // Reinicia el temporizador
            }
        }
    }
}
