using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinSetter : MonoBehaviour
{
    private List<Pin> pins;


    public void FindPins()
    {
        pins?.Clear();
        pins = new List<Pin>();
        foreach (var pinObject in GameObject.FindGameObjectsWithTag("Pin"))
        {
            pins.Add(pinObject.GetComponent<Pin>());
        }
    }

    public int CountStanding()
    {
        int count = 0;
        foreach (Pin pin in pins) {
            if (pin.IsStanding()) {
                count++;
            }
        }

        return count;
    }

    public void ResetStandingPins()
    {
        foreach (Pin pin in pins)
        {
            pin.RaiseIfStanding();
            pin.gameObject.SetActive(pin.IsStanding());
        }
    }

    public void ResetAllPins()
    {
        foreach (Pin pin in pins)
        {
            pin.MoveToStart();
            pin.gameObject.SetActive(true);
        }
    }
    
    public void ResetAndHide()
    {
        foreach (Pin pin in pins)
        {
            pin.MoveToStart();
            pin.gameObject.SetActive(false);
        }
    }

    public void SetPinsToStart()
    {
        foreach (Pin pin in pins)
        {
            if (pin.IsStanding()) pin.SetLower();
        }
    }

   

}
