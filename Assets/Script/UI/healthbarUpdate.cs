using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbarUpdate : MonoBehaviour
{
    private RectTransform rectTransform;
    public TankManager tankManager;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.localScale = new Vector3(tankManager.health / tankManager.maxHealth, 1f,1f);
    }
}
