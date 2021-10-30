using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject agree, yourGame;


    public void LoadNextScene()
    {
        // ���� �� ������ ������ �´�.
        Scene scene = SceneManager.GetActiveScene();

        // ���� ���� ���� ������ ������ �´�.
        int curScene = scene.buildIndex;

        // ���� �� �ٷ� �������� �������� ���� +1�� ���ش�.
        int nextScene = curScene + 1;

        // ���� ���� �ҷ��´�
        SceneManager.LoadScene(nextScene);
    }

    public void OnBtnDownNextPage()
    {
        agree.SetActive(false);
        agree.SetActive(true);
        yourGame.SetActive(false);
    }
}
