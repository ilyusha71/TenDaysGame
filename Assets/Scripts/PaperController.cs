using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaperController : DragChess
{
    [Header("Paper Controller")]
    public GameObject[] contents;
    int index;
    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                index++;
                index = (int)Mathf.Repeat(index, contents.Length);
                for (int i = 0; i < contents.Length; i++)
                {
                    contents[i].SetActive(false);
                }
                contents[index].SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                index--;
                index = (int)Mathf.Repeat(index, contents.Length);
                for (int i = 0; i < contents.Length; i++)
                {
                    contents[i].SetActive(false);
                }
                contents[index].SetActive(true);
            }
        }
    }
    public override void OnPointerDown(PointerEventData data)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        if (Input.GetMouseButtonDown(1) && copy)
        {
            GameObject go = Instantiate(gameObject);
            go.transform.SetParent(transform.parent, false);
            go.transform.position = transform.position;
        }
        if (Input.GetMouseButtonDown(2) && contents.Length > 0)
        {
            index++;
            index = (int)Mathf.Repeat(index, contents.Length);
            for (int i = 0; i < contents.Length; i++) 
            {
                contents[i].SetActive(false);
            }
            contents[index].SetActive(true);
        }

        if (!fixOrder)
            transform.SetSiblingIndex(99);
        pos = transform.position - Input.mousePosition;
    }
}
