using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Congrats : MonoBehaviour
{
    [SerializeField] Text Score;
    [SerializeField] Text Highscore;
    [SerializeField] Text Message;
    [SerializeField] Image Fill;

    private float fill;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        score = UIManager.instance.score;
        Score.text = "Score: " + score;
        score *= 10000;
        fill = (float)score / 200000000f;
        Fill.fillAmount = fill;
        Highscore.text = "Highscore: " + PlayerPrefs.GetInt("highscore");
        Message.text = "You have cleaned "+ score +" trash from the ocean";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
