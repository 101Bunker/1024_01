using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CouponPopUp : MonoBehaviour
{
    public GameObject toggleGrp_bar;    //����Ʈ�� �߰��Ǹ� ����Ʈ�� �ٲ�� ��.
    List<GameObject> toggleList = new List<GameObject>();
    [SerializeField] List<Toggle> toggle = new List<Toggle>();
    public GameObject toggleGrp_panel;
    List<GameObject> toggleListPanel = new List<GameObject>();
    [SerializeField] List<Toggle> togglePanel = new List<Toggle>();

    public GameObject questPopUp; //����Ʈ�� �߰��Ǹ� ����Ʈ�� �ٲ�� ��.
    public GameObject questBar;
    public GameObject parentObj;

    [SerializeField] List<GameObject> couponCharactors = new List<GameObject>();

    UIManager uiManager;
    void Awake()
    {
        questPopUp.SetActive(false);

        //���� ĳ���͵� ����Ʈȭ
        for (int i = 0; i < parentObj.transform.childCount; i++)
        {
            //�ݶ��̴��� ���� �͵� üũ(ĳ���͵��� �޽��ݶ��̴��� ������ ��)
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
        //����ڰ� ������Ʈ�� �ִ� ��ġ�� ����(������Ʈ���� active �����̸�) ����Ʈ �˾�â ����
        if (!isActive && parentObj.gameObject.activeInHierarchy)
        {
            questPopUp.SetActive(true);
            //questPopUp.transform.GetChild(0).GetComponent<Image>().sprite=
            isActive = !isActive;

        }

        //������Ʈ ��ġ�ϸ� ����Ʈ ���� ��� +1�ϰ�, ������Ʈ ����
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

        //����Ʈ �ٿ� ĳ���͸�ŭ ��� �� ������ֱ�
        for (int i = 0; i < questBar.transform.childCount; i++)
        {
            if (questBar.transform.GetChild(i).name == "Toggles")
            {
                GameObject toggleGrp = questBar.transform.GetChild(i).gameObject;

                //����Ʈ ���� ���ϴ� 3
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

            //����Ʈ �� ä��� ���� UI ����
            if (count == couponCharactors.Count)
            {
                uiManager.clearUI_Quest1.SetActive(false);
                uiManager.clearUI_Quest1.SetActive(true);
                uiManager.inventory_0.SetActive(true);
            }
        }
    }
}
