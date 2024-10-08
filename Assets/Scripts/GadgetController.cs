using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GadgetController : DragChess
{
    [Header("Gadget Controller")]
    public bool countdown = false;
    public Text value;
    int max;
    private void Awake()
    {
        max = int.Parse(value.text)+1;
    }
    public override void OnPointerDown(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (Input.GetMouseButtonDown(1) && countdown)
        {
            int i = int.Parse(value.text);
            i--;
            i = (int)Mathf.Repeat(i, max);
            value.text = i.ToString();
        }  

        if (!fixOrder)
            transform.SetSiblingIndex(99);
        pos = transform.position - Input.mousePosition;
    }
}
