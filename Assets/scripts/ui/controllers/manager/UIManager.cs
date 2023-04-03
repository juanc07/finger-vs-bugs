using UnityEngine;
using System.Collections;
using System;

public class UIManager : MonoBehaviour
{
    public HPBarController hpController;
    public UIScoreController uiScoreController;
    public UIComboController uiComboController;
    public DamageEffectController damageEffectController;
    public UIPausedButtonController uiPausedButtonController;
    public UICountdownController uiCountdownController;

    public RectTransform uiPanel;
    private UIEventManager uiEventManager;
    // bug events
    private BugEventManager bugEventManager;
    private GameEventManager gameEventManager;
    private GameState gameState;
    private GameDataManager gameDataManager;

    private void AddEventListener()
    {
        gameDataManager.OnGameStateChange += OnGameStateChange;
        bugEventManager.OnBugStatusChange += OnBugStatusChange;
        
        if (hpController != null)
        {
            hpController.OnHPBarValueChange += OnHPBarValueChange;
        }
        if (uiCountdownController != null)
        {
            uiCountdownController.OnCountDownComplete += OnCountDownComplete;
        }
    }

    private void RemoveEventListener()
    {
        gameDataManager.OnGameStateChange -= OnGameStateChange;
        bugEventManager.OnBugStatusChange -= OnBugStatusChange;

        if (hpController != null)
        {
            hpController.OnHPBarValueChange -= OnHPBarValueChange;
        }

        if (uiCountdownController != null)
        {
            uiCountdownController.OnCountDownComplete -= OnCountDownComplete;
        }
    }

    private void Awake()
    {
        gameDataManager = GameDataManager.GetInstance();
        gameEventManager = GameEventManager.GetInstance();
        bugEventManager = BugEventManager.GetInstance();
        uiEventManager = UIEventManager.GetInstance();
    }

    // Use this for initialization
    void Start()
    {
        AddEventListener();
        ShowHideUI(false);
    }

    private void OnDestroy()
    {
        RemoveEventListener();
    }

    public void ShowHideUI(bool val)
    {
        if (hpController != null)
        {
            hpController.gameObject.SetActive(val);			
        }

        if (uiScoreController != null)
        {
            uiScoreController.gameObject.SetActive(val);			
        }

        if (uiPausedButtonController != null)
        {
            uiPausedButtonController.gameObject.SetActive(val);
        }

        if (!val)
        {
            if (uiComboController != null)
            {
                uiComboController.gameObject.SetActive(false);	
            }	

            if (uiCountdownController != null)
            {
                uiCountdownController.gameObject.SetActive(false);
            }
        }
    }

    public void SetHPBar(int val, int max)
    {
        hpController.SetValue(val, max);
    }

    public void UpdateHPBar(int val)
    {
        hpController.UpdateValue(val);

        if (damageEffectController != null)
        {
            damageEffectController.ActivateDamage();
        }
    }

    public void ResetHPBar()
    {	
		
        if (hpController != null)
        {
            hpController.Reset();

            if (damageEffectController != null)
            {
                damageEffectController.ResetDamage();	
            }
        }
    }

    private void OnHPBarValueChange(float val)
    {	
        uiEventManager.DispatchUpdatePlayerHP(val);
    }

    private void OnCountDownComplete()
    {
        uiCountdownController.gameObject.SetActive(false);
        uiEventManager.DispatchResumeCountDownComplete();
        Debug.Log("OnCountDownComplete outside");
    }

    public void SetScore(int val)
    {
        uiScoreController.SetScore(val);
    }

    public void SetCombo(int val)
    {
        if (val <= 0)
        {
            uiComboController.gameObject.SetActive(false);
        }
        else
        {
            uiComboController.gameObject.SetActive(true);
            uiComboController.SetCombo(val);
        }

        uiEventManager.DispatchUpdateCombo(val);
    }

    public void ShowHideBleedCameraEffect(bool val)
    {
        if (damageEffectController != null)
        {			
            damageEffectController.ShowHide(val);
        }
    }

    public void StartResumeCountDown()
    {
        uiCountdownController.gameObject.SetActive(true);
        uiCountdownController.StartCountDown();
    }

    private void OnGameStateChange(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void OnBugStatusChange(BugEvent antEvent, BugController antController)
    {
        if (antEvent == BugEvent.ESCAPE)
        {
            AntEscape(antController);
        }
    }

    private void AntEscape(BugController antController)
    {
        if (this.gameState == GameState.PLAY)
        {
            if (antController.bugType == BugType.Worker)
            {
                UpdateHPBar(GameConfig.ANT_WORKER_LIFE_DEDUCTION);
            }
            else if (antController.bugType == BugType.Warrior)
            {
                UpdateHPBar(GameConfig.ANT_WARRIOR_LIFE_DEDUCTION);
            }
            else if (antController.bugType == BugType.Queen)
            {
                UpdateHPBar(GameConfig.ANT_QUEEN_LIFE_DEDUCTION);
            }
            else if (antController.bugType == BugType.Spider)
            {
                UpdateHPBar(GameConfig.SPIDER_LIFE_DEDUCTION);
            }
            else if (antController.bugType == BugType.SmallSpider)
            {
                UpdateHPBar(GameConfig.SMALL_SPIDER_LIFE_DEDUCTION);
            }
            else if (antController.bugType == BugType.Cockroach)
            {
                UpdateHPBar(GameConfig.COCKROACH_LIFE_DEDUCTION);
            }
        }
    }
}
