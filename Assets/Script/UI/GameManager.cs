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
        playerTM = player.GetComponent<TankManager>();

    }

    // Update is called once per frame
    void Update()
    {
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

        gameoverMenuUI.SetActive(true);
        gameoverMessage.text = "You Lose, Bye.";
        Time.timeScale = 0.0f;

    }
}
