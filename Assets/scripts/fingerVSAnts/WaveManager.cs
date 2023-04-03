using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{

    public float tick;
    private BugSpawner antSpawner;
    private Dictionary<float,WaveObject> classicWaveObjectCollection = new Dictionary<float,WaveObject>();
    private Dictionary<float,WaveObject> kidsWaveObjectCollection = new Dictionary<float,WaveObject>();
    private Dictionary<float,WaveObject> currentWaveObjectCollection = new Dictionary<float,WaveObject>();
    public bool isActivated;

    //private IData dataService;
    private GameDataManager gameDataManager;

    private void Awake()
    {
        gameDataManager = GameDataManager.GetInstance();
        antSpawner = this.gameObject.GetComponent<BugSpawner>();
    }

    public void PopulateWave()
    {
        PopulateClassicWave();
        PopulateKidsWave();
    }

    private void PopulateClassicWave()
    {
        for (float i = 0; i < 60; i += 0.5f)
        {
            //AddWaveObject( i, AntType.Worker, 0, 40f + ( i * 20f ) );
            AddWaveObject(classicWaveObjectCollection, i, BugType.Worker, 0, 38f);
        }


        for (float j = 61; j < 100; j += 1f)
        {
            //AddWaveObject( i, AntType.Worker, 0, 40f + ( i * 20f ) );
            AddWaveObject(classicWaveObjectCollection, j, BugType.Worker, 0, 0);
        }

        for (float k = 101; k < 10000; k += 0.5f)
        {
            //AddWaveObject( i, AntType.Worker, 0, 40f + ( i * 20f ) );
            AddWaveObject(classicWaveObjectCollection, k, BugType.Worker, 0, 0);
        }
    }

    private void PopulateKidsWave()
    {
        for (float i = 0; i < 10000; i += 0.75f)
        {
            AddWaveObject(kidsWaveObjectCollection, i, BugType.Worker, 0, 38f);
        }
    }
	
    // Update is called once per frame
    void Update()
    {
        // check if activated if yes continue per tick
        if (isActivated)
        {
            tick += Time.fixedDeltaTime;
            tick = (float)System.Math.Round(tick, 2);
            PlayWaveObject(tick);
            //Debug.Log( " tick " + tick );	
        }
    }

    public void UpdateWaveModeData()
    {
        //checks for the game mode
        GameMode gameMode = gameDataManager.GetGameMode();
        if (gameMode == GameMode.HARD)
        {
            currentWaveObjectCollection = classicWaveObjectCollection;
        }
        else if (gameMode == GameMode.EASY)
        {
            currentWaveObjectCollection = kidsWaveObjectCollection;
        }
    }

    public void Activate()
    {
        isActivated = true;
    }

    public void DeActivate()
    {
        isActivated = false;
    }

    private void AddWaveObject(Dictionary<float,WaveObject> waveObjectCollection, float time, BugType antType, float xPos, float zPos)
    {
        WaveObject waveObject = new WaveObject();
        waveObject.time = time;
        waveObject.bugType = antType;
        waveObject.xPos = xPos;
        waveObject.zPos = zPos;
        waveObjectCollection.Add(time, waveObject);
    }

    public void ClearWaveObject()
    {
        currentWaveObjectCollection.Clear();
    }

    public void ResetWaveObject()
    {
        ResetTick();
        foreach (KeyValuePair<float, WaveObject> pair in currentWaveObjectCollection)
        {
            WaveObject waveObject = pair.Value;
            waveObject.isComplete = false;
        }
    }

    private void ResetTick()
    {
        tick = 0;
    }

    public void ClearData()
    {
        classicWaveObjectCollection.Clear();
        kidsWaveObjectCollection.Clear();
    }

    private void PlayWaveObject(float time)
    {
        if (currentWaveObjectCollection.ContainsKey(time))
        {
            WaveObject waveObject = currentWaveObjectCollection[time];
            if (antSpawner != null && waveObject != null)
            {
                if (!waveObject.isComplete)
                {
                    waveObject.isComplete = true;
                    antSpawner.Spawn(waveObject.bugType, waveObject.xPos, waveObject.zPos);
                }
            }
        }

        if (tick >= (currentWaveObjectCollection.Count - (currentWaveObjectCollection.Count * 0.75f)))
        {
            Debug.Log(" no more data ");
            DeActivate();
            ResetWaveObject();
            Activate();			
        }
    }
}
