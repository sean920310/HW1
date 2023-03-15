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

        if (Input.GetKey(KeyCode.Equals)){
            mapSizeScaleUp();
        }
        if (Input.GetKey(KeyCode.Minus)) { 
            mapSizeScaleDown(); 
        }

    }

    public void mapSizeChange(float value)
    {
        Mathf.Clamp(value, 0f, 1f);
        Mathf.Lerp(value, 10f, 90f);
        GetComponent<Camera>().orthographicSize = value;
    }
    public void mapSizeScaleUp()
    {
        float scale = GetComponent<Camera>().orthographicSize;
        scale += Time.deltaTime * 10;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(scale, 10f, 90f);
    }
    public void mapSizeScaleDown()
    {
        float scale = GetComponent<Camera>().orthographicSize;
        scale -= Time.deltaTime * 10;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(scale, 10f, 90f);
    }
}
