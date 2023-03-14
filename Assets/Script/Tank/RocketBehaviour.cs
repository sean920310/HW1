using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour
{
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    private Rigidbody rb;
    private AudioSource explosionAudio;

    public int IdxInGWM;

    void Start()
    {

        explosionAudio = GetComponent<AudioSource>();
        explosionAudio.spatialBlend = 1.0f;
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
            collision.gameObject.GetComponent<TankManager>().damage(GlobalWeaponManager.weaponList[IdxInGWM].damage);
        }
        Instantiate(explosionPrefab, gameObject.transform);
        rocketDestory();
    }

    private void rocketDestory()
    {
        explosionAudio.Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        Destroy(gameObject, explosionAudio.clip.length);
    }
}
