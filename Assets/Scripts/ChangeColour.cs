using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{

    public Color changeable;
    private SpriteRenderer spriterenderer;
    void Start()
    {
        spriterenderer = GetComponent<SpriteRenderer>();
        spriterenderer.color = changeable;
    }

   
    void Update()
    {
        
    }
}
