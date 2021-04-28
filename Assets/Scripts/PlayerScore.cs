using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerScore : MonoBehaviour
{
    public List<int> rolls;
    private bool _isLastSpare = false;
    void Start()
    {
        rolls = new List<int>();
    }

    public void AddResultRoll(int countStanding, bool isFirst)
    {
        int score = isFirst ? 10 - countStanding : 10 - countStanding - rolls[rolls.Count - 1];
        rolls.Add(score);
        if (_isLastSpare)
        {
            rolls.Add(0);
        }
        _isLastSpare = !isFirst && IsLastXorSp();
        if (IsLastOpen())
        {
            rolls.Add(0);
            rolls.Add(0);
        }
    }

    private bool IsLastOpen()
    {
        return rolls.Count == 20 && rolls[rolls.Count - 2] + rolls[rolls.Count - 1] < 10;
    }
    private bool IsLastXorSp()
    {
        return rolls.Count == 20 && rolls[rolls.Count - 2] + rolls[rolls.Count - 1] == 10;
    }

    public void AddStrike()
    {
        rolls.Add(10);
        if (rolls.Count < 20 || _isLastSpare) 
            rolls.Add(0);
        _isLastSpare = false;
    }

    public List<int> ScoreFrames() {
        List<int> frames = new List<int>();
        // Index i points to 2nd bowl of frame
        for (int i = 1; i < rolls.Count; i += 2) {
            if (frames.Count == 10)
            {
                break;
            }				

            if (rolls[i - 1] + rolls[i] < 10)
            {
                frames.Add(rolls[i - 1] + rolls[i]);
            }

            if (rolls[i-1] == 10) {
                // int scoreNext1 = i + 1 < rolls.Count ? rolls[i + 1] : 0;
                // int scoreNext2 = i + 2 < rolls.Count ? rolls[i + 2] : 0;
                frames.Add (10 + GetScoreForStrike(i));
            } else if (rolls[i-1] + rolls[i] == 10) {
                int scoreNext = i + 1 < rolls.Count ? rolls[i + 1] : 0;
                frames.Add (10 + scoreNext);
            }
        }
        // if (rolls.Count >= 21 && !_lastFrameStrike) 
        //     frames[frames.Count - 1] += rolls[rolls.Count - 1];

        return frames;
    }

    private int GetScoreForStrike(int index_roll)
    {
        int scoreNext1 = index_roll + 1 < rolls.Count ? rolls[index_roll + 1] : 0;
        int scoreNext2;
        
        if (scoreNext1 == 10 && index_roll < 18)
            scoreNext2 = index_roll + 3 < rolls.Count ? rolls[index_roll + 3] : 0;
        else
            scoreNext2 = index_roll + 2 < rolls.Count ? rolls[index_roll + 2] : 0;
        return scoreNext1 + scoreNext2;
    }

    public List<int> ScoreCumulative()
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames())
        {
            runningTotal += frameScore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }

}
