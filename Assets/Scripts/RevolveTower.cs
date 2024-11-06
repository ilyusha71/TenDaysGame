using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RevolveTower : MonoBehaviour, IPointerDownHandler
{
    public Transform wall;
    public void OnPointerDown(PointerEventData data)
    {
        if(Input.GetMouseButtonDown(0))
            wall.rotation *= Quaternion.Euler(0, 0, -90);
        if (Input.GetMouseButtonDown(1))
            wall.rotation *= Quaternion.Euler(0, 0, 90);
    }
}
