using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
    public float duration;
    public Vector2 moveDirection;
    public float moveSpeed;

    public CirclePattern bulletPattern;
    public GameObject spawnPrefab;
    public Transform spawnPoint;
    public virtual IEnumerator execute()
    {
        if (spawnPrefab != null)
        {
            GameObject newObj = Instantiate(spawnPrefab);
            newObj.transform.position = spawnPoint.position;
        }
        if (bulletPattern != null)
        {
            yield return bulletPattern.Fire();
        }
    }
}
