using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{

    [SerializeField]
    private Transform leftAnchor, rightAnchor;


    private Camera cam;


    public bool scaleEveryFrame;



    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Start()
    {
        ScaleCamera();
    }




    public float screenAspect, screenWdith, campixelwidth;

    private void Update()
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
    private void ScaleCamera()
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
