using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOverTime : MonoBehaviour
{
    public GameObject objectToScale;
    public Vector2 scaleVector = new Vector2(1, 1);
    public Vector2 finalSize;
    protected bool scaling = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (scaling) { Scale(); }
        
    }

    protected virtual void Scale()
    {
        if(objectToScale.transform.localScale.x < finalSize.x)
        {
            objectToScale.transform.localScale += new Vector3(scaleVector.x * Time.deltaTime, 0, 0);
        }
        if (objectToScale.transform.localScale.y < finalSize.y)
        {
            objectToScale.transform.localScale += new Vector3(0, scaleVector.y * Time.deltaTime, 0);
        }
        if(objectToScale.transform.localScale.x >= finalSize.x && objectToScale.transform.localScale.y >= finalSize.y)
        {
            scaling = false;
        }
    }
}
