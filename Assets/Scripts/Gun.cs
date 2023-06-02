using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IGunInterface,IDeathIterface
{
    [Tooltip("Tipo di proiettile sparato")]
    public GameObject bullet;

    [Tooltip("Quantita di proiettili sparati in un secondo"), Range(.1f, 10f)]
    public float fireRate = 5;

    [Tooltip("Reference del componente dal quale recupero la posizione e la rotazione del proiettile")]
    public GameObject weapon1;
    [Tooltip("Secondo componente che permette di sparare piu di un colpo in conteporanea")]
    public GameObject weapon2;
    [Tooltip("Attiva lo sparo asincrono in presenza di due armi.\n  -SI: alterno le due armi \n  -NO: sparo in contemporanea")]
    public bool isAsync = true;
    [Tooltip("Abilita il fuoco automatico se l'arma è assegnata ad un IA")]
    public bool autoFire = false;

    bool isFirst;
    enum IShootType
    {
        Single, Double, Async
    }

    IShootType shootType;


    private void Start()
    {
        // seleziono il tipo di fuoco a seconda delle condizioni
        if (weapon1 & !weapon2)
            shootType = IShootType.Single;

        if (weapon1 & weapon2 & !isAsync)
            shootType = IShootType.Double;

        if (weapon1 & weapon2 & isAsync)
            shootType = IShootType.Async;

        isFirst = false;

        if (autoFire)
            StartShoot();
    }


    private IEnumerator Shoot()
    {
        switch (shootType)
        {
            case IShootType.Single:
                // colpo singolo 
                Instantiate(bullet).transform.SetPositionAndRotation(weapon1.transform.position, weapon1.transform.rotation);
                break;

            case IShootType.Double:
                // colpo doppio in conteporanea
                Instantiate(bullet).transform.SetPositionAndRotation(weapon1.transform.position, weapon1.transform.rotation);
                Instantiate(bullet).transform.SetPositionAndRotation(weapon2.transform.position, weapon2.transform.rotation);
                break;

            case IShootType.Async:
                // colpo doppio alternato
                Transform weaponTransform = isFirst ? weapon1.transform : weapon2.transform;
                Instantiate(bullet).transform.SetPositionAndRotation(weaponTransform.position, weaponTransform.rotation);
                isFirst = !isFirst;
                break;

            default: break;
        }
        yield return new WaitForSeconds(fireRate);
        StartCoroutine(Shoot());
    }

    public void StartShoot()
    {
        StartCoroutine(Shoot());
    }

    public void StopShoot()
    {
        StopAllCoroutines();
    }

    public void OnDeathEvent()
    {
        StopShoot();
        this.enabled = false;
    }
}
