using UnityEngine;

public class AIPointer : MonoBehaviour
{
    [Tooltip("Velocita con la quale il possessore cerca di puntare il player"), Range(0.5f, 5f)]
    public float pointerSpeed = 2f;
    Quaternion startRotation;
    GameObject player;
    bool isGunActive;


    void Start()
    {
        //recupero la reference del player
        player = GameObject.FindGameObjectWithTag("PlayerShip");

        isGunActive = player;

        // salvo la direzione di spawn
        startRotation = transform.rotation;
    }

    void Update()
    {
        if (player)
        {
            // recupero la direzione che punta verso il player se è vivo
            Vector3 faceDirection;
            faceDirection = player.transform.position - transform.position;
            faceDirection.Normalize();

            // trovo l angolo di rotazione e lerpo la rotazione per raggiungere quella desiderata
            float angle = Mathf.Atan2(faceDirection.y, faceDirection.x) * Mathf.Rad2Deg + 90f;
            Quaternion desiredRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), pointerSpeed * Time.deltaTime);
        }
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, pointerSpeed * Time.deltaTime);
    }
}
