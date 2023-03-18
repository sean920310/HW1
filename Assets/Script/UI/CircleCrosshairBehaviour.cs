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
    void Update()
    {

        Vector2 camXPosition = Vector2.right *
            (cam.transform.rotation.eulerAngles.y) / 360f;
        Vector2 towerXPosition = Vector2.right *
            (tower.transform.rotation.eulerAngles.y) / 360f;

        Vector2 camYPosition = Vector2.up *
            (cam.transform.rotation.eulerAngles.x) / 360f;
        Vector2 canonYPosition = Vector2.up *
            (canon.transform.rotation.eulerAngles.x) / 360f;

        //CompassImage.localPosition = compassUvPosition;
        Vector2 ansX = towerXPosition - camXPosition;
        Vector2 ansY = camYPosition - canonYPosition;

        float angleX = getAngle(tower.transform, cam.transform);
        float angleY = getAngle(cam.transform, canon.transform);

        //float finalX = angleX * (canvasRTF.rect.size.x / 2);
        //float finalY = angleY * (canvasRTF.rect.size.y / 2);

        float finalX = ansX.x * canvasRTF.rect.size.x * 1.5f;
        float finalY = ansY.y * canvasRTF.rect.size.y * 1.5f;

        if (Mathf.Abs(finalX) <= 10f)
            finalX = 0f;

        if (Mathf.Abs(finalY) <= 10f)
            finalY = 0f;

        rtf.localPosition = new Vector3(finalX, finalY, 0.0f);
    }
    private float getAngle(Transform from, Transform to)
    {
        return (Vector3.SignedAngle(from.position, to.position, Vector3.up) / 180f);
    }
}
