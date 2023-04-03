using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameModePanelController : BasePanelController
{

    public Text titleText;

    public void ClickClassicGameMode()
    {
        gameDataManager.SetGameMode(GameMode.HARD);
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.CLASSIC);
        PlayClickSound();
    }

    public void ClickKidsGameMode()
    {
        gameDataManager.SetGameMode(GameMode.EASY);
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.KIDS);
        PlayClickSound();
    }

    public void ClickMainMenu()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.MAIN_MENU);
        PlayClickSound();
    }
}
