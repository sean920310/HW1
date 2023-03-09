using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControl : MonoBehaviour
{
    public Transform[] rightWheelMeshs;
    public Transform[] leftWheelMeshs;
    public WheelCollider[] rightWheelColliders;
    public WheelCollider[] leftWheelColliders;

    public Transform cam;
    public Transform tower;
    public Transform canon;
    public Transform aimTo;

    public GameObject rocketPrefab;
    public GameObject kaBoomPrefab;

    public float Force, RotSpeed, breakForce, towerRotationSpeed;


    Vector3 pos;
    Quaternion quat;
    float rightPower, leftPower, engineBreakForce;
    float towerRotate, canonRotate;
    GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        engineBreakForce = breakForce / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        //wheelCollider move
        rightPower = Input.GetAxis("Vertical");
        leftPower = Input.GetAxis("Vertical");

        rightPower -= Input.GetAxis("Horizontal") * RotSpeed;
        leftPower += Input.GetAxis("Horizontal") * RotSpeed;

        for(int i=0;i<rightWheelMeshs.Length;i++)
        {
            rightWheelColliders[i].GetWorldPose(out pos, out quat);
            rightWheelMeshs[i].position = pos;
            rightWheelMeshs[i].rotation = quat;
        }
        for (int i = 0; i < leftWheelMeshs.Length; i++)
        {
            leftWheelColliders[i].GetWorldPose(out pos, out quat);
            leftWheelMeshs[i].position = pos;
            leftWheelMeshs[i].rotation = quat;
        }

        foreach (var wheelCols in rightWheelColliders)
        {
            wheelCols.motorTorque = rightPower * Force * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
                wheelCols.brakeTorque = breakForce;
            else if (rightPower == 0f)
                wheelCols.brakeTorque = engineBreakForce;
            else
                wheelCols.brakeTorque = 0f;
        }

        foreach (var wheelCols in leftWheelColliders)
        {
            wheelCols.motorTorque = leftPower * Force * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
                wheelCols.brakeTorque = breakForce;
            else if (leftPower == 0f)
                wheelCols.brakeTorque = engineBreakForce;
            else
                wheelCols.brakeTorque = 0f;
        }


        //tower & canon rotation
        towerRotate = cam.eulerAngles.y - tower.eulerAngles.y;
        canonRotate = cam.eulerAngles.x;
        
        if (towerRotate > 180f) towerRotate -= 360f; 
        if (towerRotate < -180f) towerRotate += 360f;
        if(towerRotate > 0.5f)
        {
            tower.Rotate(new Vector3(0f, towerRotationSpeed, 0f));
        }
        else if(towerRotate < -0.5f)
        {
            tower.Rotate(new Vector3(0f, -towerRotationSpeed, 0f));
        }
        canon.rotation = Quaternion.Euler(canonRotate, tower.eulerAngles.y, transform.rotation.eulerAngles.z);

        //fire
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        Vector3 aimDir = aimTo.position - canon.position;
        prefab = Instantiate(rocketPrefab, aimTo.position, aimTo.rotation);
        Instantiate(kaBoomPrefab, aimTo.position, aimTo.rotation);
        prefab.GetComponent<Rigidbody>().AddForce(aimDir * 30000f);
        Destroy(prefab, 5);//temp


    }
}
