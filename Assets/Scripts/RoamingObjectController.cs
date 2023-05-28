using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingObjectController : MonoBehaviour
{

    public float speed = 0.5f;

    Vector2 min;
    Vector2 max;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        int angle = UnityEngine.Random.Range(-45,45);

        direction = Quaternion.Euler(0,0,angle) * Vector3.down;

        
        // calcolo margine superiore piu extra
        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

    }

    // Update is called once per frame
    void Update()
    {  
        transform.position += direction * speed * Time.deltaTime;

          // distruggo il proiettile quando raggiungo il limite imposto
        if (transform.position.y < min.y | transform.position.x > max.x | transform.position.x < min.x)
        {
            Destroy(gameObject);
        }

    }
}
