using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreDisplay : MonoBehaviour
{
    public TMP_Text myText;

    // Start is called before the first frame update
    void Start()
    {
        myText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = (Score.instance.ReadScore()).ToString();
    }
}
