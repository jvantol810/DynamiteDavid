using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossBehavior : MonoBehaviour
{
    public float actionDelay = 1f;
    public List<BossAction> actionChain;
    public List<BossPath> pathChain;
    private int currentActionIndex;
    private BossAction currentAction;
    private float currentActionTimer;
    private void Start()
    {
        currentActionIndex = 0;
        currentAction = actionChain[currentActionIndex];
        currentActionTimer = currentAction.duration;
        //executeCurrentAction();
        StartCoroutine(executeActions());
    }

    private void Update()
    {
        //if (currentActionTimer > 0)
        //{
        //    currentActionTimer -= Time.deltaTime;
        //}
        //else
        //{
        //    nextAction();
        //}
    }

    private void FixedUpdate()
    {
        move(currentAction.moveDirection, currentAction.moveSpeed);
    }

    private void executeCurrentAction()
    {
        StartCoroutine(currentAction.execute());
    }

    public IEnumerator executeActions()
    {
        while (true)
        {
            foreach (BossAction action in actionChain)
            {
                yield return new WaitForSeconds(actionDelay);
                yield return action.execute();
            }
        }
    }
    
    private void nextAction()
    {
        if(currentActionIndex+1 >= actionChain.Count)
        {
            currentActionIndex = 0;
        }
        else
        {
            currentActionIndex++;
        }
        currentAction = actionChain[currentActionIndex];
        executeCurrentAction();
        currentActionTimer = currentAction.duration;
    }
    private void move(Vector2 direction, float speed)
    {
        transform.Translate(speed * direction * Time.fixedDeltaTime, Space.Self);
    }
}
