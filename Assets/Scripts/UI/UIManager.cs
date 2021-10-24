using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
   public Animator uiAnim;
   // public UI_QuestManager uiQM;

    public GameObject clearUI_Quest1;
    public GameObject inventory_0;
    public GameObject userName;
    void Awake()
    {
    //    uiQM.enabled = true;
        uiAnim = GetComponent<Animator>();
    }  
  

    #region Button Functions
    bool clickedQuest;
    bool clickedInven;
    bool clickedMy;

    public GameObject[] panels;
    public void OnClickQuest()
    {
        clickedQuest = !clickedQuest;
        if (clickedQuest)
        {
            panels[0].SetActive(true);
            panels[1].SetActive(false);
            panels[2].SetActive(false);
            uiAnim.Play("Panel_Quest_up");

            clickedInven = false;
            clickedMy = false;
        }
        else
        {
            //  panels[0].SetActive(false);
            uiAnim.Play("Panel_Quest_down");
        }
    }
    public void OnClickInventory()
    {
        clickedInven = !clickedInven;
        if (clickedInven)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(true);
            panels[2].SetActive(false);
            uiAnim.Play("Panel_Inventory_up");

            clickedMy = false;
            clickedQuest = false;
        }
        else
        {
            //  panels[0].SetActive(false);
            uiAnim.Play("Panel_Inventory_down");
        }
    }
    public void OnClickMy()
    {
        clickedMy = !clickedMy;
        if (clickedMy)
        {
            panels[0].SetActive(false);
            panels[1].SetActive(false);
            panels[2].SetActive(true);
            uiAnim.Play("Panel_My_up");

            clickedQuest = false;
            clickedInven = false;
        }
        else
        {
            //  panels[0].SetActive(false);
            uiAnim.Play("Panel_My_down");
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

}
