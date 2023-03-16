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

    private Vector3 oldPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 endOfBarrel = canon.position + canon.forward;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(endOfBarrel);
        screenPos.y = Screen.height * 0.5f;

        screenPos = (oldPos + screenPos) * 0.5f;

        if (Mathf.Abs(screenPos.x) < 0.5f)
            screenPos.x = 0f;

        rtf.position = screenPos;

        oldPos = screenPos;
    }
}
