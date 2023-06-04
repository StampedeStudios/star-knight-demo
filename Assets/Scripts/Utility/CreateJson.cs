using UnityEngine;
#if UNITY_EDITOR
public class CreateJson : MonoBehaviour
{
    [Tooltip("Lista di nemici spawnabili per ciascun livello")]
    public LevelDifficulty[] levelDifficulties;

    public string jsonName;

    public void CreateJsonFile()
    {
        LevelData.EncriptData(levelDifficulties, jsonName);
    }

    public void PopulateEnemySpawner()
    {
        EnemySpawner spawner = GameObject.FindAnyObjectByType<EnemySpawner>();
        if (spawner)
        {
            spawner.levelDifficulties = LevelData.DecriptData(jsonName);
        }
    }
}
#endif