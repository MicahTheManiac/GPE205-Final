using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LevelManager levelManager;

    public enum GameState { Menus, Gameplay};
    public GameState currentState;

    // Game Sub-States
    public GameObject MainMenuScreen;
    public GameObject LevelsScreen;
    public GameObject GameCreditsScreen;
    public GameObject GameOverScreen;
    public GameObject GameWinScreen;

    // Credits (Score)
    public int credits = 0;
    private int sessionCredits = 0;

    // Awake event before Start can run
    private void Awake()
    {
        if (instance == null)
        {
            // This is the instance
            instance = this;

            // Don't destory in new Scene
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.Menus;
        ActivateMainMenuScreen();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameState.Menus:
                // Do Nothing
                break;

            case GameState.Gameplay:
                DeactivateAllScreens();
                break;
        }
    }

    public void AddCredits(int amount)
    {
        credits += amount;
        sessionCredits += amount;
    }

    public void ResetSessionCredits()
    {
        sessionCredits = 0;
    }

    public void RemoveSessionCredits()
    {
        credits -= sessionCredits;
    }

    public void ResetCredits()
    {
        credits = 0;
        sessionCredits = 0;
    }

    // Change State
    public virtual void ChangeState(GameState newState)
    {
        // Change Current State
        currentState = newState;
    }

    // Deactivate All Screens (Sub-States)
    public void DeactivateAllScreens()
    {
        // SetActive to False
        MainMenuScreen.SetActive(false);
        LevelsScreen.SetActive(false);
        GameCreditsScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GameWinScreen.SetActive(false);
    }

    // Activate Main Menu
    public void ActivateMainMenuScreen()
    {
        // Deactivate All
        DeactivateAllScreens();

        // Activate Main Menu
        MainMenuScreen.SetActive(true);

        // Change State
        ChangeState(GameState.Menus);
    }

    // Activate Level Screen
    public void ActivateLevelsScreen()
    {
        // Deactivate All
        DeactivateAllScreens();

        // Activate Level Screen
        LevelsScreen.SetActive(true);

        // Change State
        ChangeState(GameState.Menus);
    }

    // Activate Game Credits
    public void ActivateGameCreditsScreen()
    {
        // Deactivate All
        DeactivateAllScreens();

        // Activate Game Credits
        GameCreditsScreen.SetActive(true);

        // Change State
        ChangeState(GameState.Menus);
    }

    // Activate Game Over
    public void ActivateGameOverScreen()
    {
        // Deactivate All
        DeactivateAllScreens();

        // Activate Main Menu
        GameOverScreen.SetActive(true);

        // Change State
        ChangeState(GameState.Menus);

        // Go To Interim
        SceneManager.LoadScene("Interim");

        // Reset Credits
        RemoveSessionCredits();
        ResetSessionCredits();
    }

    // Activate Game Win
    public void ActivateGameWinScreen()
    {
        // Deactivate All
        DeactivateAllScreens();

        // Activate Main Menu
        GameWinScreen.SetActive(true);

        // Change State
        ChangeState(GameState.Menus);

        // Go To Interim
        SceneManager.LoadScene("Interim");

        // Reset Session Credits
        ResetSessionCredits();
    }
}
