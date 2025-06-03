using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TMP_Text Scoretext;
    int score;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public int GetScore()
    {
        return score;
    }

    public void AddPoint(int AddScore)
    {
        score += AddScore;
        Scoretext.text = score.ToString();
    }

}
