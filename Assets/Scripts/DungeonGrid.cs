using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonGrid : MonoBehaviour
{
    public Color32 clear = new Color32(37, 37, 37, 255);
    public Color32 grass = new Color32(37, 137, 0, 255);
    public Color32 soil = new Color32(137, 37, 0, 255);
    public Color32 blue = new Color32(71, 255, 255, 255);
    public Color32 red = new Color32(255, 71, 71, 255);
    private Color32[] draw = new Color32[5];
    public int maxPlayer;
    public int index;
    private Image grid;
    private DungeonManager manager;

    private void Awake()
    {
        draw[0] = clear;
        draw[1] = grass;
        draw[2] = soil;  
        draw[3] = blue;
        draw[4] = red;
        grid = GetComponent<Image>();
        grid.color = clear;
        transform.parent.rotation = Quaternion.Euler(0, 0, 90*Random.Range(0,4));
        if (transform.parent.parent.name == "obsolete")
            grid.enabled = false;
    }

    public void Set(DungeonManager dm,int max)
    {
        manager = dm;
        maxPlayer = max;
    }

    public void Clear()
    {
        grid.color = clear;
    }

    public void Draw()
    {
        //if (Input.GetMouseButtonDown(0))
        //{            
        //    index = (int)Mathf.Repeat(index, maxPlayer);
        //    index++;          
        //}
        //if (Input.GetMouseButtonDown(1))
        //    index=0;

        if (Input.GetMouseButtonDown(0))
            index = (index == 1) ? 0 : 1;
        else if (Input.GetMouseButtonDown(1))
            index = (index == 2) ? 0 : 2;

        grid.color = draw[index];
        manager.CalculateGrids();
    }
}
