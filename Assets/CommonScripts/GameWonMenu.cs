using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonMenu : MonoBehaviour
{
    public GameObject gameWonUI;

    private void OnEnable()
    {
        GameManager.OnBossCleared += OpenMenu;
    }
    private void OnDisable()
    {
        GameManager.OnBossCleared -= OpenMenu;
    }

    public void Quit()
    {
        Debug.Log("Game Won, Quitting");
        gameWonUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        Application.Quit();
    }
    public void OpenMenu()
    {
        Debug.Log("Game Won, Screen Brought Up");
        gameWonUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;
    }
    public void ReturnToMenu()
    {
        Debug.Log("Game Won, Returning to Menu");
        gameWonUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        GameManager.LoadMenu();
    }
}
