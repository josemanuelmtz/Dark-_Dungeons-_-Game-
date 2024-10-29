using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D; // Referencia al componente Rigidbody2D
    private Vector2 moveDelta; // Diferencia entre la posición actual y la distancia al siguiente movimiento
    private BoxCollider2D boxCollider;
    private RaycastHit2D hit;

    public float moveSpeed = 150f; // Velocidad de movimiento del jugador

    private void Start()
    {
        // Obtiene los componentes necesarios
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        // Captura el movimiento horizontal (-1 para izquierda, 0 para ninguno, 1 para derecha)
        float x = Input.GetAxisRaw("Horizontal");

        // Captura el movimiento vertical
        float y = Input.GetAxisRaw("Vertical");

        // Restablece el delta de movimiento
        moveDelta = new Vector2(x, y);

        // Cambia la dirección del sprite dependiendo de si el jugador se mueve a la derecha o izquierda
        if (moveDelta.x > 0)
        {
            transform.localScale = Vector3.one; // Dirección normal
        }
        else if (moveDelta.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Invertir sprite en el eje X
        }

        // Comprobación de colisiones antes de mover en el eje Y
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            // Si no hay colisión, mueve al jugador en el eje Y
            rb2D.velocity = new Vector2(rb2D.velocity.x, moveDelta.y * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Si hay colisión, detén el movimiento en el eje Y
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
        }

        // Comprobación de colisiones antes de mover en el eje X
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));

        if (hit.collider == null)
        {
            // Si no hay colisión, mueve al jugador en el eje X
            rb2D.velocity = new Vector2(moveDelta.x * moveSpeed * Time.fixedDeltaTime, rb2D.velocity.y);
        }
        else
        {
            // Si hay colisión, detén el movimiento en el eje X
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }
    }
}
