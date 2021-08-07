using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitManager : MonoBehaviour
{
    public Collider2D collider;

    public Sprite openSprite;

    public void openExit()
    {
        GetComponent<SpriteRenderer>().sprite = openSprite;
        collider.enabled = true;
    }
}
