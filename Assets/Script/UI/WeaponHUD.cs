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
    public TextMeshProUGUI weaponMaxNumText;
    public RectTransform numberBar;

    private Color textOriginColor;
    // Start is called before the first frame update
    void Start()
    {
        textOriginColor = weaponNumText.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (twm.curWeaponIdx == 0)
        {
            rocketIcon.SetActive(true);
            landmineIcon.SetActive(false);
        }
        else if (twm.curWeaponIdx == 1)
        {
            rocketIcon.SetActive(false);
            landmineIcon.SetActive(true);
        }

        weaponNumText.text = twm.curWeaponNumber.ToString();
        if (twm.curWeaponNumber <= 0f)
            weaponNumText.color = new Color(1, 0, 0, 0.86274f);
        else
            weaponNumText.color = textOriginColor;
        weaponMaxNumText.text = twm.curWeaponMaxNumber.ToString();

        float numberPercent = (float)twm.curWeaponNumber / (float)twm.curWeaponMaxNumber;

        if((float)twm.curWeaponMaxNumber > 0.0001f)
            numberBar.localScale = new Vector3(numberPercent, numberBar.localScale.y, numberBar.localScale.z);

    }
}
