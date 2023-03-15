using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CircleCrosshairBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform tower;

    [SerializeField]
    private Transform canon;

    [SerializeField]
    private RectTransform rtf;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endOfBarrel = canon.position + canon.forward;

        // Convert the position to screen space
        Vector3 screenPos = Camera.main.WorldToScreenPoint(endOfBarrel);
        screenPos.y = Screen.height * 0.5f;
        // Set the position of the crosshair

        if (Mathf.Abs(screenPos.x) <= 0.1)
            rtf.localPosition = Vector3.zero;
        else
            rtf.position = screenPos;
    }
}
