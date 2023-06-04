using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuLogic : MonoBehaviour
{

    VisualElement commandsWindow = null;

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

        Button startButton = root.Q<Button>("StartButton");
        startButton.clicked += () => StartGame();

        Button quitButton = root.Q<Button>("ExitButton");
        quitButton.clicked += () => QuitGame();

        Button commandButton = root.Q<Button>("CommandButton");
        commandButton.clicked += () => CommandGame();

        commandsWindow = root.Q<VisualElement>("CommandsWindow");
    }

    private void StartGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Universe");
    }

    private void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }

    private void CommandGame()
    {
        commandsWindow.visible = true;
    }

}
