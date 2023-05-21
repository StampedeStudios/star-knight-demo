using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Velocità del Player
    public float speed = 3;
    public Vector2 halfSpriteSize;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //catturo assi 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // creo vettore direzione 
        Vector2 dir = new Vector2(x, y).normalized;

        Move(dir);


    }
    void Move(Vector2 direction)
    {
        // cerco limiti dello schermo
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        min.x += halfSpriteSize.x;
        min.y += halfSpriteSize.y;

        max.x -= halfSpriteSize.x;
        max.y -= halfSpriteSize.y;

        // posizione Player
        Vector2 position = transform.position;

        // posizione aggiornata
        position += direction * speed * Time.deltaTime;

        // costringo il Player nei limiti dello schermo
        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        // setto nuova posizione del Player
        transform.position = position;

    }

}
