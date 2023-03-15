using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    [Serializable]
    public struct StageInfo
    {
        public int EnemyCount;
        public float NextStageTime; // sec
        public Transform SpawnPoint;
        public float SpawnMaxTime;
        public float SpawnMinTime;
    }

    [ReadOnly]
    [SerializeField]
    public bool stageAllClear = false;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject enemyList;


    [SerializeField]
    public StageInfo[] _stages;

    [SerializeField]
    private float prepareTime;

    [ReadOnly]
    [SerializeField]
    private bool isPreparing = true;

    [ReadOnly]
    [SerializeField]
    private int currentStage = 0;

    [ReadOnly]
    [SerializeField]
    private int currentEnemyCount = 0;

    [ReadOnly]
    [SerializeField]
    private int enemySpawnCount = 0;

    [ReadOnly]
    [SerializeField]
    private float enemySpawnTime = 0;

    [ReadOnly]
    [SerializeField]
    private float enemySpawnCounter = 0;

    [ReadOnly]
    [SerializeField]
    private float StageTimeCounter = 0f;

    private GameObject tmpEnemy;


    // Start is called before the first frame update
    void Start()
    {
        StageTimeCounter = prepareTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (stageAllClear)
            return;

        StageTimeCounter -= Time.deltaTime;

        if (isPreparing)
        {
            if (StageTimeCounter <= 0f)
            {
                isPreparing = false;

                // stage start
                enemySpawnCount = _stages[currentStage].EnemyCount;
                spawnTimeAssign();
            }
        }
        else
        {
            // stage in progress
            if (enemySpawnCount > 0 && enemySpawnCounter <= 0f)
            {
                spawnEnemy();
            }

            if (enemyList.transform.childCount == 0 && enemySpawnCount == 0)
            {
                if(currentStage == _stages.Length)
                {
                    stageAllClear = true;
                }

                enemySpawnCount = _stages[currentStage].EnemyCount;
                spawnTimeAssign();
                currentStage++;

                StageTimeCounter = prepareTime;
                isPreparing = true;
            }
        }
    }

    private void spawnEnemy() {

        GameObject prefab = Instantiate(enemyPrefab, _stages[currentStage].SpawnPoint.position, Quaternion.identity);
        prefab.transform.parent = enemyList.transform;
        prefab.SetActive(true);
        enemySpawnCount--;
    }

    private void spawnTimeAssign()
    {
        enemySpawnTime = UnityEngine.Random.Range(_stages[currentStage].SpawnMinTime, _stages[currentStage].SpawnMaxTime);
        StageTimeCounter = enemySpawnTime;
    }
}
