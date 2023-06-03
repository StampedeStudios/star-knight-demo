using UnityEngine;
using UnityEngine.UIElements;


public class UILogic : MonoBehaviour, IStatsCommunicator
{
    private ProgressBar healthBar = null;

    private Label score = null;

    private Label ammo = null;

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

        score = root.Q<Label>("Score");

        ammo = root.Q<Label>("AmmoLeft");
    }

    public void UpdateAmmo(int ammoLeft)
    {
        ammo.text = ammoLeft.ToString();
    }

    public void UpdateHealth(int health)
    {
        healthBar.value = health;
    }

    public void UpdateScore(int score)
    {
        this.score.text = score.ToString("D3");
    }
}
