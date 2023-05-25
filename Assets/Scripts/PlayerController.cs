using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Forza di propulsione"), Range(2, 20)]
    public float accelerationForce = 7f;

    [Tooltip("Forza di attrito"), Range(-2, -10)]
    public float frictionForce = -3f;

    [Tooltip("Velocità massima raggiungibile"), Range(2, 20)]
    public float maxSpeed = 10f;

    [Tooltip("Margine esterno extra")]
    public Vector2 extraMargin = Vector2.one;

    [Tooltip("Prefab del proiettile")]
    public GameObject bullet;

    Animator animator;
    GameObject leftWeapon;
    GameObject rightWeapon;

    bool isRightWeapon = false;
    Vector2 min;
    Vector2 max;

    Vector2 residualVelocity;


    // Start is called before the first frame update
    void Start()
    {

        // recupero i componenti child che uso per calcolare lo spawn dei proiettili
        leftWeapon = transform.Find("LeftWeapon").gameObject;
        rightWeapon = transform.Find("RightWeapon").gameObject;

        // recupero l'animatore agganciato al Player
        animator = transform.GetComponent<Animator>();

        // cerco limiti dello schermo
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //aggiungo un extra bordo per evitare che il player esca fuori 
        min.x += extraMargin.x;
        min.y += extraMargin.y;
        max.x -= extraMargin.x;
        max.y -= extraMargin.y;
    }

    // Update is called once per frame
    void Update()
    {
        // premo spazio
        if (Input.GetKeyDown("space"))
            InvokeRepeating("Shoot", 0f, 0.1f);

        // rilascio spazio
        if (Input.GetKeyUp("space"))
            CancelInvoke("Shoot");

        //catturo assi 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // creo vettore direzione input
        Vector2 inputDir = new Vector2(x, y).normalized;

        // applico animazione accellerazione quando mi muovo in avanti
        if (y > 0)
            animator.SetBool("isAccelerating", true);
        else
            animator.SetBool("isAccelerating", false);

        Move(inputDir);
    }

    // muovo il player
    void Move(Vector2 inputDirection)
    {
        Vector2 velocity = Vector2.zero;

        if (inputDirection.magnitude > 0)
        {
            // input di direzione presente :  accellero
            velocity = residualVelocity + inputDirection * accelerationForce * Time.deltaTime;
        }
        else
        {
            // input di direzione assente :  decellero
            velocity = residualVelocity + residualVelocity * frictionForce * Time.deltaTime;
        }

        // imposto una velocita massima raggiungibile
        velocity = Vector2.ClampMagnitude(velocity, maxSpeed);

        residualVelocity = velocity;


        // acquisisco posizione del Player
        Vector2 position = transform.position;

        // aggiorno posizione del Player
        position += velocity * Time.deltaTime;

        // costringo il Player nei limiti inpostati e azzero la velocita residua quando li raggiungo
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

        // imposto nuova posizione del Player
        transform.position = position;
    }

    // sparo proiettili
    void Shoot()
    {
        // instanzio il proiettile
        GameObject bullet1 = (GameObject)Instantiate(bullet);

        // alterno il fuoco dalle due armi
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
