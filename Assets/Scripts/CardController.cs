using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : DragChess
{
    [Header("Card Controller")]
    public GameObject face;
    public GameObject back;
    public GameObject front;
    //public bool random = false;
    public Image imgFace;
    public Sprite[] cardSprites;

    public bool playcard;
    public Transform cardPool;

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
        if ((Input.GetMouseButtonDown(2) || data.clickCount > 0) && cardSprites.Length > 0)
            imgFace.sprite = cardSprites[Random.Range(0, cardSprites.Length)];

        if (Input.GetMouseButtonDown(0) && store)
        {
            GameObject go = Instantiate(gameObject);
            go.name = name;
            go.transform.SetParent(transform.parent, false);
            go.transform.position = transform.position;

            transform.SetParent(clonePool);
            GetComponent<CardController>().store = false;
        }
        if (Input.GetMouseButtonDown(1) && back)
            Flop();
        if (Input.GetMouseButtonDown(1) && playcard)
            transform.SetParent(cardPool, true);
        if (Input.GetKey(KeyCode.Delete) && !store)
            Destroy(gameObject);

        if (!fixOrder)
            transform.SetSiblingIndex(99);
        pos = transform.position - Input.mousePosition;
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
}
