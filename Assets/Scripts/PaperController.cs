using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaperController : DraggableUI
{
    [Header("Paper Controller")]
    public GameObject[] contents;
    [HideInInspector] public int index;
    int level;
    private PaperController RightPaper { get; set; }
    public override void OnPointerDown(PointerEventData data)
    {
        base.OnPointerDown(data);
        if (Input.GetMouseButtonDown(1) && contents.Length > 0 && level != 2)
        {
            Upgrade();
           if(!isStore)
                RightPaper.Upgrade();
        }
    }
    protected override void Clone(bool leftButton)
    {
        if (!leftButton)
        {
            GameObject go = Instantiate(gameObject);
            go.name = name;
            go.transform.SetParent(transform.parent, false);
            go.transform.position = transform.position;
            RightPaper=go.GetComponent<PaperController>();
            isStore = false;
            canDelete = true;
            level++;
        }
    }
    public void Upgrade()
    {
        if (level == 2) return;
        index++;
        index = (int)Mathf.Repeat(index, contents.Length);
        for (int i = 0; i < contents.Length; i++)
        {
            contents[i].SetActive(false);
        }
        contents[index].SetActive(true);
        if (level == 1 && index == 3) level++;
    }
}
