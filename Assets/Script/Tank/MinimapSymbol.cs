using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapSymbol : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position;
        transform.position += Vector3.up;

        transform.rotation = Quaternion.Euler(90f, transform.parent.rotation.eulerAngles.y, 0f);



    }
}
