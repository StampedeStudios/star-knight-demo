using UnityEngine;

public class AIMovemet : MonoBehaviour, IDeathIterface
{
    [Tooltip("Velocita di movimento costante del possesore"), Range(0, 5)]
    public float speed = 1f;

    [Tooltip("Se maggiore di zero randomizza in un cono la direzione iniziale del possesore"),Range(0,45)]
    public int randomInCone = 0;

    Rigidbody2D rb;
    Vector2 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // calcolo la direzione di partenza del possessore
        if (randomInCone == 0)
            moveDirection = Vector2.down;
        else
        {
            int angle = UnityEngine.Random.Range(-randomInCone, randomInCone);
            moveDirection = Quaternion.Euler(0, 0, angle) * Vector3.down;
        }
    }

    void FixedUpdate()
    {
        // aggiungo velocita nella direzione predefinita
        rb.velocity = moveDirection * speed;
    }

    public void OnDeathEvent()
    {
        // fermo il movimento e disattivo le collisioni
        moveDirection = Vector2.zero;
    }
}
