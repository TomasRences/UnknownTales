using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCameraController : MonoBehaviour
{
    public Transform target;

    public Vector3 offset;
    public float currZoom=5f;
    public float zoomSpeed=4f;
    public float minZoom=5f;
    public float maxZoom=15f;
    
    public float pitch = 2f; 


    void Update() {
        currZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currZoom = Mathf.Clamp(currZoom, minZoom, maxZoom);
    }
    private void LateUpdate() {
        transform.position = target.position - offset *currZoom;
        transform.LookAt(target.position+Vector3.up* pitch);
    }
}
