using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remove_EnemyDeathEffect : MonoBehaviour
{
    float objectLifeTime = 0.5f;

    void Update()
    {
        objectLifeTime -= Time.deltaTime;
        if (objectLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
