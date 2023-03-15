using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StageUI : MonoBehaviour
{
    [SerializeField]
    private GameStageManager gsm;

    [SerializeField]
    private TextMeshProUGUI stageText;

    [SerializeField]
    private TextMeshProUGUI enemyRemaining;

    [SerializeField]
    private GameObject timeIcon;

    [SerializeField]
    private GameObject skullIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gsm.stageAllClear)
        {
            stageText.text = "All Clear!";

        }
        else if (gsm.isPreparing)
        {
            timeIcon.SetActive(true);
            skullIcon.SetActive(false);
            stageText.text = "Preparing...";
            enemyRemaining.text = gsm.StageTimeCounter.ToString("F0");
        }
        else
        {
            timeIcon.SetActive(false);
            skullIcon.SetActive(true);
            stageText.text = "Stage: " + gsm.currentEnemyCount.ToString();
            enemyRemaining.text = (gsm.enemySpawnCount + gsm.currentEnemyCount).ToString();
        }

    }
}
