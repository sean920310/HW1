using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MonoBehaviour
{
    [SerializeField]
    private GameObject Map;

    private bool isShowingMap = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            isShowingMap = !isShowingMap;
            if (isShowingMap)
            {
                Map.SetActive(true);
            }
            else
            {
                Map.SetActive(false);
            }
        }
    }
}
