using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text= "Score: " + score;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InceraseScore(int point)
    {
        score += point;
        scoreText.text = "Score: " + score;
    }
}
