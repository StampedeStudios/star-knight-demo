using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 12f;
    float max;

    // Start is called before the first frame update
    void Start()
    {
        // calcolo margine superiore piu extra
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)).y;
        max += 5f;
    }

    // Update is called once per frame
    void Update()
    {
        // sposto il proiettile verso l alto dello schermo
        transform.position = transform.position +  Vector3.up * speed * Time.deltaTime;

        // distruggo il proiettile quando raggiungo il limite imposto
        if (transform.position.y > max)
        {
            Destroy(gameObject);
        }

    }
}
