using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [HideInInspector]
    public Animator uiAnim;

    public GameObject userName;
    void Awake()
    {
        uiAnim = GetComponent<Animator>();
    }


    #region Button Functions
    bool clickedQuest;
    bool clickedInven;
    bool clickedMy;

    public GameObject[] panels;

    public void OnClickButton(string thisButton)
    {

    }
    public void OnClickQuest()
    {
        clickedQuest = !clickedQuest;
        clickedInven = false;
        clickedMy = false;
        if (clickedQuest)
        {
            uiAnim.Play("Panel_Quest_up");
        }
        else
        {
            uiAnim.Play("Panel_Quest_down");
        }
    }
    public void OnClickInventory()
    {
        clickedInven = !clickedInven;
        clickedQuest = false;
        clickedMy = false;
        if (clickedInven)
        {
            uiAnim.Play("Panel_Inventory_up");
        }
        else
        {
            uiAnim.Play("Panel_Inventory_down");
        }
    }
    public void OnClickMy()
    {
        clickedMy = !clickedMy;
        clickedInven = false;
        clickedQuest = false;
        if (clickedMy)
        {
            uiAnim.Play("Panel_My_up");
        }
        else
        {
            uiAnim.Play("Panel_My_down");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

}
