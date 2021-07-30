using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
    public float duration;

    public CirclePattern bulletPattern;
    public GameObject spawnPrefab;
    public Transform spawnPoint;
    public virtual IEnumerator execute()
    {
        if (spawnPrefab != null)
        {
            GameObject obj = Instantiate(spawnPrefab);
            obj.transform.position = spawnPoint.position;
            obj.TryGetComponent<MineController>(out MineController mine);
            if (mine != null)
            {
                mine.active = false;
            }
        }
        if (bulletPattern != null)
        {
            yield return bulletPattern.Fire();
        }
        
    }
}
