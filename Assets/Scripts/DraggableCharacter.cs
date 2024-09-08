using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableCharacter : DraggableWidget
{
    public override void OnPointerDown(PointerEventData pointerEventData)
    {
        if (initial)
        {
            original = transform.position;
            initial = false;
        }
        pos = Camera.main.WorldToScreenPoint(transform.position) - Input.mousePosition;
        transform.SetSiblingIndex(99);

        if (Input.GetMouseButtonDown(1))
            Copy();
        else if (Input.GetMouseButtonDown(2))
            transform.position = original;
    }
    void Copy()
    {
        GameObject go = Instantiate(gameObject);
        go.name = transform.name;
        go.transform.SetParent(transform.parent, false);
        go.transform.position = original;
        go.GetComponent<DraggableCharacter>().initial = true;

        transform.SetParent(container, false);
        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + pos);
        box.isTrigger = false;
        Destroy(GetComponent<DraggableCharacter>());
    }
    public override void OnDrag(PointerEventData data) { }    
}
