using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class LoadPlayerData : MonoBehaviour
{
    [Header("Player List Panel")]
    public Transform playerListPanel;
    public GameObject playerTab;
    public class Cat
    {
        public Image avatar;
        public Text name;
        //public Sprite avatar;
        //public string name;        
    }
    Dictionary<string, Cat> playerList = new Dictionary<string, Cat>();
    //string[] playerList;
    [Header("Cat")]
    public Image player;
    public InputField input;
    [Header("Page")]
    public GameObject[] pages;
    int index;
    [Header("List")]
    public ListLoader[] listLoader;
    [Header("Info")]
    public Text catbell;
    public Text casino;
    public Text judgement;
    public Transform info;
    public Text[] textInfo;
    [Header("Dice")]
    public RandomDice[] dice;
    public Text luck7d6;
    public Text[] table;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.PageDown))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index++;
            index = (int)Mathf.Repeat(index, pages.Length);
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.PageUp))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index--;
            index = (int)Mathf.Repeat(index, pages.Length);
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.End))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = pages.Length - 1;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 0;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F1))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 1;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 2;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 3;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 4;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 5;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 6;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 7;
            pages[index].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            for (int i = 0; i < pages.Length; i++)
            {
                pages[i].SetActive(false);
            }
            index = 8;
            pages[index].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F12))
            StartCoroutine(ReloadPlayerList());
    }
    public void LoadPlayerList()
    {
        StartCoroutine(ReloadPlayerList());
    }
    IEnumerator ReloadPlayerList()
    {
        string url = Application.streamingAssetsPath + "/PlayerList.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");

                for (int i = 0; i < contents.Length; i++)
                {
                    string nickname = contents[i].Split("\t")[1];
                    GameObject tab;
                    Cat cat;
                    if (playerList.ContainsKey(nickname))
                        cat = playerList[nickname];
                    else
                    {
                        tab = Instantiate(playerTab);
                        tab.transform.SetParent(playerListPanel);
                        tab.transform.SetSiblingIndex(i);
                        cat = new Cat();
                        cat.name = tab.GetComponentInChildren<Text>();
                        cat.name.text = nickname;
                        cat.avatar = tab.GetComponentsInChildren<Image>()[1];

                        Button btn = tab.GetComponentInChildren<Button>();
                        btn.onClick.AddListener(() =>
                        {
                            input.text = cat.name.text;
                            ReloadPlayer();
                        });
                        playerList.Add(nickname, cat);
                        StartCoroutine(ReloadCat(cat));
                    }
                }
                playerListPanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 273 + 275.5f * (int)((contents.Length - 1) / 7));
            }
            else { }
        }
    }
    public void ReloadPlayer()
    {
        StartCoroutine(ReloadAvatar());
        StartCoroutine(ReloadMain(input.text));
        for (int i = 0; i < listLoader.Length; i++) { StartCoroutine(listLoader[i].LoadList(input.text)); }
    }
    void UpdateRank(string playerName) 
    {
        StartCoroutine(stageClash.ReloadRank(playerName)); //驿站风云
        StartCoroutine(royalTradeWar.ReloadRank(playerName)); //皇家商贸战
        StartCoroutine(minefieldAssault.ReloadRank(playerName)); //雷区突击
        StartCoroutine(wod.ReloadRank(playerName)); //绝望梦境
        StartCoroutine(dungeonConquest.ReloadRank(playerName)); //地城争夺
        StartCoroutine(ambushEnigma.ReloadRank(playerName)); //伏击迷局
        StartCoroutine(liarsBar.ReloadRank(playerName)); //老猫酒馆
        StartCoroutine(catBingo.ReloadRank(playerName)); //猫猫宾果
        StartCoroutine(catQuoridor.ReloadRank(playerName)); //猫猫破坏者
        StartCoroutine(sdol.ReloadRank(playerName)); //极限斗法
        StartCoroutine(throneRivalry.ReloadRank(playerName)); //王权纷争
        StartCoroutine(bicolorRacing.ReloadRank(playerName)); //双色斗猫
        StartCoroutine(nim15.ReloadRank(playerName)); //双色斗猫
        StartCoroutine(nobilityBattlefield.ReloadRank(playerName)); //双色斗猫
    }      
    IEnumerator ReloadCat(Cat cat)
    {
        string url = Application.streamingAssetsPath + "/" + cat.name.text + ".png";
        using (UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                Texture2D img = DownloadHandlerTexture.GetContent(webRequest);
                Sprite sp = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0.5f, 0.5f));
               cat.avatar.sprite = sp;
            }
            else { }
        }
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
            }
        }
    }
    public IEnumerator ReloadMain(string playerName)
    {
        catbell.text = "";
        casino.text = "";
        judgement.text = "";
        luck7d6.text = "";
        for (int i = 3; i < table.Length; i++) { table[i].text =""; }
        if (textInfo.Length == 0) textInfo = info.GetComponentsInChildren<Text>();
        else { for (int i = 0; i < textInfo.Length; i++) { textInfo[i].text = ""; } }
        string url = Application.streamingAssetsPath + "/PlayerList.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int order = 0; order < contents.Length; order++)
                {
                    string[] data = contents[order].Split("\t");
                    if (data[1] == playerName)
                    {                        
                        catbell.text = data[4];
                        int vip = int.Parse(data[6]);
                        casino.text = data[5] + (vip > 0 ? ("(VIP" + data[6] + ")") : "");
                        judgement.text = data[7];
                        textInfo[0].text = data[0]; // 降生日
                        textInfo[1].text = data[2]; // 阵营
                        textInfo[2].text = data[3]; // 玩家
                        for (int i = 3; i < textInfo.Length; i++) { textInfo[i].text = data[i+5]; }
                        textInfo[11].text += " cm"; // 身高
                        textInfo[12].text += " kg"; // 体重
                        luck7d6.text = data[19]; // 猫品
                        for (int j = 0; j < dice.Length; j++) { dice[j].Display(int.Parse(data[20+j])); }
                        CalculateDice();
                        UpdateRank(data[3]);
                    }
                }
            }
            else
                UpdateRank("");
        }
    }
    public void CalculateDice() 
    {
        table[0].text = dice[6].Index.ToString(); //1d6最大
        table[1].text = dice[3].Index.ToString(); //1d6中位
        table[2].text = dice[0].Index.ToString(); //1d6最小
        table[3].text = (dice[6].Index+ dice[5].Index).ToString(); //2d6最大
        table[4].text = (dice[6].Index+ dice[0].Index).ToString(); //2d6中位
        table[5].text = (dice[1].Index+ dice[0].Index).ToString(); //2d6最小
        table[6].text = (dice[6].Index+ dice[5].Index+ dice[4].Index).ToString(); //3d6最大
        table[7].text = (dice[6].Index+ dice[5].Index+ dice[0].Index).ToString(); //3d6极大
        table[8].text = (dice[4].Index+ dice[3].Index+ dice[2].Index).ToString(); //3d6中位
        table[9].text = (dice[6].Index+ dice[1].Index+ dice[0].Index).ToString(); //3d6极小
        table[10].text = (dice[2].Index+ dice[1].Index+ dice[0].Index).ToString(); //3d6最小
        table[11].text = (dice[6].Index+ dice[5].Index+ dice[4].Index + dice[3].Index).ToString(); //4d6最大
        table[12].text = (dice[6].Index+ dice[5].Index+ dice[1].Index + dice[0].Index).ToString(); //4d6中位
        table[13].text = (dice[3].Index+ dice[2].Index+ dice[1].Index + dice[0].Index).ToString(); //4d6最小
        table[14].text = (dice[6].Index+ dice[5].Index+ dice[4].Index + dice[3].Index + dice[2].Index).ToString(); //5d6最大
        table[15].text = (dice[6].Index+ dice[5].Index+ dice[4].Index + dice[1].Index + dice[0].Index).ToString(); //5d6极大
        table[16].text = (dice[5].Index+ dice[4].Index+ dice[3].Index + dice[2].Index + dice[1].Index).ToString(); //5d6中位
        table[17].text = (dice[6].Index+ dice[5].Index+ dice[2].Index + dice[1].Index + dice[0].Index).ToString(); //5d6极小
        table[18].text = (dice[4].Index+ dice[3].Index+ dice[2].Index + dice[1].Index + dice[0].Index).ToString(); //5d6最小
    }

    [Header("<color=green>驿站风云 Stage Clash")] //驿站风云
    public ReloadList stageClash;
    [Header("<color=green>皇家商贸战 Royal Trade War</color>")] //皇家商贸战
    public ReloadList royalTradeWar;
    [Header("<color=green>雷区突击 Minefield Assault</color>")] //雷区突击
    public ReloadList minefieldAssault;
    [Header("<color=green>绝望梦境 The Wonderland of Desperation</color>")] //绝望梦境
    public ReloadList wod;
    [Header("<color=green>地城争夺 Dungeon Conquest</color>")] //地城争夺
    public ReloadList dungeonConquest;
    [Header("<color=green>伏击迷局 Ambush Enigma</color>")] //伏击迷局
    public ReloadList ambushEnigma;
    [Header("<color=green>老猫酒馆 Liar's Bar</color>")] //老猫酒馆
    public ReloadList liarsBar;
    [Header("<color=green>猫猫宾果 Cat Bingo</color>")] //猫猫宾果
    public ReloadList catBingo;
    [Header("<color=green>猫猫破坏者 Cat Quoridor</color>")] //猫猫破坏者
    public ReloadList catQuoridor;
    [Header("<color=green>极限斗法 Sorcery Duel of Limits</color>")] //极限斗法
    public ReloadList sdol;
    [Header("<color=green>王权纷争 Throne Rivalry</color>")] //王权纷争
    public ReloadList throneRivalry;
    [Header("<color=green>双色斗猫 Bicolor Racing</color>")] //双色斗猫
    public ReloadList bicolorRacing;
    [Header("<color=green>尼姆15 Nim 15</color>")] //尼姆15
    public ReloadList nim15;
    [Header("<color=green>贵族战场 Nobility Battlefield</color>")] //贵族战场
    public ReloadList nobilityBattlefield;

    [Serializable]
    public class ReloadList
    {
        public string title;
        public GameObject panel;
        public Text[] list;
    public IEnumerator ReloadRank(string playerName)
        {
            for (int i = 0; i < list.Length; i++) { list[i].text = ""; }
            //yield return new WaitForSecondsRealtime(0.1f);
            string url = Application.streamingAssetsPath + "/Game_" + title + ".txt";
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                    for (int data = 0; data < contents.Length; data++)
                    {
                        string[] records = contents[data].Split("\t");
                        if (records[0]==playerName)
                        {                            
                            for (int i = 0; i < list.Length; i++)
                            {
                                list[i].text = records[i + 2];
                            }
                        }
                    }
                }
            }
        }
    }
}
