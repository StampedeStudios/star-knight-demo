using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public LevelDifficulty[] levelDifficulties;
    EnemyType[] currentEnemyPercentage;

    float spawnLenght;
    private float maxSpawnTime;

    private GameObject player;

    private UILogic gameUI;

    private DeathHandler deathHandler;

    private int currentScore = 0;

    public int CurrentScore { get { return currentScore; } }

    private int currentLevel = 0;

    public int CurrentLevel { get { return currentLevel; } }

    private void Awake()
    {
        // calcolo la metà dell area di spawn
        spawnLenght = GetComponent<BoxCollider2D>().size.x / 2;
        gameUI = GameObject.FindObjectOfType<UILogic>();
        deathHandler = GameObject.FindObjectOfType<DeathHandler>();
    }

    private void Start()
    {
        StartEnemySpawning();// Usato per spawnare nemici in fase di test
    }

    public void StartEnemySpawning()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!player)
            return;

        if (levelDifficulties != null & levelDifficulties.Length > 0)
        {
            IncreaseDifficulty(currentLevel);
            StartCoroutine(CallSpawn());
        }
    }

    public void UpdateScore(int score)
    {
        currentScore += score;
        gameUI.UpdateScore(currentScore);
        deathHandler.UpdateStats(currentScore, currentLevel);
        if (currentScore >= levelDifficulties[currentLevel].levelGoal)
            if (currentLevel < levelDifficulties.Length - 1)
            {
                currentLevel++;
                IncreaseDifficulty(currentLevel);
            }
            else
                EndGame();
    }

    private void EndGame()
    {
        Debug.Log("END GAME!");
    }

    void IncreaseDifficulty(int newLevel)
    {
        // incremento la difficolta
        maxSpawnTime = levelDifficulties[newLevel].maxSpawnTime;
        currentEnemyPercentage = levelDifficulties[newLevel].enemyTypes;
    }

    IEnumerator CallSpawn()
    {
        // fermo lo spawn dei nemici in caso di morte del player
        if (!player)
            StopAllCoroutines();

        // eseguo lo spawn dei nemici
        SpawnEnemy();

        // randomizzo il tempo di attesa per lo spawn succesivo
        float newDelay = UnityEngine.Random.Range(1f, maxSpawnTime);
        yield return new WaitForSeconds(newDelay);

        // richiamo la funzione dopo l' attesa
        StartCoroutine(CallSpawn());
    }

    private void SpawnEnemy()
    {
        // per ciascun nemico contenuto nell array calcolo la probabilita di spawnarlo
        foreach (var item in currentEnemyPercentage)
        {
            // genero un valore random
            int randomValue = UnityEngine.Random.Range(0, 101);

            // se la probabilita è verificata eseguo lo spawn
            if (randomValue <= item.percentage)
            {
                Vector2 pos = GetRandomPosition();
                if (!item.enemy)
                    return;
                GameObject enemy = Instantiate(item.enemy, pos, Quaternion.identity);
            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        // restituisco una posizione random all' interno del range calcolato precedentemente
        float randomPos = UnityEngine.Random.Range(-spawnLenght, spawnLenght);
        Vector2 pos = transform.position;
        pos += new Vector2(randomPos, 0);
        return pos;
    }

}
