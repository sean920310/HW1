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
        Cheat       = 1 << 4
    }

    [SerializeField] private GameObject GameoverMenu;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject MapMenu;
    [SerializeField] private GameObject SkillMenu;
    [SerializeField] private GameObject CheatMenu;

    private uint menuState = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameoverMenu.activeSelf)
            GameoverMenu.SetActive(getState(MenuStates.Gameover));

        PauseMenu.SetActive(getState(MenuStates.Pause));
        MapMenu.SetActive(getState(MenuStates.Map));
        SkillMenu.SetActive(getState(MenuStates.Skill));
        CheatMenu.SetActive(getState(MenuStates.Cheat));

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
    public bool getState(MenuStates state)
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
}
