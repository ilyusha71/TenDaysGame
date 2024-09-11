using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWidgetUI : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public bool copy = true;
    protected Vector3 pos;
    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        pos = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
        transform.SetSiblingIndex(99);
        if (Input.GetMouseButtonDown(0) && copy)
            Copy();
    }
    void Copy()
    {
        GameObject go = Instantiate(gameObject);
        go.name = transform.name;
        go.transform.SetParent(transform.parent, false);
        go.transform.position = transform.position;
        copy = false;

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
    public virtual void OnDrag(PointerEventData data)
    {
        if (!copy)
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
}
