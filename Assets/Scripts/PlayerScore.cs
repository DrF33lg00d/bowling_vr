using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class PlayerScore : MonoBehaviour
{
    public List<int> rolls;
    void Start()
    {
        rolls = new List<int>();
    }

    public void AddResultRoll(int countStanding, bool isFirst)
    {
        int score = isFirst ? 10 - countStanding : 10 - countStanding - rolls[rolls.Count - 1];
        rolls.Add(score);
    }

    public void AddStrike()
    {
        rolls.Add(10);
        rolls.Add(0);
    }

    public List<int> ScoreFrames() {
        List<int> frames = new List<int>();
        // Index i points to 2nd bowl of frame
        for (int i = 1; i < rolls.Count; i += 2) {
            if (frames.Count == 10) break;				// Prevents 11th frame score

            if (rolls[i - 1] + rolls[i] < 10)
            {
                frames.Add(rolls[i - 1] + rolls[i]);
            }

            if (rolls[i-1] == 10) {
                int scoreNext = i + 1 < rolls.Count ? rolls[i + 1] : 0;
                scoreNext += i + 2 < rolls.Count ? rolls[i + 2] : 0;
                frames.Add (10 + scoreNext);
            } else if (rolls[i-1] + rolls[i] == 10) {
                int scoreNext = i + 1 < rolls.Count ? rolls[i + 1] : 0;
                frames.Add (10 + scoreNext);
            }
        }
        return frames;
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
