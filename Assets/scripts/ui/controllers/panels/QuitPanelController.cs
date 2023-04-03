using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class QuitPanelController : BasePanelController
{
    public Text questionText;

    public void ClickYes()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.YES_QUIT);
        PlayClickSound();
    }

    public void ClickNo()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.NO_QUIT);
        PlayClickSound();
    }
}

