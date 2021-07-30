using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPath : MonoBehaviour
{
    public List<Vector2> distances;
    public float speed;
    
    private List<Vector2> destinations;
    private Vector2 currentDistance;
    private Vector2 currentDest;
    private int currentDistanceIndex = 0;
    public void Awake()
    {
        currentDistance = distances[currentDistanceIndex];
    }

    public void Start()
    {
        currentDest = transform.position + (Vector3)currentDistance;
    }

    public void Update()
    {
        currentDistance = distances[currentDistanceIndex];
        MoveAlongPath();
        if(transform.position == (Vector3)currentDest)
        {
            updateCurrentDistance();
        }
    }
    public void MoveAlongPath()
    {
        // Move one step toward the target at our given speed.
        transform.position = Vector2.MoveTowards(transform.position, currentDest, Time.deltaTime * speed);
    }

    public void updateCurrentDistance()
    {
        if(currentDistanceIndex + 1 >= distances.Count)
        {
            currentDistanceIndex = 0;
        }
        else
        {
            currentDistanceIndex++;
        }
        currentDistance = distances[currentDistanceIndex];
        currentDest = transform.position + (Vector3)currentDistance;

    }
    

}
