using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.SceneManagement;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{

    public Transform parent;
    public GameObject prefab;
    
    public void Spawn()
    {
        Instantiate(prefab, parent.position, quaternion.identity);
    }
    
}
