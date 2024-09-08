using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Image[] fieldPanelBG;
    public GameObject[] fieldPanel; 

    public ToggleGroup tg;
    public Toggle[] chess;
    private int[] knights = new int[5] { 2, 3, 4, 5, 6};
    private int[] assassins = new int[5] { 1, 3, 4, 5, 7};
    private int nowChoose;
    [SerializeField]
    public Dictionary<int,int> dictChess = new Dictionary<int, int>();
    public Button[] area;

    public ChessInfoController[] chessField = new ChessInfoController[6];


    private void Awake()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < 5)
                dictChess.Add(i,knights[i]);
            else
                dictChess.Add(i,assassins[i - 5]);
        }           
    }

    // Start is called before the first frame update
    void Start()
    {
        chess = tg.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < chess.Length; i++)
        {
            int index = i;

            chess[i].group = tg;
            Toggle t = chess[index];
            t.onValueChanged.AddListener(delegate {
                if (t.isOn)
                    nowChoose = index;
            });

            chess[i].GetComponentInChildren<Text>().text = dictChess[i].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1)) fieldPanelBG[0].enabled = !fieldPanelBG[0].enabled;
        if (Input.GetKeyUp(KeyCode.F3)) fieldPanelBG[1].enabled = !fieldPanelBG[1].enabled;
        if (Input.GetKeyUp(KeyCode.F2)) fieldPanel[0].SetActive(!fieldPanel[0].activeSelf);
        if (Input.GetKeyUp(KeyCode.F4)) fieldPanel[1].SetActive(!fieldPanel[1].activeSelf);

    }

    public void MoveForest()
    {
        int index = (nowChoose < 5) ? 0 : 3;
        chessField[index].Arrive();
        chessField[index].ui.SetActive(true);
        chessField[index].level.text = (index == 0)?dictChess[nowChoose].ToString(): (dictChess[nowChoose]+2).ToString();
    }

    public void MoveTown()
    {
        int index = (nowChoose < 5) ? 1 : 4;
        chessField[index].Arrive();
        chessField[index].ui.SetActive(true);
        chessField[index].level.text = (index == 1) ?(dictChess[nowChoose] + 2).ToString():dictChess[nowChoose].ToString();
    }

    public void MoveMist()
    {
        int index = (nowChoose < 5) ? 2 : 5;
        chessField[index].Arrive();
        chessField[index].ui.SetActive(true);
        chessField[index].level.text = dictChess[nowChoose].ToString();
    }
}
