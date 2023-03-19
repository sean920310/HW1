using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public enum MenuStates
    {
        Gameover    = 1 << 0,
        Pause       = 1 << 1,
        Map         = 1 << 2,
        Skill       = 1 << 3,
        Cheat       = 1 << 4,
        Guide       = 1 << 5,
    }

    [SerializeField] private GameObject GameoverMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject MapMenu;
    [SerializeField] private GameObject SkillMenu;
    [SerializeField] private GameObject CheatMenu;
    [SerializeField] private GameObject GuideMenu;

    public static bool firstGame = true;
    public static uint menuState = 1 << 5;

    void Start()
    {
        if(firstGame)
        {
            firstGame = false;
            showCursorAndPause();
        }
        else
        {
            hideCursorAndStart();
        }
    }

    private void OnDestroy()
    {
        menuState = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameoverMenu.activeSelf)
        {
            StartCoroutine(gameoverAnim());
        }

        PauseMenu.SetActive(getState(MenuStates.Pause));
        MapMenu.SetActive(getState(MenuStates.Map));
        SkillMenu.SetActive(getState(MenuStates.Skill));
        CheatMenu.SetActive(getState(MenuStates.Cheat));
        GuideMenu.SetActive(getState(MenuStates.Guide));

        if (!getState(MenuStates.Gameover))
        {
            if (menuState == 0)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    showCursorAndPause();
                    setState(MenuStates.Pause, true);
                }
                else if (Input.GetKeyDown(KeyCode.M))
                {
                    showCursorAndPause();
                    setState(MenuStates.Map, true);
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    showCursorAndPause();
                    setState(MenuStates.Skill, true);
                }
                else if (Input.GetKeyDown(KeyCode.Slash))
                {
                    showCursorAndPause();
                    setState(MenuStates.Cheat, true);
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    showCursorAndPause();
                    setState(MenuStates.Guide, true);
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    hideCursorAndStart();
                    menuState = 0;
                }
                else if (Input.GetKeyDown(KeyCode.M))
                {
                    hideCursorAndStart();
                    setState(MenuStates.Map, false);
                }
                else if (Input.GetKeyDown(KeyCode.G))
                {
                    hideCursorAndStart();
                    setState(MenuStates.Skill, false);
                }
                else if (Input.GetKeyDown(KeyCode.Slash))
                {
                    hideCursorAndStart();
                    setState(MenuStates.Cheat, false);
                }
                else if (Input.GetKeyDown(KeyCode.P))
                {
                    hideCursorAndStart();
                    setState(MenuStates.Guide, false);
                }
            }
        }

    }
    public void setPauseOff()
    {
        hideCursorAndStart();
        setState(MenuStates.Pause, false);
    }

    public void setState(MenuStates state, bool isOpen)
    {
        if (isOpen)
        {
            menuState = menuState | (System.UInt32)state;

        }
        else
        {
            uint tmp = (System.UInt32)state;
            tmp = ~tmp;
            menuState = menuState & tmp;
        }
    }
    public static bool getState(MenuStates state)
    {
        uint tmp = (System.UInt32)state;
        return ((menuState & tmp) != 0);
    }

    private void hideCursorAndStart()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void showCursorAndPause()
    {
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    IEnumerator gameoverAnim()
    {
        menuState = 0;
        setState(MenuStates.Gameover, true);
        Time.timeScale = 0.0f;

        yield return new WaitForSeconds(0.5f);

        showCursorAndPause();
        GameoverMenu.SetActive(getState(MenuStates.Gameover));
    }
}
