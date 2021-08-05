using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformLimits : MonoBehaviour
{
    public float minXCoord;
    public float maxXCoord;
    public float minYCoord;
    public float maxYCoord;

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > maxXCoord)
        {
            transform.position = new Vector2(maxXCoord, transform.position.y);
        }
        if(transform.position.x < minXCoord)
        {
            transform.position = new Vector2(minXCoord, transform.position.y);
        }
        if(transform.position.y > maxYCoord)
        {
            transform.position = new Vector2(transform.position.x, maxYCoord);
        }
        if (transform.position.y < minYCoord)
        {
            transform.position = new Vector2(transform.position.x, minYCoord);
        }
    }
}
