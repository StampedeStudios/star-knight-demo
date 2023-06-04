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
}
