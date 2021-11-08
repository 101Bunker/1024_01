using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickQuestList : MonoBehaviour
{
    Animator uiAnim;
    private void Awake()
    {
        uiAnim = GameObject.Find("Canvas").GetComponent<Animator>();
    }
    public void OnClick()
    {
        uiAnim.Play("QuestBarUp");
    }
}
