using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Vector2 moveDelta;
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    public float moveSpeed = 150f;
    private Animator animator;

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

    private CharStates currentState = CharStates.idleSouth; // Estado inicial por defecto es idle mirando al sur
    private CharStates lastMoveState = CharStates.idleSouth; // Última dirección de movimiento

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        
        // Iniciar en estado idle al sur por defecto
        animator.SetInteger(animationState, (int)currentState);
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
            // Cambia el estado dependiendo de la dirección del movimiento
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

            lastMoveState = currentState; // Guardar la última dirección de movimiento
            animator.SetInteger(animationState, (int)currentState);
        }
        else
        {
            // Si no hay movimiento, mantener el estado idle en la última dirección
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

        // Detener el movimiento
        rb2D.velocity = Vector2.zero;

        // Atacar en la última dirección de movimiento
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

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); // Esperar hasta que la animación de ataque termine

        isAttacking = false;

        // Volver al estado idle en la última dirección después de atacar
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
}