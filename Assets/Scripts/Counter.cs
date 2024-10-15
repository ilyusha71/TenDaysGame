using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Counter : MonoBehaviour, IPointerDownHandler
{
    public Text count;
    public int max;
    public bool add = false;
    private void Reset()
    {
        if (count == null) count = GetComponent<Text>();
        max = int.Parse(count.text)+1;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        int i = int.Parse(count.text);
        if (!add)
        {
            if (Input.GetMouseButton(0)) i--;
            else if (Input.GetMouseButtonDown(1)) i++;
        }
        else
        {
            if (Input.GetMouseButton(0)) i++;
            else if (Input.GetMouseButtonDown(1)) i--;
        }        
        i = (int)Mathf.Repeat(i, max);
        count.text = i.ToString();
    }
    public void Double()
    {
        count.enabled = true;
        max = (max - 1) * 2 + 1;
        count.text = (max - 1).ToString();
    }
    public void Half()
    {
        count.enabled = true;
        max = (int)((max - 1) * 0.5f + 1);
        count.text = (max - 1).ToString();
    }
    public void Hide()
    {
        count.enabled = false;
    }
}
