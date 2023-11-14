using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{
    private GameManager gameManager;

    public string[] levelNames;
    private int currentLevel;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        if (GameManager.instance != null)
        {
            gameManager = GameManager.instance;
        }
    }

    // Play Game
    public void AppPlay()
    {
        LoadLevel(1); // Index 0 is Start
        gameManager.DeactivateAllScreens();
        gameManager.ChangeState(GameManager.GameState.Gameplay);
    }

    // To Main Menu
    public void GoToMain()
    {
        gameManager.ActivateMainMenuScreen();
    }

    // To Level Select
    public void GoToLevelSelect()
    {
        gameManager.ActivateLevelsScreen();
    }

    // To Credits
    public void GoToCredits()
    {
        gameManager.ActivateGameCreditsScreen();
    }

    // Quit Game
    public void AppQuit()
    {
        // Quit a Built App
        Application.Quit();
    }

    // Open Project Board URL
    public void AppOpenProjectBoard()
    {
        Application.OpenURL("https://mthompson84877.weebly.com/colony.html");
    }

    // Retry Function
    public void AppRetryLevel()
    {
        LoadLevel(currentLevel);
        gameManager.DeactivateAllScreens();
        gameManager.ChangeState(GameManager.GameState.Gameplay);
    }

    // Next Level
    public void AppNextLevel()
    {
        if (currentLevel < levelNames.Length - 1)
        {
            currentLevel++;
            gameManager.DeactivateAllScreens();
            gameManager.ChangeState(GameManager.GameState.Gameplay);
        }
        else
        {
            currentLevel = 0;
            GoToMain();
            gameManager.ChangeState(GameManager.GameState.Menus);
        }

        LoadLevel(currentLevel);
    }

    // Load Level
    private void LoadLevel(int index)
    {
        SceneManager.LoadScene(levelNames[index]);
        currentLevel = index;
    }

    // Load Levels by Number
    public void LoadLevel01()
    {
        LoadLevel(1);
        gameManager.DeactivateAllScreens();
    }

    public void LoadLevel02()
    {
        LoadLevel(2);
        gameManager.DeactivateAllScreens();
    }
}
