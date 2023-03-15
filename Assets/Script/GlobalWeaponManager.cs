using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalWeaponManager : MonoBehaviour
{
    [Serializable]
    public struct WeaponValue
    {
        public string name;
        public int maxNumber;
        public float coolDownTime;
        public float damage;
        public override string ToString() => $"({name}, {maxNumber})";
    }

    [SerializeField]
    public WeaponValue[] _weaponList;

    static public WeaponValue[] weaponList;
    private void Start()
    {
        OnAfterDeserialize();
        OnBeforeSerialize();
    }
    public void OnAfterDeserialize()
    {
        weaponList = _weaponList;
    }

    public void OnBeforeSerialize()
    {
        _weaponList = weaponList;
    }
}
