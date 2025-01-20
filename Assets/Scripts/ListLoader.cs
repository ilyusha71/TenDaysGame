using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ListLoader : MonoBehaviour
{
    public string title;
    public RowLayout[] rows;
    public IEnumerator LoadList(string nickName)
    {
        if (rows.Length == 0) rows = GetComponentsInChildren<RowLayout>();
        string url = Application.streamingAssetsPath + "/" + nickName + "/" + title + ".txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < rows.Length; i++)
                {
                    if (i < contents.Length) rows[i].AnalyzeData(contents[i]);
                    else rows[i].ClearData();
                }
            }
            else { for (int i = 0; i < rows.Length; i++) { rows[i].ClearData(); } }
        }
    }
    public IEnumerator ReloadLists(string nick)
    {
        if(rows.Length==0) rows = GetComponentsInChildren<RowLayout>();
        string url = Application.streamingAssetsPath + "/" + nick + "_" + title + ".txt";
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();
            if (string.IsNullOrEmpty(webRequest.error))
            {
                string[] contents = webRequest.downloadHandler.text.Split("\r\n");
                for (int i = 0; i < rows.Length; i++)
                {
                    if (i < contents.Length) rows[i].AnalyzeData(contents[i]);
                    else rows[i].ClearData();
                }
            }
            else { for (int i = 0; i < rows.Length; i++) { rows[i].ClearData(); } }
        }
    }
}
