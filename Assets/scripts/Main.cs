using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Main : MonoBehaviour,ICleanable
{
    private const string TAG = "[ MAIN ]";
    //private GameState gameState;

    [HideInInspector]
    public GameDataManager gameDataManager;
    [HideInInspector]
    public GameEventManager gameEventManager;
    [HideInInspector]
    public UIEventManager uiEventManager;
    [HideInInspector]
    public SoundManager soundManager;

    // controllers
    public BugSpawner bugSpawner;
    public WaveManager waveManager;
    public UIManager uiManager;
    public UIPanelController uiPanelController;
    public GameRateController gameRateController;

    // this is for easy modifying data on the editor
    // this data will be use on init only
    public DataController dataController;

    // input controller
    public TouchInputController touchInputController;

    // particleManager
    public ParticleManager particleManager;

    //ground controller
    public GroundController groundController;

    //private IData dataService;
    private ISave playerPrefDataService;

    // bug events
    private BugEventManager bugEventManager;

    // audio helper for setting up bgm and sfx setup and etc.
    private AudioHelper audioHelper;
    private AnalyticsHelper analyticsHelper;
    private AdsHelper adsHelper;
    private MobileServiceHelper mobileServiceHelper;
    private CommonServiceHelper commonServiceHelper;

    private void Awake()
    {
        // initialized managers
        gameDataManager = GameDataManager.GetInstance();
        soundManager = SoundManager.GetInstance();
        soundManager.LoadAudio();

        gameEventManager = GameEventManager.GetInstance();
        uiEventManager = UIEventManager.GetInstance();

        // the ant event manager for the ants
        bugEventManager = BugEventManager.GetInstance();
    }


    // Use this for initialization
    void Start()
    {
        //Debug.Log(TAG + " Start");
        AddEventListener();
        Init();
    }

    private void OnDestroy()
    {
        //Debug.Log(TAG + " OnDestroy");
        Clean();
    }

    // clears and clean all the services,events and helpers
    public void Clean()
    {
        RemoveGameRateListener();
        RemoveEventListener();
        analyticsHelper.Clean();
        audioHelper.Clean();
        adsHelper.Clean();
        bugSpawner.Clean();
        commonServiceHelper.Clean();
        mobileServiceHelper.Clean();
    }

    private void Init()
    {
        // set the game state to init
        SetGameState(GameState.INIT);

        // loads the services
        InitLoadServices();

        // load player settings
        LoadPlayerSettings();

        AddGameRateListener();

        // inject the dependencies to controllers
        //uiPanelController.mediator = this;

        // setup the ui based on the data services information
        uiManager.SetHPBar(gameDataManager.GetHP(), gameDataManager.GetMaxHP());

        // pre spawn ants
        bugSpawner.PreSpawn();
        // pre populate wave data
        waveManager.PopulateWave();

        // set the game state to main menu
        SetGameState(GameState.MAIN_MENU);

        gameEventManager.DispatchGameInitComplete();
    }

    // initialize and load all services and helpers required
    private void InitLoadServices()
    {
        // init service Locator
        ServiceLocator.Init();

        //data service for dynamic data
        //dataService = new DataService();
        // provide the data service
        //ServiceLocator.ProvideDataService(dataService);
        //data service for dynamic data

        //save data service for player pref
        playerPrefDataService = new PlayerPrefDataService();
        // provide the save data service
        ServiceLocator.ProvidePlayerPrefDataService(playerPrefDataService);

        // loads audio settings and setup bgm and sfx volumes
        audioHelper = new AudioHelper(playerPrefDataService);

        // analytics helper
        analyticsHelper = new AnalyticsHelper();
        analyticsHelper.Init();
        // analytics helper

        commonServiceHelper = new CommonServiceHelper();
        commonServiceHelper.Init(analyticsHelper, playerPrefDataService);

        mobileServiceHelper = new MobileServiceHelper();
        mobileServiceHelper.Init(this);

        // ads helper
        adsHelper = new AdsHelper();
        //adsHelper.mediator = this;
        adsHelper.Init(this, analyticsHelper);
        // ads helper
    }

    // load player settings and save player settings
    private void LoadPlayerSettings()
    {
        //load player data settings from editor and apply it to the data services
        PlayerData playerData = this.dataController.GetPlayerData();
        playerData.bestScore = playerPrefDataService.LoadIntSaveData(PlayerDataKey.CLASSIC_BEST_SCORE);
        playerData.kidBestScore = playerPrefDataService.LoadIntSaveData(PlayerDataKey.KIDS_BEST_SCORE);
        playerData.totalGameCount = playerPrefDataService.LoadIntSaveData(PlayerDataKey.TOTAL_GAME_COUNT);
        playerData.bestCombo = playerPrefDataService.LoadIntSaveData(PlayerDataKey.CLASSIC_BEST_COMBO);
        playerData.kidsBestCombo = playerPrefDataService.LoadIntSaveData(PlayerDataKey.KIDS_BEST_COMBO);
        playerData.bugTotalKill = playerPrefDataService.LoadIntSaveData(PlayerDataKey.BUG_TOTAL_KILL);

        // load the player vibration settings
        int isVibrationOn = playerPrefDataService.LoadIntSaveData(PlayerDataKey.VIBRATION);
        if (isVibrationOn == GameConfig.VIBRATION_ON)
        {
            gameDataManager.SetVibration(true);
        }
        else if (isVibrationOn == GameConfig.VIBRATION_OFF)
        {
            gameDataManager.SetVibration(false);
        }
        else
        {
            // no save default to vibration on
            gameDataManager.SetVibration(true);
            playerPrefDataService.SaveData(PlayerDataKey.VIBRATION, 1);
        }

        gameDataManager.SetHP(playerData.hp);
        gameDataManager.SetMaxHP(playerData.maxHp);
        gameDataManager.SetScore(playerData.score);
        gameDataManager.SetCombo(playerData.combo);

        // sets game count
        gameDataManager.SetGameCount(playerData.gameCount);

        // total game play count
        gameDataManager.SetTotalGameCount(playerData.totalGameCount);

        // best score
        gameDataManager.SetBestScore(playerData.bestScore);
        gameDataManager.SetKidsBestScore(playerData.kidBestScore);

        // best combo
        gameDataManager.SetBestCombo(playerData.bestCombo);
        gameDataManager.SetKidsBestCombo(playerData.kidsBestCombo);

        // ingame
        gameDataManager.SetTotalBugKill(playerData.bugTotalKill);
    }

    // event listener for some specific events
    private void AddEventListener()
    {
        gameEventManager.OnRewardExtraLives += OnRewardExtraLives;
        gameEventManager.OnGamePausedStart += OnGamePausedStart;
        gameEventManager.OnGameStart += OnGameStart;
        gameEventManager.OnGameRestartStart += OnGameRestartStart;
        gameEventManager.OnGameQuitApplicationStart += OnGameQuitApplicationStart;

        gameEventManager.OnGameResumeStart += OnGameResumeStart;
        gameEventManager.OnGameQuitStart += OnGameQuitStart;

        uiEventManager.OnUpdatePlayerHP += OnUpdatePlayerHP;
        uiEventManager.OnResumeCountDownComplete += OnResumeCountDownComplete;
        uiEventManager.OnAcceptWatchRewardedAd += OnAcceptWatchRewardedAd;
        uiEventManager.OnRejectWatchRewardedAd += OnRejectWatchRewardedAd;

        bugEventManager.OnBugStatusChange += OnBugStatusChange;
    }

    private void RemoveEventListener()
    {   
        gameEventManager.OnRewardExtraLives -= OnRewardExtraLives;
        gameEventManager.OnGamePausedStart -= OnGamePausedStart;
        gameEventManager.OnGameStart -= OnGameStart;
        gameEventManager.OnGameRestartStart -= OnGameRestartStart;
        gameEventManager.OnGameQuitApplicationStart -= OnGameQuitApplicationStart;

        gameEventManager.OnGameResumeStart -= OnGameResumeStart;
        gameEventManager.OnGameQuitStart -= OnGameQuitStart;

        uiEventManager.OnUpdatePlayerHP -= OnUpdatePlayerHP;
        uiEventManager.OnResumeCountDownComplete -= OnResumeCountDownComplete;
        uiEventManager.OnAcceptWatchRewardedAd -= OnAcceptWatchRewardedAd;
        uiEventManager.OnRejectWatchRewardedAd -= OnRejectWatchRewardedAd;

        bugEventManager.OnBugStatusChange += OnBugStatusChange;
    }

    // for checking if player hits the bug
    void Update()
    {
        //only checks when the game is in play game state
        if (GetGameState() == GameState.PLAY)
        {
            if (touchInputController != null)
            {
                HitObject hitObject = touchInputController.HandleInput();
                if (hitObject != null)
                {
                    BugController antController = hitObject.hitObject.GetComponent<BugController>();
                    if (antController != null)
                    {
                        // check if can damage or able to damage
                        bool isDamage = antController.TakeDamage();
                        if (isDamage)
                        {
                            // shows the particle when hit some bug
                            GameObject particle = particleManager.ShowParticle(ParticleType.HIT);
                            if (particle != null)
                            {
                                // use the bug hit position on particle position
                                particle.transform.position = new Vector3(hitObject.hit.point.x, hitObject.hit.point.y,
                                    hitObject.hit.point.z);
                            }   
                        }
                    }   
                }
            }   
        }
    }

    // for game rating
    private void AddGameRateListener()
    {
        // game rate controller
        gameRateController.OnClickRateNow += OnClickRateNow;
        gameRateController.OnClickRateLater += OnClickRateLater;
        gameRateController.OnClickDontShowRate += OnClickDontShowRate;
    }

    private void RemoveGameRateListener()
    {
        // game rate controller
        gameRateController.OnClickRateNow -= OnClickRateNow;
        gameRateController.OnClickRateLater -= OnClickRateLater;
        gameRateController.OnClickDontShowRate -= OnClickDontShowRate;
        gameRateController = null;
    }

    // game rating event handlers
    private void OnClickRateNow()
    {
        gameDataManager.SetHasRate(1);
        uiPanelController.ShowHideGameOverPanel(true);
    }

    private void OnClickRateLater()
    {           
        uiPanelController.ShowHideGameOverPanel(true);
    }

    private void OnClickDontShowRate()
    {
        gameDataManager.SetDontShowRate(1);
        uiPanelController.ShowHideGameOverPanel(true);
    }
    // game rating event handlers
    // game rating

    // starts the game
    public void StartGame()
    {
        //random change ground material
        if (groundController != null && dataController != null)
        {
            if (dataController.isRandomBackground)
            {
                groundController.RandomChangeMaterial();		
            }
        }

        adsHelper.SetIsAlreadyOfferRewardedAd(false);
        uiPanelController.OnOffRaycast(false);
        uiManager.ShowHideUI(true);
        uiManager.ResetHPBar();

        // resets most of the stuffs

        // this one will reset the showing of rating
        gameDataManager.SetJustShowRateUs(0);

        //set game mode
        dataController.SetGameMode(gameDataManager.GetGameMode());

        // reset score
        gameDataManager.SetScore(0);
        gameDataManager.SetIsNewBestScore(false);

        //reset combo
        gameDataManager.SetCombo(0);

        //update game count
        gameDataManager.UpdateGameCount(1);
        gameDataManager.UpdateTotalGameCount(1);
        playerPrefDataService.SaveData(PlayerDataKey.TOTAL_GAME_COUNT, gameDataManager.GetTotalGameCount());

        uiManager.SetScore(gameDataManager.GetScore());
        gameDataManager.SetDuration(DateTime.Now.Millisecond);
        SetGameState(GameState.PLAY);

        // active bug spawner and prepare wave data
        bugSpawner.Activate();
        waveManager.UpdateWaveModeData();
        waveManager.Activate();

        adsHelper.ShowHideBannerAds(true);

        gameEventManager.DispatchGameStartComplete();
    }

    // paused the game
    public void PausedGame()
    {
        // to prevent to kill the bugs panel will handle the tap
        uiPanelController.OnOffRaycast(true);
        uiManager.ShowHideUI(false);
        bugSpawner.DeActivate();
        waveManager.DeActivate();

        gameEventManager.DispatchGamePausedComplete();
    }

    // starts the countdown timer for resume
    public void StartResumeCountdown()
    {	        
        uiManager.StartResumeCountDown();
        gameEventManager.DispatchGameResumeCountdownStart();
    }

    // resume game when paused
    public void ResumeGame()
    {		
        // remove the raycast collider of panel to allow to kill the bugs again
        uiPanelController.OnOffRaycast(false);
        SetGameState(GameState.PLAY);
        bugSpawner.Activate();
        waveManager.Activate();
        uiManager.ShowHideUI(true);

        gameEventManager.DispatchGameResumeComplete();
    }

    // restarts the game
    public void RestartGame()
    {
        if (GetGameState() == GameState.GAME_OVER)
        {
            StartGame();
        }
        else if (GetGameState() == GameState.PAUSED)
        {
            // deactivate wave manager spawn
            waveManager.DeActivate();
            // reset wave data
            waveManager.ResetWaveObject();

            // stop spawing of bugs
            bugSpawner.StopSpawn();
            // clean bug data
            bugSpawner.Clean();
            // set the game state

            StartGame();
        }

        gameEventManager.DispatchGameRestartComplete();
    }

    // continue game over
    private void ContinueGameOver()
    {
        SetGameState(GameState.PLAY);
        GameOver();
    }

    // calls when player hp reached zero
    public void GameOver()
    {
        // check if video rewarded ad is available or can be show
        if (GameConfig.HAS_REWARDED_BASED_VIDEO_ADS && adsHelper.IsUnity3dRewardedAdReady()
            && !adsHelper.GetIsAlreadyOfferRewardedAd())
        {            
            PausedGame();
            uiPanelController.ShowHideRewardBasedVideoPanel(true);
            adsHelper.SetIsAlreadyOfferRewardedAd(true);
        }
        else
        {
            // trigger the actual game over
            if (GetGameState() == GameState.PLAY)
            {
                // post score
                SaveScore();
                // update total bug kill
                gameDataManager.UpdateTotalBugKill(gameDataManager.GetBugKill());
                // save total bug kill
                playerPrefDataService.SaveData(PlayerDataKey.BUG_TOTAL_KILL, gameDataManager.GetTotalBugKill());
                commonServiceHelper.UpdateAchievement();

                waveManager.DeActivate();
                waveManager.ResetWaveObject();

                bugSpawner.StopSpawn();
                bugSpawner.Clean();

                uiPanelController.OnOffRaycast(true);
                uiManager.ShowHideUI(false);
                uiPanelController.ShowHideGameOverPanel(true);

                if (gameDataManager.GetJustShowRateUs() == 0)
                {
                    
                    if (gameDataManager.GetHasRate() == 1 || gameDataManager.GetDontShowRate() == 1)
                    {                    
                        // check if can show inter stitial ads
                        adsHelper.CheckIfNeedToShowIntertitial();
                    }
                    else
                    {
                        // check if can show rate us panel
                        if (gameDataManager.GetTotalGameCount() % gameRateController.GetTotalGameCount() == 0 &&
                            gameRateController.CanShowRate())
                        {                        
                            uiPanelController.ShowHideGameOverPanel(false);
                            SetGameState(GameState.RATE);
                            gameRateController.ShowHide(true);
                            gameDataManager.SetJustShowRateUs(1);
                        }
                        else
                        {                        
                            adsHelper.CheckIfNeedToShowIntertitial(); 
                        }   
                    }   
                }
            }

            gameEventManager.DispatchGameOverComplete();
        }
    }

    public void SaveScore()
    {
        int currentScore = gameDataManager.GetScore();
        int currentBestScore = 0;

        // sets the highscore based on game mode
        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            currentBestScore = gameDataManager.GetBestScore();

            if (currentScore > currentBestScore)
            {
                // save to dynamic
                gameDataManager.SetBestScore(currentScore);
                gameDataManager.SetIsNewBestScore(true);
                // save to playerpref
                playerPrefDataService.SaveData(PlayerDataKey.CLASSIC_BEST_SCORE, currentScore);

            }
        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            currentBestScore = gameDataManager.GetKidsBestScore();

            if (currentScore > currentBestScore)
            {           
                gameDataManager.SetKidsBestScore(currentScore);
                gameDataManager.SetIsNewBestScore(true);
                playerPrefDataService.SaveData(PlayerDataKey.KIDS_BEST_SCORE, currentScore);
            }
        }

        commonServiceHelper.PostScore(currentScore);
    }

    // to quit the current game
    public void QuitGame()
    {
        // hide in game ui
        uiManager.ShowHideUI(false);

        // deactivate wave manager spawn
        waveManager.DeActivate();
        // reset wave data
        waveManager.ResetWaveObject();

        // stop spawing of bugs
        bugSpawner.StopSpawn();
        // clean bug data
        bugSpawner.Clean();
        // set the game state
        SetGameState(GameState.MAIN_MENU);

        gameEventManager.DispatchGameQuitComplete();
    }

    // exit or close this application
    public void QuitApplication()
    {        
        gameEventManager.DispatchGameQuitApplicationComplete();
        Debug.Log("QuitGame...!");
        Application.Quit();
    }

    // sets the game states of the game
    public void SetGameState(GameState gameState)
    {
        gameDataManager.SetGameState(gameState);
        dataController.SetGameState(gameState);
    }

    // gets the game state of the game
    public GameState GetGameState()
    {
        return gameDataManager.GetGameState();
    }

    private void OnGameStart()
    {
        StartGame();
    }

    private void OnGameRestartStart()
    {
        RestartGame();
    }

    private void OnGamePausedStart()
    {        
        PausedGame();
    }

    private void OnGameResumeStart()
    {
        StartResumeCountdown();
    }

    private void OnGameQuitStart()
    {
        QuitGame();
    }

    private void OnGameQuitApplicationStart()
    {
        QuitApplication();
    }

    // triggers when there's bug that change status
    private void OnBugStatusChange(BugEvent antEvent, BugController antController)
    {
        if (antEvent == BugEvent.DIED)
        {
            BugDied(antController);
        }
        else if (antEvent == BugEvent.ESCAPE)
        {
            BugEscape(antController);
        }
    }

    // bug event handlers
    // trigger when bug escape
    private void BugEscape(BugController antController)
    {
        if (GetGameState() == GameState.PLAY)
        {
            //reset combo
            gameDataManager.SetCombo(0);
            this.uiManager.SetCombo(0);


            // vibrate if the settings for vibration is on
            if (gameDataManager.GetVibration())
            {
                Handheld.Vibrate();	
            }
        }
    }

    // trigger when bug died
    private void BugDied(BugController antController)
    {
        // update scores
        gameDataManager.UpdateScore(GameConfig.SCORE_PER_ANT_KILL);
        // save score on preve score?
        playerPrefDataService.SaveData(PlayerDataKey.PREV_SCORE, gameDataManager.GetScore());

        // update the ui score
        int currentScore = gameDataManager.GetScore();
        this.uiManager.SetScore(currentScore);

        //update combo data
        gameDataManager.UpdateCombo(1);

        // update the combo ui
        int currentCombo = gameDataManager.GetCombo();
        this.uiManager.SetCombo(currentCombo - 1);

        // saving best combo for classic
        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            if (currentCombo > gameDataManager.GetBestCombo())
            {
                // save to dynamic
                gameDataManager.SetBestCombo(currentCombo);
                // save to playerpref
                playerPrefDataService.SaveData(PlayerDataKey.CLASSIC_BEST_COMBO, currentCombo);
            }
        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            // saving best combo for kids
            if (currentCombo > gameDataManager.GetKidsBestCombo())
            {
                // save to dynamic
                gameDataManager.SetKidsBestCombo(currentCombo);
                // save to playerpref
                playerPrefDataService.SaveData(PlayerDataKey.KIDS_BEST_COMBO, currentCombo);
            }
        }

        // updates bug kll data on each bug type
        gameDataManager.UpdateBugKill(1);

        if (antController.bugType == BugType.Worker)
        {
            gameDataManager.UpdateAntWorkerKill(1);
        }
        else if (antController.bugType == BugType.Warrior)
        {
            gameDataManager.UpdateAntWarriorKill(1);
        }
        else if (antController.bugType == BugType.Queen)
        {
            gameDataManager.UpdateAntQueenKill(1);
        }
        else if (antController.bugType == BugType.Spider)
        {
            gameDataManager.UpdateSpiderKill(1);
        }
        else if (antController.bugType == BugType.SmallSpider)
        {
            gameDataManager.UpdateSmallSpiderKill(1);
        }
        else if (antController.bugType == BugType.Cockroach)
        {
            gameDataManager.UpdateCockroachKill(1);
        }
    }
    // bug event handlers

    private void OnResumeCountDownComplete()
    {
        ResumeGame();
    }

    private void OnUpdatePlayerHP(float val)
    {
        if (val <= 0)
        {
            GameOver();
        }
    }

    // video rewarded ad event handlers
    // triggers when player click the watch video rewarded ad
    private void OnAcceptWatchRewardedAd()
    {
        if (adsHelper != null)
        {
            adsHelper.ShowHideRewardBasedVideoAdsAds(true);
        }
    }

    // triggers when player don't want to watch the rewarded video ad
    private void OnRejectWatchRewardedAd()
    {
        ContinueGameOver();
    }

    // triggers when player watch the rewarded video and finish watching it
    private void OnRewardExtraLives(bool val)
    {
        Debug.Log(TAG + " RewardExtraLives");
        // give full health
        //push back all the ants
        if (val)
        {
            Debug.Log("RewardExtraLives successful completed the video!!");
            bugSpawner.KnockBackAll(GameConfig.REWARDED_BASED_VIDEO_KNOCK_BACK_VALUE);
            uiManager.ResetHPBar();
            // show count down before resuming
            StartResumeCountdown();
        }
        else
        {
            Debug.Log("RewardExtraLives failed didn't complete the video!!");
            ContinueGameOver();
        }
    }
    // video rewarded ad event handlers
}