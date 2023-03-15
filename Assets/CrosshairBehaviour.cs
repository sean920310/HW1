using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairBehaviour : MonoBehaviour
{
    private Image image;

    [SerializeField]
    private float blinkTime = 0.5f;

    private float blinkCounter;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        blinkCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        blinkCounter -= Time.deltaTime;
        if (blinkCounter <= 0f)
        {
            image.color = new Color(1,0,0,0);
        }
        else
        {
            image.color = new Color(1, 0, 0, 1);
        }
    }

    public void Blink()
    {
        blinkCounter = blinkTime;
    }
}
