using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DungeonManager : MonoBehaviour
{
    public Text[] score;
    public Text[] conquer;
    public Text unlocked;
    public GameObject guard;
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
        grids[40].Occupy(1);
        grids[92].Occupy(2);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            Clear();
    }
    public void CalculateGrids()
    {
        int grassScore = 0, soilScore = 0, grassTerritory = 0, soilTerritory = 0;
        for (int i = 0; i < grids.Length; i++)
        {
            if (grids[i].index == 1)
                grassTerritory++;
            else if (grids[i].index == 2)
                soilTerritory++;
        }
        int countdown = 61 - grassTerritory - soilTerritory;
        grassScore = grassTerritory + int.Parse(conquer[2].text) + int.Parse(conquer[4].text) + int.Parse(conquer[6].text) + int.Parse(conquer[8].text);
        soilScore = soilTerritory + int.Parse(conquer[3].text) + int.Parse(conquer[5].text) + int.Parse(conquer[7].text) + int.Parse(conquer[9].text);
        score[0].text = grassScore.ToString();
        score[1].text = soilScore.ToString();
        conquer[0].text = grassTerritory.ToString();
        conquer[1].text = soilTerritory.ToString();
        unlocked.text = countdown.ToString();
        guard.SetActive((countdown>0)?true:false);
    }
    public void Clear()
    {
        for (int i = 0; i < grids.Length; i++)
        {
            grids[i].Clear();
        }
        score[0].text = "0";
        score[1].text = "0";
        conquer[0].text = "0";
        conquer[1].text = "0";
        unlocked.text= "61";
        grids[40].Occupy(1);
        grids[92].Occupy(2);
    }
}