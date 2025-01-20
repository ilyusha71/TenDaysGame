using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryCat : MonoBehaviour
{
    public GameObject[] elements;
    public void RandomElement()
    {
        for (int i = 0; i < elements.Length; i++)
        {
            elements[i].SetActive(false);
        }
        elements[Random.Range(0,elements.Length)].SetActive(true);
    }
}
