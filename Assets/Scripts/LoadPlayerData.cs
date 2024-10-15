using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LoadPlayerData : MonoBehaviour
{
    public GameObject cat;
    public Image player;
    public InputField input;
    public GameObject[] pages;
    public Text info;
    public Text catbell;
    public Text casino;
    public Text judgement;
    public Text prestige;
    public Text pets;
    [Header("Item Panel")]
    public GameObject itemPanel;
    public Text[] items;
    [Header("Skill Panel")]
    public GameObject skillPanel;
    public Text[] skills;
    public Text championship;
    [Header("Stage Clash")]
    public Text[] scRecords;
    [Header("Royal Trade War")]
    public Text[] rtwRecords;
    private void Awake()
    {
        items = itemPanel.GetComponentsInChildren<Text>();
        skills = skillPanel.GetComponentsInChildren<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            pages[0].SetActive(true);
            pages[1].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            pages[0].SetActive(false);
            pages[1].SetActive(true);
        }
    }
    public void ReloadPlayer()
    {
        StartCoroutine(ReloadAvatar());
        StartCoroutine(ReloadInfo(input.text));
        StartCoroutine(ReloadPrestige(input.text));
        StartCoroutine(ReloadSkills(input.text));
        StartCoroutine(ReloadItems(input.text));
        StartCoroutine(ReloadChampionship(input.text));
        StartCoroutine(ReloadStageClash(input.text));
        StartCoroutine(ReloadRoyalTradeWar(input.text));
        StartCoroutine(ReloadPets(input.text));
    }
    IEnumerator ReloadAvatar()
    {
        //string url = "https://raw.githubusercontent.com/ilyusha71/TenDaysGame/refs/heads/main/Assets/StreamingAssets/" + input.text + ".png";
        string url = Application.streamingAssetsPath + "/" + input.text + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player.sprite = sp;
                cat.gameObject.SetActive(false);
                player.enabled = true;
            }
            else
            {
                StartCoroutine(ReloadMao());
            }
        }
    }
    IEnumerator ReloadMao()
    {
        string url = Application.streamingAssetsPath + "/qunmao.png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
                player.sprite = sp;
                cat.gameObject.SetActive(false);
                player.enabled = true;
            }
        }
    }
    IEnumerator ReloadInfo(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + ".txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("|");
                //string combine = "";
                //for (int i = 0; i < contents.Length; i++)
                //{
                //    combine += contents[i];
                //}
                info.text = contents[0] + contents[1] + contents[8];
                catbell.text = contents[3];
                casino.text = contents[5];
                judgement.text = contents[7];
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                info.text = "查无此猫";
            }
        }
    }
    IEnumerator ReloadPrestige(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_皇城声望.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                prestige.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                prestige.text = "未取得皇城声望";
            }
        }
    }
    IEnumerator ReloadItems(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_物品.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < items.Length; i++)
                {
                    if (i < contents.Length)
                        items[i].text = contents[i];
                    else
                        items[i].text = "";
                }
                //print(contents.Length);
                //item.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    items[i].text = "";
                }
                //item.text = "无任何物品";
            }
        }
    }
    IEnumerator ReloadSkills(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_技能.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < skills.Length; i++)
                {
                    if (i < contents.Length)
                        skills[i].text = contents[i];
                    else
                        skills[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < skills.Length; i++)
                {
                    skills[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadChampionship(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_战绩.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                championship.text = webRequest.downloadHandler.text;
            }
            else
            {
                championship.text = "无战绩记录";
            }
        }
    }
    IEnumerator ReloadStageClash(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_驿站风云.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\t");
                for (int i = 0; i < scRecords.Length; i++)
                {
                    if (i < contents.Length)
                        scRecords[i].text = contents[i];
                    else
                        scRecords[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < scRecords.Length; i++)
                {
                    scRecords[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadRoyalTradeWar(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_皇家商贸战.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\t");
                for (int i = 0; i < rtwRecords.Length; i++)
                {
                    if (i < contents.Length)
                        rtwRecords[i].text = contents[i];
                    else
                        rtwRecords[i].text = "";
                }
            }
            else
            {
                for (int i = 0; i < rtwRecords.Length; i++)
                {
                    rtwRecords[i].text = "";
                }
            }
        }
    }
    IEnumerator ReloadPets(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_宠物.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                pets.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                pets.text = "";
            }
        }
    }
}
