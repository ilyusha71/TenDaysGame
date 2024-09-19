using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MinefieldManager : MonoBehaviour
{
    public GameObject[] grids; //25 or49
    public GameObject window;
    public MinefieldGrid[] field; //
    public GameObject[] fieldMask;
    public HP[] operation;

    public RectTransform[] area;
    public Image[] setup;
    public Sprite[] level;
    public MinefieldGrid[] blueGrids;
    public MinefieldGrid[] redGrids;
    public MinefieldWrapper[] blueWrappers;
    public MinefieldWrapper[] redblueWrappers;
    public int[] state = new int[2] { 2,2};

    public MinefieldFaction[] faction;
    [Serializable]
    public class MinefieldFaction
    {
        public Image setup;
        public RectTransform areaGrid;
        public RectTransform areaWrapper;
        public MinefieldGrid[] grids;
        public MinefieldWrapper[] wrappers;
        public int state = 2;
        public HP[] operation;
    }

    private void Awake()
    {
        //blueGrids = area[0].GetComponentsInChildren<MinefieldGrid>();
        //redGrids = area[1].GetComponentsInChildren<MinefieldGrid>();
        //blueWrappers = area[2].GetComponentsInChildren<MinefieldWrapper>();
        //redblueWrappers = area[3].GetComponentsInChildren<MinefieldWrapper>();
        if (faction.Length == 2) 
        {
            for (int i = 0; i < faction.Length; i++)
            {
                faction[i].grids = faction[i].areaGrid.GetComponentsInChildren<MinefieldGrid>();
                faction[i].wrappers = faction[i].areaWrapper.GetComponentsInChildren<MinefieldWrapper>();
                faction[i].state = 2;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            for (int i = 0; i < faction[0].grids.Length; i++)
            {
                faction[0].grids[i].Clear();
                faction[0].wrappers[i].Clear();
                faction[1].grids[i].Clear();
                faction[1].wrappers[i].Clear();
            }
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    for (int i = 0; i < grids.Length; i++)
        //    {
        //        grids[i].SetActive(false);
        //    }
        //    if (operation[0].maxHP == 11)
        //    {
        //        for (int i = 0; i < operation.Length; i++)
        //        {
        //            operation[i].Half();
        //        }
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    for (int i = 0; i < grids.Length; i++)
        //    {
        //        grids[i].SetActive(true);
        //    }
        //    if (operation[0].maxHP == 6)
        //    {
        //        for (int i = 0; i < operation.Length; i++)
        //        {
        //            operation[i].Double();
        //        }
        //    }
        //}
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fieldMask[0].SetActive(!fieldMask[0].activeSelf);
            fieldMask[1].SetActive(!fieldMask[1].activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            window.SetActive(!window.activeSelf);
        }
    }

    public void SetField(int index)
    {
        faction[index].state++;
        faction[index].state = (int)Mathf.Repeat(faction[index].state, 3);
        faction[index].setup.sprite = level[faction[index].state];

        switch (faction[index].state)
        {
            case 0:
                for (int i = 0; i < faction[index].grids.Length; i++)
                {
                    faction[index].grids[i].gameObject.SetActive(false);
                    faction[index].wrappers[i].gameObject.SetActive(false);
                }
                for (int i = 0; i < 4; i++)
                {
                    faction[index].operation[i].Hide();
                }
                break;
            case 1:
                faction[index].areaGrid.sizeDelta = new Vector2(450, 450);
                faction[index].areaWrapper.sizeDelta = new Vector2(450, 450);
                for (int i = 0; i < faction[index].grids.Length; i++)
                {
                    faction[index].grids[i].gameObject.SetActive((i < 25) ? true : false);
                    faction[index].wrappers[i].gameObject.SetActive((i < 25) ? true : false);
                }
                if (faction[index].operation[0].maxHP == 11)
                {
                    for (int i = 0; i < faction[index].operation.Length; i++)
                    {
                        faction[index].operation[i].Half();
                    }
                }
                break;
            case 2            :
                faction[index].areaGrid.sizeDelta = new Vector2(600, 600);
                faction[index].areaWrapper.sizeDelta = new Vector2(600, 600);
                for (int i = 0; i < faction[index].grids.Length; i++)
                {
                    faction[index].grids[i].gameObject.SetActive(true);
                    faction[index].wrappers[i].gameObject.SetActive(true);
                }
                if (faction[index].operation[0].maxHP == 6)
                {
                    for (int i = 0; i < faction[index].operation.Length; i++)
                    {
                        faction[index].operation[i].Double();
                    }
                }
                break;
        }
    }
}
