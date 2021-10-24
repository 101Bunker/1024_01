using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject agree, yourGame;

    public void ChangeScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void OnBtnDownNextPage()
    {
            agree.SetActive(false);
            agree.SetActive(true);
            yourGame.SetActive(false);
    }
}
