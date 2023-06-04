using System.Text;
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
    [Tooltip("Massimo tempo di attesa per uno spawn. /nIl tempo varia tra 1s e questo."), Range(1, 10)]
    public float maxSpawnTime;
    [Tooltip("Punti accumulati prima del cambio livello")]
    public int levelGoal;

    public LevelDifficulty(List<EnemyType> newEnemyTypes, float newMaxSpawnTime, int newLevelGoal) : this()
    {
        this.enemyTypes = newEnemyTypes.ToArray();
        this.maxSpawnTime = newMaxSpawnTime;
        this.levelGoal = newLevelGoal;
    }
}
#if UNITY_EDITOR
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

    private static string ROOT_FILE_PATH = "Assets/Scripts/Utility/";

    public static LevelDifficulty[] DecriptData(string jsonName)
    {
        // creo il percoso completo del file
        string filePath = ROOT_FILE_PATH + jsonName;

        // se non esiste il file lo comunico e interrompo il gioco
        if (!File.Exists(filePath))
        {
            EditorUtility.DisplayDialog("Error!", "Il nome del file JSON non è valido!", "OK");
            UnityEditor.EditorApplication.isPlaying = false;
            return null;

        }

        // recupero il file e lo deserializzo all interno della struttura
        string json = File.ReadAllText(filePath);
        Container newcontainer = JsonUtility.FromJson<Container>(json);

        // popolo la struttura corretta convertendo quella salvata e sostituendo il path del prefab con il gameobject stesso
        List<LevelDifficulty> newLevelDifficulties = new List<LevelDifficulty>();
        foreach (var dataItem in newcontainer.levelDifficulties)
        {
            List<EnemyType> newEnemyTypes = new List<EnemyType>();
            foreach (var item in dataItem.enemyTypes)
            {
                // provo a caricare l' oggetto dato il percorso e interrompo in caso di fallimento
                GameObject newObj = AssetDatabase.LoadAssetAtPath<GameObject>(item.objPath);
                if (!newObj)
                {
                    string errLog = "Impossibile recuperare il Prefab:/n" + item.objPath;
                    EditorUtility.DisplayDialog("Error!", errLog, "OK");
                    UnityEditor.EditorApplication.isPlaying = false;
                    return null;
                }
                int newPercentage = item.percentage;
                EnemyType newEnemyType = new EnemyType(newObj, newPercentage);
                newEnemyTypes.Add(newEnemyType);
            }
            float newMaxSpawnTime = dataItem.maxSpawnTime;
            int newLevelGoal = dataItem.levelGoal;

            newLevelDifficulties.Add(new LevelDifficulty(newEnemyTypes, newMaxSpawnTime, newLevelGoal));
        }

        // restituisco la struttura convertita
        return newLevelDifficulties.ToArray();

    }

    public static void EncriptData(LevelDifficulty[] data, string jsonName)
    {
        // controllo che il nome del file sia valido
        if (jsonName == null | jsonName == "")
        {
            EditorUtility.DisplayDialog("Error!", "Il nome del file JSON non è valido!", "OK");
            return;
        }

        // controllo che la struttura sia valida 
        if (data == null | data.Length == 0)
        {
            EditorUtility.DisplayDialog("Error!", "La lista dei livelli è vuota!", "OK");
            return;
        }

        // creo il file JSON dai dati compilati

        // untilizzo le strutture di appoggio per sostiture il gameobject con il path del prefab
        List<EncriptLevelDifficulty> newLevelDifficulties = new List<EncriptLevelDifficulty>();
        foreach (var dataItem in data)
        {
            List<EncripEnemyType> newEnemyTypes = new List<EncripEnemyType>();

            // annullo l' operazione in caso di array nullo
            if (dataItem.enemyTypes == null | dataItem.enemyTypes.Length == 0)
            {
                EditorUtility.DisplayDialog("Error!", "Una lista di nemici non è stata popolata!", "OK");
                return;
            }

            foreach (var item in dataItem.enemyTypes)
            {
                // annullo l' operazione in caso di gameobject nullo
                if (!item.enemy)
                {
                    EditorUtility.DisplayDialog("Error!", "Un riferimento ad un nemico non è stato assegnato!", "OK");
                    return;
                }
                // recupero e sostituisco il gameobject con il path del prefab
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

        string filePath = ROOT_FILE_PATH + jsonName;

        // controllo se esiste gia un file con lo stesso nome
        if (File.Exists(filePath))

            if (!EditorUtility.DisplayDialog("Salvataggio", "Stai per sovrascrivere un salvataggio esistente!", "OK", "ANNULLA"))
                return;

        string json = JsonUtility.ToJson(newContainer);
        File.WriteAllText(filePath, json);
    }
}
#endif