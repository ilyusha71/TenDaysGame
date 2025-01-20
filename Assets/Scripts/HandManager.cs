using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HandManager
{
    public CardController[] deck;
    public Transform container;
    public bool IsDisplay { get; set; }
    public void Initialize()
    {
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i].transform.SetParent(container);
            deck[i].transform.localPosition = Vector3.zero;
            deck[i].transform.SetSiblingIndex(i);
        }
    }
}
