using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ZhongyanMap : MonoBehaviour
{
    public HexGrid[] hexGrids;

    private void Awake()
    {
        Reset();
    }

    private void Reset()
    {
        hexGrids = GetComponentsInChildren<HexGrid>();
        for (int i = 0; i < hexGrids.Length; i++)
        {
            hexGrids[i].GetComponentInChildren<Text>().text = i.ToString();
            hexGrids[i].DrawRoad();
            hexGrids[i].GetComponentInChildren<Text>().text = "";
            switch (i)
            {
                case 34:
                case 41:
                case 48:
                case 16:
                case 29:
                case 42:
                case 37:
                case 43:
                case 49:
                case 76:
                case 69:
                case 62:
                case 94:
                case 81:
                case 68:
                case 73:
                case 67:
                case 61:
                    hexGrids[i].DrawBaihu();
                    break;


                case 55:
                    hexGrids[i].DrawBell();
                    break;
                case 0:
                case 1:
                case 7:
                case 13:
                case 5:
                case 6:
                case 12:
                case 19:
                case 91:
                case 98:
                case 104:
                case 105:
                case 97:
                case 103:
                case 109:
                case 110:
                    hexGrids[i].GetComponent<Image>().enabled = false;
                    hexGrids[i].GetComponentInChildren<Text>().text = "";
                    break;

                case 22:
                case 28:
                case 35:
                case 23:
                case 30:
                case 36:
                case 47:
                case 54:
                case 60:
                case 50:
                case 56:
                case 63:
                case 74:
                case 80:
                case 87:
                case 75:
                case 82:
                case 88:

                case 8:
                case 14:
                case 20:
                case 33:  
                case 59:
                case 85:
                case 92:
                case 99:

                case 11:
                case 18:
                case 25:
                case 51: 
                case 77:
                case 90:
                case 96:
                case 102:
                case 2:
                case 4:
                case 106:
                case 108:
                    hexGrids[i].DrawBuilding();
                    break;           
                case 3:    
                case 107:    
                    hexGrids[i].DrawParticipants();
                    break;

                case 26:    
                case 84:   
                    hexGrids[i].DrawJidao();
                    break;       
                case 32: 
                case 78:
                    hexGrids[i].DrawMao();
                    break;
                default:
                    break;
            }
        }
#if UNITY_EDITOR
        print(6);
        EditorUtility.SetDirty(this);
       AssetDatabase.SaveAssets();
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
