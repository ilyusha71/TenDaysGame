using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public Text[] score;
    private DungeonGrid[] grids;
    private int maxPlayer = 2;

    // Start is called before the first frame update
    void Start()
    {
        grids = GetComponentsInChildren<DungeonGrid>();
        for (int i = 0; i < grids.Length; i++)
        {
            grids[i].Set(this, maxPlayer);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CalculateGrids()
    {
        int grassScore = 0, soilScore = 0;
        //int blueScore = 0, redScore = 0;
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].index == 1)
                grassScore++;
            else if (grids[i].index == 2)
                soilScore++;
            //else if (grids[i].index == 3)
            //    blueScore++;
            //else if (grids[i].index == 4)
            //    redScore++;
        }

        score[0].text = grassScore.ToString();
        score[1].text = soilScore.ToString();
        //score[2].text = blueScore.ToString();
        //score[3].text = redScore.ToString();
    }
}