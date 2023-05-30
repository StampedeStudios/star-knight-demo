using UnityEngine;

public class EnemyController : MonoBehaviour, IHittableInterface
{
    float screenLimit;

    GameObject player;

    public GameObject bullet;
    public string[] hittables;

    GameObject gun;

    public int life = 50;

    float speed = 1f;

    public int explosionDamage = 30;

    bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // imposto limite inferiore schermo piu un extra margine
        screenLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        screenLimit -= 2f;
        player = GameObject.FindWithTag("PlayerShip");
        gun = transform.Find("Gun").gameObject;

        InvokeRepeating(("Shoot"), 1f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // muovo verso il basso il nemico
            transform.position += Vector3.down * speed * Time.deltaTime;

            // recupero la direzione che punta verso il player
            Vector3 direction;
            if (player)
            {
                direction = player.transform.position - transform.position;
                direction.Normalize();
            }
            else
            {
                direction = Vector3.down;
                CancelInvoke("Shoot");
            }
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            // ruoto il nemico nella direzione del player
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime);

            // se il nemico supera il margine imposto viene distrutto
            if (transform.position.y < screenLimit)
                Destroy(gameObject);
        }
    }

    void Shoot()
    {
        // instanzio il proiettile
        GameObject bullet1 = (GameObject)Instantiate(bullet);

        // sparo nella direzione in cui sto puntando
        bullet1.transform.position = gun.transform.position;
        bullet1.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // controllo con chi sono entrato in conflitto e gestisco i casi
        switch (other.gameObject.tag)
        {
            default: break;
            case "PlayerShip":
                // genero danno alla nave del player sacrificando il nemico
                IHittableInterface hittableInterface = other.collider.GetComponent<IHittableInterface>();
                hittableInterface.DealDamage(explosionDamage);
                DealDamage(life);
                break;
        }
    }

    public void DealDamage(int damage)
    {
        // applico danno alla nave nemica e la distruggo se rimane senza vita
        life -= damage;
        if (life <= 0)
        {
            isDead = true;
            CancelInvoke("Shoot");
            GetComponent<Animator>().SetBool("isDead", true);
        }
    }

    void DestroyShip() // chiamata alla fine dell animazione di morte
    {
        // comunico la distruzione al GameInstance per aggiornare il punteggio
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameInstance>().UpdateScore(1, true);
        Destroy(gameObject);
    }
}
