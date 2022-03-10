using System;
using UnityEngine;

public class Scaler : MonoBehaviour {
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.
    public float initorthoSize;
    public Camera mainCamera;
    public double limit_X, limit_Y;
    public Vector3 init_pos;

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        initorthoSize = mainCamera.orthographicSize;
        limit_X = initorthoSize * Screen.width / Screen.height;
        limit_Y = limit_X * Screen.width / Screen.height * Screen.height / Screen.width;
        init_pos = mainCamera.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update () {
        if (Input.touchCount == 1)
        {
            double tmp = mainCamera.orthographicSize;
            Touch touch = Input.GetTouch(0);

            if(Math.Abs(mainCamera.gameObject.transform.position.x) < -(300 / 185.7574)*(tmp-80) + 300
                && Math.Abs(mainCamera.gameObject.transform.position.y) < -(180 / 185.7574) * (tmp - 80) + 180)
            {
                mainCamera.gameObject.transform.position = mainCamera.gameObject.transform.position - (Vector3)touch.deltaPosition;
            }
            else
            {
                if (Math.Abs(mainCamera.gameObject.transform.position.x) < -(300 / 185.7574) * (tmp - 80) + 300)
                {
                    if (mainCamera.gameObject.transform.position.y < 0)
                    {
                        mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, (float)((180 / 185.7574) * (tmp - 80) - 180));
                    }
                    else
                    {
                        mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, (float)(-(180 / 185.7574) * (tmp - 80) + 180));
                    }
                }
                else if (Math.Abs(mainCamera.gameObject.transform.position.y) < -(180 / 185.7574) * (tmp - 80) + 180)
                {
                    if (mainCamera.gameObject.transform.position.x < 0)
                    {
                        mainCamera.gameObject.transform.position = new Vector3((float)(-1 * (-(300 / 185.7574) * (tmp - 80) + 300)), mainCamera.gameObject.transform.position.y);
                    }
                    else
                    {
                        mainCamera.gameObject.transform.position = new Vector3((float)(1 * (-(300 / 185.7574) * (tmp - 80) + 300)), mainCamera.gameObject.transform.position.y);
                    }
                }
                else
                {
                    if (mainCamera.gameObject.transform.position.y < 0 && mainCamera.gameObject.transform.position.x < 0)
                    {
                        mainCamera.gameObject.transform.position = new Vector3((float)(-1 * (-(300 / 185.7574) * tmp - 80) + 300), (float)(-1 * (-(180 / 185.7574) * (tmp - 80) + 180)));
                    }
                    else if(mainCamera.gameObject.transform.position.y < 0 && mainCamera.gameObject.transform.position.x >= 0)
                    {
                        mainCamera.gameObject.transform.position = new Vector3(mainCamera.gameObject.transform.position.x, (float)(-1 * (-(180 / 185.7574) * (tmp - 80) + 180)));
                    }
                    else if (mainCamera.gameObject.transform.position.y >= 0 && mainCamera.gameObject.transform.position.x < 0)
                    {
                        mainCamera.gameObject.transform.position = new Vector3((float)(-1 * (-(300 / 185.7574) * (tmp - 80) + 300)), mainCamera.gameObject.transform.position.y);
                    }
                    else
                    {
                        mainCamera.gameObject.transform.position = new Vector3((float)(1 * (-(300 / 185.7574) * (tmp - 80) + 300)), mainCamera.gameObject.transform.position.y);
                    }
                }
            }
        }

        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            if(mainCamera.orthographicSize + deltaMagnitudeDiff * orthoZoomSpeed < 80)
            {
                mainCamera.orthographicSize = 80;
            }
            else
            {
                if (deltaMagnitudeDiff > 0)
                {
                    mainCamera.gameObject.transform.position = init_pos;
                }
                // ... change the orthographic size based on the change in distance between the touches.
                mainCamera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                mainCamera.orthographicSize = Mathf.Max(mainCamera.orthographicSize, 0.1f);
            }

            if(mainCamera.orthographicSize > initorthoSize)
            {
                mainCamera.orthographicSize = initorthoSize;
            }
            else
            {
                if (deltaMagnitudeDiff > 0)
                {
                    mainCamera.gameObject.transform.position = init_pos;
                }
                // Otherwise change the field of view based on the change in distance between the touches.
                mainCamera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
