using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Camera cam;   

    [SerializeField] GraphicRaycaster graphicRayCaster;

    [SerializeField] GlobalSettings settings;

    [SerializeField] EventSystem eventSystem;

    [SerializeField] Transform rocket;

    [SerializeField] Transform topAnchor;
    [SerializeField] Transform bottomAnchor;

    PointerEventData pointerData;

    Vector3 dragOrigin;

    bool isDragging;

    bool isTrackingRocket;

    Vector3 positionBeforeTrackingRocket;

    Vector3 bottomAnchorOriginalPosition;

    void Awake()
    {      
        bottomAnchorOriginalPosition = bottomAnchor.position;
        pointerData = new PointerEventData(null);
    }



    // lower limit must be low enough to not 

    void OnEnable()
    {
        Tank.OnRocketLaunched += TrackRocket;
        Tank.OnRocketDone += StopTrackingRocket;
        MineCreator.OnActiveMinesChanged += MoveBottomAnchor;
    }

    void MoveBottomAnchor()
    {
        int mineCount = SaveManager.Data.activeMinesData.Count;
        bottomAnchor.position = bottomAnchorOriginalPosition - new Vector3(0f, settings.mineHeight * mineCount, 0f);
    }

    void StopTrackingRocket()
    {
        isTrackingRocket = false;
        transform.position = positionBeforeTrackingRocket;
    }


    void TrackRocket()
    {
        positionBeforeTrackingRocket = transform.position;
        isTrackingRocket = true;
    }


    void LateUpdate()
    {
        if (isTrackingRocket)
        {
            transform.position = transform.position.Set(y: rocket.position.y);
        }
        else
            PanCamera();
    }


    void PanCamera()
    {
        pointerData.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();

        graphicRayCaster.Raycast(pointerData, results);


        if (results.Count > 0)
        {
            isDragging = false;
            return;
        }


        //save position of mouse in world space when drag starts (first time clicked)
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        //calculate distance between drag origin and new position if it is still held down
        if (isDragging && Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            //move the camera by that distance

            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }

        if (Input.GetMouseButtonUp(0))
            isDragging = false;
    }


    Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHeight = cam.orthographicSize;
      //  float camWidth = cam.orthographicSize * cam.aspect;

        float minY = bottomAnchor.position.y + camHeight;
        float maxY = topAnchor.position.y - camHeight;

        if (Mathf.Abs(topAnchor.position.y - bottomAnchor.position.y) < cam.orthographicSize * 2f)
        {
            minY = topAnchor.position.y - cam.orthographicSize;
        }

        float newY = Mathf.Clamp(targetPosition.y, minY, maxY);

        return new Vector3(transform.position.x, newY, targetPosition.z);
    }


    void OnDisable()
    {
        Tank.OnRocketLaunched -= TrackRocket;
        Tank.OnRocketDone -= StopTrackingRocket;
    }

}
