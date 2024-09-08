using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpectedValue : MonoBehaviour
{
    public InputField dice;
    public Text ex;

    // Start is called before the first frame update
    //void Start()
    //{
    //    dice.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    //}

  public  void ValueChangeCheck()
    {
        int[] faces = new int[dice.text.Length];

        // Convert the string of faces into an integer array
        for (int i = 0; i < dice.text.Length; i++)
        {
            faces[i] = int.Parse(dice.text[i].ToString());
        }

        // Calculate the sum of the faces
        int sum = 0;
        for (int i = 0; i < faces.Length; i++)
        {
            sum += faces[i];
        }

        // Calculate the expectation value
        ex.text = ((float)sum / faces.Length).ToString();
    }


}
