using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreDisplay : MonoBehaviour
{
    public TMP_Text myText;
    public TMP_Text highScore;
    public TMP_Text roundNum;

    public BodySourceView b;

    // Start is called before the first frame update
    void Start()
    {
        myText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = (Score.instance.ReadScore()).ToString();
        if (b.round == 4)
        {
            roundNum.text = ("GAME OVER");
        }
        else
        {
            roundNum.text = ("Ball " + b.round);
            
        }
        if (Score.instance.ReadScore() > Score.instance.ReadHighScore())
        {
            Score.instance.SetHighScore(Score.instance.ReadScore());
            highScore.text = ("CHAMP: " + Score.instance.ReadHighScore().ToString());
        }
        
    }
}
