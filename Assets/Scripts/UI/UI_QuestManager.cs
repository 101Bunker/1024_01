using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI_QuestManager : MonoBehaviour
{
    public GameObject questPopUp; //����Ʈ�� �߰��Ǹ� ����Ʈ�� �ٲ�� ��.
    public GameObject questPopUpClear;
    public GameObject questBar;
    public GameObject toggleGrp_bar;    //����Ʈ�� �߰��Ǹ� ����Ʈ�� �ٲ�� ��.
    List<GameObject> toggleList = new List<GameObject>();
    [SerializeField] List<Toggle> toggles = new List<Toggle>();
        
    public GameObject listInPanel;
    public GameObject toggleGrp_panel;

    public List<Quest> questList = new List<Quest>();

   public UIManager uiManager;

#region ĳ���� �Դ� �� ����
   void Awake()
    {
        //uiManager.enabled = true;
        //questPopUp.SetActive(false);

        //����Ʈ ���� ���� ĳ���͵� ����Ʈȭ
        for (int questListCount = 0; questListCount < questList.Count; questListCount++)
        {
            for (int i = 0; i < questList[questListCount].locationObj.transform.childCount; i++)
            {
                //�ݶ��̴��� ���� �͵� üũ(ĳ���͵��� �޽��ݶ��̴��� ������ ��)
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
        //����ڰ� ������Ʈ�� �ִ� ��ġ�� ����(������Ʈ���� active �����̸�) ����Ʈ �˾�â ����
        if (!locationObjIsActive && questList[0].locationObj.gameObject.activeInHierarchy)
        {
            locationObjIsActive = !locationObjIsActive;

            uiManager.uiAnim.Play("PopUpQuest_open");
            //questPopUp.SetActive(true);
            //����Ʈ���� �̹����� �ؽ�Ʈ �ٲ���.

            questPopUp.transform.GetChild(1).GetComponent<Text>().text = questList[0].title;
            questPopUp.transform.GetChild(2).GetComponent<Image>().sprite = questList[0].image;
            questPopUpClear.transform.GetChild(2).GetComponent<Image>().sprite = questList[0].image;
            questPopUpClear.transform.GetChild(1).GetComponent<Text>().text = questList[0].clearTitle;
            

            currentCount = questList[0].couponCharactors.Count;
        }

        //������Ʈ ��ġ�ϸ� ����Ʈ ���� ��� +1�ϰ�, ������Ʈ ����
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
            //����Ʈ �ٿ� ��� +1
            toggles[count].interactable = true;

            //����Ʈ �гο��� ����Ʈ ������ ���+1
            //����Ʈ �߰��� if�� �߰��ؾ��� 
             questList[0].toggles[count].interactable = true;
             count += 1;

            //����Ʈ �� ä��� ���� UI ����
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
            //TMP ��� �� ������ ���� ������ Text�� ��ü��.
            listInPanel.transform.GetChild(1).GetComponent<Text>().text = questList[number].title;
        }
        else
        {
            GameObject questAdd = Instantiate(listInPanel);
            questAdd.transform.SetParent(listInPanel.transform.parent);
            questAdd.transform.GetChild(1).GetComponent<TextMeshPro>().text = questList[number].title;
        }

        questBar.SetActive(true);
        //����Ʈ ��/����Ʈ �гο� ĳ���͸�ŭ ��� �� ������ֱ�
        for (int i = 0; i < questBar.transform.childCount; i++)
        {
            if (questBar.transform.GetChild(i).name == "Toggles")
            {
                GameObject toggleGrp = questBar.transform.GetChild(i).gameObject;

                //����Ʈ ���� ���ϴ� 3
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
            //����Ʈ �гο��� ����Ʈ ������ ���+1
            //����Ʈ �߰��� if�� �߰��ؾ��� 
            questList[0].toggles.Add(toggleGrp_panel.transform.GetChild(i).GetComponent<Toggle>());
        }
    }
}
