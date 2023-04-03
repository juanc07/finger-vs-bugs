using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIPanelController : MonoBehaviour
{
    public RectTransform uiPanel;

    public MainMenuPanelController mainMenuPanelController;
    public GameModePanelController gameModePanelController;
    public GameOverPanelController gameOverPanelController;
    public OptionPanelController optionPanelController;
    public QuitPanelController quitPanelController;
    public PausedPanelController pausedPanelController;
    public UIPausedButtonController uiPausedButtonController;
    public RewardVideoPanelController rewardVideoPanelController;

    private List<BasePanelController> panelCollection = new List<BasePanelController>();
    private UIPanelType activePanel;
    private UIEventManager uiEventManager;

    private GameDataManager gameDataManager;
    private GameEventManager gameEventManager;
    private GameState gameState;

    private void Awake()
    {
        gameDataManager = GameDataManager.GetInstance();
        gameEventManager = GameEventManager.GetInstance();
        uiEventManager = UIEventManager.GetInstance();

        // set default active panel
        SetActivePanel(UIPanelType.MAIN_MENU);
        AddEventListener();
    }

    // Use this for initialization
    void Start()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        // press back or escape
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.OSXEditor ||
            Application.platform == RuntimePlatform.WindowsEditor ||
            Application.platform == RuntimePlatform.LinuxEditor ||
            Application.platform == RuntimePlatform.WebGLPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PressBack();
            }
        }
    }

    private void OnDestroy()
    {
        RemoveEventListener();
    }

    private void AddEventListener()
    {        
        gameDataManager.OnGameStateChange += OnGameStateChange;
        uiEventManager.OnClickButton += OnClickButton;
        //Debug.Log("UIPanelController: AddEventListener");
    }

    private void RemoveEventListener()
    {
        gameDataManager.OnGameStateChange -= OnGameStateChange;
        uiEventManager.OnClickButton -= OnClickButton;
    }

    public void OnOffRaycast(bool val)
    {
        uiPanel.GetComponent<Image>().raycastTarget = val;
    }

    private void SetActivePanel(UIPanelType panelType)
    {
        activePanel = panelType;
    }

    private UIPanelType GetActivePanel()
    {
        return activePanel;
    }

    public void ShowHideGameOverPanel(bool val)
    {
        if (val)
        {
            SetActivePanel(UIPanelType.GAME_OVER);
            gameDataManager.SetGameState(GameState.GAME_OVER);
            uiEventManager.DispatchChangeUIPanel(UIPanelType.GAME_OVER);
        }

        if (gameOverPanelController != null)
        {
            gameOverPanelController.gameObject.SetActive(val);
            gameOverPanelController.UpdateInfo();
        }
    }

    public void ShowHideGameModePanel(bool val)
    {
        if (val)
        {
            SetActivePanel(UIPanelType.GAME_MODE_MENU);
            gameDataManager.SetGameState(GameState.GAME_MODE_MENU);
            uiEventManager.DispatchChangeUIPanel(UIPanelType.GAME_MODE_MENU);
        }

        if (gameModePanelController != null)
        {
            gameModePanelController.gameObject.SetActive(val);
        }
    }

    public void ShowHideOptionPanel(bool val)
    {
        if (val)
        {            
            SetActivePanel(UIPanelType.OPTION);
            gameDataManager.SetGameState(GameState.OPTIONS);
            uiEventManager.DispatchChangeUIPanel(UIPanelType.OPTION);
        }

        if (optionPanelController != null)
        {
            optionPanelController.gameObject.SetActive(val);
        }
    }

    public void ShowHideMainMenuPanel(bool val)
    {
        if (val)
        {           
            SetActivePanel(UIPanelType.MAIN_MENU);
            gameDataManager.SetGameState(GameState.MAIN_MENU);
            uiEventManager.DispatchChangeUIPanel(UIPanelType.MAIN_MENU);
        }

        if (mainMenuPanelController != null)
        {
            mainMenuPanelController.gameObject.SetActive(val);
        }
    }

    public void ShowHideQuitPanel(bool val)
    {
        if (val)
        { 
            SetActivePanel(UIPanelType.QUIT_MENU);
            gameDataManager.SetGameState(GameState.QUIT);
            uiEventManager.DispatchShowInterstitialAd();
            uiEventManager.DispatchChangeUIPanel(UIPanelType.QUIT_MENU);
        }

        if (quitPanelController != null)
        {
            quitPanelController.gameObject.SetActive(val);
        }
    }

    public void ShowHidePausedPanel(bool val)
    {
        if (val)
        {  
            SetActivePanel(UIPanelType.PAUSED);
            gameDataManager.SetGameState(GameState.PAUSED);
            uiEventManager.DispatchChangeUIPanel(UIPanelType.PAUSED);
        }

        if (pausedPanelController != null)
        {
            pausedPanelController.gameObject.SetActive(val);
        }
    }

    public void ShowHideRewardBasedVideoPanel(bool val)
    {
        if (val)
        {            
            SetActivePanel(UIPanelType.REWARD_BASED_VIDEO_ADS_MENU);
            gameDataManager.SetGameState(GameState.REWARD_BASED_VIDEO_ADS);
            uiEventManager.DispatchShowVideoRewardedAdPopUp();
            uiEventManager.DispatchChangeUIPanel(UIPanelType.REWARD_BASED_VIDEO_ADS_MENU);
        }

        if (rewardVideoPanelController != null)
        {
            rewardVideoPanelController.gameObject.SetActive(val);
        }
    }

    private void PressBack()
    {
        if (this.gameState != GameState.PLAY)
        {
            if (GetActivePanel() == UIPanelType.MAIN_MENU)
            {
                ShowHideMainMenuPanel(false);
                ShowHideQuitPanel(true);
                // show quit panel
            }
            else if (GetActivePanel() == UIPanelType.OPTION)
            {
                ShowHideOptionPanel(false);
                ShowHideMainMenuPanel(true);
            }
            else if (GetActivePanel() == UIPanelType.GAME_MODE_MENU)
            {
                ShowHideGameModePanel(false);
                ShowHideMainMenuPanel(true);
            }
            else if (GetActivePanel() == UIPanelType.GAME_OVER)
            {
                ShowHideGameOverPanel(false);
                ShowHideGameModePanel(true);
            }
            else if (GetActivePanel() == UIPanelType.QUIT_MENU)
            {
                ShowHideMainMenuPanel(true);
                uiEventManager.DispatchShowQuitPopUp();
                ShowHideQuitPanel(false);
            }
        }
        else
        {
            gameEventManager.DispatchGamePausedStart();
            ShowHidePausedPanel(true);
        }
    }

    private void OnClickButton(UIPanelType uiPanelType, ButtonType buttonType)
    {
        if (buttonType != ButtonType.LEADER_BOARD
            && buttonType != ButtonType.ACHIEVEMENTS
            && buttonType != ButtonType.SHARE_FACEBOOK
            && buttonType != ButtonType.SHARE_TWITTER
            && buttonType != ButtonType.SHARE)
        {
            if (uiPanelType == UIPanelType.MAIN_MENU)
            {
                ShowHideMainMenuPanel(false);
            }
            else if (uiPanelType == UIPanelType.OPTION)
            {
                ShowHideOptionPanel(false);
            }
            else if (uiPanelType == UIPanelType.GAME_MODE_MENU)
            {
                ShowHideGameModePanel(false);
            }
            else if (uiPanelType == UIPanelType.GAME_OVER)
            {
                ShowHideGameOverPanel(false);
            }
        }

        if (uiPanelType == UIPanelType.MAIN_MENU)
        {
            if (buttonType == ButtonType.GAME_MODE)
            {           
                ShowHideGameModePanel(true);
            }
            else if (buttonType == ButtonType.LEADER_BOARD)
            {           
                if (buttonType == ButtonType.LEADER_BOARD)
                {
                    uiEventManager.DispatchShowLeaderboard();
                }
            }
            else if (buttonType == ButtonType.OPTION)
            {           
                ShowHideOptionPanel(true);
            }
        }
        else if (uiPanelType == UIPanelType.GAME_MODE_MENU)
        {
            if (buttonType == ButtonType.CLASSIC || buttonType == ButtonType.KIDS)
            {
                gameEventManager.DispatchGameStart();
            }
            else if (buttonType == ButtonType.MAIN_MENU)
            {           
                ShowHideMainMenuPanel(true);
            }
        }
        else if (uiPanelType == UIPanelType.OPTION)
        {
            if (buttonType == ButtonType.MAIN_MENU)
            {           
                ShowHideMainMenuPanel(true);
            }
            else if (buttonType == ButtonType.GAME_MODE)
            {           
                ShowHideGameModePanel(true);
            }
            else if (buttonType == ButtonType.ACHIEVEMENTS)
            {            
                uiEventManager.DispatchShowAchievement();
            }
        }
        else if (uiPanelType == UIPanelType.PAUSED)
        {
            if (buttonType == ButtonType.PAUSED_RESUME)
            {
                ShowHidePausedPanel(false);
                gameEventManager.DispatchGameResumeStart();
            }
            else if (buttonType == ButtonType.PAUSED_RESTART)
            {                
                ShowHidePausedPanel(false);
                gameEventManager.DispatchGameRestartStart();
            }
            else if (buttonType == ButtonType.PAUSED_QUIT)
            {                
                ShowHidePausedPanel(false);
                ShowHideMainMenuPanel(true);
                gameEventManager.DispatchGameQuitStart();
            }
        }
        else if (uiPanelType == UIPanelType.GAME_OVER)
        {
            if (buttonType == ButtonType.TRY_AGAIN)
            {
                gameEventManager.DispatchGameRestartStart();
            }
            else if (buttonType == ButtonType.LEADER_BOARD)
            {            
                uiEventManager.DispatchShowLeaderboard();
            }
            else if (buttonType == ButtonType.SHARE)
            {            
                uiEventManager.DispatchShare();
            }
            else if (buttonType == ButtonType.MAIN_MENU)
            {
                ShowHideMainMenuPanel(true);
            }
        }
        else if (uiPanelType == UIPanelType.QUIT_MENU)
        {
            if (buttonType == ButtonType.NO_QUIT)
            {
                ShowHideQuitPanel(false);
                ShowHideMainMenuPanel(true);
            }
            else if (buttonType == ButtonType.YES_QUIT)
            {                
                gameEventManager.DispatchGameQuitApplicationStart();
            }
        }
        else if (uiPanelType == UIPanelType.REWARD_BASED_VIDEO_ADS_MENU)
        {
            if (buttonType == ButtonType.YES_WATCH_VIDEO_REWARD_AD)
            {
                ShowHideRewardBasedVideoPanel(false);
                uiEventManager.DispatchAcceptRewardedAd();
            }
            else if (buttonType == ButtonType.NO_WATCH_VIDEO_REWARD_AD)
            {
                ShowHideRewardBasedVideoPanel(false);
                uiEventManager.DispatchRejectRewardedAd();
            }
        }
        else
        {
            if (buttonType == ButtonType.PAUSED)
            {           
                gameEventManager.DispatchGamePausedStart();
                ShowHidePausedPanel(true);
            }
        }



        /*if (buttonType == ButtonType.MAIN_MENU)
        {			
            ShowHideMainMenuPanel(true);
        }


        if (buttonType == ButtonType.OPTION)
        {			
            ShowHideOptionPanel(true);
        }

        if (buttonType == ButtonType.GAME_MODE)
        {				
            ShowHideGameModePanel(true);
            //mediator.SetGameState(GameState.GAME_MODE_MENU);
        }
        else if (
            buttonType == ButtonType.START ||
            buttonType == ButtonType.CLASSIC ||
            buttonType == ButtonType.KIDS)
        {
            //mediator.StartGame();
        }
        else if (buttonType == ButtonType.PAUSED)
        {
            //mediator.PausedGame();
            ShowHidePausedPanel(true);
        }
        else if (buttonType == ButtonType.PAUSED_RESUME)
        {
            ShowHidePausedPanel(false);
            //mediator.StartResumeCountdown();
        }
        else if (buttonType == ButtonType.PAUSED_RESTART)
        {
            //mediator.RestartGame();
            ShowHidePausedPanel(false);
        }
        else if (buttonType == ButtonType.PAUSED_QUIT)
        {
            //mediator.QuitGame();
            ShowHidePausedPanel(false);
            ShowHideMainMenuPanel(true);
        }
        else if (buttonType == ButtonType.TRY_AGAIN)
        {
            //mediator.RestartGame();
        }
        else if (buttonType == ButtonType.LEADER_BOARD)
        {            
            //uiEventManager.DispatchShowLeaderboard();
        }
        else if (buttonType == ButtonType.ACHIEVEMENTS)
        {            
            //uiEventManager.DispatchShowAchievement();
        }
        else if (buttonType == ButtonType.SHARE)
        {            
            uiEventManager.DispatchShare();
        }
        else if (buttonType == ButtonType.NO_QUIT)
        {
            ShowHideQuitPanel(false);
            ShowHideMainMenuPanel(true);
        }
        else if (buttonType == ButtonType.YES_QUIT)
        {
            //mediator.QuitApplication();
        }
        else if (buttonType == ButtonType.YES_WATCH_VIDEO_REWARD_AD)
        {
            ShowHideRewardBasedVideoPanel(false);
            //uiEventManager.DispatchAcceptRewardedAd();
        }
        else if (buttonType == ButtonType.NO_WATCH_VIDEO_REWARD_AD)
        {
            ShowHideRewardBasedVideoPanel(false);
            //uiEventManager.DispatchRejectRewardedAd();
        }*/
    }

    private void OnGameStateChange(GameState gameState)
    {
        this.gameState = gameState;
    }
}
