using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private PlayerScore _playerScore;
    private DisplayScore _displayScore;

    private Text[] _rollText = new Text[21];
    private Text[] _frameText = new Text[10];

    void Start ()
    {
        _playerScore = GameObject.FindWithTag("Player").GetComponent<PlayerScore>();
        _displayScore = GameObject.Find("Canvas").transform.GetChild(0).gameObject.GetComponent<DisplayScore>();
    }
	
    public void UpdateScoreUI() {
        try
        {
            Text[] vsp = _displayScore.FillRolls(_playerScore.rolls);
            for (int i = 0; i < vsp.Length; i++)
            {
                _rollText[i] = vsp[i];
            }
            
            vsp = _displayScore.FillFrames(_playerScore.ScoreCumulative());
            
            for (int i = 0; i < vsp.Length; i++)
            {
                _frameText[i] = vsp[i];
            }

        } catch {
            Debug.LogWarning ("FillRollCard failed");
        }
    }
}
