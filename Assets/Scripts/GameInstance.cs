using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    [System.Serializable]
    public struct EnemyType
    {
        [Tooltip("Tipo di nemico")]
        public GameObject enemy;
        [Tooltip("Percentuale di spawn"), Range(0, 1)]
        public float percentage;
    }

    [System.Serializable]
    public struct LevelDifficulty
    {
        [Tooltip("Percentuale di spawn di ciascun nemico")]
        public EnemyType[] enemyTypes;
    }

    [Tooltip("Lista di nemici spawnabili per ciascun livello")]
    public LevelDifficulty[] levelDifficulties;

    int currentLevel = 1;

    Vector2[] spawnPoints;

    int nSpawn = 5;

    EnemyType[] currentEnemyPercentage;

    float difficultyTimer = 12f;

    float extraMargin = 4f;

    float enemySize = 1f;

    float minSpawnTime = 2f;
    float maxSpawnTime = 6f;


    // Start is called before the first frame update
    void Start()
    {
        SetSpanwPoints();

        // imposto il livello di partenza 
        currentEnemyPercentage = levelDifficulties[0].enemyTypes;

        // inizio lo spaw dei nemici
        StartCoroutine(CallSpawn(1f));

        // timer che incrementa la difficolta
        //InvokeRepeating("IncreaseDifficulty", difficultyTimer, difficultyTimer);

    }

    IEnumerator CallSpawn(float delay)
    {
        // eseguo il primo spawn dopo il delay
        SpawnEnemy();

        // randomizzo il tempo di spawn succesivo
        float newDelay = UnityEngine.Random.Range(minSpawnTime, maxSpawnTime);
        yield return new WaitForSeconds(newDelay);

        StartCoroutine(CallSpawn(newDelay));
    }

    private void SetSpanwPoints()
    {
        // trovo positione alta destra e sinistra dello schermo
        Vector2 topRight = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 topLeft = Camera.main.ViewportToWorldPoint(new Vector2(0, 1));

        // trovo larghezza schermo
        float screenLenght = Vector2.Distance(topLeft, topRight);

        // distanza tra i punti di spawn
        float spawnDistance = (screenLenght - (enemySize + extraMargin) * 2) / (nSpawn - 1);

        // lista provisoria per la crazione a runtime
        List<Vector2> tempList = new List<Vector2>();

        for (int i = 0; i < nSpawn; i++)
        {
            // creo nuovo punto a partire dal margine sinistro
            Vector2 newSpawn = new Vector2((topLeft.x + enemySize + extraMargin + spawnDistance * i), (topLeft.y + enemySize));
            tempList.Add(newSpawn);
        }

        // converto la lista in un array
        spawnPoints = tempList.ToArray();
    }

    void IncreaseDifficulty()
    {
        if (levelDifficulties.Length < currentLevel)
        {
            // incremento il livello corrente
            currentLevel++;

            // acquisisco nuovo set di nemici
            currentEnemyPercentage = levelDifficulties[currentLevel].enemyTypes;

            // cambio il livello
            NextLevel();
        }
        else
        {
            // concludo la partita
            StopAllCoroutines();
            CancelInvoke("IncreaseDifficulty");
            EndGame();
        }
    }

    void SpawnEnemy()
    {
        // calcolo somma delle probabilita
        float probabilitySum = 0f;
        foreach (var item in currentEnemyPercentage)
        {
            probabilitySum += item.percentage;
        }

        // genero un valore random
        float randomValue = UnityEngine.Random.Range(0f, probabilitySum);

        // scelgo la posizione di spawn
        int randomPosElem = UnityEngine.Random.Range(0, spawnPoints.Length);
        Vector2 randomPosition = spawnPoints[randomPosElem] + new Vector2(UnityEngine.Random.Range(-extraMargin, extraMargin), 0);

        // testo il set di nemici per identificare quello da spawnare
        probabilitySum = 0f;
        foreach (var item in currentEnemyPercentage)
        {
            probabilitySum += item.percentage;
            if (randomValue <= probabilitySum)
            {
                GameObject enemy = (GameObject)Instantiate(item.enemy);
                enemy.transform.position = randomPosition;
            }

        }
    }

    void NextLevel()
    {
        Debug.Log("NEXT LEVEL !");
    }

    void EndGame()
    {
        Debug.Log("END GAME !");
    }


}
