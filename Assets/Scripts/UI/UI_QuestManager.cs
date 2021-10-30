using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_QuestManager : MonoBehaviour
{
    public GameObject questPopUp; //퀘스트가 추가되면 리스트로 바꿔야 함.
    public GameObject questPopUpClear;
    public GameObject questBar;
    public GameObject toggleGrp_bar;    //퀘스트가 추가되면 리스트로 바꿔야 함.
    List<GameObject> toggleList = new List<GameObject>();
    [SerializeField] List<Toggle> toggles = new List<Toggle>();
        
    public GameObject listInPanel;
    public GameObject toggleGrp_panel;

    public List<Quest> questList = new List<Quest>();

   public UIManager uiManager;

#region 캐릭터 먹는 것 관련
   void Awake()
    {
        //uiManager.enabled = true;
        //questPopUp.SetActive(false);

        //퀘스트 별로 쿠폰 캐릭터들 리스트화
        for (int questListCount = 0; questListCount < questList.Count; questListCount++)
        {
            for (int i = 0; i < questList[questListCount].locationObj.transform.childCount; i++)
            {
                //콜라이더를 가진 것들 체크(캐릭터들은 메쉬콜라이더를 가져야 함)
                for (int j = 0; j < questList[questListCount].locationObj.transform.GetChild(j).transform.childCount; j++)
                {
                    if (questList[questListCount].locationObj.transform.GetChild(j).transform.GetChild(j).GetComponent<MeshCollider>() != null)
                    {
                        questList[questListCount].couponCharactors.Add(questList[questListCount].locationObj.transform.GetChild(i).transform.GetChild(j).gameObject);
                    }
                }
            }
        }

       uiManager = GetComponent<UIManager>();
    }
    public bool locationObjIsActive;


    int currentCount;
    void Update()
    {
        //사용자가 오브젝트가 있는 위치로 가서(오브젝트들이 active 상태이면) 퀘스트 팝업창 열기
        if (!locationObjIsActive && questList[0].locationObj.gameObject.activeInHierarchy)
        {
            locationObjIsActive = !locationObjIsActive;

            uiManager.uiAnim.Play("PopUpQuest_open");
            //questPopUp.SetActive(true);
            //퀘스트마다 이미지랑 텍스트 바꿔줌.

            questPopUp.transform.GetChild(1).GetComponent<Text>().text = questList[0].title;
            questPopUp.transform.GetChild(2).GetComponent<Image>().sprite = questList[0].image;
            questPopUpClear.transform.GetChild(2).GetComponent<Image>().sprite = questList[0].image;
            questPopUpClear.transform.GetChild(1).GetComponent<Text>().text = questList[0].clearTitle;
            

            currentCount = questList[0].couponCharactors.Count;
        }

        //오브젝트 터치하면 퀘스트 바의 토글 +1하고, 오브젝트 삭제
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hitinfo))
        {
            if (Input.GetMouseButtonDown(0) && questList[0].couponCharactors.Contains(hitinfo.transform.gameObject))
            {
                Destroy(hitinfo.transform.parent.transform.gameObject);
                GotOne();
            }
        }
    }
    int count;
    public void GotOne()
    {
        if (count < currentCount)
        {
            //퀘스트 바에 토글 +1
            toggles[count].interactable = true;

            //퀘스트 패널에서 퀘스트 각각에 토글+1
            //퀘스트 추가시 if문 추가해야함 
             questList[0].toggles[count].interactable = true;
             count += 1;

            //퀘스트 다 채우면 쿠폰 UI 나옴
            if (count == currentCount)
            {
                uiManager.uiAnim.Play("PopUpQuestClear_open");
                //uiManager.clearUI_Quest1.SetActive(false);
                //uiManager.clearUI_Quest1.SetActive(true);
                uiManager.inventory_0.SetActive(true);
            }
        }
    }
    #endregion
    public void OnClickAcceptQuest(int number)
    {
        if (number == 0)
        {
            listInPanel.SetActive(true);
            // questContent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = questList[number];
            //TMP 사용 시 깨지는 글자 때문에 Text로 대체함.
            listInPanel.transform.GetChild(1).GetComponent<Text>().text = questList[number].title;
        }
        else
        {
            GameObject questAdd = Instantiate(listInPanel);
            questAdd.transform.SetParent(listInPanel.transform.parent);
            questAdd.transform.GetChild(1).GetComponent<TextMeshPro>().text = questList[number].title;
        }

        questBar.SetActive(true);
        //퀘스트 바/퀘스트 패널에 캐릭터만큼 토글 수 만들어주기
        for (int i = 0; i < questBar.transform.childCount; i++)
        {
            if (questBar.transform.GetChild(i).name == "Toggles")
            {
                GameObject toggleGrp = questBar.transform.GetChild(i).gameObject;

                //퀘스트 조건 최하는 3
                if (currentCount > 3)
                {
                    for (int j = 0; j < currentCount - 3; j++)
                    {
                        GameObject toggle = Instantiate(toggleGrp.transform.GetChild(0).gameObject);
                        toggle.transform.SetParent(toggleGrp.transform);
                        toggle.transform.localScale=new Vector3(1, 1, 1);
                        GameObject toggle_panel = Instantiate(toggleGrp_bar.transform.GetChild(0).gameObject);
                        toggle_panel.transform.SetParent(toggleGrp_panel.transform);
                        toggle_panel.transform.localScale=new Vector3(1, 1, 1);
                    }
                }
            }
        }
        for (int i = 0; i < currentCount; i++)
        {
            print(0);
            toggles.Add(toggleGrp_bar.transform.GetChild(i).GetComponent<Toggle>());
            //퀘스트 패널에서 퀘스트 각각에 토글+1
            //퀘스트 추가시 if문 추가해야함 
            questList[0].toggles.Add(toggleGrp_panel.transform.GetChild(i).GetComponent<Toggle>());
        }
    }
}
