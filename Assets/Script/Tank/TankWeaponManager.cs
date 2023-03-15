using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankWeaponManager : MonoBehaviour
{

    [Serializable]
    public struct WeaponStatus
    {
        public int WeaponNumber;
        public float WeaponCounter;
    }

    [SerializeField]
    public WeaponStatus[] weaponStatusList;

    [HeaderAttribute("Rocket")]
    public GameObject rocketPrefab;
    public GameObject kaBoomPrefab;
    public AudioSource RocketFiringSound;

    [HeaderAttribute("LandMine")]
    public GameObject landminePrefab;
    public AudioSource LandmineFiringSound;

    [HeaderAttribute("Tank Weapon Info")]
    public bool isAmmoInfinity;
    public bool haveReloadTime;

    private int _curWeaponIdx = 0;
    public int curWeaponIdx
    {
        get { return _curWeaponIdx; }
    }

    private int _curWeaponAmmoNumber = 0;
    public int curWeaponNumber
    {
        get { return _curWeaponAmmoNumber; }
    }

    private int _curWeaponMaxNumber = 0;
    public int curWeaponMaxNumber
    {
        get { return _curWeaponMaxNumber; }
    }

    private float _curWeaponCD = 0;
    public float curWeaponCD
    {
        get { return _curWeaponCD; }
    }

    private float _curWeaponCounter = 0;
    public float curWeaponCounter
    {
        get { return _curWeaponCounter; }
    }

    // Start is called before the first frame update
    void Start()
    {
        RocketFiringSound.spatialBlend = 1.0f;
        LandmineFiringSound.spatialBlend = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        weaponUpdate();
        weaponCDUpdate();
    }
    private void weaponUpdate()
    {

        if (weaponStatusList[curWeaponIdx].WeaponNumber > GlobalWeaponManager.weaponList[curWeaponIdx].maxNumber)
            weaponStatusList[curWeaponIdx].WeaponNumber = GlobalWeaponManager.weaponList[curWeaponIdx].maxNumber;
        else if (weaponStatusList[curWeaponIdx].WeaponNumber < 0)
            weaponStatusList[curWeaponIdx].WeaponNumber = 0;

            _curWeaponMaxNumber = GlobalWeaponManager.weaponList[curWeaponIdx].maxNumber;
        _curWeaponAmmoNumber = weaponStatusList[curWeaponIdx].WeaponNumber;

        _curWeaponCounter = weaponStatusList[curWeaponIdx].WeaponCounter;
        _curWeaponCD = GlobalWeaponManager.weaponList[curWeaponIdx].coolDownTime;
    }

    private void weaponCDUpdate()
    {
        for(int i = 0; i < weaponStatusList.Length; i++)
        {
            weaponStatusList[i].WeaponCounter -= Time.deltaTime;
            if(weaponStatusList[i].WeaponCounter < 0f)
            {
                weaponStatusList[i].WeaponCounter = 0f;
            }
        }
    }

    public void addWeaponNumber(bool isAdd)
    {
        if (isAdd) _curWeaponIdx++;
        else _curWeaponIdx--;

        _curWeaponIdx = Math.Clamp(_curWeaponIdx, 0, weaponStatusList.Length - 1);
    }

    public bool currentWeaponAvailable(int number)
    {
        return (isAmmoInfinity || curWeaponNumber - number >= 0f) && (!haveReloadTime || _curWeaponCounter <= 0f);
    }

    public void currentWeaponFired(int number)
    {
        weaponStatusList[curWeaponIdx].WeaponNumber -= number;
        weaponStatusList[curWeaponIdx].WeaponCounter = GlobalWeaponManager.weaponList[curWeaponIdx].coolDownTime;
    }

    public bool isOutOfAmmo()
    {
        return (_curWeaponAmmoNumber <= 0f);
    }
    public bool isReloading()
    {
        return (_curWeaponCounter > 0f);
    }
}
