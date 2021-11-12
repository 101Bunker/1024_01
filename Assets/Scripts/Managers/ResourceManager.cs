using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    // ������ ã�����
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        // 1. orginal �̹� ��� ������ �ٷ� ���
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.Log($"Falied to load prefab : {path}");
            return null;
        }

        // 2. Ȥ�� Ǯ���� �ְ� ������?
        GameObject go = Object.Instantiate(prefab, parent);
        go.name = prefab.name;

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // ���࿡ Ǯ���� �ʿ��� ���̶�� -> Ǯ�� �Ŵ������� ��Ź

        Object.Destroy(go);
    }
}