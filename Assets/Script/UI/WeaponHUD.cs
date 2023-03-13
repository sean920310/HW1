using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponHUD : MonoBehaviour
{
    public TankWeaponManager twm;

    public GameObject rocketIcon;
    public GameObject landmineIcon;

    public TextMeshProUGUI weaponNumText;
    public RectTransform numberBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (twm.currentWeaponIdx == 0)
        {
            rocketIcon.SetActive(true);
            landmineIcon.SetActive(false);
        }
        else if (twm.currentWeaponIdx == 1)
        {
            rocketIcon.SetActive(false);
            landmineIcon.SetActive(true);
        }

        weaponNumText.text = twm.currentWeaponNumber.ToString();

        float numberPercent = (float)twm.currentWeaponNumber / (float)twm.currentWeaponMaxNumber;
        numberBar.localScale = new Vector3(numberPercent, numberBar.localScale.y, numberBar.localScale.z);
    }
}
