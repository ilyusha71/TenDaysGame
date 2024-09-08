using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Day : MonoBehaviour
{
    public Text day;
    public int maxDay;
    // Start is called before the first frame update
    void Start()
    {
        day = GetComponent<Text>();
        maxDay = int.Parse(day.text) + 1;
        day.text = "1";
    }
    public void Next()
    {
        int i = int.Parse(day.text);
        i++;
        i = (int)Mathf.Repeat(i, maxDay);
        day.text = i.ToString();
    }
}
