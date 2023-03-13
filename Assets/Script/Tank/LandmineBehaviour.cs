using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class LandmineBehaviour : MonoBehaviour
{

    private AudioSource explosion;

    public float radius;
    public string enemyTag;

    public float aliveTime;
    private float aliveCounter;

    // Start is called before the first frame update
    void Start()
    {
        explosion = GetComponent<AudioSource>();
        explosion.spatialBlend = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        aliveCounter += Time.deltaTime;
        if (aliveCounter > aliveTime)
            Explosion();

        ExplosionDetection();
    }
    void ExplosionDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                hitCollider.gameObject.GetComponent<TankManager>().damage();
                Explosion();
                break;
            }
        }
    }

    void Explosion()
    {
        explosion.Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        Destroy(gameObject, explosion.clip.length);
    }
}
