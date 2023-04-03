using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

public class BugSpawner : MonoBehaviour,ICleanable
{

    private BugEventManager antEventManager;

    public GameObject antWorkerPrefab;
    public GameObject antWarriorPrefab;
    public GameObject antQueenPrefab;
    public GameObject spiderPrefab;
    public GameObject smallSpiderPrefab;
    public GameObject cockroachPrefab;

    public GameObject antHolder;

    public Vector3 spawnPoint;
    public int minPositionX;
    public int maxPositionX;

    public int minPositionZ;
    public int maxPositionZ;

    public float initialSpawnRate;
    public float spawnRate;
    public int maxCount;
    public int minCount;

    public Vector3 rotation;

    public bool enableAntWorker;
    public bool enableAntWarrior;
    public bool enableAntQueen;
    public bool enableSpider;
    public bool enableSmallSpider;
    public bool enableCockroach;
    public BugType forceAntType;

    public int antWorkerDied;
    public int antWarriorDied;
    public int antQueenDied;
    public int antLarvaeDied;
    public int antEggDied;

    public int spiderDied;
    public int smallSpiderDied;
    public int cockroachDied;

    public List<GameObject> antCollection = new List<GameObject>();
    //private IData dataService;
    private GameDataManager gameDataManager;

    private void Awake()
    {
        gameDataManager = GameDataManager.GetInstance();
        antEventManager = BugEventManager.GetInstance();
    }

    // Use this for initialization
    void Start()
    {
        AddEventListener();
        //ShowRandomNumbers(1);
        /*TestRandom(1);
		TestRandom(7);
		TestRandom(5);
		TestRandom(2);
		TestRandom(3);
		TestRandom(10);
		TestRandom(4);
		TestRandom(6);
		TestRandom(9);
		TestRandom(8);*/

        /*int state =  TestRandom2(1);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);
		state =  TestRandom2(state);*/

        //spawn initial ants to avoid boring moment at the beginning
        //Spawn(AntType.Worker);
        //Spawn(AntType.Worker);
        //Spawn(AntType.Worker);

        //Invoke( "StartDelaySpawn",initialSpawnRate);
    }

    private void AddEventListener()
    {
        if (antEventManager != null)
        {
            antEventManager.OnBugStatusChange += OnBugStatusChange;
        }
    }

    private void RemoveEventListener()
    {
        if (antEventManager != null)
        {
            antEventManager.OnBugStatusChange -= OnBugStatusChange;
        }
    }

    public void DeActivate()
    {		
        SetAllowToMove(false);
    }

    public void Activate()
    {		
        SetAllowToMove(true);
    }

    public void StartDelaySpawn()
    {
        StartCoroutine(DelaySpawnLoop(spawnRate));
    }

    public void StopSpawn()
    {
        StopAllCoroutines();
    }

    private IEnumerator DelaySpawnLoop(float waitTime)
    {		
        yield return new WaitForSeconds(waitTime);
        Spawn(BugType.Worker, 0, 0);
        StartCoroutine(DelaySpawnLoop(spawnRate));
    }

    public IEnumerator DelaySpawn(float waitTime, BugType antType, float xPos, float zPos)
    {
        yield return new WaitForSeconds(waitTime);
        Spawn(antType, xPos, zPos);
    }

