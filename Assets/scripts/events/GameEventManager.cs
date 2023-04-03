using UnityEngine;
using System.Collections;
using System;

public class GameEventManager : MonoBehaviour
{

    private static GameEventManager instance;
    private static GameObject container;

    private Action <bool> RewardExtraLives;

    public event Action <bool>OnRewardExtraLives
    {
        add{ RewardExtraLives += value;}
        remove{ RewardExtraLives -= value;}
    }

    private Action GameInitComplete;

    public event Action OnGameInitComplete
    {
        add{ GameInitComplete += value;}
        remove{ GameInitComplete -= value;}
    }

    private Action GameInitStart;

    public event Action OnGameInitStart
    {
        add{ GameInitStart += value;}
        remove{ GameInitStart -= value;}
    }

    private Action GameStartComplete;

    public event Action OnGameStartComplete
    {
        add{ GameStartComplete += value;}
        remove{ GameStartComplete -= value;}
    }

    private Action GameStart;

    public event Action OnGameStart
    {
        add{ GameStart += value;}
        remove{ GameStart -= value;}
    }

    private Action GamePausedStart;

    public event Action OnGamePausedStart
    {
        add{ GamePausedStart += value;}
        remove{ GamePausedStart -= value;}
    }

    private Action GamePausedComplete;

    public event Action OnGamePausedComplete
    {
        add{ GamePausedComplete += value;}
        remove{ GamePausedComplete -= value;}
    }

    private Action GameResumeStart;

    public event Action OnGameResumeStart
    {
        add{ GameResumeStart += value;}
        remove{ GameResumeStart -= value;}
    }

    private Action GameResumeComplete;

    public event Action OnGameResumeComplete
    {
        add{ GameResumeComplete += value;}
        remove{ GameResumeComplete -= value;}
    }

    private Action GameResumeCountdownStart;

    public event Action OnGameResumeCountdownStart
    {
        add{ GameResumeCountdownStart += value;}
        remove{ GameResumeCountdownStart -= value;}
    }

    private Action GameResumeCountdownComplete;

    public event Action OnGameResumeCountdownComplete
    {
        add{ GameResumeCountdownComplete += value;}
        remove{ GameResumeCountdownComplete -= value;}
    }

    private Action GameRestartStart;

    public event Action OnGameRestartStart
    {
        add{ GameRestartStart += value;}
        remove{ GameRestartStart -= value;}
    }

    private Action GameRestartComplete;

    public event Action OnGameRestartComplete
    {
        add{ GameRestartComplete += value;}
        remove{ GameRestartComplete -= value;}
    }

    private Action GameOverStart;

    public event Action OnGameOverStart
    {
        add{ GameOverStart += value;}
        remove{ GameOverStart -= value;}
    }

    private Action GameOverComplete;

    public event Action OnGameOverComplete
    {
        add{ GameOverComplete += value;}
        remove{ GameOverComplete -= value;}
    }

    private Action GameQuitStart;

    public event Action OnGameQuitStart
    {
        add{ GameQuitStart += value;}
        remove{ GameQuitStart -= value;}
    }

    private Action GameQuitComplete;

    public event Action OnGameQuitComplete
    {
        add{ GameQuitComplete += value;}
        remove{ GameQuitComplete -= value;}
    }

    private Action GameQuitApplicationStart;

    public event Action OnGameQuitApplicationStart
    {
        add{ GameQuitApplicationStart += value;}
        remove{ GameQuitApplicationStart -= value;}
    }

    private Action GameQuitApplicationComplete;

    public event Action OnGameQuitApplicationComplete
    {
        add{ GameQuitApplicationComplete += value;}
        remove{ GameQuitApplicationComplete -= value;}
    }


    public static GameEventManager GetInstance()
    {
        if (instance == null)
        {
            container = new GameObject();
            container.name = "GameEventManager";
            instance = container.AddComponent(typeof(GameEventManager)) as GameEventManager;
            DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    }

    public void DispatchGameInitComplete()
    {
        if (null != GameInitComplete)
        {
            GameInitComplete();
        }
    }

    public void DispatchGameStart()
    {
        if (null != GameStart)
        {
            GameStart();
        }
    }

    public void DispatchGameStartComplete()
    {
        if (null != GameStartComplete)
        {
            GameStartComplete();
        }
    }

    public void DispatchGamePausedStart()
    {
        if (null != GamePausedStart)
        {
            GamePausedStart();
        }
    }

    public void DispatchGamePausedComplete()
    {
        if (null != GamePausedComplete)
        {
            GamePausedComplete();
        }
    }

    public void DispatchGameResumeStart()
    {
        if (null != GameResumeStart)
        {
            GameResumeStart();
        }
    }

    public void DispatchGameResumeComplete()
    {
        if (null != GameResumeComplete)
        {
            GameResumeComplete();
        }
    }

    public void DispatchGameResumeCountdownComplete()
    {
        if (null != GameResumeCountdownComplete)
        {
            GameResumeCountdownComplete();
        }
    }

    public void DispatchGameResumeCountdownStart()
    {
        if (null != GameResumeCountdownStart)
        {
            GameResumeCountdownStart();
        }
    }

    public void DispatchGameRestartStart()
    {
        if (null != GameRestartStart)
        {
            GameRestartStart();
        }
    }

    public void DispatchGameRestartComplete()
    {
        if (null != GameRestartComplete)
        {
            GameRestartComplete();
        }
    }

    public void DispatchGameOverStart()
    {
        if (null != GameOverStart)
        {
            GameOverStart();
        }
    }

    public void DispatchGameOverComplete()
    {
        if (null != GameOverComplete)
        {
            GameOverComplete();
        }
    }

    public void DispatchGameQuitStart()
    {
        if (null != GameQuitStart)
        {
            GameQuitStart();
        }
    }

    public void DispatchGameQuitComplete()
    {
        if (null != GameQuitComplete)
        {
            GameQuitComplete();
        }
    }

    public void DispatchGameQuitApplicationStart()
    {
        if (null != GameQuitApplicationStart)
        {
            GameQuitApplicationStart();
        }
    }

    public void DispatchGameQuitApplicationComplete()
    {
        if (null != GameQuitApplicationComplete)
        {
            GameQuitApplicationComplete();
        }
    }

    public void DispatchRewardExtraLives(bool val)
    {
        if (null != RewardExtraLives)
        {
            RewardExtraLives(val);
        }
    }
}
