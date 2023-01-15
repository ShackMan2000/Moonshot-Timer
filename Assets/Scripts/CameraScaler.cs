using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{

    [SerializeField] Transform leftAnchor, rightAnchor;


    Camera cam;


    public bool scaleEveryFrame;


    void Awake()
    {
        cam = GetComponent<Camera>();
    }

    void Start()
    {
        ScaleCamera();
    }




    public float screenAspect, screenWdith, campixelwidth;

    void Update()
    {

        screenWdith = Screen.width;
        campixelwidth = cam.pixelWidth;
        if (Input.GetKeyDown(KeyCode.X))
            ScaleCamera();

        if (scaleEveryFrame)
        {
            ScaleCamera();
            print("WARNING scaling every frame");
        }
    }



    [ContextMenu("Scale Camera")]
    void ScaleCamera()
    {
        screenAspect = (float)Screen.width / (float)Screen.height;
        cam.aspect = screenAspect;

        float anchorDistance = Mathf.Abs(leftAnchor.position.x - rightAnchor.position.x);
        float newHeight = anchorDistance / cam.aspect;
        cam.orthographicSize = newHeight / 2f;


        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        float newXPos = leftAnchor.position.x + width / 2f;
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);


    }







}
