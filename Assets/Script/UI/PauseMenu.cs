using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// reference: https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys
public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;

    public GameObject pauseMenuUI;
    public AudioMixer audioMixer;
    private AudioSource clickAudio;

    [SerializeField] private Slider SoundSlider;
    [SerializeField] private Slider MusicSlider;
    void Start()
    {
        float volume = 0f;
        audioMixer.GetFloat("SoundVolume", out volume);
        SoundSlider.value= volume;
        audioMixer.GetFloat("MusicVolume", out volume);
        MusicSlider.value = volume;

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
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        isPause = false;
        pauseMenuUI.SetActive(false);
    }
    private void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
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
        audioMixer.SetFloat("SoundVolume", volume);
    }

    public void musicVolumeChange(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }
}
