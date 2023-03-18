using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// reference: https://gamedev-resources.com/create-a-compass-with-ugui/

public class CompassManager : MonoBehaviour
{
    public GameObject player;
    public RawImage CompassImage;
    public float northOffset;
    private void LateUpdate() => UpdateCompassHeading();
    private void UpdateCompassHeading()
    {
        if (player == null)
        { return; }

        Vector2 compassUvPosition = Vector2.right *
            (player.transform.rotation.eulerAngles.y / 360);

        //compassUvPosition.y = 0;
        compassUvPosition.x += northOffset;

        //CompassImage.localPosition = compassUvPosition;
        CompassImage.uvRect = new Rect(compassUvPosition, Vector2.one);
    }
}
