using UnityEngine;
using UnityEngine.UIElements;

public class DeathHandler : MonoBehaviour
{
    VisualElement deathScreenDiv = null;

    Label scoreReachedLabel = null;

    Label waveReachedLabel = null;

    void OnEnable()
    {
        // Get UI Document
        UIDocument uiDocument = GetComponent<UIDocument>();

        if (uiDocument == null)
        {
            Debug.LogError("Missing UI Document in Death Handler script");
            return;
        }

        VisualElement root = uiDocument.rootVisualElement;

        deathScreenDiv = root.Q<VisualElement>("Container");
        scoreReachedLabel = root.Q<Label>("ScoreLabel");
        waveReachedLabel = root.Q<Label>("WaveLabel");

        Button menuButton = root.Q<Button>("MenuButton");
        menuButton.RegisterCallback<ClickEvent>(ev => GoToMenu());

        Button restartButton = root.Q<Button>("RestartButton");
        restartButton.RegisterCallback<ClickEvent>(ev => RestartGame());
    }

    private void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Universe");
    }

    public void ShowDeathScreen()
    {
        deathScreenDiv.visible = true;
    }

    public void UpdateStats(int scoreReached, int waveReached)
    {
        scoreReachedLabel.text = "SCORE REACHED: " + scoreReached.ToString();
        waveReachedLabel.text = "WAVE REACHED: " + waveReached.ToString();
    }
}
