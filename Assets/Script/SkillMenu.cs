using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class SkillMenu : MonoBehaviour
{
    [SerializeField] GameObject SkillMenuUI;

    [SerializeField] SkillManager skillManager;
    [SerializeField] TextMeshProUGUI skillpointText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI expText;

    [SerializeField] RectTransform expBar;

    public static bool isShopping = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        skillpointText.text = "Skill Point: " + skillManager.currentSkillPoint.ToString();
        levelText.text = "Lv. " + skillManager.currentLevel.ToString();
        if(skillManager.currentExpForNextLevel != 0f)
            expBar.localScale = new Vector3( skillManager.currentExp / skillManager.currentExpForNextLevel,1f,1f);
        expText.text = skillManager.currentExp.ToString() + "/" + skillManager.currentExpForNextLevel.ToString();


        if (Input.GetKeyDown(KeyCode.G))
        {
            isShopping = !isShopping;
            if (isShopping)
            {
                SkillMenuUI.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
            }
            else
            {
                SkillMenuUI.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
