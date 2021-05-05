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

    private Text[] _rollText = new Text[22];
    private Text[] _frameText = new Text[10];

    public bool needRestart = false;

    void Start ()
    {
        _playerScore = GetComponent<PlayerScore>();
        _displayScore = GameObject.Find("Canvas").transform.GetChild(0).gameObject.GetComponent<DisplayScore>();
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
            // _playerScore.rolls.Clear();
            // UpdateScoreUI();
        }
    }
    
    public void UpdateScoreUI() {
        try
        {
            Text[] vsp = _displayScore.FillRolls(_playerScore.rolls);
            for (int i = 0; i < vsp.Length; i++)
            {
                _rollText[i] = vsp[i];
            }
            
            vsp = _displayScore.FillFrames(_playerScore.ScoreFrames());
            
            for (int i = 0; i < vsp.Length; i++)
            {
                _frameText[i] = vsp[i];
            }

        } catch {
            Debug.LogWarning ("FillRollCard failed");
        }

        needRestart = _playerScore.rolls.Count >= 22;
    }
}
