using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private const string TAG = "[Game]: ";
    private GameState gameState;
    // ant events
    private BugEventManager antEventManager;

    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

    }

    public GameState GetGameState()
    {
        return this.gameState;
    }

    public void InitGame()
    {
        // the ant event manager for the ants
        antEventManager = BugEventManager.GetInstance();
    }

    private void AddEventListener()
    {
        antEventManager.OnBugStatusChange += OnAntStatusChange;
    }

    private void RemoveEventListener()
    {
        antEventManager.OnBugStatusChange += OnAntStatusChange;
    }

    public void StartGame()
    {
        
    }

    public void PausedGame()
    {
        
    }

    public void StartResumeCountdown()
    {
        
    }

    public void ResumeGame()
    {
        
    }

    public void RestartGame()
    {
        
    }

    public void GameOver()
    {
        
    }

    public void QuitGame()
    {
        
    }

    private void OnAntStatusChange(BugEvent antEvent, BugController antController)
    {
        if (antEvent == BugEvent.DIED)
        {
            //AntDied(antController);
        }
        else if (antEvent == BugEvent.HIT)
        {
            //AntHit(antController);
        }
        else if (antEvent == BugEvent.ESCAPE)
        {
            //AntEscape(antController);
        }
        else if (antEvent == BugEvent.SPAWN)
        {
            //AntSpawn(antController);
        }
    }
}
