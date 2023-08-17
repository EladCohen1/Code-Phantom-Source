using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MenuManager : MonoBehaviour
{
    public Canvas pauseMenu;
    public PlayerScript playerScript;
    public void Play()
    {
        pauseMenu.enabled = false;
        Time.timeScale = 1;
        playerScript.EnableMainCam();
        Cursor.lockState = CursorLockMode.Locked;
        playerScript.isControlled = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
