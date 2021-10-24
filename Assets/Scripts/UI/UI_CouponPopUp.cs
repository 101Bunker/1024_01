using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CouponPopUp : MonoBehaviour
{
    public GameObject toggleGrp_bar;    //퀘스트가 추가되면 리스트로 바꿔야 함.
    List<GameObject> toggleList = new List<GameObject>();
    [SerializeField] List<Toggle> toggle = new List<Toggle>();
    public GameObject toggleGrp_panel;
    List<GameObject> toggleListPanel = new List<GameObject>();
    [SerializeField] List<Toggle> togglePanel = new List<Toggle>();

    public GameObject questPopUp; //퀘스트가 추가되면 리스트로 바꿔야 함.
    public GameObject questBar;
    public GameObject parentObj;

    [SerializeField] List<GameObject> couponCharactors = new List<GameObject>();

    UIManager uiManager;
    void Awake()
    {
        questPopUp.SetActive(false);

        //쿠폰 캐릭터들 리스트화
        for (int i = 0; i < parentObj.transform.childCount; i++)
        {
            //콜라이더를 가진 것들 체크(캐릭터들은 메쉬콜라이더를 가져야 함)
            for (int j = 0; j < parentObj.transform.GetChild(j).transform.childCount; j++)
            {
                if (parentObj.transform.GetChild(j).transform.GetChild(j).GetComponent<MeshCollider>() != null)
                {
                    couponCharactors.Add(parentObj.transform.GetChild(i).transform.GetChild(j).gameObject);
                }
            }
        }


        uiManager = GetComponent<UIManager>();
    }
    public bool isActive;
    void Update()
    {
        //사용자가 오브젝트가 있는 위치로 가서(오브젝트들이 active 상태이면) 퀘스트 팝업창 열기
        if (!isActive && parentObj.gameObject.activeInHierarchy)
        {
            questPopUp.SetActive(true);
            //questPopUp.transform.GetChild(0).GetComponent<Image>().sprite=
            isActive = !isActive;

        }

        //오브젝트 터치하면 퀘스트 바의 토글 +1하고, 오브젝트 삭제
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if (Physics.Raycast(Camera.main.transform.position, ray.direction, out hitinfo))
        {
            if (Input.GetMouseButtonDown(0) && couponCharactors.Contains(hitinfo.transform.gameObject))
            {
                GotOne();
                Destroy(hitinfo.transform.parent.transform.gameObject);
            }
        }
    }
    public void OnClickAcceptQuest()
    {
        questBar.SetActive(true);

        //퀘스트 바에 캐릭터만큼 토글 수 만들어주기
        for (int i = 0; i < questBar.transform.childCount; i++)
        {
            if (questBar.transform.GetChild(i).name == "Toggles")
            {
                GameObject toggleGrp = questBar.transform.GetChild(i).gameObject;

                //퀘스트 조건 최하는 3
                if (couponCharactors.Count > 3)
                {
                    for (int j = 0; j < couponCharactors.Count - 3; j++)
                    {
                        GameObject toggle = Instantiate(toggleGrp.transform.GetChild(0).gameObject);
                        toggle.transform.SetParent(toggleGrp.transform);
                    }
                }
            }
        }
        for (int i = 0; i < couponCharactors.Count; i++)
        {
            toggle.Add(toggleGrp_bar.transform.GetChild(i).GetComponent<Toggle>());
            togglePanel.Add(toggleGrp_panel.transform.GetChild(i).GetComponent<Toggle>());
        }
    }
    int count;
    public void GotOne()
    {
        if (count < couponCharactors.Count)
        {
            toggle[count].interactable = true;
            togglePanel[count].interactable = true;
            count += 1;

            //퀘스트 다 채우면 쿠폰 UI 나옴
            if (count == couponCharactors.Count)
            {
                uiManager.clearUI_Quest1.SetActive(false);
                uiManager.clearUI_Quest1.SetActive(true);
                uiManager.inventory_0.SetActive(true);
            }
        }
    }
}
