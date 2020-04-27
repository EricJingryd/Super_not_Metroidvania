using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyblock : MonoBehaviour
{
    //static Destroyblock instance;

    //private void Start()
    //{
    //    if(instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bomb"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
