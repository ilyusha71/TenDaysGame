using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeanutsManager : MonoBehaviour
{
    public PeanutSystem[] peanutSystems;
    public Sprite rip;
    public Transform dealer;
    public Text dealerName;
    public PeanutsCounter dealerPeanut;

    public static PeanutsManager Instance { get; private set; }
    private void Awake() => Instance = this;
    public void Initialize()
    {
        for (int i = 0; i < peanutSystems.Length; i++)
        {
            peanutSystems[i].peanuts.Initialize();
            peanutSystems[i].dice.Throw();
        }
        dealer.position = peanutSystems[Random.Range(0, peanutSystems.Length)].dealer.position;
        dealerPeanut.Initialize();
    }
    public void ThrowDice()
    {
        for (int i = 0; i < peanutSystems.Length; i++) { peanutSystems[i].dice.Throw(); }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < peanutSystems.Length; i++)
        {
            int index = i;
            peanutSystems[i].revolver.onClick.AddListener(() => LoadPlayerAvatar.Instance.player[index].sprite = rip);
        }
        Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
            Initialize();
        if (Input.GetKeyDown(KeyCode.D))
            ThrowDice();
    }
}
