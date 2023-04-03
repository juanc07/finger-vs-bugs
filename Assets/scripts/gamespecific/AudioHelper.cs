using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHelper:ICleanable
{

    //private IAudio audioService;
    private ISave saveDataService;

    private BGM[] bgmSet = { BGM.HeartofWarrior, BGM.MattOglseby, BGM.SpinningMonkeys };
    private SFX[] sfxGruntSet = { SFX.Grunt1, SFX.Grunt2, SFX.Grunt3 };
    private SFX[] sfxGetReadySet = { SFX.GetReady1, SFX.GetReady2, SFX.GetReady3 };
    private SFX[] sfxGameOverSet = { SFX.GameOver1, SFX.GameOver2, SFX.GameOver3, SFX.GameOver4 };


    private GameDataManager gameDataManager;
    private GameEventManager gameEventManager;
    private GameState gameState;

    private UIEventManager uiEventManager;
    // ant events
    private BugEventManager antEventManager;
    private SoundManager soundManager;

    public AudioHelper(ISave saveDataService)
    {
        //this.audioService = audioService;
        this.saveDataService = saveDataService;
        Init();
    }

    private void Init()
    {
        AddEventListener();

        // load the music  volume save data from playerpref
        float bgmVolume = saveDataService.LoadFloatSaveData(PlayerDataKey.MUSIC_VOLUME);
        if (bgmVolume > 0)
        {
            soundManager.SetMainBGMVolume(bgmVolume);
        }
        else
        {
            soundManager.SetMainBGMVolume(GameConfig.MUSIC_DEFAULT_VOLUME);
            saveDataService.SaveData(PlayerDataKey.MUSIC_VOLUME, GameConfig.MUSIC_DEFAULT_VOLUME);
        }

        // load the sfx  volume save data from playerpref
        float sfxVolume = saveDataService.LoadFloatSaveData(PlayerDataKey.SOUND_VOLUME);
        if (sfxVolume > 0)
        {
            soundManager.SetMainSFXVolume(sfxVolume);
        }
        else
        {
            soundManager.SetMainSFXVolume(GameConfig.SOUND_EFFECTS_DEFAULT_VOLUME);
            saveDataService.SaveData(PlayerDataKey.SOUND_VOLUME, GameConfig.SOUND_EFFECTS_DEFAULT_VOLUME);
        }

        SetUpBGMAndSFX();
    }

    private void AddEventListener()
    {
        gameDataManager = GameDataManager.GetInstance();
        gameDataManager.OnGameStateChange += OnGameStateChange;

        soundManager = SoundManager.GetInstance();

        gameEventManager = GameEventManager.GetInstance();
        gameEventManager.OnGameInitComplete += OnGameInit;
        gameEventManager.OnGameStartComplete += OnGameStart;
        gameEventManager.OnGameRestartComplete += OnGameRestart;
        gameEventManager.OnGameResumeCountdownStart += OnGameResumeCountdown;
        gameEventManager.OnGameOverComplete += OnGameOver;
        gameEventManager.OnGameQuitComplete += OnGameQuit;
        
        uiEventManager = UIEventManager.GetInstance();
        uiEventManager.OnUpdatePlayerHP += OnUpdatePlayerHP;
        uiEventManager.OnUpdateCombo += OnUpdateCombo;
        uiEventManager.OnShowVideoRewardedAdPopUp += OnShowVideoRewardedAdPopUp;

        // the ant event manager for the ants
        antEventManager = BugEventManager.GetInstance();
        antEventManager.OnBugStatusChange += OnAntStatusChange;
    }

    private void RemoveEventListener()
    {
        gameDataManager.OnGameStateChange -= OnGameStateChange;

        gameEventManager.OnGameInitComplete -= OnGameInit;
        gameEventManager.OnGameStartComplete -= OnGameStart;
        gameEventManager.OnGameRestartComplete -= OnGameRestart;
        gameEventManager.OnGameResumeCountdownStart -= OnGameResumeCountdown;
        gameEventManager.OnGameOverComplete -= OnGameOver;
        gameEventManager.OnGameQuitComplete -= OnGameQuit;
        gameEventManager = null;
        
        uiEventManager.OnUpdatePlayerHP -= OnUpdatePlayerHP;
        uiEventManager.OnUpdateCombo -= OnUpdateCombo;
        uiEventManager.OnShowVideoRewardedAdPopUp -= OnShowVideoRewardedAdPopUp;
        uiEventManager = null;

        antEventManager.OnBugStatusChange -= OnAntStatusChange;
        antEventManager = null;
    }

    public void Clean()
    {
        RemoveEventListener();
    }

    // this setup is annoying maybe we could move this to other class
    // like setup class thingy????
    private void SetUpBGMAndSFX()
    {
        // sets the bgm for each bgm music
        soundManager.SetBGMVolume(BGM.HeartofWarrior, 0.35f);
        soundManager.SetBGMVolume(BGM.MattOglseby, 0.35f);
        soundManager.SetBGMVolume(BGM.SpinningMonkeys, 0.35f);
        soundManager.SetBGMVolume(BGM.TemptingSecrets, 0.25f);

        // set the sfx volume for each sound effects
        soundManager.SetSFXVolume(SFX.Squash, 0.6f);
        soundManager.SetSFXVolume(SFX.PunchOrWhack, 0.4f);

        soundManager.SetSFXVolume(SFX.Grunt1, 0.5f);
        soundManager.SetSFXVolume(SFX.Grunt2, 0.5f);
        soundManager.SetSFXVolume(SFX.Grunt3, 0.5f);

        soundManager.SetSFXVolume(SFX.EnergyLow1, 0.6f);
        soundManager.SetSFXVolume(SFX.Danger1, 0.6f);
        soundManager.SetSFXVolume(SFX.Danger2, 0.6f);

        soundManager.SetSFXVolume(SFX.Incoming2, 0.3f);
        soundManager.SetSFXVolume(SFX.Incoming3, 0.3f);
        soundManager.SetSFXVolume(SFX.Incoming5, 0.6f);

        soundManager.SetSFXVolume(SFX.GetReady1, 0.5f);
        soundManager.SetSFXVolume(SFX.GetReady2, 0.5f);
        soundManager.SetSFXVolume(SFX.GetReady3, 0.5f);

        soundManager.SetSFXVolume(SFX.GameOver1, 0.5f);
        soundManager.SetSFXVolume(SFX.GameOver2, 0.5f);
        soundManager.SetSFXVolume(SFX.GameOver3, 0.5f);
        soundManager.SetSFXVolume(SFX.GameOver4, 0.5f);

        soundManager.SetSFXVolume(SFX.Combo, 0.5f);
        soundManager.SetSFXVolume(SFX.ComboMonster, 0.5f);
        soundManager.SetSFXVolume(SFX.ComboSuper, 0.5f);
        soundManager.SetSFXVolume(SFX.ComboUltra, 0.5f);
        soundManager.SetSFXVolume(SFX.ComboMega, 0.5f);

        soundManager.SetSFXVolume(SFX.Tap, 0.5f);
        soundManager.SetSFXVolume(SFX.Checkboxhit, 0.5f);
    }

    public void PlayNoneInGameBGM()
    {
        soundManager.StopAllBGM();
        soundManager.PlayBGM(BGM.TemptingSecrets, true);
    }

    public void PlayRandomBGM()
    {
        soundManager.PlayRandomBGM(bgmSet, true);
    }

    public void PlayRandomGrunt()
    {
        soundManager.PlayRandomSfx(sfxGruntSet);
    }

    public void PlayRandomGameOver()
    {
        soundManager.PlayRandomSfx(sfxGameOverSet);
    }

    public void PlayRandomGetReady()
    {
        soundManager.PlayRandomSfx(sfxGetReadySet);
    }

    public void PlayCombo(int combo)
    {
        if (combo == 700)
        {
            soundManager.PlaySfx(SFX.ComboUltra);
        }
        else if (combo == 500)
        {
            soundManager.PlaySfx(SFX.ComboMega);
        }
        else if (combo == 300)
        {	
            soundManager.PlaySfx(SFX.ComboSuper);
        }
        else if (combo == 100)
        {
            soundManager.PlaySfx(SFX.ComboMonster);
        }
        else if (combo == 50)
        {
            soundManager.PlaySfx(SFX.Combo);
        }
    }

    private void ChangeBGMToPlay()
    {
        if (this.gameState == GameState.MAIN_MENU)
        {
            PlayNoneInGameBGM();
        }
    }

    private void OnGameStateChange(GameState gameState)
    {
        this.gameState = gameState;
        ChangeBGMToPlay();
    }

    private void OnGameInit()
    {
        PlayNoneInGameBGM();
    }

    private void OnGameStart()
    {
        // stop all playing bgm
        soundManager.StopAllBGM();
        PlayRandomBGM();
        PlayRandomGetReady();
    }

    private void OnGameRestart()
    {
        // stop all playing bgm
        //audioService.StopAllBGM();
    }

    private void OnGameResumeCountdown()
    {
        // stop all playing bgm
        soundManager.StopAllBGM();
        PlayRandomBGM();
        PlayRandomGetReady();
    }

    private void OnGameOver()
    {
        // stop all playing bgm
        soundManager.StopAllBGM();
        PlayRandomGameOver();
    }

    private void OnGameQuit()
    {
        // stop all playing bgm
        //audioService.StopAllBGM();
    }

    private void OnUpdatePlayerHP(float val)
    {
        //Debug.Log( " UpdatePlayerHP val "  + val);
        if (val == 0.6f)
        {
            soundManager.PlaySfx(SFX.EnergyLow1);
        }
        else if (val == 0.4f)
        {
            soundManager.PlaySfx(SFX.Danger1);
        }
        else if (val == 0.1f)
        {
            soundManager.PlaySfx(SFX.Danger2);
        }
    }

    private void OnUpdateCombo(int combo)
    {
        PlayCombo(combo);
    }

    private void OnShowVideoRewardedAdPopUp()
    {
        soundManager.StopAllBGM();
    }

    private void OnAntStatusChange(BugEvent antEvent, BugController antController)
    {
        if (antEvent == BugEvent.SPAWN)
        {
            AntSpawn(antController);
        }
        else if (antEvent == BugEvent.HIT)
        {
            AntHit(antController);
        }
        else if (antEvent == BugEvent.DIED)
        {
            AntDied(antController);
        }
        else if (antEvent == BugEvent.ESCAPE)
        {
            AntEscape(antController);
        }
    }

    private void AntSpawn(BugController antController)
    {
        if (antController.bugType == BugType.Spider)
        {
            soundManager.PlaySfx(SFX.Incoming3);
        }
        else if (antController.bugType == BugType.Queen)
        {
            soundManager.PlaySfx(SFX.Incoming5);
        }
        else if (antController.bugType == BugType.Cockroach)
        {
            soundManager.PlaySfx(SFX.Incoming2);
        }
    }

    private void AntHit(BugController antController)
    {
        if (antController.hp > 0)
        {           
            soundManager.PlaySfx(SFX.PunchOrWhack);
        }
    }

    private void AntDied(BugController antController)
    {
        // play sfx when bugs died
        soundManager.PlaySfx(SFX.Squash);
    }

    private void AntEscape(BugController antController)
    {
        if (this.gameState == GameState.PLAY)
        {
            PlayRandomGrunt();
        }
    }
}
