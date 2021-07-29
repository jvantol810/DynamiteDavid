using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    public List<Vector2> distances;
    public float speed;
    
    private List<Vector2> destinations;
    private Vector2 currentDest;
    private int i = 0;
    public void Awake()
    {
        destinations = new List<Vector2>();
        foreach (Vector2 distance in distances)
        {
            destinations.Add(transform.position - (Vector3)distance);
        }
        currentDest = destinations[i];
        
    }

    public void Start()
    {
        StartCoroutine(MoveAlongPath());
    }


    public IEnumerator MoveAlongPath()
    {
        if(currentDest != Vector2.zero)
        {
            // Loop until we're within Unity's vector tolerance of our target.
            while (transform.position != (Vector3)currentDest)
            {

                // Move one step toward the target at our given speed.
                transform.position = Vector2.MoveTowards(
                      transform.position,
                      currentDest,
                      speed * Time.deltaTime
                 );

                // Wait one frame then resume the loop.
                yield return null;
            }

            // We have arrived. Ensure we hit it exactly.
            transform.position = currentDest;
            i++;
            currentDest = destinations[i];
            Debug.Log("Destinations " + currentDest);
            yield return MoveAlongPath();
        }
    }

    

}
