using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,IDeathIterface
{

    [Tooltip("Rapidita con cui raggiungo la massima velocita"), Range(10, 50)]
    public float acceleration = 20f;
    [Tooltip("Rapidita con cui mi fermo quando non ci sono input"), Range(10, 50)]
    public float decelleration = 10f;
    [Tooltip("Massima velocita raggiungibile"),Range(1, 20)]
    public float maxVelocity = 20f;

    Rigidbody2D rb;
    Animator anim;

    public void OnDeathEvent()
    {
        rb.velocity=Vector2.zero;
        this.enabled = false;
    }

    private void Awake()
    {
        // recupero il componente rigidbody e transform
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        // recupero gli input dei due assi
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // creo vettore direzione input
        Vector2 inputDir = new Vector2(x, y).normalized;

        // eseguo animazione accellerazione quando vado avanti
        if (inputDir.y > 0)
            anim.SetBool("isAccelerating", true);
        else
            anim.SetBool("isAccelerating", false);

        // valuto l input esterno
        if (inputDir.magnitude > 0)
        {
            // input di direzione presente :  accellero
            rb.velocity += inputDir * acceleration * Time.fixedDeltaTime;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxVelocity);
        }
        else
        {
            // input di direzione assente :  decellero e mi fermo
            if (rb.velocity.magnitude > 0.1f)
                rb.velocity -= rb.velocity.normalized * decelleration * Time.fixedDeltaTime;
            else
                rb.velocity = Vector2.zero;
        }
    }

}
