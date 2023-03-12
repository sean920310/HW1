using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelDust : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject particalObj;
    public LayerMask ground;

    private WheelCollider wheelCollider;

    void Start()
    {
        wheelCollider = gameObject.GetComponent<WheelCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        WheelHit hit;

        if (wheelCollider.GetGroundHit(out hit))
        {
            
            if (hit.collider.gameObject.layer == 6)
                particalObj.SetActive(true);
        }
        else
        {
            particalObj.SetActive(false);
        }
    }
}
