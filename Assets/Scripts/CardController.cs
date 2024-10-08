using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : DragChess
{
    [Header("Card Controller")]
    public GameObject front;
    public bool random = false;
    public Image imgFace;
    public Sprite[] cardSprites;
    public override void OnPointerDown(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (Input.GetMouseButtonDown(1) && copy)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.SetParent(transform.parent, false);
            go.transform.position = transform.position;
        }
        if (Input.GetMouseButtonDown(1) && front)
            front.SetActive(!front.activeSelf);
        if (Input.GetMouseButtonDown(2) && cardSprites.Length > 0)
            imgFace.sprite = cardSprites[Random.Range(0, cardSprites.Length)];

        if (!fixOrder)
            transform.SetSiblingIndex(99);
        pos = transform.position - Input.mousePosition;       
    }
}
