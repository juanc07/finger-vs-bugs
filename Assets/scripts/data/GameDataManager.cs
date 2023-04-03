using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameDataManager : MonoBehaviour
{

    private static GameDataManager instance;
    private static GameObject container;

    private GameState gameState;

    private Action <GameState> GameStateChange;

    public event Action <GameState>OnGameStateChange
    {
        add{ GameStateChange += value;}
        remove{ GameStateChange -= value;}
    }

    private int score;
    private int bestScore;
    private int kidBestScore;

    private int hp;
    private int maxHP;
    private bool isNewBestScore;

    private GameMode gameMode;

    private Action <GameMode> GameModeChange;

    public event Action <GameMode>OnGameModeChange
    {
        add{ GameModeChange += value;}
        remove{ GameModeChange -= value;}
    }

    private int combo;
    private int bestCombo;
    private int kidsBestCombo;

    private bool isVibrationOn;
    private int gameCount;
    private int totalGameCount;

    private int hasRate;
    private int dontShowRate;
    private int justShowRateUS;

    private int bugKill;
    private int totalBugKill;

    private int antQueenKill;
    private int antWarriorKill;
    private int antWorkerKill;
    private int spiderKill;
    private int smallSpiderKill;
    private int cockroachKill;

    private int duration;


    public static GameDataManager GetInstance()
    {
        if (instance == null)
        {
            container = new GameObject();
            container.name = "GameDataManager";
            instance = container.AddComponent(typeof(GameDataManager)) as GameDataManager;
            DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    }

    // sets the game states of the game
    public void SetGameState(GameState gameState)
    {
        this.gameState = gameState;

        if (null != GameStateChange)
        {
            GameStateChange(gameState);
        }
    }

    // gets the game state of the game
    public GameState GetGameState()
    {
        return this.gameState;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int val)
    {
        score = val;
    }

    public void UpdateScore(int val)
    {
        score += val;
    }

    public int GetCombo()
    {
        return combo;
    }

    public void SetCombo(int val)
    {
        combo = val;
    }

    public void UpdateCombo(int val)
    {
        combo += val;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void SetBestScore(int val)
    {
        bestScore = val;
    }

    public int GetKidsBestScore()
    {
        return kidBestScore;
    }

    public void SetKidsBestScore(int val)
    {
        kidBestScore = val;
    }

    public bool GetIsNewBestScore()
    {
        return isNewBestScore;
    }

    public void SetIsNewBestScore(bool val)
    {
        isNewBestScore = val;
    }

    public int GetHP()
    {
        return hp;
    }

    public void SetHP(int val)
    {
        hp = val;
    }

    public void UpdateHP(int val)
    {
        hp += val;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }

    public void SetMaxHP(int val)
    {
        maxHP = val;
    }

    public void SetGameMode(GameMode val)
    {
        gameMode = val;

        if (null != GameModeChange)
        {
            GameModeChange(gameMode);
        }
    }

    public GameMode GetGameMode()
    {
        return gameMode;
    }

    public bool GetVibration()
    {
        return isVibrationOn;
    }

    public void SetVibration(bool val)
    {
        isVibrationOn = val;
    }

    public int GetGameCount()
    {
        return gameCount;
    }

    public void SetGameCount(int val)
    {
        gameCount = val;
    }

    public void UpdateGameCount(int val)
    {
        gameCount += val;
    }

    public int GetTotalGameCount()
    {
        return totalGameCount;
    }

    public void SetTotalGameCount(int val)
    {
        totalGameCount = val;
    }

    public void UpdateTotalGameCount(int val)
    {
        totalGameCount += val;
    }

    public int GetHasRate()
    {
        return hasRate;
    }

    public void SetHasRate(int val)
    {
        hasRate = val;
    }

    public int GetDontShowRate()
    {
        return dontShowRate;
    }

    public void SetDontShowRate(int val)
    {
        dontShowRate = val;
    }

    public int GetJustShowRateUs()
    {
        return justShowRateUS;
    }

    public void SetJustShowRateUs(int val)
    {
        justShowRateUS = val;
    }

    public int GetBestCombo()
    {
        return bestCombo;
    }

    public void SetBestCombo(int val)
    {
        bestCombo = val;
    }

    public int GetKidsBestCombo()
    {
        return kidsBestCombo;
    }

    public void SetKidsBestCombo(int val)
    {
        kidsBestCombo = val;
    }

    public int GetBugKill()
    {
        return bugKill;
    }

    public void SetBugKill(int val)
    {
        bugKill = val;
    }

    public void UpdateBugKill(int val)
    {
        bugKill += val;
    }

    public int GetTotalBugKill()
    {
        return totalBugKill;
    }

    public void SetTotalBugKill(int val)
    {
        totalBugKill = val;
    }

    public void UpdateTotalBugKill(int val)
    {
        totalBugKill += val;
    }

    public int GetAntQueenKill()
    {
        return antQueenKill;
    }

    public void SetAntQueenKill(int val)
    {
        antQueenKill = val;
    }

    public void UpdateAntQueenKill(int val)
    {
        antQueenKill += val;
    }

    public int GetAntWarriorKill()
    {
        return antWarriorKill;
    }

    public void SetAntWarriorKill(int val)
    {
        antWarriorKill = val;
    }

    public void UpdateAntWarriorKill(int val)
    {
        antWarriorKill += val;
    }

    public int GetAntWorkerKill()
    {
        return antWorkerKill;
    }

    public void SetAntWorkerKill(int val)
    {
        antWorkerKill = val;
    }

    public void UpdateAntWorkerKill(int val)
    {
        antWorkerKill += val;
    }

    public int GetSpiderKill()
    {
        return spiderKill;
    }

    public void SetSpiderKill(int val)
    {
        spiderKill = val;
    }

    public void UpdateSpiderKill(int val)
    {
        spiderKill += val;
    }

    public int GetSmallSpiderKill()
    {
        return smallSpiderKill;
    }

    public void SetSmallSpiderKill(int val)
    {
        smallSpiderKill = val;
    }

    public void UpdateSmallSpiderKill(int val)
    {
        smallSpiderKill += val;
    }

    public int GetCockroachKill()
    {
        return cockroachKill;
    }

    public void SetCockroachKill(int val)
    {
        cockroachKill = val;
    }

    public void UpdateCockroachKill(int val)
    {
        cockroachKill += val;
    }

    public int GetDuration()
    {
        return duration;
    }

    public void SetDuration(int val)
    {
        duration = val;
    }
}
