using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragChess : MonoBehaviour, IPointerDownHandler,IDragHandler ,IDropHandler
{
    [Header("Store")]
    public bool store = false;
    public Transform clonePool;
    [Header("General")]
    public bool copy = true;
    public bool reversible = false;
    public bool hasAlphaHit = false;
    public float alphaHitThreshold = 0.3f;
    public bool hasFullAlphaHit = false;
    public bool fixOrder = false;
    public bool clear = false;
    [Header("General")]
    public bool hasMark;
    public GameObject mark;

    public Sprite[] backup;
    int spriteIndex;
    protected Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
        if (hasAlphaHit)
            GetComponent<Image>().alphaHitTestMinimumThreshold = alphaHitThreshold;
        //if (!hasFullAlphaHit)
        //    GetComponent<Image>().alphaHitTestMinimumThreshold = 0.3f;
        //else
        //    SetAlphaHit();
    }

    public virtual void OnPointerDown(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);

        if (Input.GetMouseButtonDown(1) && hasMark)
            mark.SetActive(!mark.activeSelf);
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
        if (data.clickCount > 0 && backup.Length > 0)
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

        if (Input.GetMouseButtonDown(0) && store)
        {
            GameObject go = Instantiate(gameObject);
            go.name = name;
            go.transform.SetParent(transform.parent, false);
            go.transform.position = transform.position;

            transform.SetParent(clonePool);
            GetComponent<DragChess>().store = false;
        }
        if (Input.GetKey(KeyCode.Delete) && !store)
            Destroy(gameObject);

        if (!fixOrder)
            transform.SetSiblingIndex(99);
        pos= transform.position- Input.mousePosition; 
    }
    public void OnDrag(PointerEventData data)
    {
        if(!store)
            transform.position = Input.mousePosition+ pos;
    }
    public void OnDrop(PointerEventData data)
    {
        //EventSystem.current.SetSelectedGameObject(null);
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
