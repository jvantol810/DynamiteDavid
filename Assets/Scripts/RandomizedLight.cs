using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class RandomizedLight : MonoBehaviour
{
    private Light2D light2d;
    public bool SetColorRandomly;
    //public bool shiftRainbow;
    //float time;
    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light2D>();
        if (SetColorRandomly)
        {
            light2d.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if(shiftRainbow)
        //{
            //time = Mathf.PingPong(Time.time, 1f) / 1f;
            //light2d.color = Color.Lerp(Color.red, Color.blue, time);
        //}
    }
}
