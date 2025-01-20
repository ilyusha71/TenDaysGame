using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableUI : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
{
    public bool canDelete = false;
    public bool canCopy = false;
    public bool fixOrder = false;
    protected Vector3 pos;
    [Header("Store")]
    public bool isStore = false;
    public bool usePool = false;
    public Transform clonePool;
    public virtual void OnPointerDown(PointerEventData data) 
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (!fixOrder) SetSiblingIndex(99);
        if (Input.GetKey(KeyCode.Delete) && canDelete)
            Destroy(gameObject);
    }
    public virtual void OnBeginDrag(PointerEventData data)
    {    
        if (Input.GetMouseButton(1) && canCopy)
        {
            GameObject go = Instantiate(gameObject, transform.parent);
            go.transform.position = transform.position;
        }
        if (Input.GetMouseButton(0) && isStore) Clone(usePool);
        else if (Input.GetMouseButton(1) && isStore) Clone(true);
        pos = transform.position - Input.mousePosition;
    }
    public void OnDrag(PointerEventData data)
    {
        if (!isStore) transform.position = Input.mousePosition + pos;
    }
    protected virtual void Clone(bool leftButton)
    {
        GameObject go = Instantiate(gameObject, transform.parent);
        go.name = name;
        go.transform.position = transform.position;
        isStore = false;
        canDelete = true;
        if (leftButton)
        {
            transform.SetParent(clonePool);
            if (!fixOrder) SetSiblingIndex(99);
        }
    }
    protected virtual void SetSiblingIndex(int index) => transform.SetSiblingIndex(index);
}
