using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragChess : MonoBehaviour, IPointerDownHandler,IDragHandler ,IDropHandler
{
    public bool copy =true;
    public bool reversible = false;
    public Sprite[] backup;
    int spriteIndex;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    public void OnPointerDown(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (Input.GetMouseButtonDown(1) && reversible)
        {
            transform.localScale = new Vector3(-1* transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetMouseButtonDown(1) && copy)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.SetParent(transform.parent, false);
            go.transform.position=transform.position;
        }
        if (Input.GetMouseButtonDown(2) && backup.Length>0)
        {
            spriteIndex++;
            spriteIndex = (int)Mathf.Repeat(spriteIndex, 2);
            GetComponent<Image>().sprite = backup[spriteIndex];
        }
        transform.SetSiblingIndex(99);
        pos= transform.position- Input.mousePosition; ;
    }
    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition+ pos;
    }
    public void OnDrop(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }
}
