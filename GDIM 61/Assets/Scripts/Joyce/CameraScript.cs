using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Joyce Mai
public class CameraScript : MonoBehaviour
{
    public Transform player;
    //public BoxCollider2D myBoxCollider;

    public float smoothTime = 0.6f;
    private float xVelocity = 0.0f;
    private float yVelocity = 0.0f;

    private float top = 2.5f;
    private float bottom = -2.5f;
    private float right = 13f;
    private float left = -13f;

    void Start()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        float newPositionx = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref xVelocity, smoothTime);
        float newPositiony = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref yVelocity, smoothTime);
        if (newPositiony + Camera.main.orthographicSize > top)
        {
            //newPositiony = Mathf.SmoothDamp(transform.position.y, GeneralScript.top - Camera.main.orthographicSize, ref yVelocity, smoothTime);
            newPositiony = top - Camera.main.orthographicSize;
        }
        else if (newPositiony - Camera.main.orthographicSize < bottom)
        {
            //newPositiony = Mathf.SmoothDamp(transform.position.y, GeneralScript.bottom + Camera.main.orthographicSize, ref yVelocity, smoothTime);
            newPositiony = bottom + Camera.main.orthographicSize;
        }
        if (newPositionx + (Camera.main.aspect * Camera.main.orthographicSize) > right)
        {
            newPositionx = right - Camera.main.aspect * Camera.main.orthographicSize;
            //newPositionx = Mathf.SmoothDamp(transform.position.x, GeneralScript.right - Camera.main.aspect * Camera.main.orthographicSize, ref xVelocity, smoothTime);
        }
        else if (newPositionx - (Camera.main.aspect * Camera.main.orthographicSize) < left)
        {
            newPositionx = left + Camera.main.aspect * Camera.main.orthographicSize;
            //newPositionx = Mathf.SmoothDamp(transform.position.x, GeneralScript.left + Camera.main.aspect * Camera.main.orthographicSize, ref xVelocity, smoothTime);
        }

        transform.position = new Vector3(newPositionx, newPositiony, transform.position.z);
    }

    public void SnapCam(Transform borders)
    {
        Vector3 borderPos = borders.position;
        top = borderPos.y + (borders.localScale.y / 2);
        bottom = borderPos.y - (borders.localScale.y / 2);
        left = borderPos.x - (borders.localScale.x / 2);
        right = borderPos.x + (borders.localScale.x / 2);

        print("Border set: top: " + top + " bottom: " + bottom + " right: " + right + " left: " + left);
    }
}
