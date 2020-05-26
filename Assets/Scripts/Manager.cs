using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public static Manager anyManager;

    Bomb bomb;

    bool gameStart;

    private void Awake()
    {
        
        if (!gameStart)
        {
            anyManager = this;

            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);

            gameStart = true;
        }

    }

    private void Start()
    {
        if (gameStart)
        {
            FindObjectOfType<AudioManager>().StopPlaying("Menu");
            FindObjectOfType<AudioManager>().Play("BGM");
        }
    }

    public void UnloadScene(int scene)
    {
        StartCoroutine(Unload(scene));
    }

    IEnumerator Unload(int scene)
    {
        yield return null;

        SceneManager.UnloadSceneAsync(scene);
    }

}
