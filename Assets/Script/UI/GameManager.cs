using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    private TankManager playerTM;
    private GameStageManager gsm;

    public GameObject gameoverMenuUI;
    public TextMeshProUGUI gameoverMessage;

    private float dieCounter = 0.0f;

    [ReadOnly]
    [SerializeField]
    private bool dying = false;

    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        DontDestroyOnLoad(audioObj);
    }

    void Start()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        playerTM = player.GetComponent<TankManager>();

        gsm = GetComponent<GameStageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gsm.stageAllClear)
        {
            win();
        }
        if(playerTM.health == 0) // gameover
        {
            lose();
        }

        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.E))
        {
            dying = true;
            dieCounter += Time.deltaTime;
        }
        else
        {
            dying = false;
            dieCounter = 0.0f;
        }

        if(dieCounter > 3.0f )
        {
            lose();
        }
    }

    private void lose()
    {
        GameObject bgm = GameObject.FindGameObjectWithTag("BackGroundMusic");
        bgm.GetComponent<AudioSource>().Pause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        gameoverMenuUI.SetActive(true);
        gameoverMessage.text = "You Lose, Bye.";
        Time.timeScale = 0.0f;

    }
    private void win()
    {
        GameObject bgm = GameObject.FindGameObjectWithTag("BackGroundMusic");
        bgm.GetComponent<AudioSource>().Pause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        gameoverMenuUI.SetActive(true);
        gameoverMessage.text = "You Win!";
        Time.timeScale = 0.0f;

    }
}