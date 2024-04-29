using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenuController : MonoBehaviour
{
    public static bool gameIsPaused;
    public static bool optionsIsOpen;
    public GameObject pauseMenuUI;
    public GameObject player;
    private PlayerController playerController;
    public GameObject gunsControllerObject;
    private MultipleGunsController gunsControllerScript;


    private void Start()
    {

        gameIsPaused = false;
        optionsIsOpen = false;

        playerController = player.GetComponent<PlayerController>();

        gunsControllerScript = gunsControllerObject.GetComponent<MultipleGunsController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        ResumePlayer();

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;

        gameIsPaused = false;
    }

    void Pause()
    {
        PausePlayer();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        gameIsPaused = true;
    }

    public void LoadOptions()
    {
        optionsIsOpen = true;
        Debug.Log("Loading options");
    }

    public void LoadMainMenu()
    {
        Debug.Log("Loading main menu");
        Resume();
        SceneManager.LoadScene(0);
    }

    public void PausePlayer()
    {
        
        playerController.enabled = false;
        gunsControllerScript.SetCurrentGunInactive();

    }

    public void ResumePlayer()
    {
        playerController.enabled = true;
        gunsControllerScript.SetCurrentGunActive();

    }
}
