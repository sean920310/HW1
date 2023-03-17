using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;

    public float maxSize;
    public float minSize;

    public bool fixedDirection;
    void Start()
    {
        GetComponent<Camera>().orthographicSize = 55f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Camera>().orthographicSize < minSize)
        {
            GetComponent<Camera>().orthographicSize = minSize;
        }
        else if (GetComponent<Camera>().orthographicSize > maxSize)
        {
            GetComponent<Camera>().orthographicSize = maxSize;
        }
        if (!fixedDirection)
        {
            transform.position = player.position;
            transform.position += Vector3.up * 25f;

            transform.rotation = Quaternion.Euler(90f, player.rotation.eulerAngles.y, 0f);
        }

        if (Input.GetKey(KeyCode.Equals)){
            mapSizeScaleUp();
        }
        if (Input.GetKey(KeyCode.Minus)) { 
            mapSizeScaleDown(); 
        }

    }

    public void mapSizeChange(float value)
    {
        value = Mathf.Lerp(minSize, maxSize, value);
        GetComponent<Camera>().orthographicSize = value;
    }
    public void mapSizeScaleUp()
    {
        float scale = GetComponent<Camera>().orthographicSize;
        scale += Time.deltaTime * 20;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(scale, minSize, maxSize);
    }
    public void mapSizeScaleDown()
    {
        float scale = GetComponent<Camera>().orthographicSize;
        scale -= Time.deltaTime * 20;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(scale, minSize, maxSize);
    }
}
