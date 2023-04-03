using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedPanelController : BasePanelController
{

    public void ClickResume()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.PAUSED_RESUME);
        PlayClickSound();
    }

    public void ClickRestart()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.PAUSED_RESTART);
        PlayClickSound();
    }

    public void ClickQuit()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.PAUSED_QUIT);
        PlayClickSound();
    }
}
