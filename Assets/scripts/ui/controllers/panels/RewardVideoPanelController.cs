using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardVideoPanelController : BasePanelController
{
    public Text titleText;
    public Text questionText;
    public Text countDownText;

    private bool disableClick;

    public UICountdownController uiCountdownController;

    private void OnEnable()
    {
        disableClick = true;
        CancelInvoke("EnableClick");
        Invoke("EnableClick", GameConfig.REWARD_VIDEO_AD_CLICK_DELAY);

        uiCountdownController.OnCountDownComplete += OnCountDownComplete;
        uiCountdownController.StartCountDown();
    }

    private void OnDisable()
    {
        uiCountdownController.OnCountDownComplete -= OnCountDownComplete;
    }

    public void EnableClick()
    {
        disableClick = false;
    }

    public void ClickWatch()
    {
        if (disableClick)
        {
            return;
        }

        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.YES_WATCH_VIDEO_REWARD_AD);
        PlayClickSound();
    }

    public void ClickNo()
    {
        if (disableClick)
        {
            return;
        }

        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.NO_WATCH_VIDEO_REWARD_AD);
        PlayClickSound();
    }

    private void OnCountDownComplete()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.NO_WATCH_VIDEO_REWARD_AD);		
    }
}
