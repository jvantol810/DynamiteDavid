using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    //Reference to the door to exit
    public GameObject Exit;
    public void Awake()
    {
        //In case the exit is active at start, set it to inactive until the boss is defeated
        Exit.SetActive(false);
    }
    //BossDefeated function, when called, will set the Exit gameobject to active.
    //The boss will call this function when it dies.
    public void BossDefeated()
    {
        Exit.SetActive(true);
    }

}
