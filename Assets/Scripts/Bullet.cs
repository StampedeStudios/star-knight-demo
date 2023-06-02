using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Tooltip("Velocita del proiettile")]
    public float speed = 12f;
    [Tooltip("Danni del proiettile")]
    public int damage = 12;
    [Tooltip("Esplosione collegata all' impatto")]
    public GameObject explosion;

    Rigidbody2D rb;
    Vector2 direction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // imposto la direzione di sparo
        direction = transform.up;
    }

    void FixedUpdate()
    {
        // sposto il proiettile 
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // spengo la fisica
        rb.simulated = false;

        // creo esplosione proiettile
        Instantiate(explosion).transform.position = transform.position;

        // chiamo l' interfaccia di danno
        IHittableInterface hittableInterface = other.collider.GetComponent<IHittableInterface>();
        if (hittableInterface != null)
            hittableInterface.DealDamage(damage);

        // distruggo il proiettile
        Destroy(gameObject);
    }
}
