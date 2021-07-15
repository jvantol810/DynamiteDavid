using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public SpriteRenderer sp;
    // Update is called once per frame
    IEnumerator Invisiblity(float duration)
    {
        sp.enabled = false;
        yield return new WaitForSeconds(duration);
        sp.enabled = true;
    }
    
    // Start is called before the first frame update
    public void Toggle(float duration)
    {
        StartCoroutine(Invisiblity(duration));
    }
}
