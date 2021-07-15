using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleColliderOff : MonoBehaviour
{
    public Collider2D col;
    public float waitTime;

    // Update is called once per frame
    IEnumerator ToggleCollider()
    {
        col.enabled = false;
        yield return new WaitForSeconds(waitTime);
        col.enabled = true;
    }
    
    // Start is called before the first frame update
    public void Toggle()
    {
        StartCoroutine(ToggleCollider());
    }
}
