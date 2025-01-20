using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomDice : MonoBehaviour, IPointerDownHandler
{
    public GameObject[] face;
    public int Index { get; set; }
    public void OnPointerDown(PointerEventData data)
    {
        if (Input.GetMouseButton(0))
            Throw();
    }
    public void Throw()
    {
        for (int i = 0; i < face.Length; i++) { face[i].SetActive(false); }
        face[Random.Range(0, 6)].SetActive(true);
    }
    public void Display(int order)
    {
        Index = order;
        for (int i = 0; i < face.Length; i++) { face[i].SetActive(false); }
        face[order-1].SetActive(true);
    }
}
