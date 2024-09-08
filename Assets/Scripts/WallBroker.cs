using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WallBroker : MonoBehaviour, IPointerClickHandler
{
    private SpriteRenderer sr;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }   
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (Input.GetMouseButtonUp(0))
            sr.enabled=!sr.enabled;
    }
}
