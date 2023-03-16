using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// reference: https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys
public class GameoverMenu : MonoBehaviour
{
    public static bool isGameover = false;

    private AudioSource clickAudio;

    void Start()
    {
        clickAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
