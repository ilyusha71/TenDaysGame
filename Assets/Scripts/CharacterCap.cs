using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterCap : DraggableUI, ISelectHandler, IDeselectHandler
{
    public Image[] rings;
    public Image takekoputa;
    public Image[] alphaHitTest;
    void Awake()
    {
        for (int i = 0; i < alphaHitTest.Length; i++) { alphaHitTest[i].alphaHitTestMinimumThreshold = 0.3f; }
    }
    private void OnEnable()
    {
        rings[1].enabled = false;
        takekoputa.enabled = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
        rings[1].enabled = true;
        takekoputa.enabled = true;
        if (!fixOrder) SetSiblingIndex(99);
    }
    public void OnDeselect(BaseEventData eventData)
    {
        rings[1].enabled = false;
        takekoputa.enabled = false;
    }
    public void Highlight()
    {
        rings[1].enabled = true;
        takekoputa.enabled = true;
        if (!fixOrder) SetSiblingIndex(99);
    }
    public void Unhighlight()
    {
        rings[1].enabled = false;
        takekoputa.enabled = false;
    }
    public void SetStore(Transform pool)
    {
        canDelete =true;
        isStore = true;
        usePool = true;
        clonePool = pool;
    }
    protected override void SetSiblingIndex(int index)
    {
        transform.SetSiblingIndex(index);
        transform.parent.SetSiblingIndex(index);
    }
}
