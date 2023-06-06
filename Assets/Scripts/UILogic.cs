using UnityEngine;
using UnityEngine.UIElements;


public class UILogic : MonoBehaviour, IStatsCommunicator
{
    private ProgressBar healthBar = null;

    private Label scoreLabel = null;

    private Label waveLabel = null;

    private Label ammoLabel = null;

    private Label clipSizeLabel = null;

    private Label reloadingLabel = null;

    void OnEnable()
    {
        // Get UI Document
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("Missing UI Document in UILogic script");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;
        healthBar = root.Q<ProgressBar>("HealthProgressBar");

        scoreLabel = root.Q<Label>("Score");

        ammoLabel = root.Q<Label>("AmmoLeft");

        clipSizeLabel = root.Q<Label>("ClipSize");

        reloadingLabel = root.Q<Label>("ReloadingLabel");

        waveLabel = root.Q<Label>("Wave");
    }

    public void UpdateWave(int wave)
    {
        waveLabel.text = wave.ToString("D2");
    }

    public void SetIsReloading(bool isReloading)
    {
        if (isReloading)
            reloadingLabel.visible = true;
        else
            reloadingLabel.visible = false;
    }

    public void SetupAmmo(int ammoLeft, int clipSize)
    {
        ammoLabel.text = ammoLeft.ToString();
        clipSizeLabel.text = "-----\n" + clipSize.ToString();
    }

    public void UpdateAmmo(int ammoLeft)
    {
        ammoLabel.text = ammoLeft.ToString();
    }

    public void SetupHealth(int startingHealth, int maxHealth)
    {
        healthBar.value = startingHealth;
        healthBar.highValue = maxHealth;
    }

    public void UpdateHealth(int health)
    {
        healthBar.value = health;
    }

    public void UpdateScore(int score)
    {
        scoreLabel.text = score.ToString("D5");
    }

}
