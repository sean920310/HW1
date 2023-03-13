using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TankWeaponManager : MonoBehaviour
{
    [Serializable]
    public struct Weapon
    {
        public string WeaponName;
        public int WeaponNumber;
        public int WeaponMaxNumber;
        public float WeaponCoolDownTime;
        public override string ToString() => $"({WeaponName}, {WeaponMaxNumber})";
    }

    [SerializeField]
    public Weapon[] weaponList;

    [HeaderAttribute("Rocket")]
    public GameObject rocketPrefab;
    public GameObject kaBoomPrefab;
    public AudioSource RocketFiringSound;
    public float rocketCoolDown;
    public float rocketFiringCounter = 0f;
    public int rocketNumber;
    public float rocketDamage = 10;

    [HeaderAttribute("LandMine")]
    public GameObject landminePrefab;
    public AudioSource LandmineFiringSound;
    public float landmineCoolDown;
    public float landmineFiringCounter = 0f;
    public int landmineNumber;

    [HeaderAttribute("Tank Weapon Info")]
    private int _currentWeaponIdx = 0;
    public int currentWeaponIdx
    {
        get { return _currentWeaponIdx; }
    }

    private int _currentWeaponNumber = 0;
    public int currentWeaponNumber
    {
        get { return _currentWeaponNumber; }
    }

    private int _currentWeaponMaxNumber = 0;
    public int currentWeaponMaxNumber
    {
        get { return _currentWeaponMaxNumber; }
    }

    // Start is called before the first frame update
    void Start()
    {
        RocketFiringSound.spatialBlend = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        weaponUpdate();
        weaponCDUpdate();
    }
    private void weaponUpdate()
    {
        _currentWeaponMaxNumber = weaponList[currentWeaponIdx].WeaponMaxNumber;
        _currentWeaponNumber = weaponList[currentWeaponIdx].WeaponNumber;
    }

    private void weaponCDUpdate()
    {
        rocketFiringCounter -= Time.deltaTime;
        rocketFiringCounter = rocketFiringCounter <= 0f ? 0f : rocketFiringCounter;

        landmineFiringCounter -= Time.deltaTime;
        landmineFiringCounter = landmineFiringCounter <= 0f ? 0f : landmineFiringCounter;
    }

    public void changeWeapon(bool isAdd)
    {
        if (isAdd)
        {
            _currentWeaponIdx = (_currentWeaponIdx + 1) % weaponList.Length;
        }
        else
        {
            _currentWeaponIdx--;
            if (_currentWeaponIdx < 0)
                _currentWeaponIdx = weaponList.Length - 1;
        }
    }
}
