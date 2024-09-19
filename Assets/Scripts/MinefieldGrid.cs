using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinefieldGrid : MonoBehaviour
{
    public Color32 clear = Color.white;
    public Color32 safe = new Color32(194,255,155,255);
    public Color32 mine = new Color32(255, 214, 134, 255);
    public Color32 entry = new Color32(126, 206, 255, 255);
    public Color32 exit = new Color32(255, 137, 137, 255);
    private Color32[] draw = new Color32[5];
    private int index;
    private Image grid;
    private void Awake()
    {
        draw[0] = clear;
        draw[1] = safe;
        draw[2] = mine;
        draw[3] = entry;
        draw[4] = exit;
        grid = GetComponent<Image>();
        GetComponentInChildren<Text>().text = (transform.GetSiblingIndex()+1).ToString();
    }
    public void Clear()
    {
        if (!grid) grid = GetComponent<Image>();
        grid.color = clear;
        index = 0;
    }

    public void Draw()
    {
        index++;
        index=(int)Mathf.Repeat(index, 5);
        grid.color=draw[index];
    }
}
