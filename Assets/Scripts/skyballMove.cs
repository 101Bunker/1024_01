using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyballMove : MonoBehaviour
{
    int a = 1;

    private void Update()
    {
        if (transform.position.y < 55.0f)
            a = 1;
        else if (transform.position.y >= 65.0f)
            a = -1;

        transform.Translate(Vector3.up * 1.0f * Time.deltaTime * a);
    }
}
