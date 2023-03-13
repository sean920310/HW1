using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TankControl : MonoBehaviour
{
    float verticalAxis, HorizontalAxis;

    TankManager tankManager;

    public Transform cam;

    [SerializeField] CinemachineVirtualCamera firstPersonCam;
    [SerializeField] CinemachineFreeLook thridPersonCam;

    // Start is called before the first frame update
    void Start()
    {
        tankManager = transform.GetComponent<TankManager>();
        CamControl.RegisterFirstPerson(firstPersonCam);
        CamControl.RegisterThridPerson(thridPersonCam);

        CamControl.SwitchCamera(CamControl.Type.ThridPerson);
    }

    // Update is called once per frame
    void Update()
    {
        //wheelCollider move
        verticalAxis = Input.GetAxis("Vertical");
        HorizontalAxis = Input.GetAxis("Horizontal");


        tankManager.Move(verticalAxis, HorizontalAxis);

        //tower & canon rotation
        if(!Input.GetKey(KeyCode.LeftAlt))
        {
            tankManager.TowerAndCanonRotation(cam.eulerAngles);
        }

        //fire
        if (Input.GetMouseButtonDown(0))
        {
            tankManager.Fire();
        }

        //switch Cam between fps & tps
        if (Input.GetMouseButtonDown(1))
        {
            CamControl.SwitchCamera();
        }

    }
}
