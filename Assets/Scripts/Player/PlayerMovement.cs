using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    // Movimiento
    public float moveSpeed = 150f;
    public float attackSpeedMultiplier = 1.7f;
    private Animator animator;

    // Ataque
    [SerializeField] private Transform controladorGolpeSur;
    [SerializeField] private Transform controladorGolpeNorte;
    [SerializeField] private Transform controladorGolpeEste;
    [SerializeField] private Transform controladorGolpeOeste;
    [SerializeField] private float radioGolpe;
    [SerializeField] public float danoGolpe;

    string animationState = "AnimationState";

    private bool isAttacking = false;

    enum CharStates {
        walkSouth = 1,
        walkNorth = 2,
        walkEast = 3,
        walkWest = 4,
        atkSouth = 5,
        atkNorth = 6,
        atkEast = 7,
        atkWest = 8,
        idleSouth = 9,
        idleNorth = 10,
        idleEast = 11,
        idleWest = 12
    }

    private CharStates currentState = CharStates.idleSouth;
    private CharStates lastMoveState = CharStates.idleSouth;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();

        animator.SetInteger(animationState, (int)currentState);

        if (GameManager.Instance != null)
        {
            danoGolpe = GameManager.Instance.playerDamage;
        }
    }

    private void Update()
    {
        if (!isAttacking)
        {
            HandleMovement();
        }
        HandleActions();
    }

    private void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        moveDelta = new Vector2(x, y);

        if (x != 0 || y != 0)
        {
            if (y > 0)
            {
                currentState = CharStates.walkNorth;
            }
            else if (y < 0)
            {
                currentState = CharStates.walkSouth;
            }
            else if (x > 0)
            {
                currentState = CharStates.walkEast;
            }
            else if (x < 0)
            {
                currentState = CharStates.walkWest;
            }

            lastMoveState = currentState;
            animator.SetInteger(animationState, (int)currentState);
        }
        else
        {
            switch (lastMoveState)
            {
                case CharStates.walkNorth:
                    currentState = CharStates.idleNorth;
                    break;
                case CharStates.walkSouth:
                    currentState = CharStates.idleSouth;
                    break;
                case CharStates.walkEast:
                    currentState = CharStates.idleEast;
                    break;
                case CharStates.walkWest:
                    currentState = CharStates.idleWest;
                    break;
            }
            animator.SetInteger(animationState, (int)currentState);
        }

        MovePlayer();
    }

    private void HandleActions()
    {
        if (Input.GetKeyDown(KeyCode.K) && !isAttacking)
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        isAttacking = true;
        animator.speed = attackSpeedMultiplier;
        rb2D.velocity = Vector2.zero;

        switch (lastMoveState)
        {
            case CharStates.walkNorth:
            case CharStates.idleNorth:
                currentState = CharStates.atkNorth;
                break;
            case CharStates.walkSouth:
            case CharStates.idleSouth:
                currentState = CharStates.atkSouth;
                break;
            case CharStates.walkEast:
            case CharStates.idleEast:
                currentState = CharStates.atkEast;
                break;
            case CharStates.walkWest:
            case CharStates.idleWest:
                currentState = CharStates.atkWest;
                break;
        }

        animator.SetInteger(animationState, (int)currentState);
        Golpe();

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length / attackSpeedMultiplier);

        animator.speed = 1f;
        isAttacking = false;

        switch (lastMoveState)
        {
            case CharStates.walkNorth:
                currentState = CharStates.idleNorth;
                break;
            case CharStates.walkSouth:
                currentState = CharStates.idleSouth;
                break;
            case CharStates.walkEast:
                currentState = CharStates.idleEast;
                break;
            case CharStates.walkWest:
                currentState = CharStates.idleWest;
                break;
        }
        animator.SetInteger(animationState, (int)currentState);
    }

    private void MovePlayer()
    {
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, moveDelta.y * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            rb2D.velocity = new Vector2(moveDelta.x * moveSpeed * Time.fixedDeltaTime, rb2D.velocity.y);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }

    private void Golpe()
    {
        Transform controladorGolpeActual = controladorGolpeSur;

        switch (lastMoveState)
        {
            case CharStates.walkNorth:
            case CharStates.idleNorth:
                controladorGolpeActual = controladorGolpeNorte;
                break;
            case CharStates.walkEast:
            case CharStates.idleEast:
                controladorGolpeActual = controladorGolpeEste;
                break;
            case CharStates.walkWest:
            case CharStates.idleWest:
                controladorGolpeActual = controladorGolpeOeste;
                break;
        }

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpeActual.position, radioGolpe);
        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy")){
                colisionador.transform.GetComponent<EnemyLife>().TomarDano(danoGolpe);
            } else if (colisionador.CompareTag("Boss")){
                colisionador.transform.GetComponent<BossLife>().TomarDano(danoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpeSur.position, radioGolpe);
        Gizmos.DrawWireSphere(controladorGolpeNorte.position, radioGolpe);
        Gizmos.DrawWireSphere(controladorGolpeEste.position, radioGolpe);
        Gizmos.DrawWireSphere(controladorGolpeOeste.position, radioGolpe);
    }

    public void AumentarAtaque(float incremento)
    {
        danoGolpe += incremento;

        // Actualiza el GameManager con el nuevo valor
        if (GameManager.Instance != null)
        {
            GameManager.Instance.playerDamage = danoGolpe;
        }
    }
}
