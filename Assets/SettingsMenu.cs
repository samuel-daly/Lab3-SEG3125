using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SettingsMenu : MonoBehaviour{

    public static int numberOfRounds;
    public static float timerTime;

    public Text roundsText;
    public Text timerText;

    public void SetRounds (float rounds){
        
        numberOfRounds = (int)Math.Round(rounds);
        roundsText.text = numberOfRounds.ToString();
       
    }

    public void SetTimer (float timer){
        timerText.text = timer.ToString("f1");
        timerTime = timer;
    }
}
