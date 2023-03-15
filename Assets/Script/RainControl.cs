using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainControl : MonoBehaviour
{
    public Transform player;
    public float rainHeigt = 20f;

    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update Rain position
        pos = player.position;
        pos.y = rainHeigt;
        transform.position = pos;
    }
}
