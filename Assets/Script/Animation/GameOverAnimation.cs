using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    Animation animation;
    void Start()
    {
        animation= GetComponent<Animation>();
        animation.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
