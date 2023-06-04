using UnityEngine;
using UnityEngine.UIElements;


public class UILogic : MonoBehaviour, IStatsCommunicator
{
    private ProgressBar healthBar = null;

    private Label scoreLabel = null;

    private Label ammoLabel = null;

    private int playerScore = 0;

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
    }

    public void UpdateAmmo(int ammoLeft)
    {
        ammoLabel.text = ammoLeft.ToString();
    }

    public void UpdateHealth(int health)
    {
        healthBar.value = health;
    }

    public void UpdateScore(int score)
    {
        playerScore += score;
        scoreLabel.text = playerScore.ToString("D3");
    }

    public int GetScore()
    {
        return playerScore;
    }
}
