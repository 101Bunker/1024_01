using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLocationChecker : MonoBehaviour
{
    public List<GameObject> storeList = new List<GameObject>();
    [SerializeField] UI_QuestManager questManager;
    int curInt;

    private void OnTriggerEnter(Collider other)
    {
        if (storeList.Contains(other.gameObject))
        {
            curInt = int.Parse(other.gameObject.tag);
            print(curInt);
            questManager.questNum = curInt;
            questManager.QuestPopUpOpen();
            other.GetComponent<CapsuleCollider>().enabled = false;
            other.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
