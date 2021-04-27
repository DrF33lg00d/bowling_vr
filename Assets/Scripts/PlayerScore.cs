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

            if (rolls[i-1] + rolls[i] < 10) {				// Normal "OPEN" frame
                frames.Add (rolls[i-1] + rolls[i]);
            }

            if (rolls.Count - i <= 1) {break;}				// Ensure at least 1 look-ahead available

            if (rolls[i-1] == 10) {							
                i--;										// STRIKE frame has just one bowl
                frames.Add (10 + rolls[i+1] + rolls[i+2]);
            } else if (rolls[i-1] + rolls[i] == 10) {		// SPARE bonus
                frames.Add (10 + rolls[i+1]);
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
