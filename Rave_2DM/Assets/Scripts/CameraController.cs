using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector3 newPosition;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float zoomSpeed;

    private Camera camera = new Camera();

    private void Start()
    {
        newPosition = transform.position;
        moveSpeed = 10f;
        zoomSpeed = 0.25f;
        camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Vector3.Distance(newPosition, transform.position) > 0.01f)
            transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
        CheckPosition();
    }
    private void CheckPosition()
    {
        if (Math.Abs(transform.position.x) > Map.SizeX)
        {
            float newX = CountNewPosition(transform.position.x);
            newPosition.x = CountNewPosition(newPosition.x);
            transform.position = new Vector3 (newX, transform.position.y, transform.position.z);
        }
    }
    private float CountNewPosition(float positionX)
    {
        float sign = Math.Sign(positionX);
        return sign * (Math.Abs(positionX) - Map.SizeX);
    }

    public void CameraMove(Vector3 deltaPosition)
    {
        newPosition -= deltaPosition * Time.deltaTime;
    }

    public void CameraZoom(float deltaY)
    {
        float newSize = camera.orthographicSize - deltaY * zoomSpeed;
        if (newSize > 2 && newSize <= Map.SizeX / 1.0f)
            camera.orthographicSize = newSize;    
    }

    
}
