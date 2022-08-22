using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseDragRotate : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public float rotSpeed = 0.01f;
    Vector2 myPos,currentMousePos;

    public void OnBeginDrag(PointerEventData eventData)
    {
    }

    public void OnDrag(PointerEventData eventData)
    {
        myPos = this.transform.parent.position;
        currentMousePos = Input.mousePosition;
        float angle = Mathf.Atan2(currentMousePos.y - myPos.y, currentMousePos.x - myPos.x) * Mathf.Rad2Deg;
        this.transform.parent.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        
        //ó�� �巡�� �����Ҷ� ���콺 ��ġ - ���� ��ġ
        //transform.parent.Rotate(0, 0, (initialMousePos.x - currentMousePos.x) * rotSpeed);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }

 
}
