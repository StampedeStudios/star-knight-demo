using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public struct EnemyType
{
    [Tooltip("Tipo di nemico")]
    public GameObject enemy;
    [Tooltip("Percentuale di spawn"), Range(0, 100)]
    public int percentage;

    public EnemyType(GameObject newobj, int newPercentage) 
    {
        this.enemy = newobj;
        this.percentage = newPercentage;
    }
}

[System.Serializable]
public struct LevelDifficulty
{
    [Tooltip("Percentuale di spawn di ciascun nemico")]
    public EnemyType[] enemyTypes;
    public float maxSpawnTime;
    public int levelGoal;

    public LevelDifficulty(List<EnemyType> newEnemyTypes, float newMaxSpawnTime, int newLevelGoal) : this()
    {
        this.enemyTypes = newEnemyTypes.ToArray();
        this.maxSpawnTime = newMaxSpawnTime;
        this.levelGoal = newLevelGoal;
    }
}

[System.Serializable]
public class LevelData : MonoBehaviour
{
    [System.Serializable]
    public struct EncripEnemyType
    {
        public string objPath;
        public int percentage;

        public EncripEnemyType(string newPath, int newPercentage)
        {
            this.objPath = newPath;
            this.percentage = newPercentage;
        }

    }
    [System.Serializable]
    public struct EncriptLevelDifficulty
    {
        public EncripEnemyType[] enemyTypes;
        public float maxSpawnTime;
        public int levelGoal;

        public EncriptLevelDifficulty(List<EncripEnemyType> newEnemyTypes, float newMaxSpawnTime, int newLevelGoal)
        {
            this.enemyTypes = newEnemyTypes.ToArray();
            this.maxSpawnTime = newMaxSpawnTime;
            this.levelGoal = newLevelGoal;
        }
    }
    [System.Serializable]
    public struct Container
    {
        public EncriptLevelDifficulty[] levelDifficulties;

        public Container(List<EncriptLevelDifficulty> newLevelDifficulties)
        {
            this.levelDifficulties = newLevelDifficulties.ToArray();
        }
    }

    public static LevelDifficulty[] DecriptData(string jsonName)
    {
        string filePath = "C:/Unity Project/Test2D/Assets/Scripts/Utility/" + jsonName;
        string json = File.ReadAllText(filePath);
        Container newcontainer = JsonUtility.FromJson<Container>(json);

        List<LevelDifficulty> newLevelDifficulties = new List<LevelDifficulty>();
        foreach (var dataItem in newcontainer.levelDifficulties)
        {
            List<EnemyType> newEnemyTypes = new List<EnemyType>();
            foreach (var item in dataItem.enemyTypes)
            {
                GameObject newobj = AssetDatabase.LoadAssetAtPath<GameObject>(item.objPath);
                int newPercentage = item.percentage;
                EnemyType newEnemyType = new EnemyType(newobj, newPercentage);
                newEnemyTypes.Add(newEnemyType);
            }
            float newMaxSpawnTime = dataItem.maxSpawnTime;
            int newLevelGoal = dataItem.levelGoal;

            newLevelDifficulties.Add(new LevelDifficulty(newEnemyTypes, newMaxSpawnTime, newLevelGoal));
        }

        return newLevelDifficulties.ToArray();
    }

    public static void EncriptData(LevelDifficulty[] data, string jsonName)
    {
        List<EncriptLevelDifficulty> newLevelDifficulties = new List<EncriptLevelDifficulty>();
        foreach (var dataItem in data)
        {
            List<EncripEnemyType> newEnemyTypes = new List<EncripEnemyType>();
            foreach (var item in dataItem.enemyTypes)
            {
                string newPath = AssetDatabase.GetAssetPath(item.enemy);
                int newPercentage = item.percentage;
                EncripEnemyType newEnemyType = new EncripEnemyType(newPath, newPercentage);
                newEnemyTypes.Add(newEnemyType);
            }
            float newMaxSpawnTime = dataItem.maxSpawnTime;
            int newLevelGoal = dataItem.levelGoal;

            newLevelDifficulties.Add(new EncriptLevelDifficulty(newEnemyTypes, newMaxSpawnTime, newLevelGoal));
        }

        Container newContainer = new Container(newLevelDifficulties);


        string json = JsonUtility.ToJson(newContainer);
        string filePath = "C:/Unity Project/Test2D/Assets/Scripts/Utility/" + jsonName;
        File.WriteAllText(filePath, json);
    }

}