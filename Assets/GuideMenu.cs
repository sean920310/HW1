using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideMenu : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isFirstPage = true;
    Animator animator;

    [SerializeField] Button leftBtn;
    [SerializeField] Button rightBtn;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFirstPage)
        {
            leftBtn.interactable = false;
            rightBtn.interactable = true;
        }
        else
        {
            leftBtn.interactable = true;
            rightBtn.interactable = false;
        }
    }
    public void setFirstPage(bool isFirst)
    {
        isFirstPage = isFirst;  
        animator.SetBool("first page", isFirst);
    }    
}
