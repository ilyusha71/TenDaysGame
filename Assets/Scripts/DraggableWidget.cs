using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWidget : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Transform container;
    public bool initial = true;
    public bool copy = true;
    public bool trigger = true;
    protected Vector3 original;
    protected Vector3 pos;
    protected BoxCollider2D box;
    private void Awake()
    {
        box = GetComponent<BoxCollider2D>();
        box.isTrigger = true;
    }
    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        if (initial)
        {
            original = transform.position;
            initial = false;
        }
        pos = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
        transform.SetSiblingIndex(99);

        if (Input.GetMouseButtonDown(1) && copy)
            Copy();
        else if (Input.GetMouseButtonDown(2) && copy)
            transform.position = original;
    }
    void Copy()
    {
        GameObject go = Instantiate(gameObject);
        go.name = transform.name;
        go.transform.SetParent(transform.parent, false);
        go.transform.position = original;
        go.GetComponent<DraggableWidget>().initial = true;
        copy = false;
        box.isTrigger = trigger;

        transform.SetParent(container, false);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
    public virtual void OnDrag(PointerEventData data)
    {
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
    }
    public void BackOriginal()
    {
        if (!initial)
            transform.position = original;
    }
}
