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

    [SerializeField] private AudioSource winAudio;
    [SerializeField] private AudioSource lossAudio;

    [SerializeField] private GameMenuManager gameMenuManager;

    private float dieCounter = 0.0f;

    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if(audioObj != null)
            DontDestroyOnLoad(audioObj);
    }

    void Start()
    {
        playerTM = player.GetComponent<TankManager>();

        gsm = GetComponent<GameStageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gsm.stageAllClear)
        {
            StartCoroutine(win());
        }
        if(playerTM.health <= 0) // gameover
        {
            StartCoroutine(lose());
        }

        if(Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.I) && Input.GetKey(KeyCode.E))
        {
            dieCounter += Time.deltaTime;
        }
        else
        {
            dieCounter = 0.0f;
        }

        if(dieCounter > 3.0f )
        {
            StartCoroutine(lose());
        }
    }

    private IEnumerator lose()
    {
        gameMenuManager.setHUD(false);

        yield return new WaitForSeconds(2f);

        gameMenuManager.setHUD(true);

        if (!lossAudio.isPlaying)
            lossAudio.Play();
        gameMenuManager.setState(GameMenuManager.MenuStates.Gameover, true);
        GameObject bgm = GameObject.FindGameObjectWithTag("BackGroundMusic");
        bgm.GetComponent<AudioSource>().Pause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        gameoverMenuUI.SetActive(true);
        gameoverMessage.text = "You Lose, Bye.";
        Time.timeScale = 0.0f;

    }
    public IEnumerator win()
    {
        gameMenuManager.setHUD(false);

        yield return new WaitForSeconds(2f);

        gameMenuManager.setHUD(true);

        if (!winAudio.isPlaying)
            winAudio.Play();
        gameMenuManager.setState(GameMenuManager.MenuStates.Gameover, true);
        GameObject bgm = GameObject.FindGameObjectWithTag("BackGroundMusic");
        bgm.GetComponent<AudioSource>().Pause();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;

        gameoverMenuUI.SetActive(true);
        gameoverMessage.text = "You Win!";
        Time.timeScale = 0.0f;

    }

    public void CheatMenuWin()
    {
        StartCoroutine(win());
    }
}
