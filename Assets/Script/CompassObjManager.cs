using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// reference: https://gamedev-resources.com/create-a-compass-with-ugui/

public class CompassObjManager : MonoBehaviour
{
    [SerializeField] GameObject CompassObject;
    [SerializeField] GameObject CompassImage;
    private GameObject tmpPrefeb;
    [SerializeField] GameObject Camera;

    void Start()
    {
        float finalX = getAngle() * Screen.width * 2f + Screen.width * 0.5f;
        tmpPrefeb = Instantiate(CompassObject, CompassImage.transform.position, Quaternion.identity);
        tmpPrefeb.GetComponent<RectTransform>().localPosition = new Vector3(finalX, 0.0f, 0.0f);
        tmpPrefeb.transform.parent = CompassImage.transform;
        tmpPrefeb.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float finalX = getAngle() * (CompassImage.GetComponent<RectTransform>().rect.size.x / 2);
        tmpPrefeb.GetComponent<RectTransform>().localPosition = new Vector3(finalX, 0.0f, 0.0f);
    }
    private void OnDestroy()
    {
        Destroy(tmpPrefeb);
    }
    private float getAngle()
    {
        return (Vector3.SignedAngle(new Vector3( Camera.transform.forward.x, 0f, Camera.transform.forward.z), getDirection(), Vector3.up) / 180f);
    }
    private Vector3 getDirection()
    {
        return (new Vector3(transform.position.x, Camera.transform.position.y, transform.position.z) - Camera.transform.position).normalized;
    }
}
