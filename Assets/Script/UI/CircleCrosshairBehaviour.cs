using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField]
    private RectTransform canvasRTF;

    private Vector3 oldPos;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 camXPosition = Vector2.right *
            (cam.transform.rotation.eulerAngles.y / 360f);
        Vector2 towerXPosition = Vector2.right *
            (tower.transform.rotation.eulerAngles.y / 360f);

        Vector2 camYPosition = Vector2.up *
            (cam.transform.rotation.eulerAngles.x / 360f);
        Vector2 canonYPosition = Vector2.up *
            (canon.transform.rotation.eulerAngles.x / 360f);

        //CompassImage.localPosition = compassUvPosition;
        Vector2 ansX = towerXPosition - camXPosition;
        Vector2 ansY = camYPosition - canonYPosition;

        float finalX = ansX.x * Screen.width * 2f + Screen.width * 0.5f;
        float finalY = ansY.y * Screen.height * 2f + Screen.height * 0.5f;

        if (Mathf.Abs(finalX) <= 10f)
            finalX = 0f;

        if (Mathf.Abs(finalY) <= 10f)
            finalY = 0f;

        rtf.position = new Vector3(finalX, finalY, 0.0f);
    }
}
