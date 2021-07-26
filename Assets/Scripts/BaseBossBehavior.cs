using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossBehavior : MonoBehaviour
{
    [System.Serializable]
    public class Action
    {
        public float duration;
        public Vector2 movePath;
        public Attack attack;

        public void execute()
        {
            attack.execute();
        }

    }
    [System.Serializable]
    public class Attack
    {
        public BulletPattern bulletPattern;
        public void execute()
        {
            Debug.Log("attack executed!");
            bulletPattern.Fire();
        }
    }

    public Action bossAction;

    private void Start()
    {
        bossAction.execute();
    }
}
