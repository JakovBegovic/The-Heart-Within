using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PauseOptionsMenu : MonoBehaviour
{
    public static bool gameIsPaused;
    public static bool optionsIsOpen;
    public GameObject pauseMenuUI;
    public GameObject player;
    private PlayerController playerController;
    public GameObject gunsControllerObject;
    private MultipleGunsController gunsControllerScript;

    public AudioMixer audioMixer;
    public Slider audioLevelSlider;
    private float audioLevel;

    private void Start()
    {

        gameIsPaused = true;
        optionsIsOpen = true;

        audioMixer.GetFloat("volumeExposedParam", out audioLevel);
        audioLevelSlider.value = audioLevel;

        playerController = player.GetComponent<PlayerController>();

        gunsControllerScript = gunsControllerObject.GetComponent<MultipleGunsController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
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

    public void ResumePlayer()
    {
        playerController.enabled = true;
        gunsControllerScript.SetCurrentGunActive();

    }

    void Pause()
    {
        PausePlayer();

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;

        gameIsPaused = true;
    }

    public void PausePlayer()
    {

        playerController.enabled = false;
        gunsControllerScript.SetCurrentGunInactive();

    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volumeExposedParam", volume);
        Debug.Log(volume);
    }
}
