using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoudiniHugger : Hugger
{
    [Header("Invisibility Settings")]
    public float durationVisible;
    public float durationInvisible;
    // Start is called before the first frame update
    private IEnumerator VisLoop()
    {
        yield return new WaitForSeconds(durationVisible);
        GetComponent<Invisibility>().Toggle(durationInvisible);
        yield return new WaitForSeconds(durationInvisible);
        StartCoroutine(VisLoop());
    }
    

    // Update is called once per frame
    void Start()
    {
        StartCoroutine(VisLoop());
    }
}
