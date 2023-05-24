using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Velocità di moovimento"), Range(2, 20)]
    public int speed = 6;
    [Tooltip("Dimensione metà sprite")]
    public Vector2 halfSpriteSize;

    public Animator animator;

    Vector2 dir = Vector2.zero;

    public GameObject bullet;
    GameObject leftWeapon;
    GameObject rightWeapon;

    bool isRightWeapon = false;
    Vector2 min;
    Vector2 max;

    Vector2 residualVelocity;

    float gravityForce = 2f;

    float frictionForce = -3f;

    float accelerationForce = 7f;

    // Start is called before the first frame update
    void Start()
    {
        leftWeapon = transform.Find("LeftWeapon").gameObject;
        rightWeapon = transform.Find("RightWeapon").gameObject;

        // cerco limiti dello schermo
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //aggiungo un extra bordo per evitare che il player esca fuori 
        min.x += halfSpriteSize.x;
        min.y += halfSpriteSize.y;
        max.x -= halfSpriteSize.x;
        max.y -= halfSpriteSize.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            InvokeRepeating("Shoot", 0f, 0.1f);

        if (Input.GetKeyUp("space"))
            CancelInvoke("Shoot");

        //catturo assi 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // creo vettore direzione input
        Vector2 inputDir = new Vector2(x, y).normalized;


        Move(inputDir);
    }

    void Move(Vector2 inputDirection)
    {
        Vector2 velocity = Vector2.zero;

        if (inputDirection.magnitude > 0)
        {
            velocity = residualVelocity + inputDirection * accelerationForce * Time.deltaTime;
            Debug.Log(velocity.magnitude);
        }
        else
        {
            velocity = residualVelocity + residualVelocity * frictionForce * Time.deltaTime;
        }

        velocity += Vector2.down * gravityForce * Time.deltaTime;

        residualVelocity = velocity;


        // posizione Player
        Vector2 position = transform.position;

        // posizione aggiornata
        position += velocity * Time.deltaTime;

        if (position.x < min.x)
        {
            residualVelocity.x = 0f;
            position.x = min.x;
        }
        if (position.x > max.x)
        {
            residualVelocity.x = 0f;
            position.x = max.x;
        }
        if (position.y < min.y)
        {
            residualVelocity.y = 0f;
            position.y = min.y;
        }
        if (position.y > max.y)
        {
            residualVelocity.y = 0f;
            position.y = max.y;
        }


        // costringo il Player nei limiti dello schermo
        //position.x = Mathf.Clamp(position.x, min.x, max.x);
        //position.y = Mathf.Clamp(position.y, min.y, max.y);

        // setto nuova posizione del Player
        transform.position = position;
    }

    void Shoot()
    {
        GameObject bullet1 = (GameObject)Instantiate(bullet);

        if (isRightWeapon)
        {
            bullet1.transform.position = rightWeapon.transform.position;
            isRightWeapon = false;
        }
        else
        {
            bullet1.transform.position = leftWeapon.transform.position;
            isRightWeapon = true;
        }


    }
}
