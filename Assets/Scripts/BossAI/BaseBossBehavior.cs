using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossBehavior : MonoBehaviour
{
    [System.Serializable]
    public class BossAction
    {
        public List<CirclePattern> bulletPatterns;
        public GameObject spawnPrefab;
        public Transform spawnPoint;
        public float loops = 1;
        public virtual IEnumerator execute()
        {
            float loopTime = loops;
            while(loopTime > 0)
            {
                if (spawnPrefab != null)
                {
                    GameObject obj = Instantiate(spawnPrefab);
                    obj.transform.position = spawnPoint.position;
                    obj.TryGetComponent<MineController>(out MineController mine);
                    if (mine != null)
                    {
                        mine.active = false;
                        //Find player obj
                        GameObject player = GameObject.Find("Player");
                        //Calculate relative position between mine and player
                       
                        Vector2 dir = obj.transform.position - player.transform.position;
                        //obj.GetComponent<Rigidbody2D>().AddForce(-dir.normalized*2f, ForceMode2D.Force);
                        mine.SlideInDirection(-dir.normalized, 1f);
                    }
                }
                List<Coroutine> activePatterns = new List<Coroutine>();
                if (bulletPatterns != null)
                {
                    foreach(CirclePattern pattern in bulletPatterns)
                    {
                        Coroutine c = pattern.FireFromObject();
                        activePatterns.Add(c);
                    }
                    foreach(Coroutine activePattern in activePatterns)
                    {
                        yield return activePattern;
                    }
                }
                loopTime--;
                yield return new WaitForSeconds(0.5f);
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
    [HideInInspector]
    public int currentActionChainIndex = 0;
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

    public void nextActionChain()
    {
        Debug.Log("Next chain: " + currentActionChainIndex + 1);
        //stop all coroutines in the previous action chain
        StopCoroutine(executeActions());
        currentActionChainIndex++;
        currentActionChain = actionChains[currentActionChainIndex].chain;
        currentActionIndex = 0;
        executeCurrentAction();
    }

 
}
