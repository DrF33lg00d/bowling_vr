using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerScore _playerScore;
    private DisplayScore _displayScore;
    private PinSetter _setter;
    private int _savedRecordScore;
    private int _score = 0;

    public Text[] rollText = new Text[22];
    public Text[] frameText = new Text[10];
    public Text scoreText;
    public Text recordText;
    public Button restartGame;

    public bool needRestart = false;

    void Start ()
    {
        _playerScore = GetComponent<PlayerScore>();
        _displayScore = GameObject.Find("Canvas").transform.GetChild(0).gameObject.GetComponent<DisplayScore>();
        ClearTableUI();
        
        restartGame.gameObject.SetActive(false);
    }

    void ClearTableUI()
    {
        foreach (var item in rollText)
        {
            item.text = "";
        }
        foreach (var item in frameText)
        {
            item.text = "";
        }
    }

    public void SetPinSetter(PinSetter pins)
    {
        _setter = pins;
    }

    private void Update()
    {
        if (needRestart)
        {
            needRestart = false;
            _setter.ResetAndHide();
            restartGame.gameObject.SetActive(true);
            
        }
    }
    
    public void UpdateScoreUI() {
        try
        {
            List<string> rolls = _displayScore.FillRolls(_playerScore.rolls);
            for (int i = 0; i < rolls.Count; i++)
            {
                rollText[i].text = rolls[i];
            }
            
            List<string> frames = _displayScore.FillFrames(_playerScore.ScoreFrames());
            for (int i = 0; i < frames.Count; i++)
            {
                frameText[i].text = frames[i];
            }
            _score = _playerScore.ScoreCumulative();
            scoreText.text = _score.ToString();


        } catch {
            Debug.LogWarning ("FillRollCard failed");
        }

        needRestart = _playerScore.rolls.Count >= 22;
    }

    public void RestartGame()
    {
        if (_score > _savedRecordScore)
        {
            _savedRecordScore = _score;
            recordText.text = scoreText.text;
        }

        _score = 0;
        scoreText.text = "0";
        
        _setter.SetPinsToStart();
        _setter.ShowPins();
        _playerScore.rolls.Clear();
        ClearTableUI();
        restartGame.gameObject.SetActive(false);
    }
    
}
