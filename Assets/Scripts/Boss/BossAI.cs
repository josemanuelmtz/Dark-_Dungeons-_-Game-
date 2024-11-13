using System.Collections;
using UnityEngine;

public class BossAI : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;
    private Transform player;
    private Vector2 moveDirection;

    public float moveSpeed = 100f;
    public float attackRange = 1.5f;
    public float attackCooldown = 2f;
    public float damageAttack = 20f; // Daño con ataque directo
    public float damageTouch = 5f; // Daño al tocar

    private float nextAttackTime = 0f;
    private bool isAttacking = false;

    public Transform controladorGolpeSur;
    public Transform controladorGolpeNorte;
    public Transform controladorGolpeEste;
    public Transform controladorGolpeOeste;
    public float radioGolpe;

    private string animationState = "AnimationState";
    private enum BossStates
    {
        walkSouth = 1,
        walkNorth = 2,
        walkEast = 3,
        walkWest = 4,
        atkSouth = 5,
        atkNorth = 6,
        atkEast = 7,
        atkWest = 8,
        idleSouth = 0
    }

    private BossStates currentState = BossStates.idleSouth;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator.SetInteger(animationState, (int)currentState);
    }

    private void Update()
    {
        if (isAttacking || player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer <= attackRange && Time.time >= nextAttackTime)
        {
            StartCoroutine(Attack());
        }
        else
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        moveDirection = (player.position - transform.position).normalized;

        if (Mathf.Abs(moveDirection.y) > Mathf.Abs(moveDirection.x))
        {
            currentState = moveDirection.y > 0 ? BossStates.walkNorth : BossStates.walkSouth;
        }
        else
        {
            currentState = moveDirection.x > 0 ? BossStates.walkEast : BossStates.walkWest;
        }

        animator.SetInteger(animationState, (int)currentState);
        rb2D.velocity = moveDirection * moveSpeed * Time.deltaTime;
    }

    public IEnumerator Attack()
    {
        isAttacking = true;
        nextAttackTime = Time.time + attackCooldown;
        rb2D.velocity = Vector2.zero;

        // Cambiar estado a ataque según dirección
        switch (currentState)
        {
            case BossStates.walkNorth:
                currentState = BossStates.atkNorth;
                break;
            case BossStates.walkSouth:
                currentState = BossStates.atkSouth;
                break;
            case BossStates.walkEast:
                currentState = BossStates.atkEast;
                break;
            case BossStates.walkWest:
                currentState = BossStates.atkWest;
                break;
        }

        animator.SetInteger(animationState, (int)currentState);

        // Ejecutar la lógica de golpe
        Golpe();

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        isAttacking = false;
        currentState = BossStates.idleSouth;
        animator.SetInteger(animationState, (int)currentState);
    }

    private void Golpe()
    {
        Transform controladorGolpeActual = controladorGolpeSur;

        // Cambiar la dirección del golpe según la orientación del jefe
        switch (currentState)
        {
            case BossStates.atkNorth:
                controladorGolpeActual = controladorGolpeNorte;
                break;
            case BossStates.atkEast:
                controladorGolpeActual = controladorGolpeEste;
                break;
            case BossStates.atkWest:
                controladorGolpeActual = controladorGolpeOeste;
                break;
        }

        // Detectar colisiones con el área de golpe
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpeActual.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Player"))
            {
                colisionador.transform.GetComponent<PlayerLife>().TomarDano(damageAttack);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerLife playerLife = collision.gameObject.GetComponent<PlayerLife>();

            if (isAttacking)
            {
                // No aplicamos daño de contacto si el jefe está atacando.
                // El daño ya se aplica en el método 'Golpe'
                return;
            }
            else
            {
                // Aplica daño de contacto solo cuando el jefe no está atacando
                playerLife?.TomarDano(damageTouch);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // Dibujar las áreas de golpe
        if (controladorGolpeSur != null)
            Gizmos.DrawWireSphere(controladorGolpeSur.position, radioGolpe);
        if (controladorGolpeNorte != null)
            Gizmos.DrawWireSphere(controladorGolpeNorte.position, radioGolpe);
        if (controladorGolpeEste != null)
            Gizmos.DrawWireSphere(controladorGolpeEste.position, radioGolpe);
        if (controladorGolpeOeste != null)
            Gizmos.DrawWireSphere(controladorGolpeOeste.position, radioGolpe);
    }
}
