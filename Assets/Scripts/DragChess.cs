using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragChess : MonoBehaviour, IDropHandler
{
    public bool copy =true;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    public void OnClick()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (Input.GetMouseButtonDown(1)&& copy)
            {
            GameObject go = Instantiate(gameObject);
            go.transform.SetParent(transform.parent, false);
            go.transform.position=transform.position;
        }
        transform.SetSiblingIndex(99);
        pos= transform.position- Input.mousePosition; ;
    }
    public void Drag()
    {
        transform.position = Input.mousePosition+ pos;
    }
    public void OnDrop(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
