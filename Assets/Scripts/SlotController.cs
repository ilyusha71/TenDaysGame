using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class SlotController : MonoBehaviour, IPointerDownHandler
{
    public Animator  curve;
    public Transform[] slots;
    bool open = false;
    Vector3[] posSlots;
    Vector3 offsetY = new Vector3(0, -25, 0);
    void Awake()
    {
        posSlots = new Vector3[slots.Length];
        for (int i = 0; i < slots.Length; i++)
        {
            posSlots[i] = slots[i].position;
            slots[i].position += offsetY;
        }
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        open = !open;
        if (open) Open(); else Close();
    }
    void Open() 
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].DOMove(posSlots[i], 0.3f).SetEase(Ease.OutBack).SetDelay(0.05f * i);
        }
    }
    void Close()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            try
            {
                slots[i].GetComponentInChildren<DraggableWidget>().BackOriginal();
            }
            catch { }
            slots[i].DOMove(posSlots[i]+offsetY, 0.3f).SetEase(Ease.InBack).SetDelay(0.05f * (slots.Length-1-i));
        }
    }
}
