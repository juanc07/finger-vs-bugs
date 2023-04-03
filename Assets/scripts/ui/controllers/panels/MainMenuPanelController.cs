using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MainMenuPanelController : BasePanelController
{

    public Text titleText;
    public Text versionText;

    private void OnEnable()
    {
        //Debug.Log("onEnable main menu");
        UpdateGameVersion();
    }

    private void UpdateGameVersion()
    {
        if (versionText != null)
        {
            versionText.text = GameConfig.GAME_VERSION;
            //Debug.Log("UpdateGameVersion main menu");
        }
    }

    public void ClickStartGame()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.GAME_MODE);
        PlayClickSound();
    }

    public void ClickHighScore()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.LEADER_BOARD);
        PlayClickSound();
    }

    public void ClickOption()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.OPTION);
        PlayClickSound();
    }
}
