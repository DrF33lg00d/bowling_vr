using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public Text[] rollTexts, frameTexts;

    

    public Text[] FillRolls (List<int> rolls) {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++) {
            rollTexts[i].text = scoresString[i].ToString();
        }

        return rollTexts;
    }

    public Text[] FillFrames (List<int> frames) {
        for (int i = 0; i < frames.Count; i++) {
            frameTexts[i].text = frames[i].ToString();
        }

        return frameTexts;
    }

    public static string FormatRolls(List<int> rolls) {
        string output = "";
			
        for (int i = 0; i < rolls.Count; i++) {
            int box = output.Length + 1;							// Score box 1 to 21 

            if (rolls[i] == 0) {									// Always enter 0 as -
                                         // output += "-";
            } else if ((box%2 == 0 || box == 22) && rolls[i-1]+rolls[i] == 10) {	// SPARE
                output += "/";	
            } else if (box >= 21 && rolls[i] == 10)	{				// STRIKE in frame 10
                output += "X";
            } else if (rolls[i] == 10) {							// STRIKE in frame 1-9
                output += "X ";
                i++;
            } else {
                output += rolls[i].ToString();						// Normal 1-9 bowl
            }
        }
        
        return output;
    }
}
