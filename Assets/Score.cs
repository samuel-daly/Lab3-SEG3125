using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour{

    public static int score = 0;
    public static int round = 1;


    [SerializeField]
    public Text scoreText;

    [SerializeField]
    public Text roundText;

    [SerializeField]
    public Text finalScoreText;

    [SerializeField]
    public Text finalRoundText;

    private SettingsMenu settingsMenu;
    

    // Start is called before the first frame update
    void Start(){
        scoreText.text = score.ToString();
        roundText.text = round.ToString();
        finalScoreText.text = score.ToString();
        finalRoundText.text = round.ToString();
    }



}
