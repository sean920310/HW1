using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    public Transform[] rightWheelMeshs;
    public Transform[] leftWheelMeshs;
    public WheelCollider[] rightWheelColliders;
    public WheelCollider[] leftWheelColliders;

    public float Force, RotSpeed, breakForce, towerRotationSpeed;

    Vector3 pos;
    Quaternion quat;
    float rightPower, leftPower, engineBreakForce;
    float towerRotate, canonRotate;
    GameObject prefab;

    [HeaderAttribute("Rocket")]
    public Transform tower;
    public Transform canon;
    public Transform aimTo;

    public GameObject rocketPrefab;
    public GameObject kaBoomPrefab;
    public float rocketCoolDown;
    private float rocketFiringCounter;


    [HeaderAttribute("Audio")]
    public float enginePitchMax;    

    public AudioSource engineSound;
    public AudioSource RocketFiringSound;
    private Rigidbody rb;

    [HeaderAttribute("Tank Game Parameter")]
    public float health;

    // Start is called before the first frame update
    void Start()
    {
        engineSound.spatialBlend = 1.0f;
        RocketFiringSound.spatialBlend = 1.0f;

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.2f, 0);

        engineBreakForce = breakForce / 2f;

        rocketFiringCounter = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        WheelMeshUpdate();
        tankEngineSound();

        rocketFiringCounter -= Time.deltaTime;
        rocketFiringCounter = rocketFiringCounter <= 0f ? 0f : rocketFiringCounter;
    }
    private void WheelMeshUpdate()
    {
        for (int i = 0; i < rightWheelMeshs.Length; i++)
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
    }

    public void Move(float VerticalAxis, float HorizontalAxis)
    {
        rightPower = VerticalAxis;
        leftPower = VerticalAxis;

        rightPower -= HorizontalAxis * RotSpeed;
        leftPower += HorizontalAxis * RotSpeed;

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
    }

    public void Fire()
    {
        if(rocketFiringCounter <= 0)
        {
            RocketFiringSound.Play();
            rocketFiringCounter = rocketCoolDown;

            Vector3 aimDir = aimTo.position - canon.position;
            prefab = Instantiate(rocketPrefab, aimTo.position, aimTo.rotation);
            prefab.GetComponent<Rigidbody>().AddForce(aimDir * 15000f);

            Instantiate(kaBoomPrefab, aimTo.position, aimTo.rotation);

            Destroy(prefab, 10);//temp
        }
    }

    public void TowerAndCanonRotation(Vector3 eularAngle)
    {
        //tower & canon rotation
        towerRotate = eularAngle.y - tower.eulerAngles.y;
        canonRotate = eularAngle.x;

        if (towerRotate > 180f) towerRotate -= 360f;
        if (towerRotate < -180f) towerRotate += 360f;
        if (towerRotate > 0.5f)
        {
            tower.Rotate(new Vector3(0f, towerRotationSpeed, 0f));
        }
        else if (towerRotate < -0.5f)
        {
            tower.Rotate(new Vector3(0f, -towerRotationSpeed, 0f));
        }
        canon.rotation = Quaternion.Euler(canonRotate, tower.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void tankEngineSound()
    {

        float pitch = 1.0f * (rb.velocity.magnitude * 0.03f + 1.0f);
        
        if (pitch > enginePitchMax)
            pitch = enginePitchMax;

        engineSound.pitch = pitch;
    }
}
