using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandmineBehaviour : MonoBehaviour
{

    private bool exploded = false;

    public float radius;
    public string enemyTag;

    public float aliveTime;
    private float aliveCounter;

    public GameObject explosionPrefab;

    public int IdxInGWM;

    private float beepSoundCounter;
    [SerializeField]
    private float beepSoundMaxTime;
    [SerializeField]
    private float beepSoundMinTime;
    [SerializeField]
    private AnimationCurve beepSoundCurve;

    [SerializeField]
    private GameObject redLight;
    [SerializeField]
    private float redLightBlinkTime;
    private float lightBlinkCounter;

    [HeaderAttribute("Audio")]
    public AudioSource explosionAudio;
    public AudioSource onGroundAudio;
    public AudioSource beepSound;

    // Start is called before the first frame update
    void Start()
    {
        explosionAudio.spatialBlend = 1.0f;
        onGroundAudio.spatialBlend = 1.0f;
        beepSound.spatialBlend = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {

            aliveCounter += Time.deltaTime;
            if (aliveCounter > aliveTime)
            {
                Instantiate(explosionPrefab, gameObject.transform);
                Explosion();
            }

            beep();

            lightBlinkCounter += Time.deltaTime;
            if (lightBlinkCounter > redLightBlinkTime)
            {
                redLight.SetActive(false);
            }
            else
            {
                redLight.SetActive(true);
            }

            ExplosionDetection();
        }
        else
            redLight.SetActive(false);
    }
    void ExplosionDetection()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag(enemyTag))
            {
                hitCollider.gameObject.GetComponent<TankManager>().damage(GlobalWeaponManager.weaponList[IdxInGWM].damage);
                Instantiate(explosionPrefab, gameObject.transform);
                Explosion();
                break;
            }
        }
    }

    void Explosion()
    {
        exploded = true;
        explosionAudio.Play();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        Destroy(gameObject, explosionAudio.clip.length);
    }
    void beep()
    {
        beepSoundCounter += Time.deltaTime;
        float beepSoundTime = Mathf.Lerp(beepSoundMaxTime, beepSoundMinTime, beepSoundCurve.Evaluate(aliveCounter / aliveTime));
        if(beepSoundCounter >= beepSoundTime)
        {
            lightBlinkCounter = 0;
            beepSoundCounter = 0f;
            beepSound.Play();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            onGroundAudio.Play();
        }
    }
}
