using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{

    public int layer = 1;

    public static int layerOpen;

    public virtual void OpenPanel()
    {
        ClickManager.CallCloseAllPanels(layer);
        gameObject.SetActive(true);
    }




    protected virtual void OnEnable()
    {
        ClickManager.CloseAllPanels += ClosePanel;
        layerOpen = layer;
    }



    public virtual void ClosePanel(int layerClicked, UIPanel clickedPanel)
    {
        if (clickedPanel == this) return;

        if (layer >= layerClicked)
        {
            gameObject.SetActive(false);
        }
    }


    public void ClosePanel()
    {
        gameObject.SetActive(false);

    }

    protected virtual void OnDisable()
    {
        ClickManager.CloseAllPanels -= ClosePanel;
        if (layerOpen == layer)
            layerOpen -= 1;
    }

}
