using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{


    [SerializeField]
    private Camera cam;


    [SerializeField]
    private GraphicRaycaster graphicRayCaster;

    [SerializeField]
    private EventSystem eventSystem;

    private PointerEventData pointerData;


    [SerializeField]
    private MinePanel minePanel;

 
    private void Awake()
    {
        pointerData = new PointerEventData(null);
    }


    private void OnEnable()
    {
        Mine.OnMineClicked += OpenMinePanel;
    }

    public static event Action<int, UIPanel> CloseAllPanels = delegate { };



    private void OpenMinePanel(Mine m)
    {
        minePanel.InjectMineData(m);
        minePanel.OpenPanel();
    }



    



    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            //Hitting a UI element?
            pointerData.position = Input.mousePosition;
            List<RaycastResult> results = new List<RaycastResult>();
            graphicRayCaster.Raycast(pointerData, results);

            if (results.Count > 0)
            {
                //find the first one that is a ui panel, and close everything with a higher layer.
                for (int i = 0; i < results.Count; i++)
                {
                    UIPanel clickedPanel = results[i].gameObject.GetComponent<UIPanel>();
                    if(clickedPanel != null)
                    {
                        CloseAllPanels(clickedPanel.layer, clickedPanel);
                        return;
                    }
                }
                //no UI Panel was clicked, but mouse is over another UI element, so don't check for game objects and return
                return;
            }





            //No UI element hit, check for clickable objects

            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();


            }
            else
            {
                CloseAllPanels(0, null);
            }
        }
    }



    public static void CallCloseAllPanels(int layerOpened)
    {
        CloseAllPanels(layerOpened, null);
    }



    private void OnDisable()
    {
        Mine.OnMineClicked -= OpenMinePanel;
    }
}
