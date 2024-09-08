using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexGrid : MonoBehaviour
{
    public Color32 baihu = Color.white;
    public Color32 bell = new Color32(255, 137, 0, 255);
    public Color32 building = new Color32(71, 71, 71, 255);
    public Color32 road = new Color32(163, 163, 163, 255);
    public Color32 jidao = new Color32(216, 197, 255, 255);
    public Color32 mao = new Color32(197, 173, 137, 255);
    public Color32 safe = new Color32(194, 255, 155, 255);
    public Color32 mine = new Color32(255, 214, 134, 255);
    public Color32 entry = new Color32(126, 206, 255, 255);
    public Color32 exit = new Color32(255, 137, 137, 255);
    private Color32[] draw = new Color32[5];
    private int index;
    private Image grid;

    private void Awake()
    {
        draw[0] = baihu;
        draw[1] = safe;
        draw[2] = mine;
        draw[3] = entry;
        draw[4] = exit;
        grid = GetComponent<Image>();
       
        //GetComponentInChildren<Text>().text = (transform.GetSiblingIndex() + 1).ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw()
    {
        index++;
        index = (int)Mathf.Repeat(index, 5);
        //grid.color = draw[index];
    }
    public void DrawBaihu()
    {
        GetComponent<Image>().color = baihu;
    }
    public void DrawBell()
    {
        GetComponent<Image>().color = bell;
    }
    public void DrawBuilding()
    {
        GetComponent<Image>().color = building;
    }
    public void DrawRoad()
    {
        GetComponent<Image>().color = road;
    }

    public void DrawParticipants()
    {
        GetComponent<Image>().color = exit;
    }
    public void DrawJidao()
    {
        GetComponent<Image>().color = jidao;
    }    public void DrawMao()
    {
        GetComponent<Image>().color = mao;
    }
}
