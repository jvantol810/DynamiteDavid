using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossBehavior : MonoBehaviour
{
    [System.Serializable]
    public class BossAction
    {
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
    public float actionDelay = 1f;
    [System.Serializable]
    public class ActionChain
    {
        public bool random = false;
        public List<BossAction> chain;
    }
    [SerializeField]
    public List<ActionChain> actionChains;
    private List<BossAction> currentActionChain;
    private int currentActionChainIndex = 0;
    public List<BossPath> pathChain;
    private int currentPathIndex;
    private int currentActionIndex = 0;
    private BossAction currentAction;
    private void Start()
    {
        currentActionIndex = 0;
        currentActionChain = actionChains[currentActionChainIndex].chain;
        currentAction = currentActionChain[currentActionIndex];
        //executeCurrentAction();
        StartCoroutine(executeActions());
        foreach(BossPath path in pathChain)
        {
            path.enabled = false;
        }
        pathChain[currentPathIndex].enabled = true;
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    private void executeCurrentAction()
    {
        StartCoroutine(currentAction.execute());
    }

    public IEnumerator executeActions()
    {
        while (true)
        {
            if (actionChains[currentActionChainIndex].random)
            {
                int actionIndex = Random.Range(0, currentActionChain.Count);
                yield return new WaitForSeconds(actionDelay);
                yield return currentActionChain[actionIndex].execute();
            }
            else
            {
                yield return new WaitForSeconds(actionDelay);
                yield return currentActionChain[currentActionIndex].execute();
                nextAction();
            }
            
            
            
            //foreach (BossAction action in actionChain)
            //{
            //    yield return new WaitForSeconds(actionDelay);
            //    yield return action.execute();
            //}
        }
    }
    
    private void nextAction()
    {
        if(currentActionIndex+1 >= currentActionChain.Count)
        {
            currentActionIndex = 0;
        }
        else
        {
            currentActionIndex++;
        }
        currentAction = currentActionChain[currentActionIndex];
    }

    private void nextActionChain()
    {
        currentActionChainIndex++;
        currentActionChain = actionChains[currentActionChainIndex].chain;
        currentActionIndex = 0;
        executeCurrentAction();
    }

 
}
