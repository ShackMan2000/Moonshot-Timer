using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickManager : MonoBehaviour
{
    [SerializeField] Camera cam;


    [SerializeField] GraphicRaycaster graphicRayCaster;

    [SerializeField] EventSystem eventSystem;

    PointerEventData pointerData;

    [SerializeField] bool debug;

    [SerializeField] MinePanel minePanel;


    void Awake()
    {
        pointerData = new PointerEventData(null);
    }


    void OnEnable()
    {
        Mine.OnMineClicked += OpenMinePanel;
    }

    public static event Action<int, UIPanel> CloseAllPanels = delegate { };


    void OpenMinePanel(Mine m)
    {
        minePanel.InjectMineData(m);
        minePanel.OpenPanel();
    }


    void Update()
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
                    if (clickedPanel != null)
                    {
                        CloseAllPanels(clickedPanel.layer, clickedPanel);
                        return;
                    }
                }

                if (debug)
                    Debug.Log("No UIPanel found in raycast results, but another UI element was hit.");
                
                return;
            }


            //No UI element hit, check for clickable objects

          //   Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            // RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            
           // Ray ray = Camera.main.ScreenPointToRay(mousePos);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

          //  RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(ray, Mathf.Infinity);


            if (hit)
            {
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
                
                if(debug)
                    Debug.Log("Clicked on " + hit.collider.name);
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


    void OnDisable()
    {
        Mine.OnMineClicked -= OpenMinePanel;
    }
}