using UnityEngine;

public class StatsHandler : MonoBehaviour
{

    private UILogic gameUI;

    public void Awake()
    {
        gameUI = GameObject.FindObjectOfType<UILogic>();
    }

    public void SetupAmmo(int ammoLeft, int clipSize)
    {
        gameUI.SetupAmmo(ammoLeft, clipSize);
    }

    public void UpdateAmmo(int ammoLeft)
    {
        gameUI.UpdateAmmo(ammoLeft);
    }

    public void SetIsReloading(bool isReloading)
    {
        gameUI.SetIsReloading(isReloading);
    }

    public void SetupHealth(int startingHealth, int maxHealth)
    {
        gameUI.SetupHealth(startingHealth, maxHealth);
    }

    public void UpdateHealth(int health)
    {
        gameUI.UpdateHealth(health);
    }

}
