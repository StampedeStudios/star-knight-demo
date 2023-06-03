using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsHandler : MonoBehaviour
{

    private GameObject gameUI = null;

    public void Awake()
    {
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
    }

    public void UpdateAmmo(int ammoLeft)
    {
        gameUI.GetComponent<UILogic>().UpdateAmmo(ammoLeft);
    }

    public void UpdateHealth(int health)
    {
        gameUI.GetComponent<UILogic>().UpdateHealth(health);
    }

    public void UpdateScore(int score)
    {
        gameUI.GetComponent<UILogic>().UpdateScore(score);
    }
}
