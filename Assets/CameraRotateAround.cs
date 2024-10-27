using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRotateAround : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float sensitivity = 3;
    [SerializeField] private float limit = 50;
    [SerializeField] private float zoom = 0.25f;
    [SerializeField] private float zoomMax = 10;
    [SerializeField] private float zoomMin = 3;
    [SerializeField] bool clipCamera;
    private float X, Y;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        limit = Mathf.Abs(limit);
        if (limit > 90) limit = 90; 
        offset = new Vector3(offset.x, offset.y, -Mathf.Abs(zoomMax) / 2);
        transform.position = target.position + offset;
    }
    void Update()
    {
            if (Input.GetAxis("Mouse ScrollWheel") > 0) offset.z += zoom;
            else if (Input.GetAxis("Mouse ScrollWheel") < 0) offset.z -= zoom; 
            offset.z = Mathf.Clamp(offset.z, zoomMin, zoomMax);
            X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;
            Y += Input.GetAxis("Mouse Y") * sensitivity;
            Y = Mathf.Clamp(Y, -limit, limit); 
            transform.localEulerAngles = new Vector3(-Y, X, 0);
            Vector3 negDistance = new Vector3(-offset.x, -offset.y, -offset.z);
            transform.position = transform.localRotation * negDistance + target.position;

            if (clipCamera)
            {
                RaycastHit hitInfo;

                var ray =
                    new Ray(target.position, transform.position - target.position);

                var hit = Physics.Raycast(ray, out hitInfo, offset.z);

                if (hit)
                {
                    transform.position = hitInfo.point;
                }
            }
            Debug.LogFormat("rotateAround:{0}", offset.z);
        
    }
}
