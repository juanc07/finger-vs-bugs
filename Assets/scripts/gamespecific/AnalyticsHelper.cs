using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnalyticsHelper:ICleanable
{
    private const string TAG = "[ AnalyticsHelper ]: ";

    private IAnalytics analyticsService;
    private UIEventManager uiEventManager;
    private GameDataManager gameDataManager;
    private GameEventManager gameEventManager;
    private GameState gameState;
    private BugEventManager bugEventManager;

    public void Init()
    {
        // analytics service
        analyticsService = new  FirebaseAnalyticsService();
        // provide analytics service
        ServiceLocator.ProvideAnalyticsService(analyticsService);
        // analytics service

        gameDataManager = GameDataManager.GetInstance();
        gameEventManager = GameEventManager.GetInstance();
        uiEventManager = UIEventManager.GetInstance();
        bugEventManager = BugEventManager.GetInstance();
        RemoveEventListener();
    }

    private void AddEventListener()
    {
        gameDataManager.OnGameStateChange += OnGameStateChange;
        gameEventManager.OnGameInitComplete += OnGameInit;
        gameEventManager.OnGameOverComplete += OnGameOver;
        gameEventManager.OnGameQuitApplicationComplete += OnGameQuitApplication;

        uiEventManager.OnClickButton += OnClickButton;
        uiEventManager.OnMusicValueChange += OnMusicValueChange;
        uiEventManager.OnSoundValueChange += OnSoundValueChange;
        uiEventManager.OnVibrationValueChange += OnVibrationValueChange;
        uiEventManager.OnShowQuitPopUp += OnShowQuitPopUp;

        bugEventManager.OnBugStatusChange += OnBugStatusChange;
    }

    private void RemoveEventListener()
    {
        gameDataManager.OnGameStateChange -= OnGameStateChange;
        gameEventManager.OnGameInitComplete -= OnGameInit;
        gameEventManager.OnGameOverComplete -= OnGameOver;
        gameEventManager.OnGameQuitApplicationComplete -= OnGameQuitApplication;
        
        uiEventManager.OnClickButton -= OnClickButton;
        uiEventManager.OnMusicValueChange -= OnMusicValueChange;
        uiEventManager.OnSoundValueChange -= OnSoundValueChange;
        uiEventManager.OnVibrationValueChange -= OnVibrationValueChange;
        uiEventManager.OnShowQuitPopUp -= OnShowQuitPopUp;

        bugEventManager.OnBugStatusChange -= OnBugStatusChange;
    }

    public void Clean()
    {
        RemoveEventListener();
    }

    public void LogOpenGame()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.COMMON, GameAnalyticsConfig.OPEN_GAME, thisDay.ToString());
    }

    public void LogQuitGame()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.COMMON, GameAnalyticsConfig.QUIT_GAME, thisDay.ToString());
    }

    // main menu
    public void LogClickPlayMainMenuButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_PLAY_MAIN_MENU_BUTTON, thisDay.ToString());
    }

    public void LogClickHighScoreMainMenuButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_HIGH_SCORE_MAIN_MENU_BUTTON, thisDay.ToString());
    }

    public void LogClickOptionMainMenuButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_OPTION_MAIN_MENU_BUTTON, thisDay.ToString());
    }
    // main menu

    // in game click
    public void LogClickInGamePausedButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_IN_GAME_PAUSED_BUTTON, thisDay.ToString());
    }

    public void LogClickInGamePausedResumeButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_IN_GAME_PAUSED_RESUME_BUTTON, thisDay.ToString());
    }

    public void LogClickInGamePausedRestartButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_IN_GAME_PAUSED_RESTART_BUTTON, thisDay.ToString());
    }

    public void LogClickInGamePausedQuitButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_IN_GAME_PAUSED_QUIT_BUTTON, thisDay.ToString());
    }

    // option
    public void LogClickPlayOptionButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_PLAY_OPTION_BUTTON, thisDay.ToString());
    }

    public void LogClickAchievementsOptionButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_ACHIEVEMENT_OPTION_BUTTON, thisDay.ToString());
    }

    public void LogClickMainMenuOptionButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_MAIN_MENU_GAME_OPTION_BUTTON, thisDay.ToString());
    }

    public void LogMusicSliderValueOption(float val)
    {		
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.MUSIC_OPTION_VALUE, val);
    }

    public void LogSFXSliderValueOption(float val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.SFX_OPTION_VALUE, val);
    }

    public void LogClickVibratorOption()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_VIBRATION_OPTION, thisDay.ToString());
    }

    public void LogVibratorValue(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.VIBRATION_VALUE_OPTION, val);
    }
    // option

    // game mode
    public void LogClickClassicGameModeButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_CLASSIC_GAME_MODE_BUTTON, thisDay.ToString());
    }

    public void LogClickKidsGameModeButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_KIDS_GAME_MODE_BUTTON, thisDay.ToString());
    }

    public void LogClickMainMenuGameModeButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_MAIN_MENU_GAME_MODE_BUTTON, thisDay.ToString());
    }

    // game mode

    // game over
    public void LogClickMainMenuGameOverButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_MAIN_MENU_GAME_OVER_BUTTON, thisDay.ToString());
    }

    public void LogClickTryAgainGameOverButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_TRY_AGAIN_GAME_OVER_BUTTON, thisDay.ToString());
    }

    public void LogClickHighScoreGameOverButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_HIGH_SCORE_GAME_OVER_BUTTON, thisDay.ToString());
    }

    public void LogClickShareGameOverButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_SHARE_GAME_OVER_BUTTON, thisDay.ToString());
    }
    // game over

    // rate us
    public void LogClickRateItNowRateUSButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_RATE_IT_RATE_US_BUTTON, thisDay.ToString());
    }

    public void LogClickRemindMeLaterRateUSButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_REMIND_ME_LATER_RATE_US_BUTTON, thisDay.ToString());
    }

    public void LogClickNoThanksRateUSButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_NO_THANKS_RATE_US_BUTTON, thisDay.ToString());
    }
    // rate us

    // quit panel
    public void LogClickYesQuitButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_YES_QUIT_BUTTON, thisDay.ToString());
    }

    public void LogClickNoQuitButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_NO_QUIT_BUTTON, thisDay.ToString());
    }

    public void LogOpenQuitPopUp()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.OPEN_QUIT_POPUP, thisDay.ToString());
    }

    public void LogClickQuitButton()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_QUIT_BUTTON, thisDay.ToString());
    }

    // quit panel


    // features
    public void LogGooglePlayLogIn()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.GOOGLE_PLAY_LOGIN, thisDay.ToString());
    }

    public void LogOpenGooglePlayHighScore()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.OPEN_GOOGLE_PLAY_HIGH_SCORE, thisDay.ToString());
    }

    public void LogOpenGooglePlayAchievements()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.OPEN_GOOGLE_PLAY_ACHIEVEMENT, thisDay.ToString());
    }

    public void LogPostGooglePlayHighScore()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.POST_GOOGLE_PLAY_HIGH_SCORE, thisDay.ToString());
    }

    public void LogPostGooglePlayAchievements()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.POST_GOOGLE_PLAY_ACHIEVEMENT, thisDay.ToString());
    }


    public void LogGameCenterLogIn()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.GAME_CENTER_LOGIN, thisDay.ToString());
    }

    public void LogOpenGameCenterHighScore()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.OPEN_GAME_CENTER_HIGH_SCORE, thisDay.ToString());
    }

    public void LogOpenGameCenterAchievements()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.OPEN_GAME_CENTER_ACHIEVEMENT, thisDay.ToString());
    }

    public void LogPostGameCenterHighScore()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.POST_GAME_CENTER_HIGH_SCORE, thisDay.ToString());
    }

    public void LogPostGameCenterAchievements()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.FEATURES, GameAnalyticsConfig.POST_GAME_CENTER_ACHIEVEMENT, thisDay.ToString());
    }
    // features


    public void LogShare()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.CLICK, GameAnalyticsConfig.CLICK_SHARE_GAME_OVER_BUTTON, thisDay.ToString());
    }

    // ads
    public void LogLoadBannerAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.LOAD_BANNER_ADS, thisDay.ToString());
    }

    public void LogLoadFailBannerAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.LOAD_FAILED_BANNER_ADS, thisDay.ToString());
    }

    public void LogOpenBannerAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.OPEN_BANNER_ADS, thisDay.ToString());
    }

    public void LogCloseBannerAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.CLOSE_BANNER_ADS, thisDay.ToString());
    }

    public void LogLeavingApplicationBannerAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.LEAVING_APPLICAION_BANNER_ADS, thisDay.ToString());
    }

    // inter stitial

    public void LogLoadInterStitialAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.LOAD_INTER_STITIAL_ADS, thisDay.ToString());
    }

    public void LogLoadFailInterStitialAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.LOAD_FAILED_INTER_STITIAL_ADS, thisDay.ToString());
    }

    public void LogOpenInterStitialAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.OPEN_INTER_STITIAL_ADS, thisDay.ToString());
    }

    public void LogCloseInterStitialAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.CLOSE_INTER_STITIAL_ADS, thisDay.ToString());
    }

    public void LogLeavingApplicationInterStitialAds()
    {
        DateTime thisDay = DateTime.Today;
        analyticsService.LogEvent(GameAnalyticsConfig.ADS, GameAnalyticsConfig.LEAVING_APPLICAION_INTER_STITIAL_ADS, thisDay.ToString());
    }
    // ads

    // in game
    public void LogClassicMinCombo(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_MIN_COMBO, val);
    }

    public void LogClassicBestCombo(int val)
    {		
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_BEST_COMBO, val);
    }


    public void LogClassicMinHighScore(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_MIN_HIGH_SCORE, val);
    }

    public void LogClassicBestHighScore(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_BEST_HIGH_SCORE, val);
    }

    public void LogClassicTotalBugsKillPerGame(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_TOTAL_BUGS_KILLS_PER_GAME, val);
    }

    public void LogClassicOverAllTotalBugsKills(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_OVER_ALL_BUG_TOTAL_KILLS, val);
    }

    public void LogTotalGameCountPerSession(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.TOTAL_GAME_COUNT_PER_SESSION, val);
    }

    public void LogClassicDurationPerRound(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_DURATION_PER_ROUND, val);
    }

    public void LogClassicAntQueenKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_ANT_QUEEN_KILL, val);
    }

    public void LogClassicAntWarriorKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_ANT_WARRIOR_KILL, val);
    }

    public void LogClassicAntWorkerKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_ANT_WORKER_KILL, val);
    }

    public void LogClassicSpiderKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_SPIDER_KILL, val);
    }

    public void LogClassicSmallSpiderKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_SMALL_SPIDER_KILL, val);
    }

    public void LogClassicCockroachKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.CLASSIC_COCKROACH_KILL, val);
    }

    // kids
    public void LogKidsMinCombo(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_MIN_COMBO, val);
    }

    public void LogKidsBestCombo(int val)
    {		
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_BEST_COMBO, val);
    }


    public void LogKidsMinHighScore(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_MIN_HIGH_SCORE, val);
    }

    public void LogKidsBestHighScore(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_BEST_HIGH_SCORE, val);
    }

    public void LogKidsTotalBugsKillPerGame(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_TOTAL_BUGS_KILLS_PER_GAME, val);
    }

    public void LogKIDSOverAllTotalBugsKills(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_OVER_ALL_BUG_TOTAL_KILLS, val);
    }

    public void LogKidsDurationPerRound(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_DURATION_PER_ROUND, val);
    }

    public void LogKidsAntQueenKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_ANT_QUEEN_KILL, val);
    }

    public void LogKidsAntWarriorKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_ANT_WARRIOR_KILL, val);
    }

    public void LogKidsAntWorkerKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_ANT_WORKER_KILL, val);
    }

    public void LogKidsSpiderKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_SPIDER_KILL, val);
    }

    public void LogKidsSmallSpiderKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_SMAL_SPIDER_KILL, val);
    }

    public void LogKidsCockroachKill(int val)
    {
        analyticsService.LogEvent(GameAnalyticsConfig.IN_GAME, GameAnalyticsConfig.KIDS_COCKROACH_KILL, val);
    }
    // kids
    // in game

    public void UpdateAntsAnalytics()
    {
        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            LogClassicDurationPerRound(gameDataManager.GetDuration() - DateTime.Now.Millisecond);

            LogClassicTotalBugsKillPerGame(gameDataManager.GetBugKill());
            LogClassicOverAllTotalBugsKills(gameDataManager.GetTotalBugKill());

            LogClassicAntQueenKill(gameDataManager.GetAntQueenKill());
            LogClassicAntWarriorKill(gameDataManager.GetAntWarriorKill());
            LogClassicAntWorkerKill(gameDataManager.GetAntWorkerKill());
            LogClassicSpiderKill(gameDataManager.GetSpiderKill());
            LogClassicSmallSpiderKill(gameDataManager.GetSmallSpiderKill());
            LogClassicCockroachKill(gameDataManager.GetCockroachKill());

        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            LogKidsDurationPerRound(gameDataManager.GetDuration() - DateTime.Now.Millisecond);

            LogKidsTotalBugsKillPerGame(gameDataManager.GetBugKill());
            LogKIDSOverAllTotalBugsKills(gameDataManager.GetTotalBugKill());

            LogKidsAntQueenKill(gameDataManager.GetAntQueenKill());
            LogKidsAntWarriorKill(gameDataManager.GetAntWarriorKill());
            LogKidsAntWorkerKill(gameDataManager.GetAntWorkerKill());
            LogKidsSpiderKill(gameDataManager.GetSpiderKill());
            LogKidsSmallSpiderKill(gameDataManager.GetSmallSpiderKill());
            LogKidsCockroachKill(gameDataManager.GetCockroachKill());
        }

        // reset
        gameDataManager.SetBugKill(0);
    }

    private void OnGameStateChange(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void OnGameInit()
    {
        LogOpenGame();
    }

    private void OnGameOver()
    {
        int score = gameDataManager.GetScore();
        int bestScore = 0;

        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            bestScore = gameDataManager.GetBestScore();
            LogClassicMinHighScore(score);
            LogClassicBestHighScore(bestScore);
        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            bestScore = gameDataManager.GetKidsBestScore();
            LogKidsMinHighScore(score);
            LogKidsBestHighScore(bestScore);
        }

        UpdateAntsAnalytics();
    }

    private void OnGameQuitApplication()
    {
        LogTotalGameCountPerSession(gameDataManager.GetGameCount());
        LogQuitGame();
    }

    private void OnClickButton(UIPanelType panelType, ButtonType buttonType)
    {
        if (panelType == UIPanelType.MAIN_MENU)
        {
            if (buttonType == ButtonType.GAME_MODE)
            {
                LogClickPlayMainMenuButton();
            }
            else if (buttonType == ButtonType.LEADER_BOARD)
            {
                LogClickHighScoreMainMenuButton();
            }
            else if (buttonType == ButtonType.OPTION)
            {
                LogClickOptionMainMenuButton();
            }           
        }
        else if (panelType == UIPanelType.OPTION)
        {
            if (buttonType == ButtonType.GAME_MODE)
            {
                LogClickPlayOptionButton();
            }
            else if (buttonType == ButtonType.ACHIEVEMENTS)
            {
                LogClickAchievementsOptionButton();
            }
            else if (buttonType == ButtonType.MAIN_MENU)
            {
                LogClickMainMenuOptionButton();
            }
        }
        else if (panelType == UIPanelType.GAME_MODE_MENU)
        {
            if (buttonType == ButtonType.CLASSIC)
            {
                LogClickClassicGameModeButton();
            }
            else if (buttonType == ButtonType.KIDS)
            {
                LogClickKidsGameModeButton();
            }
            else if (buttonType == ButtonType.MAIN_MENU)
            {
                LogClickMainMenuGameModeButton();
            }
        }
        else if (panelType == UIPanelType.GAME_OVER)
        {
            if (buttonType == ButtonType.TRY_AGAIN)
            {
                LogClickTryAgainGameOverButton();
            }
            else if (buttonType == ButtonType.LEADER_BOARD)
            {
                LogClickHighScoreGameOverButton();
            }
            else if (buttonType == ButtonType.MAIN_MENU)
            {
                LogClickMainMenuGameOverButton();
            }
            else if (buttonType == ButtonType.SHARE)
            {
                LogClickShareGameOverButton();
            }
        }
        else if (panelType == UIPanelType.QUIT_MENU)
        {
            if (buttonType == ButtonType.YES_QUIT)
            {
                LogClickYesQuitButton();
            }
            else if (buttonType == ButtonType.NO_QUIT)
            {
                LogClickNoQuitButton();
            }
        }
        else if (panelType == UIPanelType.RATE_MENU)
        {
            if (buttonType == ButtonType.RATE_IT_NOW)
            {
                LogClickRateItNowRateUSButton();
            }
            else if (buttonType == ButtonType.RATE_IT_LATER)
            {
                LogClickRemindMeLaterRateUSButton();
            }
            else if (buttonType == ButtonType.RATE_NO_THANKS)
            {
                LogClickNoThanksRateUSButton();
            }
        }
        else if (panelType == UIPanelType.PAUSED)
        {
            if (buttonType == ButtonType.PAUSED_RESUME)
            {
                LogClickInGamePausedResumeButton();
            }
            else if (buttonType == ButtonType.PAUSED_RESTART)
            {
                LogClickInGamePausedRestartButton();
            }
            else if (buttonType == ButtonType.PAUSED_QUIT)
            {
                LogClickInGamePausedQuitButton();
            }
            else
            {
                if (buttonType == ButtonType.PAUSED)
                {                   
                    LogClickInGamePausedButton();
                }
                else if (buttonType == ButtonType.PAUSED_RESUME)
                {
                    LogClickInGamePausedResumeButton();
                }
                else if (buttonType == ButtonType.PAUSED_RESTART)
                {                    
                    LogClickInGamePausedRestartButton();
                }
                else if (buttonType == ButtonType.PAUSED_QUIT)
                {
                    LogClickInGamePausedQuitButton();
                }
            }
        }
    }

    private void OnMusicValueChange(float val)
    {
        LogMusicSliderValueOption(val);
    }

    private void OnSoundValueChange(float val)
    {
        LogSFXSliderValueOption(val);
    }

    private void OnVibrationValueChange(int val)
    {
        LogVibratorValue(val);
    }

    private void OnShowQuitPopUp()
    {
        LogOpenQuitPopUp();
    }

    // triggers when there's bug that change status
    private void OnBugStatusChange(BugEvent antEvent, BugController antController)
    {
        if (antEvent == BugEvent.ESCAPE)
        {
            BugEscape(antController);
        }
    }

    // bug event handlers
    // trigger when bug escape
    private void BugEscape(BugController antController)
    {
        if (this.gameState == GameState.PLAY)
        {
            if (gameDataManager.GetGameMode() == GameMode.HARD)
            {
                LogClassicMinCombo(gameDataManager.GetCombo());
            }
            else if (gameDataManager.GetGameMode() == GameMode.EASY)
            {
                LogKidsMinCombo(gameDataManager.GetCombo());
            }

        }
    }
}
