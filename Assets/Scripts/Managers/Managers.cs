using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성을 보장
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 가져온다

    SoundManager _sound = new SoundManager();
    ResourceManager _resouce = new ResourceManager();


    public static SoundManager Sound { get { return Instance._sound; } }
    public static ResourceManager Resource { get { return Instance._resouce; } }

    private void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._sound.Init();
        }
    }
}
