using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityActive : MonoBehaviour, IPointerDownHandler
{
    public bool cast =false;
    public bool shoot = true;
    public GameObject ability;
    DraggableWidgetLite dw;
    private void Awake()
    {
        dw = GetComponent<DraggableWidgetLite>();
    }
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (!dw.copy && Input.GetMouseButtonDown(1))
        {
            if (cast)
                ability.SetActive(!ability.activeSelf);
            if (shoot)
            {
                GameObject go = Instantiate(ability);
                go.name = ability.name;
                go.transform.SetParent(transform, false);
                go.transform.position = ability.transform.position;
                go.SetActive(true);
            }
        }
    }
}
