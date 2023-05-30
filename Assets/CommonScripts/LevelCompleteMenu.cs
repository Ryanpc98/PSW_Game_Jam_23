using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour
{
    public GameObject completeMenuUI;

    private void OnEnable()
    {
        GameManager.OnLevelCleared += Pause;
    }
    private void OnDisable()
    {
        GameManager.OnLevelCleared -= Pause;
    }

    public void Continue()
    {
        Debug.Log("Moving to Map");
        completeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        SceneManager.LoadScene("IslandSelection");
    }
    public void Pause()
    {
        Debug.Log("Level Cleared, Screen Brought Up");
        completeMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.gameIsPaused = true;
    }
    public void ReturnToMenu()
    {
        Debug.Log("Level cleared, Returning to Menu");
        completeMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.gameIsPaused = false;
        GameManager.LoadMenu();
    }
}
