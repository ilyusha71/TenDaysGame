using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class HP : MonoBehaviour, IPointerClickHandler
{
    public Text hp;
    public int maxHP;
    // Start is called before the first frame update
    void Start()
    {
        hp = GetComponent<Text>();
        maxHP = int.Parse(hp.text)+1;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Reduce();
    }
    public void Reduce()
    {
        int i = int.Parse(hp.text);
        i--;
        i = (int)Mathf.Repeat(i, maxHP);
        hp.text = i.ToString();
    }

    public void Double()
    {
        hp.enabled = true;
        maxHP = (maxHP - 1) * 2 + 1;
        hp.text = (maxHP - 1).ToString();
    }
    public void Half()
    {
        hp.enabled = true;
        maxHP = (int)((maxHP - 1) * 0.5f + 1);
        hp.text = (maxHP - 1).ToString();
    }
    public void Hide()
    {
        hp.enabled = false;
    }
}