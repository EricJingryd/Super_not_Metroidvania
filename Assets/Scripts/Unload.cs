using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unload : MonoBehaviour
{
    public int scene;

    bool unloaded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!unloaded)
            {
                unloaded = true;

                Manager.anyManager.UnloadScene(scene);
            }
        }
    }
}
