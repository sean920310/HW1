using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MapScale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setScale(float scale)
    {
        scale = Mathf.Lerp(1f,3f,scale);
        GetComponent<RectTransform>().localScale = new Vector3(scale, scale,1f);
    }
}
