public interface IStatsCommunicator
{
    public void UpdateHealth(int health);

    public void UpdateAmmo(int ammo);

    public void UpdateScore(int score);

    public int GetScore();
}
