using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemBehaviours : MonoBehaviour
{
    public ItemSpawer.ItemType itemType;
    public AudioSource pickupAudio;
    public int number;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.transform.root.gameObject.tag == "Player" || other.transform.root.gameObject.tag == "Enemy") && gameObject.GetComponent<MeshRenderer>().enabled)
        {
            TankWeaponManager twm = other.transform.root.gameObject.GetComponent<TankWeaponManager>();
            switch (itemType)
            {
                case ItemSpawer.ItemType.Rocket:
                    twm.addRocketAmmo(number);
                    break;
                case ItemSpawer.ItemType.Landmine:
                    twm.addLandmineAmmo(number);
                    break;
                case ItemSpawer.ItemType.MedicalKit:
                    other.transform.root.gameObject.GetComponent<TankManager>().health += number;
                    break;
            }
            pickupAudio.Play();
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, pickupAudio.clip.length);
        }
    }

    private void Update()
    {
        gameObject.transform.Rotate(0, 25 * Time.deltaTime, 0);
    }
}
