using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float screenLimit;

    GameObject player;

    public GameObject bullet;

    float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // imposto limite inferiore schermo piu un extra margine
        screenLimit = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)).y;
        screenLimit -= 2f;
        player = GameObject.FindWithTag("Player");

        InvokeRepeating(("Shoot"), 1f, 2f);

    }

    // Update is called once per frame
    void Update()
    {
        // muovo verso il basso il nemico
        transform.position += Vector3.down * speed * Time.deltaTime;

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // se il nemico supera il margine imposto viene distrutto
        if (transform.position.y < screenLimit)
            Destroy(gameObject);
    }

    void Shoot()
    {
        // instanzio il proiettile
        GameObject bullet1 = (GameObject)Instantiate(bullet);

        bullet1.transform.position = transform.position;
        bullet1.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
    }
}
