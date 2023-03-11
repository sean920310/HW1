using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// reference: https://ithelp.ithome.com.tw/articles/10187347
public class BGM : MonoBehaviour
{
    private AudioSource bgMusicAudioSource;

    void OnEnable()
    {
        bgMusicAudioSource = GameObject.FindGameObjectWithTag("BackGroundMusic").GetComponent<AudioSource>();

        bgMusicAudioSource.Pause();
    }

    void OnDisable()
    {
        bgMusicAudioSource.UnPause();
    }
}
