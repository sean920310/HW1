using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    void Start()
    {
        GetComponent<Camera>().orthographicSize = 55f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
        transform.position += Vector3.up * 25f;

        transform.rotation = Quaternion.Euler(90f, player.rotation.eulerAngles.y, 0f);
    }

    public void mapSizeChange(float value)
    {
        value = 10f + 90f * value;
        GetComponent<Camera>().orthographicSize = value;
    }
}
