using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if(audioObj != null )
            DontDestroyOnLoad(audioObj);
    }

    private void Start()
    {

        Time.timeScale = 1.0f;
    }

    void Update()
    {

    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
