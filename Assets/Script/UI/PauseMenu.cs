using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// reference: https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys
public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;

    public GameObject pauseMenuUI;
    public AudioMixer audioMixer;
    private AudioSource clickAudio;

    void Start()
    {
        clickAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUpdate();
        }
    }

    public void pauseUpdate()
    {
        if (isPause)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
    private void Resume()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 1.0f;
        isPause = false;
        pauseMenuUI.SetActive(false);
    }
    private void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 0.0f;
        isPause = true;
        pauseMenuUI.SetActive(true);
    }
    public void LoadMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Menu");
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void soundVolumeChange(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("SoundVolume", volume);
    }
    public void musicVolumeChange(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
}
