using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : DraggableUI, ISelectHandler, IDeselectHandler
{
    [Header("Card Controller")]
    public Image halo;
    public GameObject face;
    public GameObject back;
    public bool playcard;
    public Transform cardPool;

    public override void OnPointerDown(PointerEventData data)
    {
        base.OnPointerDown(data);
        if (Input.GetMouseButtonDown(1) && back)
            Flop();
        if ((Input.GetMouseButtonDown(0)|| Input.GetMouseButtonDown(1)) && playcard)
            transform.SetParent(cardPool, true);
    }
    public void Open()
    {
        face.SetActive(true);
        back.SetActive(false);
    }
    public void Flop()
    {
        face.SetActive(!face.activeSelf);
        back.SetActive(!back.activeSelf);
    }
    public void Fold()
    {
        face.SetActive(false);
        back.SetActive(true);
    }
    public void PlayCard()
    {
        transform.SetParent(cardPool, true);
        face.SetActive(true);
        back.SetActive(false);
    }
    private void OnEnable()
    {
        if (halo) halo.enabled = false;
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (halo) halo.enabled = true;
    }
    public void OnDeselect(BaseEventData eventData)
    {
        if (halo) halo.enabled = false;
    }
}
