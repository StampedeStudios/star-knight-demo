using System;
using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour, IGunInterface, IDeathIterface
{
    [Tooltip("Tipo di proiettile sparato")]
    public GameObject bullet;

    [Tooltip("Quantita di proiettili sparati in un secondo"), Range(.1f, 20f)]
    public float fireRate = 5;

    [Tooltip("Reference del componente dal quale recupero la posizione e la rotazione del proiettile")]
    public GameObject weapon1;
    [Tooltip("Secondo componente che permette di sparare piu di un colpo in conteporanea")]
    public GameObject weapon2;
    [Tooltip("Attiva lo sparo asincrono in presenza di due armi.\n  -SI: alterno le due armi \n  -NO: sparo in contemporanea")]
    public bool isAsync = true;
    [Tooltip("Abilita il fuoco automatico se l'arma è assegnata ad un IA")]
    public bool autoFire = false;

    [Tooltip("Abilita la ricarica")]
    public bool hasReload = false;

    [Tooltip("Tempo di ricarica in secondi"), Range(.1f, 5f)]
    public float reloadTime = 1f;

    private StatsHandler statsHandler = null;

    public int bulletPerClip = 90;

    private int ammoLeft = 90;

    private bool isReloading = false;

    bool isFirst;
    enum IShootType
    {
        Single, Double, Async
    }

    Coroutine shootCoorutine;
    IShootType shootType;
    GameObject player;

    private void Awake()
    {
        statsHandler = GetComponent<StatsHandler>();
    }

    private void Start()
    {
        if (statsHandler)
            statsHandler.SetupAmmo(ammoLeft, bulletPerClip);

        // ottengo riferimento al player
        player = GameObject.FindGameObjectWithTag("Player");

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
        if (!player)
            StopAllCoroutines();

        if (!isReloading)
        {
            switch (shootType)
            {
                case IShootType.Single:
                    // colpo singolo 
                    Instantiate(bullet).transform.SetPositionAndRotation(weapon1.transform.position, weapon1.transform.rotation);
                    ammoLeft -= 1;
                    break;

                case IShootType.Double:
                    // colpo doppio in conteporanea
                    Instantiate(bullet).transform.SetPositionAndRotation(weapon1.transform.position, weapon1.transform.rotation);
                    Instantiate(bullet).transform.SetPositionAndRotation(weapon2.transform.position, weapon2.transform.rotation);
                    ammoLeft -= 2;
                    break;

                case IShootType.Async:
                    // colpo doppio alternato
                    Transform weaponTransform = isFirst ? weapon1.transform : weapon2.transform;
                    Instantiate(bullet).transform.SetPositionAndRotation(weaponTransform.position, weaponTransform.rotation);
                    isFirst = !isFirst;
                    ammoLeft -= 1;
                    break;

                default: break;
            }
        }

        if (statsHandler)
            statsHandler.UpdateAmmo(ammoLeft);

        if (hasReload & ammoLeft <= 0)
        {
            StartCoroutine(HandleReload());
        }
        yield return new WaitForSeconds(1f / fireRate);
        shootCoorutine = StartCoroutine(Shoot());
    }

    private IEnumerator HandleReload()
    {
        StopShoot();
        isReloading = true;
        statsHandler.SetIsReloading(isReloading);
        yield return new WaitForSeconds(reloadTime);
        ammoLeft = bulletPerClip;
        statsHandler.UpdateAmmo(ammoLeft);
        isReloading = false;
        statsHandler.SetIsReloading(isReloading);
    }

    public void StartShoot()
    {
        shootCoorutine = StartCoroutine(Shoot());
    }

    public void StopShoot()
    {
        if (shootCoorutine != null)
            StopCoroutine(shootCoorutine);
    }

    public void OnDeathEvent()
    {
        StopShoot();
        this.enabled = false;
    }
}
