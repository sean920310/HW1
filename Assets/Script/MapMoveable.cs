using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapMoveable : MonoBehaviour
{

    [SerializeField] GameObject scrollbar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.mouseScrollDelta.magnitude > 0.0f)
        {
            scrollbar.SetActive(true);
        }

    }
}
