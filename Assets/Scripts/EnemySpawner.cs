using System.Linq;
using System;
using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Nome del file dal quale caricare il Level Difficulties")]
    public string jsonName = "test";

    LevelDifficulty[] levelDifficulties;
    EnemyType[] currentEnemyPercentage;
    float spawnLenght;
    private float minSpawnTime;
    private float maxSpawnTime;

    private void Awake()
    {
        spawnLenght = GetComponent<BoxCollider2D>().size.x;
    }

    private void Start()
    {
        StartEnemySpawning();// Usato per spawnare nemici in fase di test
    }

    public void StartEnemySpawning()
    {
        if (jsonName == null)
            Debug.LogError("Nome file JSON non valido!");
        else
        {
            levelDifficulties = LevelData.DecriptData(jsonName).ToArray();
            if (levelDifficulties == null)
                Debug.LogError("File JSON non trovato!");
            else
                StartCoroutine(CallSpawn());


        }
    }

    IEnumerator CallSpawn()
    {
        // eseguo il primo spawn dopo il delay
        SpawnEnemy();

        // randomizzo il tempo di spawn succesivo
        float newDelay = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(newDelay);

        StartCoroutine(CallSpawn());
    }

    private void SpawnEnemy()
    {
        foreach (var item in currentEnemyPercentage)
        {
            if (0.1f <= item.percentage)
            {
                GameObject enemy = Instantiate(item.enemy);
            }

        }
    }
}