    public void SpawnGroup(BugType type, float xPos, float zPos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Spawn(type, xPos, zPos);
        }
    }

    public void DelaySpawnGroup(float waitTime, BugType type, float xPos, float zPos, int count, int offsetX = 0, int offsetZ = 0)
    {
        for (int i = 0; i < count; i++)
        {			
            StartCoroutine(DelaySpawn(waitTime * i, type, xPos + (offsetX * i), zPos + (offsetZ * i)));
        }
    }

    public void DelaySpawnGroupNoRandomXZ(float waitTime, BugType type, float xPos, float zPos, int count)
    {
        for (int i = 0; i < count; i++)
        {			
            StartCoroutine(DelaySpawn(waitTime * i, type, xPos, zPos));
        }
    }

    public void PreSpawn()
    {        
        for (int i = 0; i < 20; i++)
        {
            Spawn(BugType.Worker, 0, 0, true);
        }

        for (int i = 0; i < 4; i++)
        {
            Spawn(BugType.Warrior, 0, 0, true);
        }

        for (int i = 0; i < 2; i++)
        {
            Spawn(BugType.Queen, 0, 0, true);
        }

        for (int i = 0; i < 2; i++)
        {
            Spawn(BugType.Spider, 0, 0, true);
        }

        for (int i = 0; i < 10; i++)
        {
            Spawn(BugType.SmallSpider, 0, 0, true);
        }

        for (int i = 0; i < 10; i++)
        {
            Spawn(BugType.Cockroach, 0, 0, true);
        }
    }

    public void Spawn(BugType antType, float xPos, float zPos, bool preSpawn = false)
    {
		
        if (antType == BugType.Worker && !enableAntWorker)
        {
            return;
        }
        else if (antType == BugType.Warrior && !enableAntWarrior)
        {
            return;
        }
        else if (antType == BugType.Queen && !enableAntQueen)
        {
            return;
        }
        else if (antType == BugType.Spider && !enableSpider)
        {
            return;
        }
        else if (antType == BugType.SmallSpider && !enableSmallSpider)
        {
            return;
        }
        else if (antType == BugType.Cockroach && !enableCockroach)
        {
            return;
        }

        if (forceAntType != BugType.None)
        {
            antType = forceAntType;
        }

        BugController antController = null;

        if (xPos == 0)
        {
            float rndX = UnityEngine.Random.Range(minPositionX, maxPositionX);
            spawnPoint.x = rndX;
        }
        else
        {
            spawnPoint.x = xPos;
        }

        if (zPos == 0)
        {
            float rndZ = UnityEngine.Random.Range(minPositionZ, maxPositionZ);
            spawnPoint.z = rndZ;
        }
        else
        {
            spawnPoint.z = zPos;
        }

        if (!preSpawn)
        {
            antController = GetNotActiveAnt(antType);
        }

        if (antController != null)
        {
            SetAntStats(antController);
            antController.Activate(spawnPoint.x, spawnPoint.z);
            //Debug.Log("Reuse Ant type " + antType.ToString());
        }
        else
        {
            if (antCollection.Count < maxCount)
            {

                GameObject currentPrefab = GetPrefab(antType);

                GameObject ant = Instantiate(currentPrefab, spawnPoint, Quaternion.Euler(new Vector3(rotation.x, rotation.y, rotation.z))) as GameObject;
                ant.transform.SetParent(antHolder.transform);
                antController = ant.gameObject.GetComponent<BugController>();

                if (antController != null)
                {					
                    SetAntStats(antController);
                }

                antController.setID(antCollection.Count);
                antCollection.Add(ant);	
                if (preSpawn)
                {					
                    antController.DeActivate(true);
                }
            }
        }
    }

    private void SetAntStats(BugController antController)
    {
		
        //checks for the game mode
        GameMode gameMode = gameDataManager.GetGameMode();

        /*if(antController.gameMode == gameMode){
			return;	
		}*/

        if (gameMode == GameMode.HARD)
        {			
            antController.gameMode = GameMode.HARD;

            if (antController.bugType == BugType.Worker)
            {
                antController.fullHP = GameConfig.CLASSIC_ANT_WORKER_HP;	
                antController.moveSpeed = GameConfig.CLASSIC_ANT_WORKER_SPEED;
            }
            else if (antController.bugType == BugType.Warrior)
            {
                antController.fullHP = GameConfig.CLASSIC_ANT_WARRIOR_HP;	
                antController.moveSpeed = GameConfig.CLASSIC_ANT_WARRIOR_SPEED;
            }
            else if (antController.bugType == BugType.Queen)
            {
                antController.fullHP = GameConfig.CLASSIC_ANT_QUEEN_HP;	
                antController.moveSpeed = GameConfig.CLASSIC_ANT_QUEEN_SPEED;
            }
            else if (antController.bugType == BugType.Spider)
            {
                antController.fullHP = GameConfig.CLASSIC_SPIDER_HP;
                antController.moveSpeed = GameConfig.CLASSIC_SPIDER_SPEED;
            }
            else if (antController.bugType == BugType.SmallSpider)
            {				
                antController.fullHP = GameConfig.CLASSIC_SMALL_SPIDER_HP;
                antController.moveSpeed = GameConfig.CLASSIC_SMALL_SPIDER_SPEED;
            }
            else if (antController.bugType == BugType.Cockroach)
            {				
                antController.fullHP = GameConfig.CLASSIC_COCKROACH_HP;
                antController.moveSpeed = GameConfig.CLASSIC_COCKROACH_SPEED;
            }

        }
        else if (gameMode == GameMode.EASY)
        {
            antController.gameMode = GameMode.EASY;

            if (antController.bugType == BugType.Worker)
            {
                antController.fullHP = GameConfig.KIDS_ANT_WORKER_HP;	
                antController.moveSpeed = GameConfig.KIDS_ANT_WORKER_SPEED;
            }
            else if (antController.bugType == BugType.Warrior)
            {
                antController.fullHP = GameConfig.KIDS_ANT_WARRIOR_HP;	
                antController.moveSpeed = GameConfig.KIDS_ANT_WARRIOR_SPEED;
            }
            else if (antController.bugType == BugType.Queen)
            {
                antController.fullHP = GameConfig.KIDS_ANT_QUEEN_HP;	
                antController.moveSpeed = GameConfig.KIDS_ANT_QUEEN_SPEED;
            }
            else if (antController.bugType == BugType.Spider)
            {
                antController.fullHP = GameConfig.KIDS_SPIDER_HP;
                antController.moveSpeed = GameConfig.KIDS_SPIDER_SPEED;
            }
            else if (antController.bugType == BugType.SmallSpider)
            {				
                antController.fullHP = GameConfig.KIDS_SMALL_SPIDER_HP;
                antController.moveSpeed = GameConfig.KIDS_SMALL_SPIDER_SPEED;
            }
            else if (antController.bugType == BugType.Cockroach)
            {				
                antController.fullHP = GameConfig.KIDS_COCKROACH_HP;
                antController.moveSpeed = GameConfig.KIDS_COCKROACH_SPEED;
            }
        }
    }

    private GameObject GetPrefab(BugType antType)
    {
        if (antType == BugType.Worker)
        {
            return antWorkerPrefab;
        }
        else if (antType == BugType.Warrior)
        {
            return antWarriorPrefab;
        }
        else if (antType == BugType.Queen)
        {
            return antQueenPrefab;
        }
        else if (antType == BugType.Spider)
        {
            return spiderPrefab;
        }
        else if (antType == BugType.SmallSpider)
        {
            return smallSpiderPrefab;
        }
        else if (antType == BugType.Cockroach)
        {
            return cockroachPrefab;
        }
        else
        {
            return antWorkerPrefab;
        }
    }

    private BugController GetNotActiveAnt(BugType antType)
    {
        int len = antCollection.Count;
        BugController found = null;

        for (int i = 0; i < len; i++)
        {			
            BugController antController = antCollection[i].gameObject.GetComponent<BugController>();
            if (antController != null)
            {
                if (antController.isInPool && antController.bugType == antType)
                {
                    found = antController;
                    break;
                }
            }
        }

        return found;
    }

    // only use this when you want to remove and clear all ants and their listener
    private void DestroyAllAnts()
    {
        RemoveEventListener();

        int len = antCollection.Count;
        for (int i = 0; i < len; i++)
        {
            if (antCollection[i] != null)
            {
                BugController antController = antCollection[i].gameObject.GetComponent<BugController>();
                if (antController != null)
                {					
                    Destroy(antCollection[i].gameObject);
                }
            }
        }

        antCollection.Clear();
    }

    private void DeactivateAll()
    {
        if (antCollection != null)
        {
            int len = antCollection.Count;
            for (int i = 0; i < len; i++)
            {
                if (antCollection[i] != null)
                {
                    BugController antController = antCollection[i].gameObject.GetComponent<BugController>();
                    if (antController != null)
                    {
                        antController.ResetAllowToMove();
                        antController.DeActivate(true);
                    }
                }
            }
        }
    }

    private void SetAllowToMove(bool val)
    {
        if (antCollection != null)
        {
            int len = antCollection.Count;
            for (int i = 0; i < len; i++)
            {
                if (antCollection[i] != null)
                {
                    BugController antController = antCollection[i].gameObject.GetComponent<BugController>();
                    if (antController != null)
                    {
                        antController.SetAllowToMove(val);
                    }
                }
            }	
        }
    }

    public void KnockBackAll(float val)
    {
        if (antCollection != null)
        {
            int len = antCollection.Count;
            for (int i = 0; i < len; i++)
            {
                if (antCollection[i] != null)
                {
                    BugController antController = antCollection[i].gameObject.GetComponent<BugController>();
                    if (antController != null)
                    {
                        antController.KnockBack(val);
                    }
                }
            }
        }
    }

    private void ResetAllKills()
    {
        antWorkerDied = 0;
        antWarriorDied = 0;
        antQueenDied = 0;
        antLarvaeDied = 0;
        antEggDied = 0;
        spiderDied = 0;
        smallSpiderDied = 0;
        cockroachDied = 0;
    }

    public void Clean()
    {
        ResetAllKills();
        DeactivateAll();
    }

    public void TestRandom(int seed)
    {
        int random = (7 * seed) % 11;
        Debug.Log("seed: " + seed + " random: " + random);
    }

    public int TestRandom2(int seed)
    {
        int j = (7 * seed) % 101;
        int random = ((j - 1) % 10) + 1;
        Debug.Log("seed: " + seed + " state: " + j + " random: " + random);
        return j;
    }

    // use this for generating ants spawns
    private void ShowRandomNumbers(int seed)
    {
        System.Random rnd = new System.Random(seed);
        for (int ctr = 0; ctr <= 20; ctr++)
        {			
            //Debug.Log(rnd.NextDouble());
            Debug.Log(rnd.Next());
        }
			
    }

    private void OnBugStatusChange(BugEvent antEvent, BugController antController)
    {
        //checks for the game mode
        GameMode gameMode = gameDataManager.GetGameMode();

        if (antEvent == BugEvent.DIED)
        {
            if (antController.bugType == BugType.Worker)
            {
                antWorkerDied++;
                if (antWorkerDied % 7 == 0)
                {
                    Spawn(BugType.Warrior, 0, 44f);
                }

                if (antWorkerDied % 115 == 0)
                {
                    Spawn(BugType.Spider, 0, 52f);
                }

                if (antWorkerDied % 170 == 0)
                {
                    Spawn(BugType.Cockroach, 0, 49f);
                }
            }

            if (antController.bugType == BugType.Warrior)
            {
                antWarriorDied++;
                if (antWarriorDied % 2 == 0)
                {
                    SpawnGroup(BugType.Worker, 0, 0, 3);
                }
                else if (antWarriorDied % 5 == 0)
                {
                    Spawn(BugType.Queen, 0, 54f);
                }
            }

            if (antController.bugType == BugType.Queen)
            {
                antQueenDied++;
                SpawnGroup(BugType.Worker, 0, 0, 2);
                DelaySpawnGroup(0.5f, BugType.Warrior, 0, 44f, 2, 0, 15);
                DelaySpawnGroup(0.5f, BugType.Worker, 0, 41f, 2, 0, 20);
            }

            if (antController.bugType == BugType.Spider)
            {
                spiderDied++;

                if (spiderDied % 3 == 0)
                {
                    Spawn(BugType.Spider, 0, 52f);
                }

                DelaySpawnGroupNoRandomXZ(
                    0.55f,
                    BugType.SmallSpider,
                    antController.gameObject.transform.position.x,
                    antController.gameObject.transform.position.z,
                    4
                );
            }

            if (antController.bugType == BugType.SmallSpider)
            {
                smallSpiderDied++;

                if (smallSpiderDied % 16 == 0)
                {
                    Spawn(BugType.Spider, 0, 52f);
                }
            }

            if (antController.bugType == BugType.Cockroach)
            {
                cockroachDied++;

                if (cockroachDied % 2 == 0)
                {
                    Spawn(BugType.Cockroach, 0, 0);
                }
            }

        }
        else if (antEvent == BugEvent.HIT)
        {
            if (antController.bugType == BugType.Queen)
            {

                if (gameMode == GameMode.HARD)
                {
                    if (antController.hp % 7 == 0)
                    {
                        Spawn(BugType.Worker, 0, 42f);
                    }
                }
                else if (gameMode == GameMode.EASY)
                {	
                    if (antController.hp % 5 == 0)
                    {
                        Spawn(BugType.Worker, 0, 42f);
                    }
                }

            }
        }
        else if (antEvent == BugEvent.ESCAPE)
        {
			
        }
    }
}
