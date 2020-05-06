using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] int scene;

    bool loaded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!loaded)
            {
                SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
                loaded = true;

            }
        }
    }
}
