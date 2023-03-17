using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawer : MonoBehaviour
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

    [SerializeField]
    private float spawnCounter = 3f;

    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private SpriteRenderer minimapSymbol;

    [SerializeField]
    private Color itemExistColor;
    [SerializeField]
    private Color itemNotExistColor;

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.childCount >= 2)
        {
            minimapSymbol.color = itemExistColor;
            spawnCounter = spawnTime;
            return;
        }
        minimapSymbol.color = itemNotExistColor;
        spawnCounter -= Time.deltaTime;

        if(spawnCounter <= 0 )
        {
            spawnCounter = spawnTime;
            if (itemList.Length > 0)
            {
                spawnItem();
            }
        }
    }

    private void spawnItem()
    {
        int spawnIdx = UnityEngine.Random.Range(0, itemList.Length);
        Vector3 spawnPos = transform.position + Vector3.up * 1.2f;
        if(spawnIdx == ((int)ItemType.Rocket))
            spawnPos -= Vector3.forward * 0.5f;
        GameObject item = GameObject.Instantiate(itemList[spawnIdx], spawnPos, Quaternion.identity);
        item.transform.SetParent(transform);
        item.SetActive(true);
    }
}
