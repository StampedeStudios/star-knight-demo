using UnityEngine;

public class StatsHandler : MonoBehaviour
{

    private UILogic gameUI;

    private DeathHandler deathHandler;

    public void Awake()
    {
        gameUI = GameObject.FindObjectOfType<UILogic>();
        deathHandler = GameObject.FindObjectOfType<DeathHandler>();
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

    public void ShowDeathScreen()
    {
        deathHandler.ShowDeathScreen();
    }

    public void UpdatePlayerStats(int score, int wave)
    {
        deathHandler.UpdateStats(score, wave);
    }

}
