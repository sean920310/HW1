using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    float verticalAxis, HorizontalAxis;

    TankManager tankManager;

    public Transform cam;

    // Start is called before the first frame update
    void Start()
    {
        tankManager = transform.GetComponent<TankManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //wheelCollider move
        verticalAxis = Input.GetAxis("Vertical");
        HorizontalAxis = Input.GetAxis("Horizontal");

        tankManager.Move(verticalAxis, HorizontalAxis);

        //tower & canon rotation
        tankManager.TowerAndCanonRotation(cam.eulerAngles);

        //fire
        if (Input.GetMouseButtonDown(0))
        {
            tankManager.Fire();
        }
    }
}
