using UnityEngine;

public class ScoreHandler : MonoBehaviour, IDeathIterface
{
    [Tooltip("Punteggio assegnato al player per l'uccisione"), Range(0,10)]
    public int score = 2;

    public void OnDeathEvent()
    { 
       GameObject gameUI = GameObject.FindGameObjectWithTag("GameUI");
       if (gameUI)
        gameUI.GetComponent<UILogic>().UpdateScore(score);
    }
}
