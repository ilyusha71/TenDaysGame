using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MinefieldManager : MonoBehaviour
{
    public GameObject window;
    public GameObject[] fieldMask;
    public Sprite[] level;
    public MinefieldFaction[] faction;
    [Serializable]
    public class MinefieldFaction
    {
        public Image chess;
        public Image setup;
        public RectTransform areaGrid;
        public RectTransform areaWrapper;
        public MinefieldGrid[] grids;
        public MinefieldWrapper[] wrappers;
        public int state = 2;
        public Counter[] operation;
    }

    private void Awake()
    {
        if (faction.Length == 2) 
        {
            for (int i = 0; i < faction.Length; i++)
            {
                SetAlphaHit(faction[i].setup.transform.parent);
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
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            fieldMask[0].SetActive(!fieldMask[0].activeSelf);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            fieldMask[1].SetActive(!fieldMask[1].activeSelf);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            fieldMask[0].SetActive(!fieldMask[0].activeSelf);
            fieldMask[1].SetActive(!fieldMask[1].activeSelf);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            window.SetActive(!window.activeSelf);

        if (Input.GetKeyDown(KeyCode.F9))
            StartCoroutine(ReloadMap(1));
        if (Input.GetKeyDown(KeyCode.F10))
            StartCoroutine(ReloadMap(2));
    }
    IEnumerator ReloadMap(int index)
    {
        string url = Application.streamingAssetsPath + "/MinefieldMap_P" + index + ".txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                char[] contents = webRequest.downloadHandler.text.ToCharArray();
                for (int i = 0; i < contents.Length; i++)
                {
                    //if(index==1)
                        faction[index - 1].grids[i].Set(contents[i] - 48);
                    //else
                    //    faction[index - 1].grids[contents.Length-i-1].Set(contents[i] - 48);
                }
            }
            else { }
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
                faction[index].chess.enabled = false;
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
                faction[index].chess.enabled = true;
                faction[index].areaGrid.sizeDelta = new Vector2(450, 450);
                faction[index].areaWrapper.sizeDelta = new Vector2(450, 450);
                for (int i = 0; i < faction[index].grids.Length; i++)
                {
                    faction[index].grids[i].gameObject.SetActive((i < 25) ? true : false);
                    faction[index].wrappers[i].gameObject.SetActive((i < 25) ? true : false);
                }
                if (faction[index].operation[0].max == 11)
                {
                    for (int i = 0; i < faction[index].operation.Length; i++)
                    {
                        faction[index].operation[i].Half();
                    }
                }
                break;
            case 2:
                faction[index].chess.enabled = true;
                faction[index].areaGrid.sizeDelta = new Vector2(600, 600);
                faction[index].areaWrapper.sizeDelta = new Vector2(600, 600);
                for (int i = 0; i < faction[index].grids.Length; i++)
                {
                    faction[index].grids[i].gameObject.SetActive(true);
                    faction[index].wrappers[i].gameObject.SetActive(true);
                }
                if (faction[index].operation[0].max == 6)
                {
                    for (int i = 0; i < faction[index].operation.Length; i++)
                    {
                        faction[index].operation[i].Double();
                    }
                }
                break;
        }
    }
    void SetAlphaHit(Transform obj)
    {
        Image[] img = obj.GetComponentsInChildren<Image>();
        for (int i = 0; i < img.Length; i++)
        {
            try { img[i].alphaHitTestMinimumThreshold = 0.3f; }
            catch { }
        }
    }
}
