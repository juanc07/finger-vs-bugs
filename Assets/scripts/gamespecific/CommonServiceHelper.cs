using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonServiceHelper:ICleanable
{
    private const string TAG = "[ CommonServiceHelper ]";
    private ITPCommon commonService;
    private int lastAction;

    private AnalyticsHelper analyticsHelper;
    private ISave playerPrefDataService;

    private GameDataManager gameDataManager;
    private GameEventManager gameEventManager;
    private GameMode gameMode;

    private UIEventManager uiEventManager;

    public void Init(AnalyticsHelper analyticsHelper, ISave playerPrefDataService)
    {
        gameDataManager = GameDataManager.GetInstance();
        this.analyticsHelper = analyticsHelper;
        this.playerPrefDataService = playerPrefDataService;

        AddEventListener();

        // itp for accessing google play and game center leaderboard and achievements
        // load service depends on which platform  in our case android or ios
        //itpCommon service for google play and game center and other third party api
        #if UNITY_ANDROID
        commonService = new GooglePlayService();
        commonService.Init();

        // can be remove??? it anoying sign everytime?
        commonService.SignIn(OnITPCommonServiceSignIn);

        #elif UNITY_IOS
        commonService = new GameCenterService();
        commonService.Init();
        // provide mobile service
        #endif

        // provide the itpcommon service
        ServiceLocator.ProvideTPCommonService(commonService);

        // auto sign in when ios because wsometimes doing signin and show leader board have delays
        #if UNITY_IOS
        commonService.SignIn(OnITPCommonServiceSignIn);
        #endif
    }

    private void AddEventListener()
    {
        gameEventManager = GameEventManager.GetInstance();
        gameDataManager.OnGameModeChange += OnGameModeChange;

        uiEventManager = UIEventManager.GetInstance();
        uiEventManager.OnShowLeaderBoard += OnShowLeaderBoard;
        uiEventManager.OnShowAchievement += OnShowAchievement;
    }

    private void RemoveEventListener()
    {        
        gameDataManager.OnGameModeChange -= OnGameModeChange;
        gameEventManager = null;


        uiEventManager.OnShowLeaderBoard -= OnShowLeaderBoard;
        uiEventManager.OnShowAchievement -= OnShowAchievement;
        uiEventManager = null;
    }

    public void Clean()
    {
        RemoveEventListener();
    }

    public void ShowLeaderBoard()
    {       
        if (commonService.IsLogIn())
        {
            ShowLeaderBoardByGameMode();
        }
        else
        {
            lastAction = GameConfig.ACTION_SHOW_LEADER_BOARD;
            commonService.SignIn(OnITPCommonServiceSignIn);
        }
    }

    // post score on googleplay and gamecenter
    public void PostScore(int currentScore)
    {
        // sets the highscore based on game mode
        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            // post score on common third party services if sign in
            #if UNITY_ANDROID
            commonService.PostScore(GameConfig.leaderboard_classic_highscore, currentScore, OnTPCommonServicePostScore);
            analyticsHelper.LogPostGooglePlayHighScore();
            #elif UNITY_IOS
            commonService.PostScore(GameConfig.GAME_CENTER_LEADER_BOARD_CLASSIC, currentScore, OnTPCommonServicePostScore);
            analyticsHelper.LogPostGameCenterHighScore();
            #endif

        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            // post score on common third party services if sign in
            #if UNITY_ANDROID
            commonService.PostScore(GameConfig.leaderboard_kids_highscore, currentScore, OnTPCommonServicePostScore);
            analyticsHelper.LogPostGooglePlayHighScore();
            #elif UNITY_IOS
            commonService.PostScore(GameConfig.GAME_CENTER_LEADER_BOARD_KIDS, currentScore, OnTPCommonServicePostScore);
            analyticsHelper.LogPostGameCenterHighScore();
            #endif
        }
    }

    public void ShowLeaderBoardByGameMode()
    {
        if (this.gameMode == GameMode.HARD)
        {
            #if UNITY_ANDROID
            Debug.Log("try to show leader board for classic ");
            commonService.ShowLeaderBoard(GameConfig.leaderboard_classic_highscore);
            analyticsHelper.LogOpenGooglePlayHighScore();
            #elif UNITY_IOS
            commonService.ShowLeaderBoard(GameConfig.GAME_CENTER_LEADER_BOARD_CLASSIC);
            analyticsHelper.LogOpenGameCenterHighScore();
            #endif

        }
        else if (this.gameMode == GameMode.EASY)
        {                   
            #if UNITY_ANDROID
            Debug.Log("try to show leader board for kids ");
            commonService.ShowLeaderBoard(GameConfig.leaderboard_kids_highscore);
            analyticsHelper.LogOpenGooglePlayHighScore();
            #elif UNITY_IOS
            commonService.ShowLeaderBoard(GameConfig.GAME_CENTER_LEADER_BOARD_KIDS);
            analyticsHelper.LogOpenGameCenterHighScore();
            #endif
        }
        else
        {
            Debug.Log("Main no  game mode selected cannot show any leaderboard......");
        }
    }

    public void ShowAchievement()
    {
        #if UNITY_IOS
        commonService.ShowAchievement();
        analyticsHelper.LogOpenGameCenterAchievements();
        #elif UNITY_ANDROID
        if (commonService.IsLogIn())
        {
            // call and show achievement here
            commonService.ShowAchievement();
            #if UNITY_ANDROID
            analyticsHelper.LogOpenGooglePlayAchievements();
            #elif UNITY_IOS
        analyticsHelper.LogOpenGameCenterAchievements();
            #endif
        }
        else
        {
            lastAction = GameConfig.ACTION_SHOW_ACHIEVEMENT;
            commonService.SignIn(OnITPCommonServiceSignIn);
        }
        #endif
        Debug.Log(TAG + "ShowAchievement!!");
    }

    public void LatePostHighScore()
    {
        if (this.gameMode == GameMode.HARD)
        {
            // post score on common third party services if sign in
            #if UNITY_ANDROID
            commonService.PostScore(GameConfig.leaderboard_classic_highscore,
                playerPrefDataService.LoadIntSaveData(PlayerDataKey.CLASSIC_BEST_SCORE), OnTPCommonServicePostScore);
            analyticsHelper.LogPostGooglePlayHighScore();
            #elif UNITY_IOS
            commonService.PostScore(GameConfig.GAME_CENTER_LEADER_BOARD_CLASSIC,
            playerPrefDataService.LoadIntSaveData(PlayerDataKey.CLASSIC_BEST_SCORE), OnTPCommonServicePostScore);
            analyticsHelper.LogPostGameCenterHighScore();
            #endif


        }
        else if (this.gameMode == GameMode.EASY)
        {
            // post score on common third party services if sign in
            #if UNITY_ANDROID
            commonService.PostScore(GameConfig.leaderboard_kids_highscore,
                playerPrefDataService.LoadIntSaveData(PlayerDataKey.KIDS_BEST_SCORE), OnTPCommonServicePostScore);
            analyticsHelper.LogPostGooglePlayHighScore();
            #elif UNITY_IOS
            commonService.PostScore(GameConfig.GAME_CENTER_LEADER_BOARD_KIDS,
            playerPrefDataService.LoadIntSaveData(PlayerDataKey.KIDS_BEST_SCORE), OnTPCommonServicePostScore);
            analyticsHelper.LogPostGameCenterHighScore();
            #endif
        }
    }

    public void LatePostAchievement()
    {

        //GameMode gameMode = dataService.GetGameMode();
        int bugTotalKill = playerPrefDataService.LoadIntSaveData(PlayerDataKey.BUG_TOTAL_KILL);


        // check bug squash achievement
        UpdateBugSquash(bugTotalKill, gameMode);  
               

        int bestCombo = 0;
        if (this.gameMode == GameMode.HARD)
        {
            bestCombo = playerPrefDataService.LoadIntSaveData(PlayerDataKey.CLASSIC_BEST_COMBO);
            analyticsHelper.LogClassicBestCombo(bestCombo);
        }
        else if (this.gameMode == GameMode.EASY)
        {
            bestCombo = playerPrefDataService.LoadIntSaveData(PlayerDataKey.KIDS_BEST_COMBO);
            analyticsHelper.LogKidsBestCombo(bestCombo);
        }


        // check combo achievements
        CheckCombo(bestCombo, gameMode);
    }

    public void UpdateAchievement()
    {

        int bestCombo = 0;
        int currentScore = gameDataManager.GetScore();

        GameMode gameMode = gameDataManager.GetGameMode();


        // check achievement
        UpdateBugSquash(currentScore, gameMode);



        if (gameMode == GameMode.HARD)
        {
            bestCombo = playerPrefDataService.LoadIntSaveData(PlayerDataKey.CLASSIC_BEST_COMBO);
            analyticsHelper.LogClassicBestCombo(bestCombo);
        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            bestCombo = playerPrefDataService.LoadIntSaveData(PlayerDataKey.KIDS_BEST_COMBO);
            analyticsHelper.LogKidsBestCombo(bestCombo);
        }


        // check combo achievements
        CheckCombo(bestCombo, gameMode);
    }

    public void UpdateBugSquash(int val, GameMode gameMode)
    {
        Debug.Log(TAG + "UpdateBugSquash: " + val + "  gameMode " + gameMode);

        if (gameMode == GameMode.HARD)
        {
            #if UNITY_ANDROID
            commonService.IncrementAchievement(GameConfig.achievement_bug_hater, val, OnIncrementAchievement);
            commonService.IncrementAchievement(GameConfig.achievement_bug_hunter, val, OnIncrementAchievement);
            commonService.IncrementAchievement(GameConfig.achievement_bug_terminator, val, OnIncrementAchievement);
            #elif UNITY_IOS
            commonService.IncrementAchievement( GameConfig.GAME_CENTER_KILL_100_ANTS,val,OnIncrementAchievement);
            commonService.IncrementAchievement( GameConfig.GAME_CENTER_KILL_300_ANTS,val,OnIncrementAchievement);
            commonService.IncrementAchievement( GameConfig.GAME_CENTER_KILL_500_ANTS,val,OnIncrementAchievement);
            #endif
        }
        else if (gameMode == GameMode.EASY)
        {
            #if UNITY_ANDROID
            commonService.IncrementAchievement(GameConfig.achievement_kids_bug_hater, val, OnIncrementAchievement);
            commonService.IncrementAchievement(GameConfig.achievement_kids_bug_hunter, val, OnIncrementAchievement);
            commonService.IncrementAchievement(GameConfig.achievement_kids_bug_terminator, val, OnIncrementAchievement);
            #elif UNITY_IOS
            commonService.IncrementAchievement( GameConfig.GAME_CENTER_KIDS_KILL_100_ANTS,val,OnIncrementAchievement);
            commonService.IncrementAchievement( GameConfig.GAME_CENTER_KIDS_KILL_300_ANTS,val,OnIncrementAchievement);
            commonService.IncrementAchievement( GameConfig.GAME_CENTER_KIDS_KILL_500_ANTS,val,OnIncrementAchievement);
            #endif
        }
    }

    public void CheckCombo(int bestCombo, GameMode gameMode)
    {
        Debug.Log(TAG + "bestCombo: " + bestCombo + "  gameMode " + gameMode);

        if (gameMode == GameMode.HARD)
        {
            if (bestCombo >= 50)
            {
                #if UNITY_ANDROID
                commonService.UnlockAchievement(GameConfig.achievement_casual_tapper, OnUnlockAchievement);                 
                #elif UNITY_IOS
                commonService.UnlockAchievement( GameConfig.GAME_CENTER_COMBO_50 ,OnUnlockAchievement);  
                #endif
            }

            if (bestCombo >= 100)
            {
                #if UNITY_ANDROID
                commonService.UnlockAchievement(GameConfig.achievement_pro_tapper, OnUnlockAchievement);                    
                #elif UNITY_IOS
                commonService.UnlockAchievement( GameConfig.GAME_CENTER_COMBO_100,OnUnlockAchievement ); 
                #endif
            }

            if (bestCombo >= 500)
            {
                #if UNITY_ANDROID
                commonService.UnlockAchievement(GameConfig.achievement_ultimate_tapper, OnUnlockAchievement);                   
                #elif UNITY_IOS
                commonService.UnlockAchievement( GameConfig.GAME_CENTER_COMBO_500,OnUnlockAchievement );
                #endif
            }
        }
        else if (gameMode == GameMode.EASY)
        {

            if (bestCombo >= 50)
            {
                #if UNITY_ANDROID
                commonService.UnlockAchievement(GameConfig.achievement_kids_casual_tapper, OnUnlockAchievement);                    
                #elif UNITY_IOS
                commonService.UnlockAchievement( GameConfig.GAME_CENTER_KIDS_COMBO_50 ,OnUnlockAchievement); 
                #endif
            }

            if (bestCombo >= 100)
            {
                #if UNITY_ANDROID
                commonService.UnlockAchievement(GameConfig.achievement_kids_pro_tapper, OnUnlockAchievement);                   
                #elif UNITY_IOS
                commonService.UnlockAchievement( GameConfig.GAME_CENTER_KIDS_COMBO_100,OnUnlockAchievement );    
                #endif
            }

            if (bestCombo >= 500)
            {
                #if UNITY_ANDROID
                commonService.UnlockAchievement(GameConfig.achievement_kids_ultimate_tapper, OnUnlockAchievement);                  
                #elif UNITY_IOS
                commonService.UnlockAchievement( GameConfig.GAME_CENTER_KIDS_COMBO_500,OnUnlockAchievement );
                #endif
            }
        }
    }

    public void OnIncrementAchievement(bool status)
    {
        Debug.Log(TAG + "  OnIncrementAchievement status : " + status);
    }

    public void OnUnlockAchievement(bool status)
    {
        Debug.Log(TAG + "  OnUnlockAchievement status : " + status);
    }

    private void OnITPCommonServiceSignIn(bool status)
    {
        if (status)
        {
            #if UNITY_ANDROID
            analyticsHelper.LogGooglePlayLogIn();
            #elif UNITY_IOS
            analyticsHelper.LogGameCenterLogIn();
            #endif


            if (lastAction == GameConfig.ACTION_SHOW_LEADER_BOARD)
            {
                lastAction = GameConfig.ACTION_NONE;
                ShowLeaderBoard();
            }
            else if (lastAction == GameConfig.ACTION_SHOW_ACHIEVEMENT)
            {
                lastAction = GameConfig.ACTION_NONE;
                ShowAchievement();
            }

            LatePostHighScore();
            LatePostAchievement();
        }
        else
        {
            Debug.Log(" Main OnITPCommonServiceSignIn failed status : " + status);
        }
    }

    private void OnGameModeChange(GameMode gameMode)
    {
        this.gameMode = gameMode;
    }

    public void OnTPCommonServicePostScore(bool status)
    {
        Debug.Log(TAG + "  OnTPCommonServicePostScore status : " + status);
    }

    private void OnShowLeaderBoard()
    {
        ShowLeaderBoard();
    }

    private void OnShowAchievement()
    {
        ShowAchievement();
    }
}
