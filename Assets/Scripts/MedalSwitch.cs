using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalSwitch : MonoBehaviour
{
    public Sprite[] medals;
    private Image img;
    private int index;

    private void Awake()
    {
        index = Random.Range(0, 3);
        img = GetComponent<Image>();
        img.sprite = medals[index];
    }
    public void Switch()
    {
        index++;
        index = (int)Mathf.Repeat(index, 3);
        img.sprite = medals[index];       
    }
}
