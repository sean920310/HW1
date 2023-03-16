using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{
    private TankWeaponManager twm;

    public GameObject headLight;

    public GameObject explosionPrefab;
    public GameObject malfunctionEffect;
    public bool malfunction = false;

    public Transform[] rightWheelMeshs;
    public Transform[] leftWheelMeshs;
    public WheelCollider[] rightWheelColliders;
    public WheelCollider[] leftWheelColliders;

    public float Force, RotSpeed, breakForce, towerRotationSpeed, canonRotationSpeed;

    Vector3 pos;
    Quaternion quat;
    float rightPower, leftPower, engineBreakForce;
    float towerRotate, canonRotate;
    GameObject weaponPrefab;

    public Transform tower;
    public Transform canon;
    public Transform aimTo;
    public Transform landmineSpot;


    [TagSelector]
    public string enemyTag;

    [HeaderAttribute("Audio")]
    public float enginePitchMax;    

    public AudioSource engineSound;
    private Rigidbody rb;

    [SerializeField] AudioSource gunClickSound;

    [HeaderAttribute("Tank Game Parameter")]
    public float health;
    public float maxHealth = 100;

    // Start is called before the first frame update
    void Start()
    {
        twm = GetComponent<TankWeaponManager>();

        engineSound.spatialBlend = 1.0f;

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.2f, 0);

        engineBreakForce = breakForce / 2f;
    }

    // Update is called once per frame
    void Update()
    {

        WheelMeshUpdate();
        tankEngineSound();
        tankInWaterDetect();

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
        if (twm.currentWeaponAvailable(1))
        {
            if (twm.curWeaponIdx == 0)
            {
                twm.currentWeaponFired(1);

                twm.RocketFiringSound.Play();

                Vector3 aimDir = aimTo.position - canon.position;
                weaponPrefab = Instantiate(twm.rocketPrefab, aimTo.position, aimTo.rotation);
                weaponPrefab.SetActive(true);
                weaponPrefab.GetComponent<Rigidbody>().AddForce(aimDir * 15000f);

                Instantiate(twm.kaBoomPrefab, aimTo.position, aimTo.rotation);

                Destroy(weaponPrefab, 10);//temp

            }
            else if (twm.curWeaponIdx == 1)
            {
                twm.currentWeaponFired(1);

                //twm.LandmineFiringSound.Play();

                weaponPrefab = Instantiate(twm.landminePrefab, landmineSpot.position, Quaternion.identity);
                weaponPrefab.SetActive(true);
                weaponPrefab.GetComponent<LandmineBehaviour>().enemyTag = enemyTag;
                //Destroy(weaponPrefab, 10);//temp
            }
        }
        else
        {
            if (twm.isOutOfAmmo() && !twm.isReloading())
            {
                gunClickSound.Play();
            }
        }
    }

    public void TowerAndCanonRotation(Vector3 eularAngle)
    {
        //tower & canon rotation
        towerRotate = eularAngle.y - tower.eulerAngles.y;
        canonRotate = eularAngle.x - canon.eulerAngles.x;

        float canonTilte = canon.localEulerAngles.x;
        if (canonTilte > 180f) canonTilte -= 360f;


        if (canonRotate > 180f) canonRotate -= 360f;
        if (canonRotate < -180f) canonRotate += 360f;

        if (towerRotate > 180f) towerRotate -= 360f;
        if (towerRotate < -180f) towerRotate += 360f;
        if (towerRotate > 0.1f)
        {
            tower.Rotate(new Vector3(0f, towerRotationSpeed * Time.deltaTime, 0f));
        }
        else if (towerRotate < -0.1f)
        {
            tower.Rotate(new Vector3(0f, -towerRotationSpeed * Time.deltaTime, 0f));
        }

        if (canonRotate > 0.1f && canonTilte < 20f)
        {
            canon.Rotate(new Vector3(towerRotationSpeed * Time.deltaTime, 0f, 0f));
        }
        else if (canonRotate < -0.1f && canonTilte > -20f)
        {
            canon.Rotate(new Vector3(-towerRotationSpeed * Time.deltaTime, 0f, 0f));
        }
        
    }

    public void damage(float damageNumber)
    {
        health -= damageNumber;
        if(health < 0) health = 0;
    }


    private void tankEngineSound()
    {

        float pitch = 1.0f * (rb.velocity.magnitude * 0.03f + 1.0f);
        
        if (pitch > enginePitchMax)
            pitch = enginePitchMax;

        engineSound.pitch = pitch;
    }

    public void headLightControl(bool enable)
    {
        headLight.SetActive(enable);
    }

    private void tankInWaterDetect()
    { 
        malfunction = transform.position.y <= 5.5f; //lower than water surface
        malfunctionEffect.SetActive(malfunction);
    }
}
