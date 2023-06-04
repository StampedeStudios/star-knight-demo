using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuLogic : MonoBehaviour
{

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
        VisualElement commandsWindow = root.Q<VisualElement>("CommandsWindow");
        VisualElement creditsWindow = root.Q<VisualElement>("CreditsWindow");

        Button startButton = root.Q<Button>("StartButton");
        startButton.clicked += () => StartGame();

        Button quitButton = root.Q<Button>("ExitButton");
        quitButton.clicked += () => QuitGame();

        Button commandButton = root.Q<Button>("CommandButton");
        commandButton.clicked += () => commandsWindow.visible = true;

        Button creditsButton = root.Q<Button>("CreditsButton");
        creditsButton.clicked += () => creditsWindow.visible = true;

        Button howToPlayCloseButton = root.Q<Button>("HowToPlayCloseButton");
        howToPlayCloseButton.clicked += () => commandsWindow.visible = false;


        Button creditsCloseButton = root.Q<Button>("CloseCreditsButton");
        creditsCloseButton.clicked += () => creditsWindow.visible = false;
    }

    private void StartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Universe");
    }

    private void QuitGame()
    {
        Application.Quit();
    }

}
