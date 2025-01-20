using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RowLayout : MonoBehaviour
{
    public Image icon;
    public TMP_Text textLevel;
    public Text level;
    public Text contents;
    public LevelFormat levelFormat;
    public bool hasLevel = false;

    public void AnalyzeData(string data)
    {
        string[] col = data.Split("|");
        if (col.Length < 2) { ClearData(); return; }
        contents.text = col[1];
        if (!hasLevel) return;
        int index = int.Parse(col[0]);
        icon.enabled = true;
        icon.sprite = levelFormat.level[index].sprite;
        textLevel.color = levelFormat.level[index].color;
        textLevel.text = col[0];
    }
    public void ClearData()
    {
        contents.text = "";
        if (!hasLevel) return;
        icon.enabled = false;
        textLevel.text = "";   
    }
}
