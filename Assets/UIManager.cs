using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore;
using TMPro;

public class UIManager : MonoBehaviour
{
    public uint ScoreExponentOffset;
    public int score {get; protected set;} = 0;
    [SerializeField]TextMeshProUGUI scoreText;

    public byte health {get; protected set;} = 3;
    [SerializeField]TextMeshProUGUI healthText;
    
    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if(scoreText)scoreText.text = $"Score: {score * (Mathf.Pow(10,ScoreExponentOffset))}";
        if(healthText)healthText.text = $"Health: {health.ToString()}";
    }

    public void addScore(int amount)
    {
        score += amount;
        UpdateUI();
    }
}
