using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEventHandler : MonoBehaviour
{
    VisualElement endScreen = null;

    Label scoreReachedLabel = null;

    Label waveReachedLabel = null;

    Label windowTitle = null;

    VisualElement deathIcon = null;

    Button nextLevelButton = null;

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

        endScreen = root.Q<VisualElement>("Container");
        scoreReachedLabel = root.Q<Label>("ScoreLabel");
        waveReachedLabel = root.Q<Label>("WaveLabel");
        deathIcon = root.Q<VisualElement>("DeathIcon");
        windowTitle = root.Q<Label>("WindowTitle");

        Button menuButton = root.Q<Button>("MenuButton");
        menuButton.RegisterCallback<ClickEvent>(ev => GoToMenu());

        Button restartButton = root.Q<Button>("RestartButton");
        restartButton.RegisterCallback<ClickEvent>(ev => RestartGame());

        nextLevelButton = root.Q<Button>("NextLevelButton");
    }

    private void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Universe");
    }

    public void ShowEndScreen(int scoreReached, int waveReached, bool isPlayerDead)
    {
        scoreReachedLabel.text = "SCORE REACHED: " + scoreReached.ToString();
        waveReachedLabel.text = "WAVE REACHED: " + waveReached.ToString();

        if (isPlayerDead)
        {
            windowTitle.text = "YOUR SHIP HAS BEEN DESTROYED";
            nextLevelButton.style.display = DisplayStyle.None;
        }

        StartCoroutine(ShowEndScreenCoroutine(isPlayerDead));
    }

    private IEnumerator ShowEndScreenCoroutine(bool isPlayerDead)
    {
        yield return new WaitForSeconds(0.3f);
        if (isPlayerDead)
            deathIcon.style.display = DisplayStyle.Flex;

        endScreen.visible = true;
    }
}
