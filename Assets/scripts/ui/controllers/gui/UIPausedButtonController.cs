using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPausedButtonController : BasePanelController
{

    public void ClickPaused()
    {
        uiEventManager.DispatchClickButton(uiPanelType, ButtonType.PAUSED);
        PlayClickSound();
    }
}
