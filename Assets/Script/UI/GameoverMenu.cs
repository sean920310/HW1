using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

// reference: https://www.youtube.com/watch?v=JivuXdrIHK0&ab_channel=Brackeys
public class GameoverMenu : MonoBehaviour
{
    private AudioSource clickAudio;

    private bool isAnimationPlay = false;
    private Animator animator;

    void Start()
    {
        clickAudio = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAnimationPlay && GameMenuManager.getState(GameMenuManager.MenuStates.Gameover))
        {
            animationPlay();
        }
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

    public void animationPlay()
    {
        animator.SetTrigger("open");
    }

}
