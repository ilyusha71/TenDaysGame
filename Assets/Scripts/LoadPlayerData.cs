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
    public Text info;
    public Text catbell;
    public Text casino;
    public Text judgement;
    public Text prestige;
    public Text skill;
    public Text item;
    public Text championship;

    public void ReloadPlayer()
    {
        StartCoroutine(ReloadAvatar());
        StartCoroutine(ReloadInfo(input.text));
        StartCoroutine(ReloadPrestige(input.text));
        StartCoroutine(ReloadSkill(input.text));
        StartCoroutine(ReloadItem(input.text));
        StartCoroutine(ReloadChampionship(input.text));
    }
    IEnumerator ReloadAvatar()
    {
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
    }    IEnumerator ReloadMao()
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
    IEnumerator ReloadSkill(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_技能.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                skill.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                skill.text = "未习得技能";
            }
        }
    }
    IEnumerator ReloadItem(string nick)
    {
        string url = Application.streamingAssetsPath + "/" + nick + "_物品.txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                item.text = webRequest.downloadHandler.text;
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                item.text = "无任何物品";
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
                //Debug.Log(webRequest.downloadHandler.text);
            }
            else
            {
                championship.text = "无战绩记录";
            }
        }
    }
}
