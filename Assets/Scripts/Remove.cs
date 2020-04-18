using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove : MonoBehaviour
{
    float objectLifeTime = 0.3f;

    void Update()
    {
        objectLifeTime -= Time.deltaTime;
        if (objectLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
