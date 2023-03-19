using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    [Serializable]
    public enum ItemType
    {
        Rocket,
        Landmine,
        MedicalKit
    }

    [SerializeField]
    private GameObject[] itemList;

    public void spawnItem()
    {
        int spawnIdx = UnityEngine.Random.Range(0, itemList.Length);
        Vector3 spawnPos = transform.position + Vector3.up * 1.2f;
        if (spawnIdx == ((int)ItemType.Rocket))
            spawnPos -= Vector3.forward * 0.5f;
        GameObject item = GameObject.Instantiate(itemList[spawnIdx], spawnPos, Quaternion.identity);
        item.SetActive(true);
    }
}