using UnityEngine;

public class StatsHandler : MonoBehaviour
{

    private GameObject gameUI = null;

    public void Awake()
    {
        gameUI = GameObject.FindGameObjectWithTag("GameUI");
    }

    public void SetupAmmo(int ammoLeft, int clipSize)
    {
        gameUI.GetComponent<UILogic>().SetupAmmo(ammoLeft, clipSize);
    }

    public void UpdateAmmo(int ammoLeft)
    {
        gameUI.GetComponent<UILogic>().UpdateAmmo(ammoLeft);
    }

    public void SetIsReloading(bool isReloading)
    {
        gameUI.GetComponent<UILogic>().SetIsReloading(isReloading);
    }

    public void SetupHealth(int startingHealth, int maxHealth)
    {
        gameUI.GetComponent<UILogic>().SetupHealth(startingHealth, maxHealth);
    }

    public void UpdateHealth(int health)
    {
        gameUI.GetComponent<UILogic>().UpdateHealth(health);
    }

}
