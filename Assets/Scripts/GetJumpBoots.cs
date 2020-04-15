using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJumpBoots : MonoBehaviour
{

    Player player;
    public GameObject playerscript;

    // Start is called before the first frame update
    void Start()
    {
        player = playerscript.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.playerHasJumpBoots = true;
        Destroy(gameObject);
        FindObjectOfType<AudioManager>().Play("Upgrade");
    }
}
