using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFailedMenu : MonoBehaviour
{
    public GameObject failedMenuUI;

    private void OnEnable()
    {
        PlayerStats.OnPlayerKilled += OpenMenu;
    }
    private void OnDisable()
    {
        PlayerStats.OnPlayerKilled -= OpenMenu;
    }

    public void Quit()
    {
        Debug.Log("Level Failed, Quitting");
        failedMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        Application.Quit();
    }
    public void OpenMenu()
    {
        Debug.Log("Level Failed, Screen Brought Up");
        failedMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;
    }
    public void ReturnToMenu()
    {
        Debug.Log("Level failed, Returning to Menu");
        failedMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        GameManager.LoadMenu();
    }
}
