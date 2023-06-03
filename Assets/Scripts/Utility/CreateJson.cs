using UnityEngine;

public class CreateJson : MonoBehaviour
{
    [Tooltip("Lista di nemici spawnabili per ciascun livello")]
    public LevelDifficulty[] levelDifficulties;
    public string jsonName;

    public void CreateJsonFile()
    {
        LevelData.EncriptData(levelDifficulties,jsonName);
    }

    
}
