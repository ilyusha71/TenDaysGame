using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatCardManager : MonoBehaviour
{
    public GameObject[] games;
    public GameObject deck;
    public GameObject shuffle;
    public Image[] cover;

    public GameObject[] ksGame;
    int ks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < games.Length; i++) 
            {
                games[i].SetActive(false);
            }
            games[0].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < games.Length; i++)
            {
                games[i].SetActive(false);
            }
            games[1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i < games.Length; i++)
            {
                games[i].SetActive(false);
            }
            games[2].SetActive(true);
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    cover[0].enabled = !cover[0].enabled;
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    cover[1].enabled = !cover[1].enabled;
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //    cover[2].enabled = !cover[2].enabled;
        //if (Input.GetKeyDown(KeyCode.Alpha4))
        //    cover[3].enabled = !cover[3].enabled;

        if (Input.GetKeyDown(KeyCode.D))
            deck.SetActive(!deck.activeSelf);
        if (Input.GetKeyDown(KeyCode.S))
            shuffle.SetActive(!shuffle.activeSelf);
    }

    public void Ready()
    {
        
    }

    public void SwitchDuel()
    {
        for (int i = 0; i < ksGame.Length; i++)
        {
            ksGame[i].SetActive(false);
        }
        ks++;
        ks = (int)Mathf.Repeat(ks, ksGame.Length);
        ksGame[ks].SetActive(true);
    }
    public void Shuffle()
    {

    }
}
