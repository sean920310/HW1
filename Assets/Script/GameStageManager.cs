using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStageManager : MonoBehaviour
{
    [SerializeField] AudioSource StageStartSound;
    [SerializeField] AudioSource CountDownAudio;
    [SerializeField] AudioSource FirstBGM;

    private bool firstBGMCanPlay;
    private bool firstBGMPlayed;

    [SerializeField] SkillManager skillManager;

    [Serializable]
    public struct StageInfo
    {
        public int EnemyCount;
        public float NextStageTime; // sec
        public Transform SpawnPoint;
        public float SpawnMaxTime;
        public float SpawnMinTime;

        public float EnemyMaxHealth;
        public float EnemyMinHealth;

        public float stageClearExp;
    }

    [ReadOnly]
    [SerializeField]
    private bool _stageAllClear = false;

    public bool stageAllClear
    {
        get
        {
            return _stageAllClear;
        }
    }

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
    private bool _isPreparing = true;

    public bool isPreparing
    {
        get
        {
            return _isPreparing;
        }
    }

    [ReadOnly]
    [SerializeField]
    private int _currentStage = 0;

    public int currentStage
    {
        get
        {
            return _currentStage;
        }
    }

    [ReadOnly]
    [SerializeField]
    private int _currentEnemyCount = 0;

    public int currentEnemyCount
    {
        get
        {
            return _currentEnemyCount;
        }
    }

    [ReadOnly]
    [SerializeField]
    private int _enemySpawnCount = 0;

    public int enemySpawnCount
    {
        get
        {
            return _enemySpawnCount;
        }
    }

    [ReadOnly]
    [SerializeField]
    private float enemySpawnTime = 0;


    [ReadOnly]
    [SerializeField]
    private float enemySpawnCounter = 0;

    [ReadOnly]
    [SerializeField]
    private float _StageTimeCounter = 0f;

    public float StageTimeCounter
    {
        get
        {
            return _StageTimeCounter;
        }
    }

    private GameObject tmpEnemy;


    // Start is called before the first frame update
    void Start()
    {
        _StageTimeCounter = prepareTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentStage >= _stages.Length)
        {
            _stageAllClear = true;
            return;
        }

        if (_stageAllClear)
            return;

        if(firstBGMCanPlay && !firstBGMPlayed && !StageStartSound.isPlaying)
        {
            FirstBGM.Play();
            firstBGMPlayed = true;
        }

        _StageTimeCounter -= Time.deltaTime;
        enemySpawnCounter -= Time.deltaTime;

        _currentEnemyCount = enemyList.transform.childCount;

        if (_isPreparing)
        {
            if(Mathf.Abs(_StageTimeCounter - 3f) < 0.01f)
            {
                CountDownAudio.Play();
            }

            if (_StageTimeCounter <= 0f)
            {
                _isPreparing = false;

                firstBGMCanPlay = true;

                // stage start
                _enemySpawnCount = _stages[_currentStage].EnemyCount;
                spawnTimeAssign();
                enemySpawnCounter = 0;

                StageStartSound.Play();

            }
        }
        else if (_currentStage < _stages.Length)
        {
            if (enemyList.transform.childCount == 0 && _enemySpawnCount == 0)
            {
                // current stage clear
                skillManager.addExp(_stages[_currentStage].stageClearExp);

                _enemySpawnCount = _stages[_currentStage].EnemyCount;
                //spawnTimeAssign();
                _currentStage++;

                _StageTimeCounter = prepareTime;
                _isPreparing = true;

            }
            else if (_enemySpawnCount > 0 && enemySpawnCounter <= 0f)
            {
                // stage in progress
                spawnTimeAssign();
                spawnEnemy();
            }
        }
    }

    private void spawnEnemy() {

        GameObject prefab = Instantiate(enemyPrefab, _stages[_currentStage].SpawnPoint.position, Quaternion.identity);
        prefab.transform.parent = enemyList.transform;
        prefab.SetActive(true);

        prefab.GetComponent<TankManager>().maxHealth = UnityEngine.Random.Range(_stages[_currentStage].EnemyMinHealth, _stages[_currentStage].EnemyMaxHealth);
        prefab.GetComponent<TankManager>().health = UnityEngine.Random.Range(_stages[_currentStage].EnemyMinHealth, _stages[_currentStage].EnemyMaxHealth);

        _enemySpawnCount--;
    }

    private void spawnTimeAssign()
    {
        enemySpawnTime = UnityEngine.Random.Range(_stages[_currentStage].SpawnMinTime, _stages[_currentStage].SpawnMaxTime);
        enemySpawnCounter = enemySpawnTime;
    }
}
