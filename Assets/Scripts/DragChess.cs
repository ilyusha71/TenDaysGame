using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragChess : MonoBehaviour, IPointerDownHandler,IDragHandler ,IDropHandler
{
    public bool copy =true;
    public bool reversible = false;
    public bool alphaHit = false;
    public bool fixOrder = false;
    public bool clear = false;

    public Sprite[] backup;
    int spriteIndex;
 protected   Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        if(!alphaHit)
            GetComponent<Image>().alphaHitTestMinimumThreshold = 0.3f;
        else
            SetAlphaHit();
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (Input.GetMouseButtonDown(1) && reversible)
        {
            transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        if (Input.GetMouseButtonDown(1) && copy)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.SetParent(transform.parent, false);
            go.transform.position = transform.position;
        }
        if (Input.GetMouseButtonDown(2) && backup.Length > 0)
        {
            spriteIndex++;
            spriteIndex = (int)Mathf.Repeat(spriteIndex, 2);
            GetComponent<Image>().sprite = backup[spriteIndex];
        }
        if (Input.GetMouseButtonDown(2) && clear)
        {
            float f = GetComponent<CanvasGroup>().alpha;
            f++;
            GetComponent<CanvasGroup>().alpha = Mathf.Repeat(f, 1);
        }
        if (!fixOrder)
            transform.SetSiblingIndex(99);
        pos= transform.position- Input.mousePosition; 
    }
    public void OnDrag(PointerEventData data)
    {
        transform.position = Input.mousePosition+ pos;
    }
    public void OnDrop(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    void SetAlphaHit()
    {
        Image[] img = GetComponentsInChildren<Image>();
        for (int i = 0; i < img.Length; i++)
        {
            try { img[i].alphaHitTestMinimumThreshold = 0.3f; }
            catch { }
        }
    }
}
