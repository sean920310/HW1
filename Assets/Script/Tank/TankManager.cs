using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankManager : MonoBehaviour
{

    [SerializeField]
    private SkillManager skillManager;

    private TankWeaponManager twm;

    public GameObject headLight;

    public GameObject explosionPrefab;
    public GameObject malfunctionEffect;
    public bool malfunction = false;

    public Transform[] rightWheelMeshs;
    public Transform[] leftWheelMeshs;
    public WheelCollider[] rightWheelColliders;
    public WheelCollider[] leftWheelColliders;

    public float Force, MaxSpeed, RotSpeed, breakForce, towerRotationSpeed, canonRotationSpeed;

    [ReadOnly]
    public float originTowerRotationSpeed;
    [ReadOnly]
    public float originCanonRotationSpeed;

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
    public float originMaxHealth = 100;
    public float lowHealthHint = 20f;

    private float regenerationCounter;
    private float regenerationTime = 1.0f;

    private float waterDamageCounter = 1f;

    // Start is called before the first frame update
    void Start()
    {
        twm = GetComponent<TankWeaponManager>();

        engineSound.spatialBlend = 1.0f;

        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.2f, 0);

        engineBreakForce = breakForce / 2f;

        originMaxHealth = maxHealth;
        originTowerRotationSpeed = towerRotationSpeed;
        originCanonRotationSpeed = canonRotationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (skillManager != null)
        {
            regenerationCounter += Time.deltaTime;
            if (regenerationCounter > regenerationTime)
            {
                health += skillManager.TankRegeneration.getValueAfterCalc(0);
                regenerationCounter = 0f;
            }
            maxHealth = skillManager.TankHealth.getValueAfterCalc(originMaxHealth);
            towerRotationSpeed = skillManager.TankTowerRotation.getValueAfterCalc(originTowerRotationSpeed);
            canonRotationSpeed = skillManager.TankTowerRotation.getValueAfterCalc(originCanonRotationSpeed);
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }else if (health < 0f)
        {
            health = 0;
        }

        WheelMeshUpdate();
        tankEngineSound();
        tankInWaterDetect();

        if(gameObject.tag == "Player")
            DamageEffect.LowHealth(health <= lowHealthHint);

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

        //if(VerticalAxis < 0f) // backu
        //{
        //    rightPower -= HorizontalAxis * -RotSpeed;
        //    leftPower += HorizontalAxis * -RotSpeed;
        //}
        //else
        //{
        //    rightPower -= HorizontalAxis * RotSpeed * HorizontalForce;
        //    leftPower += HorizontalAxis * RotSpeed * HorizontalForce;
        //}

        if(HorizontalAxis != 0 )
        {
            gameObject.transform.Rotate(0, HorizontalAxis * RotSpeed * Time.deltaTime, 0);
        }


        foreach (var wheelCols in rightWheelColliders)
        {
            if(wheelCols.attachedRigidbody.velocity.magnitude <= 0.1f)
            {
                wheelCols.motorTorque = rightPower * Force * 10f;
            }
            else if(wheelCols.attachedRigidbody.velocity.magnitude >= MaxSpeed)
            {
                wheelCols.motorTorque = 0;
            }
            else
            {
                wheelCols.motorTorque = rightPower * Force ;
            }

            if (Input.GetKey(KeyCode.Space))
                wheelCols.brakeTorque = breakForce;
            else if (rightPower == 0f)
                wheelCols.brakeTorque = engineBreakForce;
            else
                wheelCols.brakeTorque = 0f;

            
        }

        foreach (var wheelCols in leftWheelColliders)
        {
            if (wheelCols.attachedRigidbody.velocity.magnitude <= 0.1f)
            {
                wheelCols.motorTorque = leftPower * Force * 10f;
            }
            else if (wheelCols.attachedRigidbody.velocity.magnitude >= MaxSpeed)
            {
                wheelCols.motorTorque = 0;
            }
            else
            {
                wheelCols.motorTorque = leftPower * Force;
            }

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

                if(skillManager == null)
                    weaponPrefab.GetComponent<RocketBehaviour>().damage = GlobalWeaponManager.weaponList[0].damage;
                else
                    weaponPrefab.GetComponent<RocketBehaviour>().damage = skillManager.RocketAttackPoint.getValueAfterCalc(GlobalWeaponManager.weaponList[0].damage);
                
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
                if (skillManager == null)
                {
                    weaponPrefab.GetComponent<RocketBehaviour>().damage = GlobalWeaponManager.weaponList[1].damage;
                }
                else
                {
                    weaponPrefab.GetComponent<LandmineBehaviour>().radius = skillManager.RocketAttackPoint.getValueAfterCalc(weaponPrefab.GetComponent<LandmineBehaviour>().radius);
                    weaponPrefab.GetComponent<LandmineBehaviour>().damage = skillManager.RocketAttackPoint.getValueAfterCalc(GlobalWeaponManager.weaponList[1].damage);
                }

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
            canon.Rotate(new Vector3(canonRotationSpeed * Time.deltaTime, 0f, 0f));
        }
        else if (canonRotate < -0.1f && canonTilte > -20f)
        {
            canon.Rotate(new Vector3(-canonRotationSpeed * Time.deltaTime, 0f, 0f));
        }
        
    }

    public void damage(float damageNumber)
    {
        health -= damageNumber;
        if(health < 0) health = 0;

        if(gameObject.tag == "Player")
        {
            StartCoroutine(DamageEffect.GetDamage());
        }
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
        if (malfunction)
        {
            waterDamageCounter -= Time.deltaTime;
            if(waterDamageCounter<=0)
            {
                damage(10f);
                waterDamageCounter += 1f;
            }
        }
    }
}
