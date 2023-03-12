using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private AudioSource explosion;


    void Start()
    {
        explosion = GetComponent<AudioSource>();
        explosion.spatialBlend = 1.0f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<TankManager>().damage();
        }
        rocketDestory();
    }

    private void rocketDestory()
    {
        explosion.Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        Destroy(gameObject, explosion.clip.length);
    }
}
