using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChessInfoController : MonoBehaviour
{
    public GameObject ui;
    public Text level;
    public Image kill;
    public Image black;
    public TextMeshProUGUI description;

    // Start is called before the first frame update
    void Start()
    {
        Arrive();
    }

    public void Arrive()
    {
        ui.SetActive(false);
        level.text = "x";
        kill.enabled = false;
        black.enabled = false;
        description.text = "";
    }
}
