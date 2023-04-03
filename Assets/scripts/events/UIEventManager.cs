using UnityEngine;
using System.Collections;
using System;

public class UIEventManager : MonoBehaviour
{

    private static UIEventManager instance;
    private static GameObject container;

    private Action <float> UpdatePlayerHP;

    public event Action <float>OnUpdatePlayerHP
    {
        add{ UpdatePlayerHP += value; }
        remove{ UpdatePlayerHP -= value; }
    }


    private Action <int> UpdateCombo;

    public event Action <int>OnUpdateCombo
    {
        add{ UpdateCombo += value; }
        remove{ UpdateCombo -= value; }
    }

    private Action <float> MusicValueChange;

    public event Action <float>OnMusicValueChange
    {
        add{ MusicValueChange += value; }
        remove{ MusicValueChange -= value; }
    }

    private Action <float> SoundValueChange;

    public event Action <float>OnSoundValueChange
    {
        add{ SoundValueChange += value; }
        remove{ SoundValueChange -= value; }
    }

    private Action <int> VibrationValueChange;

    public event Action <int>OnVibrationValueChange
    {
        add{ VibrationValueChange += value; }
        remove{ VibrationValueChange -= value; }
    }

    private Action <UIPanelType,ButtonType> ClickButton;

    public event Action <UIPanelType,ButtonType>OnClickButton
    {
        add{ ClickButton += value; }
        remove{ ClickButton -= value; }
    }

    private Action ShowQuitPopUp;

    public event Action OnShowQuitPopUp
    {
        add{ ShowQuitPopUp += value; }
        remove{ ShowQuitPopUp -= value; }
    }

    private Action ShowVideoRewardedAdPopUp;

    public event Action OnShowVideoRewardedAdPopUp
    {
        add{ ShowVideoRewardedAdPopUp += value; }
        remove{ ShowVideoRewardedAdPopUp -= value; }
    }

    private Action ResumeCountDownComplete;

    public event Action OnResumeCountDownComplete
    {
        add{ ResumeCountDownComplete += value; }
        remove{ ResumeCountDownComplete -= value; }
    }

    private Action AcceptWatchRewardedAd;

    public event Action OnAcceptWatchRewardedAd
    {
        add{ AcceptWatchRewardedAd += value; }
        remove{ AcceptWatchRewardedAd -= value; }
    }

    private Action RejectWatchRewardedAd;

    public event Action OnRejectWatchRewardedAd
    {
        add{ RejectWatchRewardedAd += value; }
        remove{ RejectWatchRewardedAd -= value; }
    }

    private Action ShowLeaderBoard;

    public event Action OnShowLeaderBoard
    {
        add{ ShowLeaderBoard += value; }
        remove{ ShowLeaderBoard -= value; }
    }

    private Action ShowAchievement;

    public event Action OnShowAchievement
    {
        add{ ShowAchievement += value; }
        remove{ ShowAchievement -= value; }
    }

    private Action Share;

    public event Action OnShare
    {
        add{ Share += value; }
        remove{ Share -= value; }
    }

    private Action ShowIntertitialAd;

    public event Action OnShowIntertitialAd
    {
        add{ ShowIntertitialAd += value; }
        remove{ ShowIntertitialAd -= value; }
    }

    // panel events
    private Action <UIPanelType> ChangeUIPanel;

    public event Action <UIPanelType>OnChangeUIPanel
    {
        add{ ChangeUIPanel += value; }
        remove{ ChangeUIPanel -= value; }
    }
    // panel events


    public static UIEventManager GetInstance()
    {
        if (instance == null)
        {
            container = new GameObject();
            container.name = "UIEventManager";
            instance = container.AddComponent(typeof(UIEventManager)) as UIEventManager;
            DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    }

    // Use this for initialization
    void Start()
    {

    }

    public void DispatchUpdatePlayerHP(float val)
    {
        if (null != UpdatePlayerHP)
        {
            UpdatePlayerHP(val);
        }
    }

    public void DispatchUpdateCombo(int val)
    {
        if (null != UpdateCombo)
        {
            UpdateCombo(val);
        }
    }

    public void DispatchMusicValueChange(float val)
    {
        if (null != MusicValueChange)
        {
            MusicValueChange(val);
        }
    }

    public void DispatchSoundValueChange(float val)
    {
        if (null != SoundValueChange)
        {
            SoundValueChange(val);
        }
    }

    public void DispatchVibrationValueChange(int val)
    {
        if (null != VibrationValueChange)
        {
            VibrationValueChange(val);
        }
    }

    public void DispatchClickButton(UIPanelType panelType, ButtonType buttonType)
    {
        if (null != ClickButton)
        {
            ClickButton(panelType, buttonType);
        }
    }

    public void DispatchShowQuitPopUp()
    {
        if (null != ShowQuitPopUp)
        {
            ShowQuitPopUp();
        }
    }

    public void DispatchShare()
    {
        if (null != Share)
        {
            Share();
        }
    }

    public void DispatchAcceptRewardedAd()
    {
        if (null != AcceptWatchRewardedAd)
        {
            AcceptWatchRewardedAd();
        }
    }

    public void DispatchRejectRewardedAd()
    {
        if (null != RejectWatchRewardedAd)
        {
            RejectWatchRewardedAd();
        }
    }

    public void DispatchShowLeaderboard()
    {
        if (null != ShowLeaderBoard)
        {
            ShowLeaderBoard();
        }
    }

    public void DispatchShowAchievement()
    {
        if (null != ShowAchievement)
        {
            ShowAchievement();
        }
    }

    public void DispatchShowInterstitialAd()
    {
        if (null != ShowIntertitialAd)
        {
            ShowIntertitialAd();
        }
    }

    public void DispatchShowVideoRewardedAdPopUp()
    {
        if (null != ShowVideoRewardedAdPopUp)
        {
            ShowVideoRewardedAdPopUp();
        }
    }

    public void DispatchResumeCountDownComplete()
    {
        if (null != ResumeCountDownComplete)
        {
            ResumeCountDownComplete();
        }
    }

    public void DispatchChangeUIPanel(UIPanelType uiPanelType)
    {
        if (null != ChangeUIPanel)
        {
            ChangeUIPanel(uiPanelType);
        }
    }

}
