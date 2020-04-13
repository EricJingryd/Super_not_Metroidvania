using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJumpBoots : MonoBehaviour
{
    Player player;
    public GameObject playerscript;

    void Start()
    {
        player = playerscript.GetComponent<Player>();
    }

    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.playerHasJumpBoots = true;
        Destroy(gameObject);
    }
}
