using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinefieldManager : MonoBehaviour
{
    public GameObject[] grids;
    public GameObject window;
    public MinefieldGrid[] field;
    public GameObject[] fieldMask;
    public HP[] operation;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            for (int i = 0; i < field.Length; i++)
            {
                field[i].Clear();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].SetActive(false);
            }
            if (operation[0].maxHP == 11) 
            {
                for (int i = 0; i < operation.Length; i++)
                {
                    operation[i].Half();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].SetActive(true);
            }
            if (operation[0].maxHP == 6)
            {
                for (int i = 0; i < operation.Length; i++)
                {
                    operation[i].Double();
                }
            }
        }
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
}
