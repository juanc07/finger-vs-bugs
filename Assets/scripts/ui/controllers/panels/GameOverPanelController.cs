using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameOverPanelController : BasePanelController
{

    public Text gameTitleLabelText;
    public Text gameModeLabelText;

    public Text scoreLabelText;
    public Text scoreText;

    public Text bestScoreLabelText;
    public Text newBestScoreText;
    public Text bestScoreText;

    private bool disableClick;

    public void UpdateInfo()
    {
        int score = gameDataManager.GetScore();
        int bestScore = 0;

        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            bestScore = gameDataManager.GetBestScore();
        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            bestScore = gameDataManager.GetKidsBestScore();
        }

        // this is to prevent accidentally click any buttons on game over panel
        disableClick = false;

        if (scoreText != null)
        {
            scoreText.text = string.Format("{0}", score);
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = string.Format("{0}", bestScore);
        }

        if (gameModeLabelText != null)
        {
            gameModeLabelText.text = gameDataManager.GetGameMode().ToString();
        }		

        ShowHideNewBestScoreText(gameDataManager.GetIsNewBestScore());

        CancelInvoke("EnableClick");
        Invoke("EnableClick", GameConfig.GAME_OVER_CLICK_DELAY);
    }

    private void EnableClick()
    {
        disableClick = true;
    }

    private void ShowHideNewBestScoreText(bool val)
    {
        newBestScoreText.gameObject.SetActive(val);
    }

    public void ClickPlayAgain()
    {		
        // this is to prevent accidentally click any buttons on game over panel
        if (!disableClick)
        {
            return;
        }

        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.TRY_AGAIN);
        PlayClickSound();

    }

    public void ClickLeaderBoard()
    {
        // this is to prevent accidentally click any buttons on game over panel
        if (!disableClick)
        {
            return;
        }

        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.LEADER_BOARD);
        PlayClickSound();
    }

    public void ClickMainMenu()
    {
        // this is to prevent accidentally click any buttons on game over panel
        if (!disableClick)
        {
            return;
        }

        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.MAIN_MENU);
        PlayClickSound();
    }

    public void ClickShare()
    {
        // this is to prevent accidentally click any buttons on game over panel
        if (!disableClick)
        {
            return;
        }

        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.SHARE);
        PlayClickSound();
    }
}
