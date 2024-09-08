using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWidgetLite : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Transform container;
    public bool copy = true;
    public bool trigger = true;
    protected Vector3 pos;
    protected BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
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
        box.isTrigger = trigger;

        transform.SetParent(container, false);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
    public virtual void OnDrag(PointerEventData data)
    {
        if(!copy)
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
}
