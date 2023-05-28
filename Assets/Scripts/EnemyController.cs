using System;
using System.Collections;
using System.Collections.Generic;
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
        // muovo verso il basso il nemico
        transform.position += Vector3.down * speed * Time.deltaTime;
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

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime);

        // se il nemico supera il margine imposto viene distrutto
        if (transform.position.y < screenLimit)
            Destroy(gameObject);
    }

    void Shoot()
    {
        // instanzio il proiettile
        GameObject bullet1 = (GameObject)Instantiate(bullet);

        bullet1.transform.position = gun.transform.position;
        bullet1.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        foreach (var item in hittables)
        {
            if (other.gameObject.CompareTag(item))
            {
                IHittableInterface hittableInterface = other.collider.GetComponent<IHittableInterface>();
                hittableInterface.DealDamage(explosionDamage);
                Destroy(gameObject);
            }
        }
    }

    public void DealDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
            Destroy(gameObject);
    }

    private void OnDestroy()
    {

    }
}
