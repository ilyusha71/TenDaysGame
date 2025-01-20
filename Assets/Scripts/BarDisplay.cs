using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BarDisplay : MonoBehaviour, IPointerDownHandler
{
    public Image bar;
    public int section;
    float increment;
    int order;
    public void OnPointerDown(PointerEventData data)
    {
        if (Input.GetMouseButtonDown(0))
            bar.fillAmount += increment;
        if (Input.GetMouseButtonDown(1))
            bar.fillAmount -= increment;
    }
    // Start is called before the first frame update
    void Start()
    {
        increment = 1.0f / section;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
