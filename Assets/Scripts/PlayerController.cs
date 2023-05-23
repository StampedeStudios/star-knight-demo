using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Tooltip("Velocità di moovimento"), Range(2, 20)]
    public int speed = 6;
    [Tooltip("Dimensione metà sprite")]
    public Vector2 halfSpriteSize;

    public Animator animator;

    public AnimationCurve accelerationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    float inputTime = 0f;
    Vector2 dir = Vector2.zero;

    public GameObject bullet;
    GameObject leftWeapon;
    GameObject rightWeapon;

    bool isRightWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        leftWeapon= transform.Find("LeftWeapon").gameObject;
        rightWeapon = transform.Find("RightWeapon").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("space"))
        {
           InvokeRepeating("Shoot",0f,0.1f);
        }

        if(Input.GetKeyUp("space"))
        {
            CancelInvoke("Shoot");
        }

        //catturo assi 
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        // creo vettore direzione input
        Vector2 inputDir = new Vector2(x, y).normalized;

        // creo vettore direzione

        if (inputDir.Equals(Vector2.zero))
        {
            animator.SetBool("IsAccelerating", false);
            inputTime -= Time.deltaTime;
            inputTime = Mathf.Clamp(inputTime, 0, 1);
        }
        else if (inputDir.y > 0)
        {
            animator.SetBool("IsAccelerating", true);
            dir = inputDir;
            inputTime += Time.deltaTime;
            inputTime = Mathf.Clamp(inputTime, 0, 1);
        }
        else
        {
            dir = inputDir;
            inputTime += Time.deltaTime;
            inputTime = Mathf.Clamp(inputTime, 0, 1);
        }

        Move(dir, inputTime);
    }

    void Move(Vector2 direction, float inputTime)
    {
        // cerco limiti dello schermo
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        //aggiungo l altezza e la larghezza della sprite per evitare che esca fuori
        min.x += halfSpriteSize.x;
        min.y += halfSpriteSize.y;
        max.x -= halfSpriteSize.x;
        max.y -= halfSpriteSize.y;

        // posizione Player
        Vector2 position = transform.position;

        // calcolo curva di accellerazione
        float acceleration = accelerationCurve.Evaluate(inputTime);

        // posizione aggiornata
        position += direction * speed * acceleration * Time.deltaTime;

        // costringo il Player nei limiti dello schermo
        position.x = Mathf.Clamp(position.x, min.x, max.x);
        position.y = Mathf.Clamp(position.y, min.y, max.y);

        // setto nuova posizione del Player
        transform.position = position;
    }

    void Shoot()
    {
        GameObject bullet1 = (GameObject)Instantiate(bullet);

        if(isRightWeapon)
        {
            bullet1.transform.position = rightWeapon.transform.position;
            isRightWeapon=false;
        }
        else
        {
            bullet1.transform.position = leftWeapon.transform.position;
            isRightWeapon=true;
        }
        

    }
}
