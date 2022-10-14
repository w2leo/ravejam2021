using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchController : MonoBehaviour, IPointerClickHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{ 
    [SerializeField] private CameraController camera;
    private Vector2 f0start, f1start;
    private bool zoomNow = false;
    private float zoomSpeed = 0.25f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        camera.CameraMove(new Vector3(eventData.delta.x, eventData.delta.y, 0));
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !eventData.dragging && !zoomNow)
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(eventData.position), Vector2.zero);
            if (hit.collider != null)
            {
                hit.collider.gameObject.GetComponent<TileGameObject>().PrintData();
            }
        }
    }
    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        if (!zoomNow)
        {
            camera.CameraMove(new Vector3(eventData.delta.x, eventData.delta.y, 0));
        }
 
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        
    }

    private void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            camera.CameraZoom(Input.mouseScrollDelta.y);
        }
        if (Input.touchCount < 2)
        {
            zoomNow = false;
            f0start = Vector2.zero;
            f1start = Vector2.zero;
        }
        if (Input.touchCount == 2)
        {
            zoomNow = true;
            TouchZoom(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }
    }
    private void TouchZoom(Vector2 f0, Vector2 f1)
    {
        Vector2 f0position = f0;
        Vector2 f1position = f1;
        if (f0start == Vector2.zero && f1start == Vector2.zero)
        {
            f0start = f0;
            f1start = f1;
        }
        float dir = Vector2.Distance(f0position, f1position) - Vector2.Distance(f1start, f0start);
        camera.CameraZoom(dir*Time.deltaTime*zoomSpeed);
    }
}
