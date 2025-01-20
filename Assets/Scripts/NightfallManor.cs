using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NightfallManor : MonoBehaviour
{
    public LoadPlayerAvatar lpa;
    public GameObject secretGadget;
    public GameObject[] chess;
    public Image[] player;
    public Image[] bar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            chess[0].SetActive(!chess[0].activeSelf);
        if (Input.GetKeyDown(KeyCode.F2))
            chess[1].SetActive(!chess[1].activeSelf);
        if (Input.GetKeyDown(KeyCode.F3))
            chess[2].SetActive(!chess[2].activeSelf);
        if (Input.GetKeyDown(KeyCode.F4))
            chess[3].SetActive(!chess[3].activeSelf);
        if (Input.GetKeyDown(KeyCode.Alpha1))
            UpdateHP(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            UpdateHP(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            UpdateHP(2);
        if (Input.GetKeyDown(KeyCode.Alpha4))
            UpdateHP(3);
        if (Input.GetKeyDown(KeyCode.F5))
        {
            secretGadget.SetActive(!secretGadget.activeSelf);
            UpdatePlayer();
        }
    }
    public void UpdatePlayer()
    {
        for (int i = 0; i < player.Length; i++)
        {
            player[i].sprite = lpa.player[i].sprite;
        }
    }
    public void UpdateHP(int index)
    {
        if (bar[index].fillAmount == 0)
            bar[index].fillAmount = 1.0f;
        else
            bar[index].fillAmount -= 0.3333333333f;
    }
}
