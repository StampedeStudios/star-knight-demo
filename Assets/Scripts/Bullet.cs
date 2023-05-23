using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 12f;
    Vector2 max;

    // Start is called before the first frame update
    void Start()
    {
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        pos += Vector2.up * speed * Time.deltaTime;

        transform.position = pos;

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }

    }
}
