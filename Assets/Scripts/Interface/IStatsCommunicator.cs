public interface IStatsCommunicator
{

    public void SetupAmmo(int ammoLeft, int clipSize);

    public void UpdateAmmo(int ammo);

    public void SetIsReloading(bool isReloading);

    public void SetupHealth(int startingHealth, int maxHealth);

    public void UpdateHealth(int health);

    public void UpdateScore(int score);

    public void UpdateWave(int wave);

}
